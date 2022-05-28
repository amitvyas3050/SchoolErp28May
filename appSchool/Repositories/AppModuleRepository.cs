using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;

namespace appSchool.Repositories
{
    public class AppModuleRepository : GenericRepository<AppModule> 
    {
        public AppModuleRepository() : base(new dbSchoolAppEntities()) { }
        public AppModuleRepository(dbSchoolAppEntities dbContext): base(dbContext) {}


        public List<AppModule> GetAppModuleList(byte mCompID, byte mBranchID)
        {
            List<AppModule> obj = new List<AppModule>();
            obj = this.context.AppModules.Where(x => x.CompID == mCompID && x.BranchID == mBranchID ).ToList();

            
            return obj;
        }

        public AppModule GetAppModuleName(int moduleID ,byte mCompID, byte mBranchID)
        {
            AppModule obj = new AppModule();
            obj = this.context.AppModules.Where(x => x.Id == moduleID &&  x.CompID == mCompID && x.BranchID == mBranchID).SingleOrDefault();
            return obj;
        }
       

       


        public void AddNewAppModule(AppModule obj) 
        {
            this.Insert(new AppModule() {Name= obj.Name, MenuIcon= obj.MenuIcon,  });
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
        public void UpdateAppModule(AppModule obj)
        {
            AppModule c = this.GetByID(obj.Id);
            c.Name = obj.Name;
            c.MenuIcon = obj.MenuIcon;
            //c.DisplayOrder = obj.DisplayOrder;
            //c.UIDMod = obj.UIDMod;
            //c.ModDate = obj.ModDate;
            this.Update(c);
            return;
        }
        public void DeleteAppModule(AppModule obj)
        {
            AppModule c = this.GetByID(obj.Id);
            this.Delete(c);
            return;
        }

        


    }








}
