using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class ExamCategoryRepository : GenericRepository<ExamMaster> 
    {
        public ExamCategoryRepository () : base(new dbSchoolAppEntities()) { }
        public  ExamCategoryRepository (dbSchoolAppEntities dbContext): base(dbContext) {}


        public List<ExamMaster> GetExamCategoryList(byte mCompID, byte mBranchID)
        {
            List<ExamMaster> obj = new List<ExamMaster>();
            obj = this.context.ExamMasters.Where(x => x.ExamID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public ExamMaster CheckDuplicateExamName(ExamMaster obj)
        {

            ExamMaster objnew = this.context.ExamMasters.Where(x => x.ExamName == obj.ExamName && x.CompID == obj.CompID && x.BranchID == obj.BranchID).FirstOrDefault();
            
            return objnew;
        }

        public void UpdateExamMaster(ExamMaster obj)
        {
            ExamMaster objNew = this.GetByID(obj.ExamID);
            if (objNew != null)
            {
                objNew.ExamName = obj.ExamName;

                this.Update(objNew);
            }

        }




    }


   


}
