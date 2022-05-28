using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class TaxDetailRepository : GenericRepository<TaxDetail> 
    {
        public TaxDetailRepository() : base(new dbSchoolAppEntities()) { }
        public TaxDetailRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<TaxDetail> GetTaxDetailListByBusID(int mBusID, byte mCompID, byte mBranchID)
        {
            List<TaxDetail> objDetail = new List<TaxDetail>();
            objDetail = this.context.TaxDetails.Where(x => x.VehicleID == mBusID && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return objDetail;
        }

        public void AddTaxDetail(TaxDetail obj)
        {
            this.Insert(obj);
        }

        public void UpdateTaxDetail(TaxDetail obj)
        {
            TaxDetail objnew = this.GetByID(obj.TaxDetailID);
            if (objnew != null)
            {
                objnew.FromDate = obj.FromDate;
                objnew.TaxAmount = obj.TaxAmount;
                objnew.TaxPaidDate = obj.TaxPaidDate;
                objnew.TaxReceiptNo = obj.TaxReceiptNo;
                objnew.ToDate = obj.ToDate;
                objnew.UIdMod = obj.UIdMod;
                objnew.ModDate = obj.ModDate;

                this.Update(objnew);
            }

        }
      



    }
  


}
