using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class FeesCategoryRepository: GenericRepository<FeesCategory>
    {
        public FeesCategoryRepository() : base(new dbSchoolAppEntities()) { }
        public FeesCategoryRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<FeesCategory> GetFeesCategoryList(byte mCompID, byte mBranchID)
        {
            List<FeesCategory> obj = new List<FeesCategory>();
            obj = this.context.FeesCategories.Where(x => x.CategoryID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;

        }


        public void UpdateFeesCategory(FeesCategory obj)
        {
            FeesCategory ObjNew = this.GetByID(obj.CategoryID);
            if (ObjNew != null)
            {
                ObjNew.CategoryName = obj.CategoryName;
                ObjNew.ModDate = obj.ModDate;
                ObjNew.UIDMod = obj.UIDMod;
                
                this.Update(ObjNew);
            }




        }




    }
  
}