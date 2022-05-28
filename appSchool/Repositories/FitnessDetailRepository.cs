using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class FitnessDetailRepository : GenericRepository<FitnessDetail> 
    {
        public FitnessDetailRepository() : base(new dbSchoolAppEntities()) { }
        public FitnessDetailRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<FitnessDetail> GetFitnessDetailListByBusID(int mBusID, byte mCompID, byte mBranchID)
        {
            List<FitnessDetail> objDetail = new List<FitnessDetail>();
            objDetail = this.context.FitnessDetails.Where(x => x.VehicleID == mBusID && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return objDetail;
        }

        public void AddFitnessDetail(FitnessDetail obj)
        {
            this.Insert(obj);
        }

        public void UpdateFitnessDetail(FitnessDetail obj)
        {
            FitnessDetail objnew = this.GetByID(obj.FitnessID);
            if (objnew != null)
            {
                objnew.FitnessDate = obj.FitnessDate;
                objnew.FitnessDocNo = obj.FitnessDocNo;
                objnew.FromDate = obj.FromDate;
                objnew.ToDate = obj.ToDate;
                objnew.UIdMod = obj.UIdMod;
                objnew.ModDate = obj.ModDate; 

                this.Update(objnew);
            }

        }
      



    }
  


}
