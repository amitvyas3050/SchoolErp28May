using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using System.IO;
using appSchool.Model;

namespace appSchool.Repositories
{
    public class ThoughtMasterRepository : GenericRepository<ThoughtMaster>
    {
        public ThoughtMasterRepository() : base(new dbSchoolAppEntities()) { }
        public ThoughtMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public void InsertData(ThoughtMaster obj)
        {
            this.Insert(obj);
        }


        public List<ThoughtMaster> GetThoughtMasterList(byte mCompID, byte mBranchID)
        {
            List<ThoughtMaster> obj = new List<ThoughtMaster>();
            obj = this.context.ThoughtMasters.Where(x => x.ThoughtID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).OrderBy(x => x.ThoughtID).ToList();
            return obj;
        }


        public void AddNewThoughtMaster(ThoughtMaster obj)
        {
            this.Insert(new ThoughtMaster() { Thought = obj.Thought,  ThoughtOrder = obj.ThoughtOrder, FromDate = obj.FromDate, ToDate = obj.ToDate, UIDAdd = obj.UIDAdd, AddDate = obj.AddDate,  CompID = obj.CompID, BranchID = obj.BranchID, });
            return;
        }

        public void UpdateThoughtMaster(ThoughtMaster obj)
        {
            ThoughtMaster c = this.GetByID(obj.ThoughtID);
            c.Thought = obj.Thought;
            c.ThoughtOrder = obj.ThoughtOrder;
            c.FromDate = obj.FromDate;
            c.ToDate = obj.ToDate;
            c.ModDate = obj.ModDate;
            c.UIDMod = obj.UIDMod;

            this.Update(c);
            return;
        }
        public void DeleteThoughtMaster(ThoughtMaster obj)
        {
            ThoughtMaster c = this.GetByID(obj.ThoughtID);
            c.Thought = obj.Thought;
            c.ThoughtOrder = obj.ThoughtOrder;
            c.FromDate = obj.FromDate;
            c.ToDate = obj.ToDate;



            this.Delete(c);
            return;
        }

        public int CheckThoughtDelete(int mThoughtID)
        {
            int ID = 0;
            ID = this.context.ThoughtMasters.Where(x => x.ThoughtID == mThoughtID).Count();
            return ID;
        }



    }






}