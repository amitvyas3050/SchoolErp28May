using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class ExamSetupDetailRepository : GenericRepository<ExamSetupDetail>
    {
        public ExamSetupDetailRepository() : base(new dbSchoolAppEntities()) { }
        public ExamSetupDetailRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<ExamSetupDetail> GetExamSetupDetailListByExamSetupID(int mExamSetupID, byte mCompID, byte MBranchID)
        {
            List<ExamSetupDetail> obj = new List<ExamSetupDetail>();
            obj = this.context.ExamSetupDetails.Where(x => x.ExamSetupID == mExamSetupID && x.CompID == mCompID && x.BranchID == MBranchID).ToList();
            return obj;
        }

        public void InsertExamSetupDetail(ExamSetupDetail obj)
        {
            this.Insert(obj);
        }

        public void UpdateExamSetupDetail(ExamSetupDetail obj)
        {
            ExamSetupDetail objNew = this.GetByID(obj.ExamSetupDetailID);
            if (objNew != null)
            {
                objNew.ExamDate = obj.ExamDate;
                objNew.ExamTime = obj.ExamTime;
                objNew.MarksType = obj.MarksType;
                objNew.MaxMark = obj.MaxMark;
                objNew.MinMark = obj.MinMark;
                objNew.OrderNo = obj.OrderNo;
               
                this.Update(objNew);
            }

        }

        public List<SubjectAllotment> GetSubjectAllotmentListByClassID(int mClassID, byte mCompID, byte mBranchID)
        {

            List<SubjectAllotment> obj = new List<SubjectAllotment>();
            obj = this.context.SubjectAllotments.Where(x => x.ClassID == mClassID && x.CompID == mCompID && x.BranchID == mBranchID).ToList();

            return obj;
        }


    }



}