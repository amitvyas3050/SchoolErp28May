using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace appSchool.Repositories
{
    public class vAttendanceDataExportRepository : GenericRepository<VAttendanceDataExport> 
    {
        public vAttendanceDataExportRepository() : base(new dbSchoolAppEntities()) { }
        public vAttendanceDataExportRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        //public List<VAttendanceDataExport> GetAttendanceListByClassSetupIDandDatewise(string mClassSetupID,DateTime mFromDate, DateTime mToDate, int mSessionID)
        //{
        //    // StudentSession obj = this.context.StudentSessions.Where(i => i.ClassSetupID == ClassSetupID ).firstordefault();

        //    List<VAttendanceDataExport> obj1 = this.context.VAttendanceDataExports.SqlQuery("SELECT * FROM dbo.vAttendanceDataExport where ClassSetupID in (" + mClassSetupID + ") and SessionID=" + mSessionID + "  and AttendanceDate>=" + mFromDate + " and AttendanceDate<="+mToDate +"  ").ToList();

        // //  List<VAttendanceDataExport> obj1 = this.context.VAttendanceDataExports.Where(x=>x.ClassSetupID in mClassSetupID )
        //    //var orderKeys = new int[] { 1, 12, 306, 284, 50047};

        //    //var orders = (from vAttendanceDateExport in this.context.VAttendanceDataExports 
        //    //  where orderKeys.Contains(vAttendanceDateExport.ClassSetupID)
        //    //  select vAttendanceDateExport).ToList();

        //    var orderKeys = new int[] { 1, 12, 306, 284, 50047 };
        //    var orders = (from vAttendanceDataExport in this.context.VAttendanceDataExports
        //                  where (orderKeys.Contains(vAttendanceDataExport.ClassSetupID))
        //                  select vAttendanceDataExport).ToList();



        
        //    return obj1;
        //}



        public IEnumerable<VAttendanceDataExport> GetAttendanceListByClassSetupIDandDatewise(string mClassSetupID, DateTime mFromDate, DateTime mToDate, int mSessionID, byte mCompID, byte mBranchID)
        {

            List<VAttendanceDataExport> objAttDataExport = new List<VAttendanceDataExport>();


            var param = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                           new SqlParameter("@CompID", mCompID),
                           new SqlParameter("@BranchID", mBranchID),
                            new SqlParameter("@ClassSetupID", mClassSetupID),
                            new SqlParameter("@FromDate", mFromDate),
                            new SqlParameter("@ToDate", mToDate),

                            };

            objAttDataExport=this.context.Database.SqlQuery<VAttendanceDataExport>(
                                     "GetClassAttendanceRecordBetweenDates @SessionID,@CompID,@BranchID,@ClassSetupID,@FromDate,@ToDate",
                                      param
                             ).ToList();



            return objAttDataExport;
        }


        public IEnumerable<VAttendanceDataExport> GetAttendanceListByClassSetupIDandMonthWise(string mClassSetupID, string MonthID, int mSessionID, byte mCompID, byte mBranchID)
        {

            List<VAttendanceDataExport> objAttDataExport = new List<VAttendanceDataExport>();


            var param = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                           new SqlParameter("@CompID", mCompID),
                           new SqlParameter("@BranchID", mBranchID),
                            new SqlParameter("@ClassSetupID", mClassSetupID),
                             new SqlParameter("@MonthID", MonthID),
                           
                            };

            objAttDataExport = this.context.Database.SqlQuery<VAttendanceDataExport>(
                                     "GetClassAttendanceRecordMonthWise @SessionID,@CompID,@BranchID,@ClassSetupID,@MonthID",
                                      param
                             ).ToList();



            return objAttDataExport;
        }




       
    }

  
}
