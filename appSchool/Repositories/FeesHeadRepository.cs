using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class FeesHeadRepository: GenericRepository<FeesHead>
    {
        public FeesHeadRepository() : base(new dbSchoolAppEntities()) { }
        public FeesHeadRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<FeesHead> GetFeesHeadList(byte mCompID, byte mBranchID)
        {
            List<FeesHead> obj = new List<FeesHead>();
            obj = this.context.FeesHeads.Where(x => x.FeesHeadID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public void UpdateFeesHead(FeesHead obj)
        {
            FeesHead objNew = this.GetByID(obj.FeesHeadID);
            if (objNew != null)
            {
                objNew.FeesHeadName = obj.FeesHeadName;
                objNew.FeesHeadType = obj.FeesHeadType;
                objNew.PrintHeadName = obj.PrintHeadName;
                objNew.DefaultAmount = obj.DefaultAmount;
                objNew.ISRebate = obj.ISRebate;
                objNew.UIDMod = obj.UIDMod;
                objNew.ModDate = obj.ModDate;
                
                this.Update(objNew);
            }
        }




        public List<VFeesCompulsory> GetTermHeadForFeeStructure(string mTermIDs, byte mCompID, byte mBranchID, byte mSessionID)
        {
            List<VFeesCompulsory> obj = new List<VFeesCompulsory>();
            string sql = "Select * from VFeesCompulSory where FeeTermID in (" + mTermIDs + ") AND CompID=" + mCompID + " AND BranchID=" + mBranchID + " AND SessionID=" + mSessionID + " and FeesHeadBranchID= 2 ";
            obj = this.context.VFeesCompulsories.SqlQuery("Select * from VFeesCompulSory where FeeTermID in (" + mTermIDs + ") AND CompID=" + mCompID + " AND BranchID=" + mBranchID + " AND SessionID=" + mSessionID + " and FeesHeadBranchID= 2  ").ToList();
            return obj;
        }




        public int CheckDelete(int mID)
        {
            int ID = 0;
            ID = this.context.FeesStructureDetails.Where(x => x.FeeHeadID == mID).Count();
            return ID;
        }







    }


     





    #region METADATA



    public class modelFeesStructure
    {
        public modalClass vClassList { get; set; }
        public List<VFeesCompulsory> vFeesCompulsoryList { get; set; }
    }


    public class modalClass
    {
        public short ClassID { get; set; } 
        public string ClassName { get; set; } 


    }








    [MetadataType(typeof(FeesHeadMetadata))]
    public partial class FeesHeadMaster
    {
    }

    public class FeesHeadMetadata
    {
        [Key]
        public short FeesHeadID { get; set; } // Has to have the same type and name as your model
        [Required(ErrorMessage = "FeesHeadName is required")]
        public string FeesHeadName { get; set; } // Has to have the same type and name as your model

    }
    #endregion
}