using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace appSchool.Repositories
{
    public class vStudentExamResultRepository : GenericRepository<vStudentExamResult> 
    {
        public vStudentExamResultRepository() : base(new dbSchoolAppEntities()) { }
        public vStudentExamResultRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<vStudentDataExport> GetStudentListByClassSetupIDs(string mClassSetupID, int mSessionID, byte mCompID, byte mBranchID)
        {
            // StudentSession obj = this.context.StudentSessions.Where(i => i.ClassSetupID == ClassSetupID ).firstordefault();

            List<vStudentDataExport> obj1 = this.context.vStudentDataExports.SqlQuery("SELECT * FROM dbo.vStudentDataExport where ClassSetupID in (" + mClassSetupID + ") and SessionID=" + mSessionID + "  and CompID="+mCompID+" AND BranchID=" + mBranchID +" ").ToList();
        
            return obj1;
        }




        public List<vStudentExamResult> GetStudentExamResultByClassIDandExamID(int mClassID, int mExamID,byte mSessionID, byte mCompID, byte mBranchID)
        {

            List<vStudentExamResult> objStudentExamResult = new List<vStudentExamResult>();

            var param = new[] { 
                           new SqlParameter("@ClassID", mClassID),
                           new SqlParameter("@ExamID", mExamID),
                           new SqlParameter("@SessionID", mSessionID),
                           new SqlParameter("@BranchID", mBranchID),
                           new SqlParameter("@CompID", mCompID),
                            };

            objStudentExamResult = this.context.Database.SqlQuery<vStudentExamResult>(
                                     "GetStudentExamResultList @ClassID,@ExamID, @SessionID, @BranchID, @CompID",
                                      param
                             ).ToList();

            return objStudentExamResult;
        }





       
    }

  
}
