using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class SubjectLevel2Repository: GenericRepository<SubjectLevelTwo>
    {
        public SubjectLevel2Repository() : base(new dbSchoolAppEntities()) { }
        public SubjectLevel2Repository(dbSchoolAppEntities dbContext) : base(dbContext) { }

        public void UpdateSubL2Master(SubjectLevelTwo obj)
        {
            SubjectLevelTwo c = this.GetByID(obj.IdL2);
            c.SubjectCodeL2 = obj.SubjectCodeL2;
            c.SubjectNameL2 = obj.SubjectNameL2;
            c.Order2 = obj.Order2;
            c.IdL1 = obj.IdL1;
            c.UIDMod = obj.UIDMod;
            c.ModDate = DateTime.Now;
            this.Update(c);
            return;
        }


        public List<SubjectLevelTwo> GetSubjectLevel2List(Byte mCompID, Byte mBranchID)
        {
            List<SubjectLevelTwo> lst = new List<SubjectLevelTwo>();
            lst = this.context.SubjectLevelTwoes.Where(x => x.IdL1 > 0 && x.CompID==mCompID && x.BranchID==mBranchID ).ToList();
            return lst;
        }

        public List<SubjectLevelTwo> GetSubjectLevel2Data()
        {


            List<SubjectLevelTwo> lst = new List<SubjectLevelTwo>();
            lst = this.context.SubjectLevelTwoes.Where(x => x.IdL1 > 0).ToList();

            return lst;

        }

        public List<SubjectLevelTwo> GetSubjectLevel2ListByIDL1(int mIDL1)
        {
            List<SubjectLevelTwo> obj = new List<SubjectLevelTwo>();
            obj = this.context.SubjectLevelTwoes.Where(x => x.IdL1 == mIDL1).OrderBy(x=>x.Order2).ToList();

            return obj;
        } 

        public int CheckSubjectLevelTwoDelete( SubjectLevelTwo  obj)
        {
            int ID = 0;

            ID = this.context.SubjectLevelThrees.Where(x => x.IdL2 == obj.IdL2).FirstOrDefault().IdL3;
            if (ID == null)
            {
                ID = 0;
            }

            return ID;
        }


    }
    #region METADATA
    //[MetadataType(typeof(DepartmentMasterMetadata))]
    //public partial class DepartmentMaster
    //{
    //}

    //public class DepartmentMasterMetadata
    //{
    //    [Key]
    //    public short DepartmentID { get; set; } // Has to have the same type and name as your model
    //    [Required(ErrorMessage = "Department Name is required")]
    //    public string DepartmentName { get; set; } // Has to have the same type and name as your model

    //}
    #endregion
}