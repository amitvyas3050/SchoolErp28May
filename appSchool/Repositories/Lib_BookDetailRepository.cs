using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class Lib_BookDetailRepository : GenericRepository<Lib_BookDetail> 
    {
        public Lib_BookDetailRepository () : base(new dbSchoolAppEntities()) { }
        public Lib_BookDetailRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }




      



    }



    #region MATADATA

    public class modelBookLostEntry
    {
        [Key]
        public int BookId { get; set; }
        public string AccessionNo { get; set; }
        public string Publisher { get; set; }
        public string MemberType { get; set; }
        public int LostByStudent { get; set; }
        public int LostByTeacher { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> LostDate { get; set; }
        public string Remark { get; set; }
       
    }



    #endregion

}
