using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class ExamRemarkMasterRepository: GenericRepository<ExamRemarkMaster>
    {
        public ExamRemarkMasterRepository() : base(new dbSchoolAppEntities()) { }
        public ExamRemarkMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<ExamRemarkMaster> GetExamRemarkMasterList(byte mCompID, byte mBranchID)
        {
            List<ExamRemarkMaster> obj = new List<ExamRemarkMaster>();
            obj = this.context.ExamRemarkMasters.Where(x => x.ERID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }


        public void UpdateExamRemarkMaster(ExamRemarkMaster obj)
        {
            ExamRemarkMaster c = this.GetByID(obj.ERID);
            c.ExamRemark = obj.ExamRemark;
            c.RemarkGrade = obj.RemarkGrade;
            c.UIDMod = obj.UIDMod;
            c.ModDate = DateTime.Now;
            this.Update(c);
            return;
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