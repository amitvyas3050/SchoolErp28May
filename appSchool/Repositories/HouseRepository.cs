using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;

namespace appSchool.Repositories
{
    public class HouseRepository : GenericRepository<House> 
    {
        public HouseRepository() : base(new dbSchoolAppEntities()) { }
        public HouseRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }
        //public IEnumerable<vClass> GetSectionsView()
        //{
        //    return this.context.vClasses.ToList<vClass>();
        //}

        public List<House> GetHouseList(byte mCompID,byte mBranchID)
        {
            List<House> obj = new List<House>();
            obj = this.context.Houses.Where(x => x.HouseID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).OrderBy(i=>i.HouseName).ToList();
            return obj;
        }


        public void AddNewHouse(House obj, byte UserID) 
        {
            this.Insert(new House() { HouseName = obj.HouseName, Description=obj.Description , UIDAdd = UserID, HouseColor=obj.HouseColor  ,AddDate = DateTime.Now, CompID=obj.CompID, BranchID=obj.BranchID });
            return;
        }
        public void UpdateHouse(House obj, byte UserID)
        {
            House c = this.GetByID(obj.HouseID);
            c.HouseName = obj.HouseName;
            c.Description = obj.Description;
            c.HouseColor = obj.HouseColor;
            c.UIDMod = UserID;
            c.ModDate = DateTime.Now;
            this.Update(c);
            return;
        }
        public void DeleteHouse(House obj)
        {
            this.Delete(obj);
            return;
        }


        public IEnumerable<listItem> GetHousesforSelectWithNone()
        {
            List<listItem> lst = new List<listItem>();
            lst.Add(new listItem() { Value = -1, Description = "(None)" });
            lst.AddRange(
                from xx in this.context.Houses
                select new listItem() { Value = xx.HouseID, Description = xx.HouseName }
                );
            return lst;
        }
        public IEnumerable<listItem> GetHousesforSelect()
        {
            IEnumerable<listItem> lst = null;
            lst = (
                from xx in this.context.Houses
                select new listItem() { Value = xx.HouseID, Description = xx.HouseName }
                );
            return lst;
        }
        public int CheckDelete(int mID)
        {
            int ID = 0;
            ID = this.context.StudentSessions.Where(x => x.HouseID == mID).Count();
            return ID;
        }



    }
}
