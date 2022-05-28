using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class InstalmentMasterRepository: GenericRepository<InstalmentMaster>
    {
        public InstalmentMasterRepository() : base(new dbSchoolAppEntities()) { }
        public InstalmentMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }
        

        public List<InstalmentMaster> GetInstalmentMasterList(byte mCompID, byte mBranchID)
        {
            List<InstalmentMaster> obj = new List<InstalmentMaster>();
            return obj;
    }
        


    }
    #region METADATA
    [MetadataType(typeof(InstalmentMasterMetadata))]
    public partial class InstalmentMaster
    {
    }

    public class InstalmentMasterMetadata
    {
        [Key]
        public short InstalmentID { get; set; } // Has to have the same type and name as your model
        [Required(ErrorMessage = "Instalment Name is required")]
        public string InstalmentName { get; set; } // Has to have the same type and name as your model

    }
    #endregion
}