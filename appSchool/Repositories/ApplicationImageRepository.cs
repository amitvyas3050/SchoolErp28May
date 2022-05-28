using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class ApplicationImageRepository : GenericRepository<ApplicationImage>
    {
        public ApplicationImageRepository() : base(new dbSchoolAppEntities()) { }
        public ApplicationImageRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        //public IEnumerable<modelApplicationImage> GetAllUnassignedStudentsForClass(int SessionID, int ClassSetupID)
        //{
        //    var param = new[] { 
        //                   new SqlParameter("@SessionID", SessionID),
        //                    new SqlParameter("@ClassSetupID", ClassSetupID),
        //                    };
        //    return this.context.Database.SqlQuery<StudentSelectionModel>(
        //                             "GetAllUnassignedStudentsForClass @SessionID,@ClassSetupID",
        //                              param
        //                     ).ToList();
        //}


    }

   
    #region METADATA
    public class modelApplicationImage
    {
        [Key]
        public int AppImageID { get; set; }
        public Nullable<short> AppImageType { get; set; }
        public Nullable<int> AppImageSourceID { get; set; }
        public byte[] AppImage { get; set; }
        public string AppImageName { get; set; }
    }

    public class modelStudentPhotoSelection
    {
        [Key]
        public int AppImageID { get; set; }
        public Nullable<short> AppImageType { get; set; }
        public Nullable<int> AppImageSourceID { get; set; }
        public byte[] AppImage { get; set; }
        public string AppImageName { get; set; }
    }

#endregion
}