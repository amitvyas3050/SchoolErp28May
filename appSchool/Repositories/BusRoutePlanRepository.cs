using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class BusRoutePlanRepository : GenericRepository<BusRoutePlan>
    {
        public BusRoutePlanRepository() : base(new dbSchoolAppEntities()) { }
        public BusRoutePlanRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<BusRoutePlan> GetBusRoutePlanList(byte mCompID, byte mBranchID)
        {


            List<BusRoutePlan> obj = new List<BusRoutePlan>();
            obj = this.context.BusRoutePlans.Where(x => x.RoutePlanID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public void UpdateBusRoutePlan(BusRoutePlan obj)
        {
            BusRoutePlan objnew = this.GetByID(obj.RoutePlanID);
            if (objnew != null)
            {
                objnew.CleanerID = obj.CleanerID;
                objnew.DriverID = obj.DriverID;
                objnew.RouteID = obj.RouteID;
                objnew.UIDMod = obj.UIDMod;
                objnew.ModDate = obj.ModDate;
                this.Update(objnew);
            }
            return;
        }


        public void DeleteBusRoutePlan(BusRoutePlan obj)
        {
            this.Delete(obj.RoutePlanID);
            return;
        }

    }
  
}