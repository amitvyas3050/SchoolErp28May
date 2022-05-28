using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class ClassCategoryRepository : GenericRepository<ClassCategory> 
    {
        public ClassCategoryRepository() : base(new dbSchoolAppEntities()) { }
        public ClassCategoryRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<ClassCategory> GetClassCategoryList(byte mCompID, byte mBranchID)
        {
            List<ClassCategory> obj = new List<ClassCategory>();
            obj = this.context.ClassCategories.Where(x => x.ClassCategoryID > 0 && x.CompID==mCompID && x.BranchID==mBranchID).ToList();
            return obj;
        }

        public IEnumerable<ClassCategory> GetClassCategoriesforSelect(byte mCompID, byte mBranchID)
        {
            List<ClassCategory> lst = new List<ClassCategory>();
            lst.Add(new ClassCategory() { ClassCategoryID = -1, ClassCategoryName = "(None)" });
            lst.AddRange(this.GetClassCategoryList(mCompID,mBranchID));
            return lst;
        }

        public void AddNewCategory(ClassCategory obj) 
        {
            this.Insert(obj);
            return;
        }
        public void UpdateCategory(ClassCategory obj)
        {
            ClassCategory c = this.GetByID(obj.ClassCategoryID);
            c.ClassCategoryName = obj.ClassCategoryName;
            c.ModDate = obj.ModDate;
            c.UIDMod = obj.UIDMod;
            this.Update(c);
            return;
        }
        public void DeleteCategory(ClassCategory obj)
        {
            this.Delete(obj);
            return;
        }

        public int CheckDelete(int mID)
        {
            int ID = 0;
            ID = this.context.ClassSetups.Where(x => x.ClassCategoryID == mID).Count();
            return ID;
        }


    }

    #region ClassCategory
    [MetadataType(typeof(ClassCategoryMetadata))]
    public partial class ClassCategory
    {
    }
    public class ClassCategoryMetadata
    {
        [Key]
        public int ClassCategoryID { get; set; } // Has to have the same type and name as your model
        [Required(ErrorMessage = "Cat Name needed")]
        [StringLength(50, ErrorMessage = "Can't be more than 50 chars.")]
        public string ClassCategoryName { get; set; } // Has to have the same type and name as your model

    }
    #endregion


}
