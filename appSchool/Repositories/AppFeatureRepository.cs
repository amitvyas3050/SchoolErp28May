using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;

namespace appSchool.Repositories
{
    public class AppFeatureRepository : GenericRepository<AppFeature> 
    {

        public AppFeatureRepository() : base(new dbSchoolAppEntities()) { }
        public AppFeatureRepository(dbSchoolAppEntities dbContext): base(dbContext) {}


        public List<AppFeature> GetAppFeatureList(byte mCompID, byte mBranchID)
        {
            List<AppFeature> obj = new List<AppFeature>();
            obj = this.context.AppFeatures.Where(x => x.CompID == mCompID && x.BranchID == mBranchID ).ToList();

            
            return obj;
        }

        public List<AppFeature> GetAppFeatureListByModuleId(int moduleID, byte mCompID, byte mBranchID)
        {
            List<AppFeature> obj = new List<AppFeature>();
            obj = this.context.AppFeatures.Where(x => x.CompID == mCompID && x.BranchID == mBranchID && x.ModuleId == moduleID).ToList();


            return obj;
        }

        public AppFeature GetAppFeatureName(int ClassID ,byte mCompID, byte mBranchID)
        {
            AppFeature obj = new AppFeature();
            obj = this.context.AppFeatures.Where(x =>  x.CompID == mCompID && x.BranchID == mBranchID).SingleOrDefault();
            return obj;
        }

        public List<vUserRoleModulePermission> GetAllModuleAndFeatureList(byte mCompID, byte mBranchID)
        {
            List<vUserRoleModulePermission> obj = new List<vUserRoleModulePermission>();
            obj = this.context.vUserRoleModulePermissions.Where(x => x.CompID == mCompID && x.BranchID == mBranchID).ToList();

            return obj;
        }

        public List<vUserRoleModulePermission> GetAllModuleAndFeatureListByRoleID(int mRoleID, byte mCompID, byte mBranchID)
        {
            List<vUserRoleModulePermission> obj = new List<vUserRoleModulePermission>();
            obj = this.context.vUserRoleModulePermissions.Where(x => x.CompID == mCompID && x.BranchID == mBranchID && x.RoleId == mRoleID && x.CanView == true).ToList();

            return obj;
        }

        public List<vUserRoleModulePermission> GetAllModuleAndFeatureListByRoleIDOrderbyIndex(int moduleID, int mRoleID, byte mCompID, byte mBranchID)
        {
            List<vUserRoleModulePermission> obj = new List<vUserRoleModulePermission>();
            obj = this.context.vUserRoleModulePermissions.Where(x => x.ModuleId == moduleID && x.CompID == mCompID && x.BranchID == mBranchID && x.RoleId == mRoleID && x.CanView == true).OrderBy(y => y.FeatureIndex).ToList();

            return obj;
        }

        public List<vUserRoleModulePermission> GetAllModuleAndFeatureListByFiltertext(int mRoleID, byte mCompID, byte mBranchID,string FilterText)
        {
            List<vUserRoleModulePermission> obj = new List<vUserRoleModulePermission>();

            string sql = "SELECT * FROM dbo.vUserRoleModulePermission where CompID=" + mCompID + " AND BranchID=" + mBranchID + " AND CanView = 1  AND RoleId=" + mRoleID + " and FMenuText Like '%" + FilterText+"%' ";

            obj = this.context.vUserRoleModulePermissions.SqlQuery(sql).ToList();

            return obj;
        }



        public void AddNewAppModule(AppFeature obj) 
        {
            this.Insert(new AppFeature() {Name= obj.Name, MenuIcon= obj.MenuIcon,  });
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
        public void UpdateAppFeature(AppFeature obj)
        {
            AppFeature c = this.GetByID(obj.Id);
            c.Name = obj.Name;
            c.MenuIcon = obj.MenuIcon;
            //c.DisplayOrder = obj.DisplayOrder;
            //c.UIDMod = obj.UIDMod;
            //c.ModDate = obj.ModDate;
            this.Update(c);
            return;
        }
        public void DeleteAppModule(AppFeature obj)
        {
            AppFeature c = this.GetByID(obj.Id);
            this.Delete(c);
            return;
        }

        


    }








}
