using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;



namespace appSchool.Repositories
{
    public class VStudentFeesReminderRepository : GenericRepository<vStudentFeesReminder>
    {
        public VStudentFeesReminderRepository() : base(new dbSchoolAppEntities()) { }
        public VStudentFeesReminderRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }





        public List<vStudentFeesReminder> GetFeesReminderBySessionWise(int mSessionID, byte mCompID, byte mBranchID)
        {

            List<vStudentFeesReminder> objFeesReminder = new List<vStudentFeesReminder>();


            var param = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                            new SqlParameter("@CompID", mCompID),
                             new SqlParameter("@BranchID", mBranchID),

                            };

            objFeesReminder = this.context.Database.SqlQuery<vStudentFeesReminder>(
                                     "GetStudentListForFeesDefaulter @SessionID, @CompID, @BranchID ",
                                      param
                             ).ToList();

            return objFeesReminder;
        }

        public List<vTotalFeesReminder> GetTotalFeesReminderStudentwise(int mSessionID, int mStudentID, byte mCompID, byte mBranchID)
        {

            List<vTotalFeesReminder> objFeesReminder = new List<vTotalFeesReminder>();

            var param = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                           new SqlParameter("@StudentID", mStudentID),
                           new SqlParameter("@CompID", mCompID),
                           new SqlParameter("@BranchID", mBranchID),

                            };

            objFeesReminder = this.context.Database.SqlQuery<vTotalFeesReminder>(
                                     "GetTotalFeesReminder @SessionID, @StudentID, @CompID, @BranchID",
                                      param
                             ).ToList();

            return objFeesReminder;
        }




    }







}