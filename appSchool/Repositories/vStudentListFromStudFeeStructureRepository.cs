using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class vStudentListFromStudFeeStructureRepository: GenericRepository<vStudentListFromStudFeeStructure>
    {
        public vStudentListFromStudFeeStructureRepository() : base(new dbSchoolAppEntities()) { }
        public vStudentListFromStudFeeStructureRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<vStudentListFromStudFeeStructure> GetStudentListFromFeesStructure(int mSessionID, byte mCompID, byte mBranchID)
        {

          
            List<vStudentListFromStudFeeStructure> obj1 = this.context.vStudentListFromStudFeeStructures.Where(x => x.SessionID == mSessionID && x.CompID==mCompID && x.BranchID==mBranchID ).ToList();

            return obj1;
        }



        public List<vTermListFromStudFeeMaster> GetTermListByStudentID(int mStudentID, int mSessionID, byte mCompID, byte mBranchID)
        {

            List<vTermListFromStudFeeMaster> obj1 = this.context.vTermListFromStudFeeMasters.Where(x => x.StudentID == mStudentID && x.SessionID == mSessionID ).ToList();

            return obj1;
        }


    }


     




}