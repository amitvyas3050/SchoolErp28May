using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;

namespace appSchool.Repositories
{
    public class ClassSetupRepository : GenericRepository<ClassSetup> 
    {
        public ClassSetupRepository() : base(new dbSchoolAppEntities()) { }
        public ClassSetupRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<ClassSetup> GetClassSetupList(byte mCompID, byte mBranchID)
        {
            List<ClassSetup> obj = new List<ClassSetup>();
            obj = this.context.ClassSetups.Where(i => i.ClassSetupID > 0 && i.CompID == mCompID && i.BranchID== mBranchID).OrderBy(i => i.DisplayOrder).ToList();

            return obj;
        }


        public void AddNewClassSetup(ClassSetup obj)
        {
            
            obj.ClassDescription = SetDescription(obj);
            Insert(obj);
            return;
        }
        public void UpdateClassSetup(ClassSetup obj, byte UserID)
        {
            ClassSetup objNew = this.GetByID(obj.ClassSetupID);
            if (objNew != null)
            {
                objNew.ClassDescription = SetDescription(obj);
                objNew.ClassCategoryID = obj.ClassCategoryID;
                objNew.ClassID = obj.ClassID;
                objNew.DisplayOrder = obj.DisplayOrder;
                objNew.SectionID = obj.SectionID;
                objNew.UIDMod = UserID;
                objNew.ModDate = DateTime.Now;

                Update(objNew);
            }
            return;
        }
        private string SetDescription(ClassSetup obj)
        {
            string strDescription = string.Empty;
            strDescription += this.context.Classes.Where(x => x.ClassID == obj.ClassID).FirstOrDefault().ClassName+" ";
            strDescription += this.context.Sections.Where(x => x.SectionID == obj.SectionID).FirstOrDefault().SectionName + "  ";
            //if(obj.ClassCategoryID!=null)
            //    strDescription += this.context.ClassCategories.Where(x => x.ClassCategoryID == obj.ClassCategoryID).FirstOrDefault().ClassCategoryName;
            return strDescription;
        }

        public IEnumerable<listItem> GetClassSetupItemsforClass(int typeID)
        {
            List<listItem> lst = new List<listItem>();
            lst.AddRange(
                from xx in this.context.ClassSetups
                where xx.ClassID == typeID
                orderby xx.DisplayOrder 
                select new listItem() { Value = xx.ClassSetupID, Description = xx.ClassDescription }
                );
            return lst;
        }
        public IEnumerable<listItem> GetClassSetupforSelectWithNone()
        {
            List<listItem> lst = new List<listItem>();
            lst.Add(new listItem() { Value = -1, Description = "(None)" });
            lst.AddRange(
                from xx in this.context.ClassSetups
                select new listItem() { Value = xx.ClassSetupID, Description = xx.ClassDescription }
                );
            return lst;
        }
        public IEnumerable<listItem> GetClassSetupforSelect()
        {
            IEnumerable<listItem> lst = null;
            lst =(
                from xx in this.context.ClassSetups
                select new listItem() { Value = xx.ClassSetupID, Description = xx.ClassDescription }
                );
            return lst;
        }
        public List<vClass> GetAllClassForSMS()
        {
            List<vClass> obj = new List<vClass>();
            obj = this.context.vClasses.Where(i => i.ClassID >0).ToList();

            return obj;
        }

        public List<ClassSetup> GetAllClassNameByClassID(int mClassID)
        {
            List<ClassSetup> obj = new List<ClassSetup>();
            obj = this.context.ClassSetups.Where(i => i.ClassID == mClassID).OrderBy(i => i.DisplayOrder).ToList();

            return obj;
        }

        public string GetClassNameByClassID(int classID)
        {
           
            string mclassName = this.context.vClasses.Where(x => x.ClassID == classID).SingleOrDefault().ClassName;

            return mclassName ;
            
        }
        
      

        public int CheckDelete(int mID)
        {
            int ID = 0;
            ID = this.context.StudentSessions.Where(x => x.ClassSetupID == mID).Count();
            return ID;
        }

        
    }








}
