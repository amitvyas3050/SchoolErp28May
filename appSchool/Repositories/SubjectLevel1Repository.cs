using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class SubjectLevel1Repository: GenericRepository<SubjectLevelOne>
    {
        public SubjectLevel1Repository() : base(new dbSchoolAppEntities()) { }
        public SubjectLevel1Repository(dbSchoolAppEntities dbContext) : base(dbContext) { }

        public void UpdateSubjectLevel1(SubjectLevelOne obj)
        {
            SubjectLevelOne c = this.GetByID(obj.IdL1);
            c.SubjectCodeL1 = obj.SubjectCodeL1;
            c.SubjectNameL1 = obj.SubjectNameL1;
            c.Order1 = obj.Order1;
            c.UIDMod = obj.UIDMod;
            c.ModDate = DateTime.Now;
            this.Update(c);
            return;
        }

        public List<SubjectLevelOne> GetSubjectLevel1List(Byte mCompID, Byte mBranchID)
        {
            List<SubjectLevelOne> lst = new List<SubjectLevelOne>();
            lst = this.context.SubjectLevelOnes.Where(x => x.IdL1 > 0 && x.CompID == mCompID && x.BranchID == mBranchID).OrderBy(x => x.Order1).ToList();
            return lst;

        }


        public List<SubjectLevelOne> GetSubjectLevel1Data()
        {


            List<SubjectLevelOne> lst = new List<SubjectLevelOne>();
            lst = this.context.SubjectLevelOnes.Where(x => x.IdL1 > 0).ToList();

            return lst;

        }

        public int CheckSubjectevelOneDelete(int mIdL1)
        {
            int ID = 0;
            ID = this.context.SubjectLevelTwoes.Where(x => x.IdL1 == mIdL1).Count();
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