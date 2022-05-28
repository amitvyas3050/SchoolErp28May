using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class vSubjectAllotmentDataExportRepository : GenericRepository<vSubjectAllotmentDataExport> 
    {
        public vSubjectAllotmentDataExportRepository() : base(new dbSchoolAppEntities()) { }
        public vSubjectAllotmentDataExportRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<vSubjectAllotmentDataExport> GetSubjectListByClassSetupIDs(string mClassesID, int mSessionID, byte mCompID, byte mBranchID)
        {
           
            //             " Where  dbo.StudentSession.ClassSetupID in (" + mClassSetupID + ") and dbo.StudentRegistration.TCGiven=0 AND dbo.StudentSession.SessionID=" + mSessionID + "  and dbo.StudentSession.CompID=" + mCompID + " AND dbo.StudentSession.BranchID=" + mBranchID;




            string sql = "SELECT   dbo.SubjectAllotment.ClassID, dbo.SubjectAllotment.IDL1, dbo.SubjectAllotment.IDL2, dbo.SubjectAllotment.IDL3, dbo.Class.ClassName, "+
                         " dbo.SubjectLevelOne.SubjectNameL1, dbo.SubjectLevelTwo.SubjectNameL2, dbo.SubjectLevelThree.SubjectNameL3 "+
                         " FROM   dbo.SubjectAllotment INNER JOIN "+
                        " dbo.SubjectLevelOne ON dbo.SubjectAllotment.IDL1 = dbo.SubjectLevelOne.IdL1 AND dbo.SubjectAllotment.BranchID = dbo.SubjectLevelOne.BranchID AND " +
                         " dbo.SubjectAllotment.CompID = dbo.SubjectLevelOne.CompID INNER JOIN " +
                         " dbo.Class ON dbo.SubjectAllotment.ClassID = dbo.Class.ClassID AND dbo.SubjectAllotment.CompID = dbo.Class.CompID AND " +
                         " dbo.SubjectAllotment.BranchID = dbo.Class.BranchID LEFT OUTER JOIN "+
                         " dbo.SubjectLevelThree ON dbo.SubjectAllotment.CompID = dbo.SubjectLevelThree.CompID AND " +
                         " dbo.SubjectAllotment.BranchID = dbo.SubjectLevelThree.BranchID AND dbo.SubjectAllotment.IDL3 = dbo.SubjectLevelThree.IdL3 LEFT OUTER JOIN " +
                         " dbo.SubjectLevelTwo ON dbo.SubjectAllotment.CompID = dbo.SubjectLevelTwo.CompID AND dbo.SubjectAllotment.BranchID = dbo.SubjectLevelTwo.BranchID AND " +
                         " dbo.SubjectAllotment.IDL2 = dbo.SubjectLevelTwo.IdL2 " +
            " Where  dbo.SubjectAllotment.ClassID in (" + mClassesID + ")   and dbo.SubjectAllotment.CompID=" + mCompID + " AND dbo.SubjectAllotment.BranchID=" + mBranchID;
          
            
            List<vSubjectAllotmentDataExport> obj = this.context.vSubjectAllotmentDataExports.SqlQuery(sql).ToList();
            return obj; 
        }


        //public List<vSubjectAllotment> GetStudentListForPhotoReport(int mClassID, int mSessionID, byte mCompID, byte mBranchID)
        //{
        //    List<vSubjectAllotment> objlst = new List<vSubjectAllotment>();
        //    objlst = this.context.vSubjectAllotments.Where(x => x.ClassID == mClassID && x.SessionID == mSessionID && x.CompID == mCompID && x.BranchID == mBranchID && x.TCGiven == false).ToList();
        //    return objlst;
        //}


        //string sql = " SELECT  dbo.Class.ClassID, dbo.Class.ClassName, dbo.SubjectAllotment.IDL1, dbo.SubjectAllotment.IDL2, dbo.SubjectAllotment.IDL3, dbo.SubjectLevelOne.SubjectNameL1, " +
        //              " dbo.SubjectLevelTwo.SubjectNameL2, dbo.SubjectLevelThree.SubjectNameL3 " +
        //              " FROM  dbo.Class INNER JOIN " +
        //              " dbo.SubjectAllotment ON dbo.Class.ClassID = dbo.SubjectAllotment.ClassID AND dbo.Class.BranchID = dbo.SubjectAllotment.BranchID AND " +
        //             "  dbo.Class.CompID = dbo.SubjectAllotment.CompID INNER JOIN " +
        //               " dbo.SubjectLevelOne ON dbo.SubjectAllotment.IDL1 = dbo.SubjectLevelOne.IdL1 AND dbo.SubjectAllotment.BranchID = dbo.SubjectLevelOne.BranchID AND " +
        //                   " dbo.SubjectAllotment.CompID = dbo.SubjectLevelOne.CompID LEFT OUTER JOIN " +
        //                   "  dbo.SubjectLevelTwo ON dbo.SubjectAllotment.CompID = dbo.SubjectLevelTwo.CompID AND dbo.SubjectAllotment.BranchID = dbo.SubjectLevelTwo.BranchID AND "+
        //                   " dbo.SubjectAllotment.IDL2 = dbo.SubjectLevelTwo.IdL2 LEFT OUTER JOIN " +
        //                   " dbo.SubjectLevelThree ON dbo.SubjectAllotment.CompID = dbo.SubjectLevelThree.CompID AND " +
        //                   " dbo.SubjectAllotment.BranchID = dbo.SubjectLevelThree.BranchID AND dbo.SubjectAllotment.IDL3 = dbo.SubjectLevelThree.IdL3 "+
        //" Where  dbo.SubjectAllotment.ClassID in (" + mClassesID + ")   and dbo.SubjectAllotment.CompID=" + mCompID + " AND dbo.SubjectAllotment.BranchID=" + mBranchID;




       
    }

  
}
