using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class StudentPriviousDetailsRepository : GenericRepository<StudentPreviousDetail>
    {
        public StudentPriviousDetailsRepository() : base(new dbSchoolAppEntities()) { }
        public StudentPriviousDetailsRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }

        public void UpdateStudentPriviousDetail(StudentPreviousDetail obj)
        {
            StudentPreviousDetail c = this.GetByID(obj.StudentID);
            c.PreviousSchoolName = obj.PreviousSchoolName;
            c.PreviousClass = obj.PreviousClass;
            c.DateOfLeaving = obj.DateOfLeaving;
            c.ExamResult = obj.ExamResult;
            c.UIDMod = obj.UIDMod;
            c.ModDate = DateTime.Now;
            this.Update(c);
            return;
        }

       






        public StudentPreviousDetail GetStudentPriviousInfo(int ID)
        {
            StudentPreviousDetail obj = new StudentPreviousDetail();

            StudentPreviousDetail obj1 = new StudentPreviousDetail();
            obj1.StudentID = ID;


            obj = this.context.StudentPreviousDetails.Where(x => x.StudentID == ID).FirstOrDefault();
            if (obj == null)
            {
                return obj1;
            }
            else
            {
                return obj;
            }
           
        }


        public int CheckPreviousDatainTable(int mStudentID)
        {
            int ID = 0;
            StudentPreviousDetail obj = new StudentPreviousDetail();
            obj = this.context.StudentPreviousDetails.Where(x => x.StudentID == mStudentID).FirstOrDefault();
            if (obj!=null)
            {
                ID = obj.StudentID;
            }

            return ID;
        }

    }
    #region METADATA
    //[MetadataType(typeof(DepartmentMasterMetadata))]
    //public partial class DepartmentMaster
    //{
    //}

    //public class DepartmentMasterMetadata
    //{
    //    [Key]
    //    public short DepartmentID { get; set; } // Has to have the same type and name as your model
    //    [Required(ErrorMessage = "Department Name is required")]
    //    public string DepartmentName { get; set; } // Has to have the same type and name as your model

    //}
    #endregion
}