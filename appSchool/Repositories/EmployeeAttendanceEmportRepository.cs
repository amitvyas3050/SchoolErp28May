using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace appSchool.Repositories
{
    public class EmployeeAttendanceEmportRepository : GenericRepository<EmployeeAttendance> 
    {


        public EmployeeAttendanceEmportRepository() : base(new dbSchoolAppEntities()) { }
        public EmployeeAttendanceEmportRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }

        public List<EmployeeBioMetric> GetBiometricEmployeeData(string AttendanceDate)
        {
            List<EmployeeBioMetric> objlist = new List<EmployeeBioMetric>();

            string sql = " SELECT  dbo.Employees.EmployeeCode, dbo.Employees.EmployeeName, dbo.AttendanceLogs.AttendanceDate, dbo.AttendanceLogs.InTime, dbo.AttendanceLogs.OutTime, " +
                         " case  when dbo.AttendanceLogs.InTime >'2017-08-05 07:00:00' Then 'Absent' when dbo.AttendanceLogs.InTime ='1900-01-01 00:00:00' Then 'Absent' when dbo.AttendanceLogs.InTime < '2017-08-05 07:00:00' Then 'Present' END as AbsentStatus, " +
                         " dbo.AttendanceLogs.Present, dbo.AttendanceLogs.Absent, dbo.AttendanceLogs.Status,dbo.AttendanceLogs.StatusCode, dbo.AttendanceLogs.Duration, dbo.AttendanceLogs.PunchRecords " +
                         " FROM dbo.Categories INNER JOIN dbo.Departments INNER JOIN dbo.Companies INNER JOIN " +
                         " dbo.AttendanceLogs INNER JOIN dbo.Employees ON dbo.AttendanceLogs.EmployeeID = dbo.Employees.EmployeeID ON dbo.Companies.CompanyId = dbo.Employees.CompanyId ON " +
                         " dbo.Departments.DepartmentId = dbo.Employees.DepartmentId ON dbo.Categories.CategoryId = dbo.Employees.CategoryId INNER JOIN " +
                         " dbo.Shifts ON dbo.AttendanceLogs.ShiftId = dbo.Shifts.ShiftId" +
                         " Where AttendanceLogs.AttendanceDate = '" + AttendanceDate + "' Order by dbo.Employees.EmployeeCode ";
            DataTable dt = new DataTable();
            dt = DB.ExecuteBiometricQuery(sql);

            foreach (DataRow dr in dt.Rows)
            {
                EmployeeBioMetric obj = new EmployeeBioMetric();

                obj.EmployeeCode = dr["EmployeeCode"].ToString();
                obj.EmployeeName = dr["EmployeeName"].ToString();
                obj.AttendanceDate = DateTime.Parse(dr["AttendanceDate"].ToString());
                obj.Duration = double.Parse(dr["Duration"].ToString());
                obj.InTime = (dr["InTime"].ToString());
                obj.OutTime = (dr["OutTime"].ToString());
                obj.AbsentStatus = (dr["AbsentStatus"].ToString());
                obj.Status = (dr["Status"].ToString());
                obj.StatusCode = (dr["StatusCode"].ToString());
                obj.PunchRecords = (dr["PunchRecords"].ToString());
               

                objlist.Add(obj);

            }


            return objlist;

        }


        public List<EmployeeAttendance> GetEmployeAttenDanceListDayWise(DateTime mAttenDanceDate, byte mCompID, byte mBranchID, int mSessionID)
        {
            List<EmployeeAttendance> obj = new List<EmployeeAttendance>();
            obj = this.context.EmployeeAttendances.Where(x => x.AttendanceDate == mAttenDanceDate && x.CompID == mCompID && x.BranchID == mBranchID && x.SessionID == mSessionID).ToList();
            return obj;
        }

        public List<EmployeeAttendance> GetEmployeAbsentList(DateTime mAttenDanceDate, byte mCompID, byte mBranchID, int mSessionID)
        {
            List<EmployeeAttendance> obj = new List<EmployeeAttendance>();
            obj = this.context.EmployeeAttendances.Where(x => x.AttendanceDate == mAttenDanceDate && x.Status == "Absent" && x.CompID == mCompID && x.BranchID == mBranchID && x.SessionID == mSessionID).ToList();
            return obj;
        }


        public List<EmployeeBioMetric> GetEmployeeListEmportDaywise(DateTime mAttenDanceDate, byte mCompID, byte mBranchID, int mSessionID)
        {

            List<EmployeeBioMetric> lst = new List<EmployeeBioMetric>();
            var attandancelist = this.context.EmployeeAttendances.
                        Join(this.context.EmployeeAttendances, u => u.EmployeeCode, uir => uir.EmployeeCode,
                        (u, uir) => new { u, uir }).
                        Join(this.context.Teachers, r => r.uir.EmployeeCode, ro => ro.EmployeeCode, (r, ro) => new { r, ro })
                        .Where(m => m.r.u.AttendanceDate == mAttenDanceDate && m.r.uir.AttendanceDate == mAttenDanceDate )
                        .Select(m => new EmployeeBioMetric
                        {
                            EmployeeCode = m.r.u.EmployeeCode,
                            EmployeeName = m.ro.FirstName + " "+ m.ro.LastName,
                            AttendanceDate = m.r.u.AttendanceDate,
                            InTime = m.r.u.InTime,
                            OutTime = m.r.u.OutTime,
                            Status = m.r.u.Status,

                        }).ToList();


            if (attandancelist.Count > 0)
                lst = attandancelist;
            return lst;

        }

        public void AddNewEmployeeAttendance(EmployeeAttendance obj)
        {
            this.Insert(obj);
        }

        public void deletemultiple(DateTime mDate)
        {
            EmployeeAttendance obj = this.context.EmployeeAttendances.Where(x => x.AttendanceDate == mDate).FirstOrDefault();
            
            if (obj != null)
            {
               this.context.EmployeeAttendances.Where(p => p.AttendanceDate == mDate)
               .ToList().ForEach(p => context.EmployeeAttendances.Remove(p));
               this.context.SaveChanges();
            } 
           

        }

        public void UpdateEmployeeAttendance(EmployeeAttendance Stud)
        {
            EmployeeAttendance editSession = this.GetByID(Stud.AttendanceLogId);
            if (editSession != null)
            {
                editSession.Status = Stud.Status;
                editSession.StatusCode = Stud.StatusCode;

                this.Update(editSession);
            }

        }

    }
  
     public class EmployeeBioMetric
    {
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public  System.DateTime?  AttendanceDate { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public double Duration { get; set; }
        public string ShiftName { get; set; }
        public string AbsentStatus { get; set; }
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string PunchRecords { get; set; }
        public string Remark { get; set; }
    
   }


}
