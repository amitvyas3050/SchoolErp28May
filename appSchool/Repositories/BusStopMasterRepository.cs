using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{


    public class BusStopMasterRepository : GenericRepository<BusStopMaster>
    {
        public BusStopMasterRepository() : base(new dbSchoolAppEntities()) { }
        public BusStopMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<BusStopMaster> GetBusStopMasterList(byte mCompID, byte mBranchID)
        {
           

            List<BusStopMaster> obj = new List<BusStopMaster>();
            obj = this.context.BusStopMasters.Where(x => x.StopID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public void UpdateBusStopMaster(BusStopMaster obj)
        {
            BusStopMaster objnew = this.GetByID(obj.StopID);
            if (objnew != null)
            {
                objnew.StopName = obj.StopName;
                objnew.Description = obj.Description;
                objnew.UIDMod = obj.UIDMod;
                objnew.ModDate = obj.ModDate;
                this.Update(objnew);
            }
            return;
        }


        public void DeleteBusStopMaster(BusStopMaster obj)
        {
            this.Delete(obj.StopID);
            return;
        }



    }
  
}