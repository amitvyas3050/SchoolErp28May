using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class ExamSyllabusDetailRepository : GenericRepository<ExamSyllabusDetail>
    {
        public ExamSyllabusDetailRepository() : base(new dbSchoolAppEntities()) { }
        public ExamSyllabusDetailRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        //public List<ExamSyllabusDetail> GetExamSyllabusDetailListByExamSyllabusID(int mExamSyllabusID, byte mCompID, byte MBranchID)
        //{
        //    List<ExamSyllabusDetail> obj = new List<ExamSyllabusDetail>();
        //    obj = this.context.ExamSyllabusDetails.Where(x => x.ExamSyllabusID == mExamSyllabusID && x.CompID == mCompID && x.BranchID == MBranchID).ToList();
        //    return obj;
        //}

        public List<vExamSyllabusDetail> GetExamSyllabusDetailListByExamSyllabusID(int mExamSyllabusID, byte mCompID, byte MBranchID)
        {
            List<vExamSyllabusDetail> obj = new List<vExamSyllabusDetail>();
            obj = this.context.vExamSyllabusDetails.Where(x => x.ExamSyllabusID == mExamSyllabusID && x.CompID == mCompID && x.BranchID == MBranchID).ToList();
            return obj;
        }


        public void InsertExamSyllabusDetail(ExamSyllabusDetail obj)
        {
            this.Insert(obj);
        }

        //public void UpdateFacultyAllotmentDetail(FacultyAllotmentDetail obj)
        //{
        //    FacultyAllotmentDetail objNew = this.GetByID(obj.ID);
        //    if (objNew != null)
        //    {
        //        objNew.SubjectIDL1 = obj.SubjectIDL1;
        //        objNew.SubjectIDL2 = obj.SubjectIDL2;
        //        objNew.SubjectIDL3 = obj.SubjectIDL3;

        //        this.Update(objNew);
        //    }

        //}


        //public List<vSubjectListByClassID> GetSubjectListByClassID(int mClassID, byte mCompID, byte mBranchID )
        //{
        //    List<vSubjectListByClassID> obj = new List<vSubjectListByClassID>();

        //    obj = this.context.vSubjectListByClassIDs.Where(x => x.ClassID == mClassID).OrderBy(x => x.Order1).ToList();

        //    return obj;
        //}


    }



}