using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;
using System.Data.SqlClient;

namespace appSchool.Repositories
{
    public class SubjectAllotmentRepository : GenericRepository<SubjectAllotment> 
    {
        public SubjectAllotmentRepository() : base(new dbSchoolAppEntities()) { }
        public SubjectAllotmentRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }

      


        public List<SubjectLevelOne> GetSubjectlevelOneListByClassID(int mClassID, int mSubjectlevel, byte mCompID, byte mBranchID)
        {

          
            List<SubjectLevelOne> obj = new List<SubjectLevelOne>();
            var param = new[] { 
                new SqlParameter("@ClassID", mClassID),           
                new SqlParameter("@IDL1", mSubjectlevel),
                new SqlParameter("@CompID",mCompID),
                new SqlParameter("@BranchID", mBranchID),
                new SqlParameter("@SubjectLevelType" ,mSubjectlevel)
                };
            obj = this.context.Database.SqlQuery<SubjectLevelOne>(
                                     "GetSubjectAllotmentList @ClassID,@IDL1,@CompID,@BranchID,@SubjectLevelType",
                                      param
                             ).ToList();

            return obj;
      }


        public List<vSubjectAllotmentwithIDLTwo> GetSubjectlevelTwoListByClassID(int mClassID, int mSubjectlevel, byte mCompID, byte mBranchID)
        {


            List<vSubjectAllotmentwithIDLTwo> obj = new List<vSubjectAllotmentwithIDLTwo>();
            var param = new[] { 
                new SqlParameter("@ClassID", mClassID),           
                new SqlParameter("@IDL1", mSubjectlevel),
                new SqlParameter("@CompID",mCompID),
                new SqlParameter("@BranchID", mBranchID),
                new SqlParameter("@SubjectLevelType" ,mSubjectlevel)
                };
            obj = this.context.Database.SqlQuery<vSubjectAllotmentwithIDLTwo>(
                                     "GetSubjectAllotmentList @ClassID,@IDL1,@CompID,@BranchID,@SubjectLevelType",
                                      param
                             ).ToList();

            return obj;
        }

        public List<vSubjectAllotmentwithIDLThree> GetSubjectlevelThreeListByClassID(int mClassID, int mSubjectlevel, byte mCompID, byte mBranchID)
        {


            List<vSubjectAllotmentwithIDLThree> obj = new List<vSubjectAllotmentwithIDLThree>();
            var param = new[] { 
                new SqlParameter("@ClassID", mClassID),           
                new SqlParameter("@IDL1", mSubjectlevel),
                new SqlParameter("@CompID",mCompID),
                new SqlParameter("@BranchID", mBranchID),
                new SqlParameter("@SubjectLevelType" ,mSubjectlevel)
                };
            obj = this.context.Database.SqlQuery<vSubjectAllotmentwithIDLThree>(
                                     "GetSubjectAllotmentList @ClassID,@IDL1,@CompID,@BranchID,@SubjectLevelType",
                                      param
                             ).ToList();

            return obj;
        }


        public List<SubjectLevelOne> GetSubjectlevelOneList(int mCompID, int mBranchID)
        {
            List<SubjectLevelOne> obj = new List<SubjectLevelOne>();
            obj = this.context.SubjectLevelOnes.Where(x => x.IdL1 > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public List<vSubjectAllotmentwithIDLTwo> GetSubjectlevelTwoList(int mCompID, int mBranchID)
        {
            List<vSubjectAllotmentwithIDLTwo> obj = new List<vSubjectAllotmentwithIDLTwo>();
            obj = this.context.vSubjectAllotmentwithIDLTwoes.Where(x => x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public List<vSubjectAllotmentwithIDLThree> GetSubjectlevelThreeList(int mCompID, int mBranchID)
        {
            List<vSubjectAllotmentwithIDLThree> obj = new List<vSubjectAllotmentwithIDLThree>();
            obj = this.context.vSubjectAllotmentwithIDLThrees.Where(x => x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }


        public List<SubjectLevelTwo> GetListSubjectlevelTwo(byte mCompID, byte mBranchID)
        {
            List<SubjectLevelTwo> obj = new List<SubjectLevelTwo>();
            obj = this.context.SubjectLevelTwoes.Where(x => x.IdL2 > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public List<SubjectLevelThree> GetListSubjectlevelhree(byte mCompID, byte mBranchID)
        {
            List<SubjectLevelThree> obj = new List<SubjectLevelThree>();
            obj = this.context.SubjectLevelThrees.Where(x => x.IdL3 > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

   

        public Class GetSubjectLevelByClassID(int mClassID)
        {
            byte SubjectLevel= 0;
            Class obj = new Class();
            obj = this.context.Classes.Where(x => x.ClassID == mClassID).SingleOrDefault();
            
            if (obj != null)
            {
                SubjectLevel = byte.Parse(obj.SubjectLevel.ToString());
            }

            return obj;
        }

        public SubjectLevelTwo GetSubjectlevelOneByLevelTwo(int mSubjectlevel2)
        {
            SubjectLevelTwo obj = new SubjectLevelTwo();
            obj = this.context.SubjectLevelTwoes.Where(x => x.IdL2 == mSubjectlevel2).SingleOrDefault();
            return obj;
        }

        public SubjectLevelThree GetSubjectlevelOneAndTwoByLevelThree(int mSubjectlevel3)
        {
            SubjectLevelThree obj = new SubjectLevelThree();
            obj = this.context.SubjectLevelThrees.Where(x => x.IdL3 == mSubjectlevel3).SingleOrDefault();
            return obj;
        }

        public List<SubjectAllotment> GetSubjectAllotmentList(int mClassID, int mSubjectlevelID)
        {
             List<SubjectAllotment> obj = new List<SubjectAllotment>();
             obj = this.context.SubjectAllotments.Where(x => x.ClassID == mClassID  ).ToList();
             return obj;
        }
      


      


       
    
    }

}
