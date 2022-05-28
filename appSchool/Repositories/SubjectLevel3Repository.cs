using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class SubjectLevel3Repository: GenericRepository<SubjectLevelThree>
    {
        public SubjectLevel3Repository() : base(new dbSchoolAppEntities()) { }
        public SubjectLevel3Repository(dbSchoolAppEntities dbContext) : base(dbContext) { }

        public void UpdateSubL3Master(SubjectLevelThree obj)
        {
            SubjectLevelThree c = this.GetByID(obj.IdL3);
            c.SubjectCodeL3 = obj.SubjectCodeL3;
            c.SubjectNameL3 = obj.SubjectNameL3;
            c.IdL2 = obj.IdL2;
            c.Order3 = obj.Order3;
            c.UIDMod = obj.UIDMod;
            c.ModDate = DateTime.Now;
            this.Update(c);
            return;
        }

        //public void UpdateSubjectLevel3Info(SubjectLevelThree obj )
        //{
        //    SubjectLevelThree newObj = this.GetByID(obj.IdL3);
        //    newObj.IdL2 = obj.IdL2;
        //    newObj.Order3 = int.Parse(obj.Order3.ToString());
        //    newObj.SubjectCodeL3 = obj.SubjectCodeL3;
        //    newObj.SubjectNameL3 = obj.SubjectNameL3;
           
        //    newObj.ModDate = DateTime.Now;
        //    this.Update(newObj);
        //}

        public List<SubjectLevelThree> GetSubjectLevel3List(Byte mCompID, byte mBranchID)
        {

            List<SubjectLevelThree> lst = new List<SubjectLevelThree>();
            lst = this.context.SubjectLevelThrees.Where(x => x.IdL3 > 0 && x.CompID==mCompID && x.BranchID==mBranchID).ToList();

            return lst;

        }

        public List<SubjectLevelThree> GetSubjectLevel3Data()
        {


            List<SubjectLevelThree> lst = new List<SubjectLevelThree>();
            lst = this.context.SubjectLevelThrees.Where(x => x.IdL3 > 0).ToList();

            return lst;

        }
        public List<SubjectLevelTwo> GetAllsubjectsNameByIDL1(int mIdL1)
        {
            List<SubjectLevelTwo> obj = new List<SubjectLevelTwo>();
            obj = this.context.SubjectLevelTwoes.Where(i => i.IdL1 == mIdL1).OrderBy(x=>x.Order2).ToList();
            return obj;
        }

        public List<vSubjectLevelThreeByIDLOne> GetAllsubjectsLevel3ByIDL1(int mIdL1, byte mCompID, byte mBranchID)
        {
            List<vSubjectLevelThreeByIDLOne> obj = new List<vSubjectLevelThreeByIDLOne>();
            obj = this.context.vSubjectLevelThreeByIDLOnes.Where(i => i.IdL1 == mIdL1 && i.CompID==mCompID && i.BranchID==mBranchID).OrderBy(x => x.Order3).ToList();
            return obj;
        }

    }

  
}