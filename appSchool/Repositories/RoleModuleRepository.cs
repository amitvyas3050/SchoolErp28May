using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;

namespace appSchool.Repositories
{
    public class RoleModuleRepository : GenericRepository<RoleModule> 
    {
        public RoleModuleRepository() : base(new dbSchoolAppEntities()) { }
        public RoleModuleRepository(dbSchoolAppEntities dbContext): base(dbContext) {}


        public List<RoleModule> GetRoleModuleList(int mRoleID, byte mCompID, byte mBranchID)
        {
            List<RoleModule> obj = new List<RoleModule>();
            obj = this.context.RoleModules.Where(x => x.RoleId == mRoleID && x.CompID == mCompID && x.BranchID == mBranchID ).ToList();

            
            return obj;
        }



    }








}
