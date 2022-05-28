using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;



namespace appSchool.Repositories
{
    public class vStudentFeesStructDataExportRepository : GenericRepository<vStudentFeesStructDataExport>
    {
        public vStudentFeesStructDataExportRepository() : base(new dbSchoolAppEntities()) { }
        public vStudentFeesStructDataExportRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }





        public IEnumerable<vStudentFeesStructDataExport> GetFeesStructureDataExportBySessionWise(string mClassID, int mSessionID, bool mPaidFlag, byte mCompID, byte mBranchID)
        {

            List<vStudentFeesStructDataExport> objFeesStructureDataExport = new List<vStudentFeesStructDataExport>();


            var param = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                           new SqlParameter("@ClassID", mClassID),
                           new SqlParameter("@PaidFlag", mPaidFlag),
                           new SqlParameter("@CompID", mCompID),
                           new SqlParameter("@BranchID", mBranchID),

                            };

            objFeesStructureDataExport = this.context.Database.SqlQuery<vStudentFeesStructDataExport>(
                                     "GetStudentFeesStructureDataExport @SessionID,@ClassID,@PaidFlag, @CompID,@BranchID",
                                      param
                             ).ToList();

            return objFeesStructureDataExport;
        }



    }







}