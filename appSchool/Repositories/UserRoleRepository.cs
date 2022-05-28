using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;

namespace appSchool.Repositories
{
    public class UserRoleRepository : GenericRepository<UserRole> 
    {
        public UserRoleRepository() : base(new dbSchoolAppEntities()) { }
        public UserRoleRepository(dbSchoolAppEntities dbContext): base(dbContext) {}


        public List<UserRole> GetUserRoleList(byte mCompID, byte mBranchID)
        {
            List<UserRole> obj = new List<UserRole>();
            obj = this.context.UserRoles.Where(x => x.CompID == mCompID && x.BranchID == mBranchID ).ToList();

            
            return obj;
        }

        public UserRole GetUserRoleName(int roleID ,int mCompID, int mBranchID)
        {
            UserRole obj = new UserRole();
            obj = this.context.UserRoles.Where(x => x.Id == roleID &&  x.CompID == mCompID && x.BranchID == mBranchID).FirstOrDefault();
            return obj;
        }
       

       


        

        


    }








}
