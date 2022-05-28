using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class EmployeeAttendanceDailyRepository : GenericRepository<vEmployeeattendancelist> 
    {
        public EmployeeAttendanceDailyRepository() : base(new dbSchoolAppEntities()) { }
        public EmployeeAttendanceDailyRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }

        dbSchoolAppEntities dbcontaxt = new dbSchoolAppEntities();

        public List<vEmployeeattendancelist> GetEmployeAbsentPresentList(DateTime mAttenDanceDate, byte mCompID, byte mBranchID, int mSessionID)
        {
            List<vEmployeeattendancelist> list = new List<vEmployeeattendancelist>();

            list = this.context.vEmployeeattendancelists.Where(x => x.AttendanceDate == mAttenDanceDate && x.CompID == mCompID && x.BranchID == mBranchID && x.SessionID == mSessionID).OrderBy(y=> y.EmployeeName).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                DateTime EmpInTime = DateTime.Parse(list[i].InTime);
                string Date = EmpInTime.ToString("yyyy-MM-dd");

                int TeacherID = list[i].TeacherID;
                Teacher objemp = dbcontaxt.Teachers.Where(x => x.TeacherID == TeacherID).FirstOrDefault();
                objemp.StartTime = objemp.StartTime + ":00";
                DateTime EmpShiftTime = DateTime.Parse(Date + " " + objemp.StartTime);

                int compare = TimeSpan.Compare(EmpShiftTime.TimeOfDay, EmpInTime.TimeOfDay);  //-1  if  t1 is shorter than t2. //0   if  t1 is equal to t2. //1   if  t1 is longer than t2.


               
                if(compare == -1)
                {
                    list[i].StatusCode = "Lt";

                }
                
                string StatusCode = list[i].StatusCode;
                int AttendanceLogId = list[i].AttendanceLogId;
                TimeSpan duration = DateTime.Parse(EmpShiftTime.ToString("hh:mm")).Subtract(DateTime.Parse(EmpInTime.ToString("hh:mm")));

                if (list[i].Status == "Absent" || list[i].Status == "Leave")
                {
                    list[i].StatusCode = "A";
                    duration = TimeSpan.Parse("00:00:00".ToString());
                }
                
                string sql = "Update EmployeeAttendance set TimeStatus = '" + duration + "' , StatusCode ='" + StatusCode + "' Where  AttendanceLogId =" + AttendanceLogId + "";
                int res = DB.ExecuteQueryNoResult(sql);
                list[i].TimeStatus = duration.ToString(); 


            }

            return list;
        }

        public List<vEmployeeattendancelist> GetEmployeAbsentPresentListReport(DateTime mFromDate, DateTime mToDate, byte mCompID, byte mBranchID, int mSessionID)
        {
            List<vEmployeeattendancelist> list = new List<vEmployeeattendancelist>();

            list = this.context.vEmployeeattendancelists.Where(x => x.AttendanceDate >= mFromDate && x.AttendanceDate <= mToDate && x.CompID == mCompID && x.BranchID == mBranchID && x.SessionID == mSessionID).OrderBy(y => y.EmployeeName).ToList();

            return list;
        }


        public List<vEmployeeattendancelist> GetEmployeAbsentPresentListFilter(DateTime mAttenDanceDate, byte mCompID, byte mBranchID, int mSessionID)
        {
            List<vEmployeeattendancelist> list = new List<vEmployeeattendancelist>();

            list = this.context.vEmployeeattendancelists.Where(x => x.AttendanceDate == mAttenDanceDate && x.CompID == mCompID && x.BranchID == mBranchID && x.SessionID == mSessionID).ToList();
            return list;
        }


        public List<vEmployeeattendancelist> GetEmployeAbsentPresentListByAttendanceDate(DateTime mAttenDanceDate)
        {
            List<vEmployeeattendancelist> list = new List<vEmployeeattendancelist>();

            list = this.context.vEmployeeattendancelists.Where(x => x.AttendanceDate == mAttenDanceDate && x.Status == "Absent").ToList();
            return list;
        }


        

    }
  
   

}
