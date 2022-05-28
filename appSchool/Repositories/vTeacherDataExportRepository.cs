using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;


namespace appSchool.Repositories
{
    public class vTeacherDataExportRepository : GenericRepository<vTeacherDataExport> 
    {
        public vTeacherDataExportRepository() : base(new dbSchoolAppEntities()) { }
        public vTeacherDataExportRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        //public List<vTeacherDataExport> GetTeacherListForDataExport()
        //{
        //    // StudentSession obj = this.context.StudentSessions.Where(i => i.ClassSetupID == ClassSetupID ).firstordefault();

        //    List<vTeacherDataExport> obj1 = this.context.vTeacherDataExports.SqlQuery("SELECT * FROM dbo.vTeacherDataExport where TeacherID>0 ").ToList();
        
        //    return obj1;
        //}

          public List<vTeacherDataExport> GetTeacherListForDataExport( int mSessionID, byte mCompID, byte mBranchID)
        {

            List<vTeacherDataExport> objTeacherDataExport = new List<vTeacherDataExport>();

            var param = new[] { 
                           new SqlParameter("@SessionID", mSessionID),
                           new SqlParameter("@CompID", mCompID),
                           new SqlParameter("@BranchID", mBranchID),

                            };

            objTeacherDataExport = this.context.Database.SqlQuery<vTeacherDataExport>(
                                     "GetTeacherDataExport @SessionID, @CompID, @BranchID",
                                      param
                             ).ToList();

            return objTeacherDataExport;
        }









       
    }

  
}
