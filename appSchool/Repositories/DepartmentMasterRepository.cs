using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class DepartmentMasterRepository: GenericRepository<DepartmentMaster>
    {
        public DepartmentMasterRepository() : base(new dbSchoolAppEntities()) { }
        public DepartmentMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<DepartmentMaster> GetDepartmentMasterList(byte mCompID, byte mBranchID)
        {
            List<DepartmentMaster> obj = new List<DepartmentMaster>();
            obj = this.context.DepartmentMasters.Where(x => x.DepartmentID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }


        public void UpdateDepartmentMaster(DepartmentMaster obj)
        {
            DepartmentMaster c = this.GetByID(obj.DepartmentID);
            c.DepartmentName = obj.DepartmentName;
            c.UIDMod = obj.UIDMod;
            c.ModDate = DateTime.Now;
            this.Update(c);
            return;
        }

    }
    #region METADATA
    [MetadataType(typeof(DepartmentMasterMetadata))]
    public partial class DepartmentMaster
    {
    }

    public class DepartmentMasterMetadata
    {
        [Key]
        public short DepartmentID { get; set; } // Has to have the same type and name as your model
        [Required(ErrorMessage = "Department Name is required")]
        public string DepartmentName { get; set; } // Has to have the same type and name as your model

    }
    #endregion
}