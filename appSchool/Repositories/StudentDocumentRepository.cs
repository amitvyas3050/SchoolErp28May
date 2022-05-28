using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using DevExpress.Xpo;


namespace appSchool.Repositories
{
    public class StudentDocumentRepository : GenericRepository<StudentDocumentDetail> 
    {
        public StudentDocumentRepository() : base(new dbSchoolAppEntities()) { }
        public StudentDocumentRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<StudentDocumentDetail> GetStudentDetailListByStudentID(int mStudentID)
        {
            List<StudentDocumentDetail> objlst = new List<StudentDocumentDetail>();
            objlst = this.context.StudentDocumentDetails.Where(x => x.StudentID == mStudentID).ToList();
            return objlst;
        }




    }
   
}
