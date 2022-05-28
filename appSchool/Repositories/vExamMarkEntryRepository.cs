using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;
using System.Data.SqlClient;

namespace appSchool.Repositories
{
    public class vExamMarkEntryRepository : GenericRepository<vExamMarkEntry> 
    {
        public vExamMarkEntryRepository() : base(new dbSchoolAppEntities()) { }
        public vExamMarkEntryRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        //public List<vExamMarkEntry> GetStudentListForMarkEntry()
        //{
        //    List<vExamMarkEntry> obj=new List<vExamMarkEntry>();

        //    return obj;
        //}


        public List<vExamMarkEntry> GetStudentListForMarkEntry(int mClassID,int mClassSetupID,int mExamID,int mExamOrder,int mMaxMark,int mMinMark,int mSubjectID1,byte mUidAdd ,byte mCompID, byte mBranchID,byte mSessionID)
        {

            List<vExamMarkEntry> objList = new List<vExamMarkEntry>();

            var param = new[] { 
                           new SqlParameter("@ClassID", mClassID),
                           new SqlParameter("@ClassSetupID", mClassSetupID),
                           new SqlParameter("@ExamID", mExamID),
                           new SqlParameter("@SubjectID", mSubjectID1),
                           new SqlParameter("@ExamOrder", mExamOrder),
                           new SqlParameter("@MaxMark", mMaxMark),
                           new SqlParameter("@MinMark", mMinMark),
                           new SqlParameter("@UidAdd", mUidAdd),
                           new SqlParameter("@CompID", mCompID),
                           new SqlParameter("@BranchID", mBranchID),
                           new SqlParameter("@SessionID", mSessionID),
                            };

            objList = this.context.Database.SqlQuery<vExamMarkEntry>(
                                     "GetStudentListForExamMarkEntry @ClassID,@ClassSetupID,@ExamID,@SubjectID,@ExamOrder,@MaxMark,@MinMark,@UidAdd, @CompID, @BranchID, @SessionID",
                                      param
                             ).ToList();

            return objList;
        }


        public void UpdateStudentMarks(vExamMarkEntry obj,int mClassID,int mClassSetupID, int mExamID, int mSubjectID)
        {
            if (obj != null)
            {
                




            }

        }




    }








}
