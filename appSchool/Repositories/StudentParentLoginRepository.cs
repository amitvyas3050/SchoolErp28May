using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class StudentParentLoginRepository : GenericRepository<StudentParentLogin>
    {
        public StudentParentLoginRepository() : base(new dbSchoolAppEntities()) { }
        public StudentParentLoginRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public void AddNewStudentParentLogin(StudentRegistration obj)
        {
            if (obj.StudentID != null)
            {
            StudentParentLogin newObj = new StudentParentLogin();
            newObj.StudentID = obj.StudentID;
            newObj.ParentLoginId = string.Empty;
            newObj.ParentPassword = string.Empty;
            newObj.StudentLoginID = string.Empty;
            newObj.StudentPassword = string.Empty;
            newObj.CompID =obj.CompID;
            newObj.BranchID = obj.BranchID;

            this.Insert(newObj);
            }
        }

       
     










       
    }
  
}