using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class ImportedURLRepository : GenericRepository<ImportedURLMaster>
    {
        public ImportedURLRepository() : base(new dbSchoolAppEntities()) { }
        public ImportedURLRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<ImportedURLMaster> GetImportedURLMasterList(byte mCompID, byte mBranchID)
        {
            List<ImportedURLMaster> obj = new List<ImportedURLMaster>();
            obj = this.context.ImportedURLMasters.Where(x => x.URLID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).OrderByDescending(x => x.URLID).ToList();
            return obj;
        }

        public void AddNewNotice(ImportedURLMaster obj, byte UserID)
        {
            obj.UIDAdd = UserID;
            obj.AddDate = DateTime.Now;
           
            this.Insert(obj);
        }

        public void UpdateNotice(ImportedURLMaster obj, byte UserID)
        {
            ImportedURLMaster newObj = this.GetByID(obj.URLID);
            newObj.URL = obj.URL;
            newObj.URLHeading = obj.URLHeading;
            newObj.URLText = obj.URLText;
            newObj.FromDate = obj.FromDate;
            newObj.ToDate   = obj.ToDate;
            newObj.ModDate = DateTime.Now;
            
            this.Update(newObj);
        }


    }

    #region METADATA


    public partial class modelImportedURLMaster
    {
        public int URLID { get; set; }
        [Required(ErrorMessage = "Heading can't be Blank!!")]
        public string Heading { get; set; }
        [Required(ErrorMessage = "URLText can't be Blank!!")]
        public string URLText { get; set; }
        [Required(ErrorMessage = "URL can't be Blank!!")]
        public string URL { get; set; }
        [Required(ErrorMessage = "FromDate can't be Blank!!")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> FromDate { get; set; }
        [Required(ErrorMessage = "ToDate can't be Blank!!")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<byte> UIDAdd { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<byte> UIDMod { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }
    }

   

 
  
    #endregion


}