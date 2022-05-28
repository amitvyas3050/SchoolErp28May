using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class InsuranceDetailRepository : GenericRepository<InsuranceDetail> 
    {
        public InsuranceDetailRepository() : base(new dbSchoolAppEntities()) { }
        public InsuranceDetailRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }
       

        public List<InsuranceDetail> GetInsurenceDetailListByBusID(int mBusID, byte mCompID, byte mBranchID)
        {
            List<InsuranceDetail> objInsDetail = new List<InsuranceDetail>();
            objInsDetail = this.context.InsuranceDetails.Where(x => x.VehicleID == mBusID && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return objInsDetail;
        }

        public void AddInsuranceDetail(InsuranceDetail obj)
        {
            this.Insert(obj);         
        }

        public void UpdateInsuranceDetail(InsuranceDetail obj)
        {
            InsuranceDetail objnew = this.GetByID(obj.InsuranceID);
            if (objnew != null)
            {
                objnew.Address1 = obj.Address1;
                objnew.Address2 = obj.Address2;
                objnew.Address3 = obj.Address3;
                objnew.City = obj.City;
                objnew.ContactPerson = obj.ContactPerson;
                objnew.FromDate = obj.FromDate;
                objnew.InsuranceBranch = obj.InsuranceBranch;
                objnew.InsuranceCertificateNo = obj.InsuranceCertificateNo;
                objnew.InsuranceCompany = obj.InsuranceCompany;
                objnew.InsuranceDate = obj.InsuranceDate;
                objnew.MobileNo = obj.MobileNo;
                objnew.Phone = obj.Phone;
                objnew.PinCode = obj.PinCode;
                objnew.PolicyNo = obj.PolicyNo;
                objnew.ToDate = obj.ToDate;
                objnew.UIdMod = obj.UIdMod;
                objnew.ModDate = obj.ModDate;
                
                this.Update(objnew);
            }

        }
      



    }
  
  


}
