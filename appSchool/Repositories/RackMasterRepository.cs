using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class RackMasterRepository : GenericRepository<Lib_RackMaster>
    {
        public RackMasterRepository() : base(new dbSchoolAppEntities()) { }
        public RackMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }

        public List<Lib_RackMaster> GetRackMasterList(byte mCompID, byte mBranchID)
        {
            List<Lib_RackMaster> obj = new List<Lib_RackMaster>();
            obj = this.context.Lib_RackMaster.Where(x => x.RackId > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public void UpdateRackMaster(Lib_RackMaster obj)
        {
            Lib_RackMaster c = this.GetByID(obj.RackId);
            c.RackName = obj.RackName;
            c.DisplayOrder = obj.DisplayOrder;
            c.UIDMod = obj.UIDMod;
            c.ModDate = DateTime.Now;
            this.Update(c);
            return;
        }

    }
    #region METADATA
    //[MetadataType(typeof(RackMasterMetadata))]
    //public partial class RackMaster
    //{
    //}

    //public class RackMasterMetadata
    //{
    //    [Key]
    //    public short RackId { get; set; } // Has to have the same type and name as your model
    //    [Required(ErrorMessage = "Rack Name is required")]
    //    public string RackName { get; set; } // Has to have the same type and name as your model

    //}
    #endregion
}