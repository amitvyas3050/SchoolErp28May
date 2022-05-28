using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class PermitDetailRepository : GenericRepository<PermitDetail> 
    {
        public PermitDetailRepository() : base(new dbSchoolAppEntities()) { }
        public PermitDetailRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<PermitDetail> GetPermitDetailListByBusID(int mBusID, byte mCompID, byte mBranchID)
        {
            List<PermitDetail> objDetail = new List<PermitDetail>();
            objDetail = this.context.PermitDetails.Where(x => x.VehicleID == mBusID && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return objDetail;
        }

        public void AddPermitDetail(PermitDetail obj)
        {
            this.Insert(obj);         
        }

        public void UpdatePermitDetail(PermitDetail obj)
        {
            PermitDetail objnew = this.GetByID(obj.PermitId);
            if (objnew != null)
            {
                objnew.PermitDate = obj.PermitDate;
                objnew.PermitDocNo = obj.PermitDocNo;
                objnew.PermitType = obj.PermitType;
                objnew.State1 = obj.State1;
                objnew.State2 = obj.State2;
                objnew.State3 = obj.State3;
                objnew.ToDate = obj.ToDate;
                objnew.UIdMod = obj.UIdMod;
                objnew.ModDate = obj.ModDate;

                this.Update(objnew);
            }

        }
      



    }
  
  


}
