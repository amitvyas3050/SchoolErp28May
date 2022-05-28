using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class ShelfMasterRepository : GenericRepository<Lib_ShelfMaster>
    {
        public ShelfMasterRepository() : base(new dbSchoolAppEntities()) { }
        public ShelfMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<Lib_ShelfMaster> GetShelfMasterList(byte mCompID, byte mBranchID)
        {
            List<Lib_ShelfMaster> obj = new List<Lib_ShelfMaster>();
            obj = this.context.Lib_ShelfMaster.Where(x => x.ShelfId > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }


        public void UpdateShelfMaster(Lib_ShelfMaster obj)
        {
            Lib_ShelfMaster c = this.GetByID(obj.ShelfId);
            c.ShelfName = obj.ShelfName;
            c.RackId = obj.RackId;
            c.DisplayOrder = obj.DisplayOrder;
            c.UIDMod = obj.UIDMod;
            c.ModDate = DateTime.Now;
            this.Update(c);
            return;
        }


    }
    #region METADATA
    [MetadataType(typeof(ShelfMasterMetadata))]
    public partial class ShelfMaster
    {
    }

    public class ShelfMasterMetadata
    {
        [Key]
        public short ShelfId { get; set; } // Has to have the same type and name as your model
        [Required(ErrorMessage = "Shelf Name is required")]
        public string ShelfName { get; set; } // Has to have the same type and name as your model

    }
    #endregion
}