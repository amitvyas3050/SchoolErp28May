using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class VStudentDetailRepository : GenericRepository<VStudentDetail> 
    {
        public VStudentDetailRepository() : base(new dbSchoolAppEntities()) { }
        public VStudentDetailRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }
        //public IEnumerable<vClass> GetSectionsView()
        //{
        //    return this.context.vClasses.ToList<vClass>();
        //}

        public List<VStudentDetail> GetVStudentDetailList(byte mCompID, byte mBranchID)
        {
            List<VStudentDetail> obj = new List<VStudentDetail>();
            obj = this.context.VStudentDetails.Where(x => x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            if (obj == null)
            {
                List<VStudentDetail> objNew = new List<VStudentDetail>();
                return objNew;
            }
            else
            {
                return obj;
            }
        }

    }


}
