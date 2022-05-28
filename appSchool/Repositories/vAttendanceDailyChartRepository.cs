using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;


namespace appSchool.Repositories
{
    public class vAttendanceDailyChartRepository : GenericRepository<vAttendanceDailyChart> 
    {
        public vAttendanceDailyChartRepository() : base(new dbSchoolAppEntities()) { }
        public vAttendanceDailyChartRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }

        
          public List<vAttendanceDailyChart> GetAttendanceSummaryDatewise( int mSessionID,DateTime mAttendanceDate, byte mCompID, byte mBranchID)
        {
            List<vAttendanceDailyChart> objAttendanceDailyChart = new List<vAttendanceDailyChart>();
            var param = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                            new SqlParameter("@CompID", mCompID),
                             new SqlParameter("@BranchID", mBranchID),
                            new SqlParameter("@AttendanceDate", mAttendanceDate),

                            };
            objAttendanceDailyChart = this.context.Database.SqlQuery<vAttendanceDailyChart>(
                                     "GetAttendanceSummaryDatewise @SessionID,@CompID,@BranchID, @AttendanceDate",
                                      param
                             ).ToList();

            return objAttendanceDailyChart;
        }

          public List<vAttendanceDailyChartClasswise> GetAttendanceDatewiseAndClassWise(int mSessionID, byte mCompID, byte mBranchID, DateTime mAttendanceDate, string mClassSetupID)
          {
              List<vAttendanceDailyChartClasswise> objAttendanceDailyChart = new List<vAttendanceDailyChartClasswise>();
              var param = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                           new SqlParameter("@CompID", mCompID),
                           new SqlParameter("@BranchID", mBranchID),
                            new SqlParameter("@AttendanceDate", mAttendanceDate),
                             new SqlParameter("@ClassSetupID", mClassSetupID),

                            };
              objAttendanceDailyChart = this.context.Database.SqlQuery<vAttendanceDailyChartClasswise>(
                                       "GetAttendanceDailyChartClasswise @SessionID, @CompID, @BranchID, @AttendanceDate, @ClassSetupID",
                                        param
                               ).ToList();

              return objAttendanceDailyChart;
          }









       
    }

  
}
