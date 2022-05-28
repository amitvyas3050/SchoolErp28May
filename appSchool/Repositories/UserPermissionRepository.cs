using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class UserPermissionRepository : GenericRepository<UserPermission>
    {
        public UserPermissionRepository() : base(new dbSchoolAppEntities()) { }
        public UserPermissionRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public int CheckDuplicatePermission(int mUserID, byte mCompID, byte mBranchID)
        {

            int objUserID = 0;
            UserPermission objuser = new UserPermission();
            objuser = this.context.UserPermissions.Where(i => i.UserId == mUserID && i.CompID==mCompID && i.BranchID==mBranchID ).FirstOrDefault();
            if (objuser != null)
            {
                objUserID = int.Parse(objuser.UserId.ToString());
            }

            return objUserID;
        }


        public List<vUserPermission> GetAllFormNameForPermission()
        {
            List<vUserPermission> objvUserPermission = new List<vUserPermission>();
            objvUserPermission = this.context.vUserPermissions.Where(x => x.MenuId > 0).ToList();
            return objvUserPermission;
        }


        public UserPermission CheckUserPermissionModulewise(int mUserID ,int mMenuID, byte mCompID, byte mBranchID )
        {
            UserPermission obj = new UserPermission();
            
            obj = this.context.UserPermissions.Where(a =>a.UserId == mUserID && a.MenuId == mMenuID && a.CompID==mCompID && a.BranchID==mBranchID).SingleOrDefault();
            return obj;
        }


        public List<vUserPermissionList> GetUserPermissionListUserwise(int UserID, byte mCompID, byte mBranchID)
        {
            byte muserID = byte.Parse(UserID.ToString());

            List<vUserPermissionList> objvUserPermission = new List<vUserPermissionList>();
            objvUserPermission = this.context.vUserPermissionLists.Where(x => x.UserId==muserID && x.CompID==mCompID && x.BranchID==mBranchID ).ToList();
            return objvUserPermission;
        }
        public void UpdateAllPermissionToUser(vUserPermissionList UPermit, bool Add, bool Mod, bool Del)
        {

            UserPermission editUPermit = this.GetByID(UPermit.PermitId);
            if (editUPermit != null)
            {

                editUPermit.AddP = Add;
                editUPermit.ModP = Mod;
                editUPermit.DelP = Del;

                this.Update(editUPermit);
            }
        }


        public void UpdateUserPermission(vUserPermissionList UPermit)
        {
            UserPermission editUPermit = this.GetByID(UPermit.PermitId);
            if (editUPermit != null)
            {
              
                editUPermit.AddP = UPermit.AddP;
                editUPermit.ModP = UPermit.ModP;
                editUPermit.DelP = UPermit.DelP;

                this.Update(editUPermit);
            }




        }


       
    }
  
}