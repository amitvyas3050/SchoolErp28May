using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class StudentFeesDetailRepository: GenericRepository<StudentFeesDetail>
    {
        public StudentFeesDetailRepository() : base(new dbSchoolAppEntities()) { }
        public StudentFeesDetailRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<StudentFeesDetail> GetStudentFeesDetailByTermIDandStudentID(int mStudentID, int mTermID, int mSessionID,byte mCompID, byte mBranchID)
        {
            List<StudentFeesDetail> obj = new List<StudentFeesDetail>();
            obj = this.context.StudentFeesDetails.Where(x => x.StudentID ==mStudentID && x.TermID == mTermID && x.SessionId == mSessionID && x.CompID==mCompID && x.BranchID==mBranchID).ToList();
            return obj;
        }

        public List<StudentFeesDetail> GetStudentFeesDetailByStudMasterID(int mStudMasterID, int mSessionID)
        {
            List<StudentFeesDetail> obj = new List<StudentFeesDetail>();
            obj = this.context.StudentFeesDetails.Where(x => x.StudMasterId == mStudMasterID && x.SessionId == mSessionID).ToList();
            return obj;

        }

    }



}