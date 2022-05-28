using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;

namespace appSchool.Repositories
{
    public class BranchRepository : GenericRepository<Branch> 
    {
        public BranchRepository() : base(new dbSchoolAppEntities()) { }
        public BranchRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<Branch> GetBranchList(byte mCompID)
        {
            List<Branch> obj = new List<Branch>();
            obj = this.context.Branches.Where(x =>x.CompID == mCompID).ToList();
            return obj;
        }

        public string GetBranchName(byte BranchID)
        {
            string mBranch = string.Empty;

            Branch obj = this.context.Branches.Where(x => x.BranchID == BranchID).FirstOrDefault();
            if (obj != null)
            {
                mBranch = obj.BranchName;
            }

            return mBranch;
        }


    }








}
