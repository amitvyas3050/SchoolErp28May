using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class GradeMasterRepository : GenericRepository<GradeMaster>
    {
        public GradeMasterRepository() : base(new dbSchoolAppEntities()) { }
        public GradeMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<GradeMaster> GetGradeMasterList(byte mCompID, byte mBranchID)
        {
            List<GradeMaster> obj = new List<GradeMaster>();
            obj = this.context.GradeMasters.Where(x => x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public void UpdateGradeMaster(GradeMaster obj, byte UserID)
        {
            GradeMaster objGM = this.GetByID(obj.GradeID);
            objGM.GradeName = obj.GradeName;
            objGM.GradePoint = obj.GradePoint;
            objGM.PFrom = obj.PFrom;
            objGM.PTo = obj.PTo;
            objGM.Rank = obj.Rank;
            objGM.Remark = obj.Remark;
            objGM.UIDMod = UserID;
            objGM.ModDate = DateTime.Now;
            this.Update(objGM);
            return;
        }

    }
  
}