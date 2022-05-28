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
    public class   TeacherDocumentRepository : GenericRepository<TeacherDocumentDetail> 
    {
        public TeacherDocumentRepository() : base(new dbSchoolAppEntities()) { }
        public TeacherDocumentRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<TeacherDocumentDetail> GeTeacherDetailListByTeacherID(int mTeacherID)
        {
            List<TeacherDocumentDetail> objlst = new List<TeacherDocumentDetail>();
            objlst = this.context.TeacherDocumentDetails.Where(x => x.TeacherID == mTeacherID).ToList();
            return objlst;
        }

        

    }
   
}
