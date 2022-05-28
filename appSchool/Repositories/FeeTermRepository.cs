using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class FeeTermRepository : GenericRepository<FeeTerm>
    {
        public FeeTermRepository() : base(new dbSchoolAppEntities()) { }
        public FeeTermRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<FeeTerm> GetFeeTermList(byte mCompID, byte mBranchID, byte mSessionID)
        {
            List<FeeTerm> obj = new List<FeeTerm>();
            obj = this.context.FeeTerms.Where(x => x.FeeTermID > 0 && x.CompID == mCompID && x.BranchID == mBranchID && x.SessionId==mSessionID).ToList();
            return obj;
        }

        public void UpdateFeeTerm(FeeTerm obj)
        {
            FeeTerm newObj = this.GetByID(obj.ID);
            newObj.FeeTermFromDate = obj.FeeTermFromDate;
            newObj.FeeTermName = obj.FeeTermName;
            newObj.FeeTermToDate = obj.FeeTermToDate;
            newObj.FeeTermType = obj.FeeTermType;
            newObj.FineAmount = obj.FineAmount;
            newObj.ModDate = obj.ModDate;
            newObj.UIDMod = obj.UIDMod;
            this.Update(newObj);
        }

        public int GetFeeTermID(byte mCompID, byte mBranchID)
        {
            int ID = 0;
            FeeTerm obj = new FeeTerm();
            obj = this.context.FeeTerms.Where(x => x.FeeTermID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).FirstOrDefault();
            if (obj != null)
            {
                ID = this.context.FeeTerms.Where(x => x.FeeTermID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).Max(x => x.FeeTermID);
            }
            ID = ID + 1;
            return ID;
        }



        public int CheckDelete(int mID)
        {
            int ID = 0;
            ID = this.context.FeesStructureDetails.Where(x => x.FeeTermID == mID).Count();
            return ID;
        }


    }




    //#region METADATA
    //[MetadataType(typeof(FeeTermMetadata))]
    //public partial class FeeTermMaster
    //{
    //}

    //public class FeeTermMetadata
    //{
    //    [Key]
    //    public short FeeTermID { get; set; } // Has to have the same type and name as your model
    //    [Required(ErrorMessage = "FeeTermName is required")]
    //    public string FeeTermName { get; set; } // Has to have the same type and name as your model
    //    public string FeeTermType { get; set; }
    //    public string FromDate { get; set; }
    //    public string ToDate { get; set; }
        
    //}
    //#endregion
}