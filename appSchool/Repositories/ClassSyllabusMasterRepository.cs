using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class ClassSyllabusMasterRepository: GenericRepository<ClassSyllabusMaster>
    {
        public ClassSyllabusMasterRepository() : base(new dbSchoolAppEntities()) { }
        public ClassSyllabusMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }




        public int GetClassSyllabusIDByClassIDANDSubjectID(int mClassID, int mSubjectID, byte mCompID, byte mBranchID)
        {
            int ClassSyllabusID = 0;

            ClassSyllabusMaster objnew = new ClassSyllabusMaster();
            objnew = this.context.ClassSyllabusMasters.Where(x => x.ClassID == mClassID && x.SubjectIDL1 == mSubjectID && x.CompID == mCompID && x.BranchID == mBranchID).SingleOrDefault();
            if (objnew!=null)
            {
                ClassSyllabusID = objnew.ClassSyllabusID;
            }
            else
            {
                ClassSyllabusID = 0;
            }
            return ClassSyllabusID;
        }

        public ClassSyllabusMaster GetClassSyllabusDataByClassIDANDSubjectID(int mClassID, int mSubjectID, byte mCompID, byte mBranchID)
        {
            int ClassSyllabusID = 0;

            ClassSyllabusMaster objnew = new ClassSyllabusMaster();
            objnew = this.context.ClassSyllabusMasters.Where(x => x.ClassID == mClassID && x.SubjectIDL1 == mSubjectID && x.CompID == mCompID && x.BranchID == mBranchID).SingleOrDefault();
            if (objnew != null)
            {
                ClassSyllabusID = objnew.ClassSyllabusID;
            }
            else
            {
                ClassSyllabusID = 0;
            }
            return objnew;
        }



        public void InsertClassSyllabusMaster(ClassSyllabusMaster  obj)
        {
            this.Insert(obj);
        }

        public void UpdateSyllabusMaster(ClassSyllabusMaster obj)
        {
            this.Update(obj);

            //ClassSyllabusMaster objnew = this.GetByID(obj.ClassSyllabusID);
            //if (objnew != null)
            //{
            //    objnew.PreferredBooks = obj.PreferredBooks;
            //    objnew.UIDMod = obj.UIDMod;
            //    objnew.ModDate = obj.ModDate;

            //    this.Update(objnew);
            //}
        }




        public List<vSubjectAllotmentWithIDLOne> GetSubjectList(int mClassID)
        {

            List<vSubjectAllotmentWithIDLOne> obj = new List<vSubjectAllotmentWithIDLOne>();

            obj = this.context.vSubjectAllotmentWithIDLOnes.Where(x => x.ClassID == mClassID).ToList();


            return obj;
        }


      

    }



}