using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class ClassSyllabusDetailRepository: GenericRepository<ClassSyllabusDetail>
    {
        public ClassSyllabusDetailRepository() : base(new dbSchoolAppEntities()) { }
        public ClassSyllabusDetailRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<ClassSyllabusDetail> GetClassSyllabusDetailList(byte mCompID, byte mBranchID)
        {
            List<ClassSyllabusDetail> obj = new List<ClassSyllabusDetail>();

            obj = this.context.ClassSyllabusDetails.Where(x => x.CompID == mCompID && x.BranchID == mBranchID).ToList();

            return obj;
        }

        public List<ClassSyllabusDetail> GetClassSyllabusDetailListByClassSyllabusID(int mClassSyllabusID,byte mCompID, byte mBranchID)
        {
            List<ClassSyllabusDetail> obj = new List<ClassSyllabusDetail>();

            obj = this.context.ClassSyllabusDetails.Where(x =>x.ClassSyllabusID==mClassSyllabusID && x.CompID == mCompID && x.BranchID == mBranchID).ToList();

            return obj;
        }


        public void InsertClassSyllabusDetail(ClassSyllabusDetail obj)
        {
            this.Insert(obj);

        }


        public void UpdateClassSyllabusDetail( ClassSyllabusDetail obj)
        {
            ClassSyllabusDetail objnew = this.GetByID(obj.ID);
            if (objnew != null)
            {
                objnew.Description = obj.Description;
                objnew.OrderNo = obj.OrderNo;
                objnew.SubjectUnit = obj.SubjectUnit;
                objnew.Topic = obj.Topic;

                this.Update(objnew);
            }
        }



    }



}