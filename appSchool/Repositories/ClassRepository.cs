using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;

namespace appSchool.Repositories
{
    public class ClassRepository : GenericRepository<Class> 
    {
        public ClassRepository() : base(new dbSchoolAppEntities()) { }
        public ClassRepository(dbSchoolAppEntities dbContext): base(dbContext) {}


        public List<Class> GetClassList(byte mCompID, byte mBranchID)
        {
            List<Class> obj = new List<Class>();
            obj = this.context.Classes.Where(x => x.ClassID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).OrderByDescending(x=>x.ClassID).ToList();
            return obj;
        }

        public Class GetClassName(int ClassID ,byte mCompID, byte mBranchID)
        {
            Class obj = new Class();
            obj = this.context.Classes.Where(x => x.ClassID == ClassID && x.CompID == mCompID && x.BranchID == mBranchID).SingleOrDefault();
            return obj;
        }


        public List<Class> GetToClassListForSessionTransfer(int mOrderID, byte mCompID,byte mBranchID)
        {
            List<Class> obj = new List<Class>();

            obj = this.context.Classes.Where(x => x.DisplayOrder >= mOrderID && x.CompID == mCompID && x.BranchID==mBranchID).OrderBy(x=>x.DisplayOrder).ToList();

            return obj;
        }

        public  IEnumerable<MVCxNavBarGroup> GetClassSetupNavigationBar(byte mCompID, byte mBranchID)
        {
            MVCxNavBarGroup grpMasters;
            List<MVCxNavBarGroup> lst = new List<MVCxNavBarGroup>();

            foreach (listItem typeItem in ((new ClassRepository()).GetClassesItems(mCompID,mBranchID)))
            {
                grpMasters = new MVCxNavBarGroup() { Text = typeItem.Description };
                grpMasters.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.MailAttach16x16;
                foreach (listItem termplateItem in ((new ClassSetupRepository()).GetClassSetupItemsforClass(typeItem.Value)))
                {
                    grpMasters.Items.Add(termplateItem.Description, termplateItem.Value.ToString()).Image.IconID = DevExpress.Web.ASPxThemes.IconID.ChartChart16x16;
                }
                lst.Add(grpMasters);
            }
            return lst;
        }


        public IEnumerable<vClass> GetClassView()
        {
            return this.context.vClasses.ToList<vClass>();
        }

        public IEnumerable<listItem> GetClassesItems(byte mCompID, byte mBranchID)
        {
            List<listItem> lst = new List<listItem>();
            lst.AddRange(
                from xx in this.context.Classes 
                where xx.ClassID>0  && xx.CompID==mCompID && xx.BranchID==mBranchID
                orderby xx.DisplayOrder 
                select new listItem() { Value = xx.ClassID, Description = xx.ClassName  } 
                );
            return lst;
        }

        public List<Class> GetClassList()
        {
            List<Class> obj = new List<Class>();
            obj = this.context.Classes.Where(i => i.ClassID > 0 ).OrderBy(i => i.DisplayOrder).ToList();

            return obj;
        }

        public void UpdateSubjectLevel(Class obj)
        {
            Class c = this.GetByID(obj.ClassID);
            c.SubjectLevel = obj.SubjectLevel;
            this.Update(c);
            return;
        }


        public void AddNewClass(Class obj) 
        {
            this.Insert(new Class() {ClassName= obj.ClassName, Description= obj.Description, DisplayOrder=obj.DisplayOrder ,UIDAdd=obj.UIDAdd,AddDate=obj.AddDate,CompID=obj.CompID, BranchID=obj.BranchID ,SubjectLevel=1 });
            return;
        }

        //public bool SaveUserLogClassHistory(Class obj, byte UserID)
        //{
        //    bool res = false;

        //    ClassHistory objHistory = new ClassHistory();
        //    objHistory.ClassID = obj.ClassID;
        //    objHistory.ClassName = obj.ClassName;
        //    objHistory.Description = obj.Description;
        //    objHistory.DisplayOrder = obj.DisplayOrder;
        //     objHistory.ChangedBy = UserID;
        //    objHistory.ChangedDate = DateTime.Now;
        //    objHistory.Deleted = false;
          
        //    return res;
        //}
        public void UpdateClass(Class obj)
        {
            Class c = this.GetByID(obj.ClassID);
            c.ClassName = obj.ClassName;
            c.Description = obj.Description;
            c.DisplayOrder = obj.DisplayOrder;
            c.UIDMod = obj.UIDMod;
            c.ModDate = obj.ModDate;
            this.Update(c);
            return;
        }
        public void DeleteClass(Class obj)
        {
            Class c = this.GetByID(obj.ClassID);
            this.Delete(c);
            return;
        }

        public int CheckClassDelete(int mClassID)
        {
            int ID = 0;
            ID = this.context.TopperNoticeBoards.Where(x => x.TNoticeID == mClassID).Count();
            return ID;
        }



    }








}
