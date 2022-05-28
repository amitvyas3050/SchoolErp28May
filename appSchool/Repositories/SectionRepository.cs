using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;

namespace appSchool.Repositories
{
    public class SectionRepository : GenericRepository<Section> 
    {
        public SectionRepository() : base(new dbSchoolAppEntities()) { }
        public SectionRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }
        //public IEnumerable<vClass> GetSectionsView()
        //{
        //    return this.context.vClasses.ToList<vClass>();
        //}

        public List<Section> GetSectionList(byte mCompID,byte mBranchID)
        {
            List<Section> obj = new List<Section>();
            obj = this.context.Sections.Where(x => x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        
        }

        public void AddNewSection(Section obj,byte UserID) 
        {
            this.Insert(new Section() { SectionName = obj.SectionName, UIDAdd = UserID, AddDate = DateTime.Now, BranchID=obj.BranchID, CompID=obj.CompID });
            return;
           
        }
        public void UpdateSection(Section obj, byte UserID)
        {
            Section c = this.GetByID(obj.SectionID);
            c.SectionName = obj.SectionName;
            c.UIDMod = UserID;
            c.ModDate = DateTime.Now;
            this.Update(c);
            return;
        }
        public void DeleteSection(Section obj)
        {
            this.Delete(obj);
            return;
        }
        public int CheckDelete(int mID)
        {
            int ID = 0;
            ID = this.context.ClassSetups.Where(x => x.SectionID == mID).Count();
            return ID;
        }
    
    
    }
}
