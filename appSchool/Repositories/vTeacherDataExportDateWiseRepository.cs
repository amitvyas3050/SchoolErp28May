using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;


namespace appSchool.Repositories
{
    public class vTeacherDataExportDateWiseRepository : GenericRepository<vTeacherDataExportDateWise> 
    {
        public vTeacherDataExportDateWiseRepository() : base(new dbSchoolAppEntities()) { }
        public vTeacherDataExportDateWiseRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<vTeacherDataExportDateWise> GetTeacherListForDataExport(int mSessionID, byte mCompID, byte mBranchID)
            {

                List<vTeacherDataExportDateWise> objTeacherDataExport = new List<vTeacherDataExportDateWise>();

            var param = new[] 
            { 
                           new SqlParameter("@SessionID", mSessionID),
                           new SqlParameter("@CompID", mCompID),
                           new SqlParameter("@BranchID", mBranchID),

             };

            objTeacherDataExport = this.context.Database.SqlQuery<vTeacherDataExportDateWise>(
                                     "GetTeacherDataExport @SessionID, @CompID, @BranchID",
                                      param
                             ).ToList();

            return objTeacherDataExport;
        }
       
    }

}
