using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class DesignationMasterRepository : GenericRepository<DesignationMaster>
    {
        public DesignationMasterRepository() : base(new dbSchoolAppEntities()) { }
        public DesignationMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<DesignationMaster> GetDesignationMasterList(byte mCompID, byte mBranchID)
        {
            List<DesignationMaster> obj = new List<DesignationMaster>();
            obj = this.context.DesignationMasters.Where(x => x.DesignationID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public List<DesignationMaster> GetDesignationListWithTeaching(byte mCompID, byte mBranchID)
        {
            string type="Teaching";
            List<DesignationMaster> obj = new List<DesignationMaster>();
            obj = this.context.DesignationMasters.Where(x => x.DesignationID > 0 && x.DesignationType==type && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public List<DesignationMaster> GetDesignationListWithNonTeaching(byte mCompID, byte mBranchID)
        {
            string type = "NonTeaching";
            List<DesignationMaster> obj = new List<DesignationMaster>();
            obj = this.context.DesignationMasters.Where(x => x.DesignationID > 0 && x.DesignationType == type && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }



        public void UpdateDesignationMaster(DesignationMaster obj)
        {
            DesignationMaster c = this.GetByID(obj.DesignationID);
            c.DesignationName = obj.DesignationName;
            c.DesignationType = obj.DesignationType;
            c.UIDMod = obj.UIDMod;
            c.ModDate = DateTime.Now;
            this.Update(c);
            return;
        }

    }
    #region METADATA
    [MetadataType(typeof(DesignationMasterMetadata))]
    public partial class DesignationMaster
    {
    }

    public class DesignationMasterMetadata
    {
        [Key]
        public short DesignationID { get; set; } // Has to have the same type and name as your model
        [Required(ErrorMessage = "Designation Name is required")]
        public string DesignationName { get; set; } // Has to have the same type and name as your model

    }
    #endregion
}