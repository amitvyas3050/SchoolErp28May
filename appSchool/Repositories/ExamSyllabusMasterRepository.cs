using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class ExamSyllabusMasterRepository : GenericRepository<ExamSyllabusMaster>
    {
        public ExamSyllabusMasterRepository() : base(new dbSchoolAppEntities()) { }
        public ExamSyllabusMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<vSubjectListByClassID> GetSubjectListByClassID(int mClassID)
        {
            List<vSubjectListByClassID> obj = new List<vSubjectListByClassID>();
            obj = this.context.vSubjectListByClassIDs.Where(x => x.ClassID == mClassID).ToList();
            return obj;
        }

       



        public ExamSyllabusMaster GetExamSyllabusMasterData(ExamSyllabusMaster obj)
        {
            ExamSyllabusMaster objNew = new ExamSyllabusMaster();
            objNew = this.context.ExamSyllabusMasters.Where(x => x.ClassID == obj.ClassID && x.SubjectIDL1 == obj.SubjectIDL1 && x.ExamID == obj.ExamID && x.CompID == obj.CompID && x.BranchID == obj.BranchID).SingleOrDefault();
            return objNew;
        }

        public void InsertExamSyllabusMaster(ExamSyllabusMaster obj)
        {
            this.Insert(obj);
        }
        public void UpdateExamSyllabusMaster(ExamSyllabusMaster obj)
        {
            ExamSyllabusMaster objnew = this.GetByID(obj.ExamSyllabusID);
            if (objnew != null)
            {
                objnew.UIDMod = obj.UIDMod;
                objnew.ModDate = obj.ModDate;
            }

        }

        public List<vExamListFromExamSetup> GetExamListFromExamSetup(byte mSessionID,byte mCompID, byte mBranchID)
        {

            List<vExamListFromExamSetup> obj = new List<vExamListFromExamSetup>();

            obj = this.context.vExamListFromExamSetups.Where( x =>x.SessionID==mSessionID &&  x.CompID == mCompID && x.BranchID == mBranchID).ToList();

            return obj;
        }


        public List<vClassSyllabusSubjectwise> GetClassSyllabusSubjectandClassWise(int mClassID, int mSubjectID, byte mCompID, byte mBranchID)
        {
            List<vClassSyllabusSubjectwise> obj = new List<vClassSyllabusSubjectwise>();
            obj = this.context.vClassSyllabusSubjectwises.Where(x => x.ClassID == mClassID && x.SubjectIDL1 == mSubjectID).ToList();
            return obj;
        }



    }




    public class modelSubjectOne
    {
        private int _SubjectID;
        private string _SubjectName;


        public int SubjectID
        {
            set { _SubjectID = value; }
            get { return _SubjectID; }

        }

        public string SubjectName
        {
            set { _SubjectName = value; }
            get { return _SubjectName; }

        }

        public modelSubjectOne()
        {
            _SubjectID = 0; _SubjectName = string.Empty;
        }



    }


}