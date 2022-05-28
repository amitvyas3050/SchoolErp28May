using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using System.IO;
using appSchool.Model;

namespace appSchool.Repositories
{
    public class PhotoGalleryMasterRepository : GenericRepository<PhotoGalleryMaster>
    {
        public PhotoGalleryMasterRepository() : base(new dbSchoolAppEntities()) { }
        public PhotoGalleryMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public void InsertData(PhotoGalleryMaster obj)
        {
            this.Insert(obj);
        }


        public List<PhotoGalleryMaster> GetPhotoGalleryList(byte mCompID, byte mBranchID)
        {
            List<PhotoGalleryMaster> obj = new List<PhotoGalleryMaster>();
            obj = this.context.PhotoGalleryMasters.Where(x => x.GalleryID > 0).OrderByDescending(x => x.GalleryID).ToList();
            return obj;
        }

        public PhotoGalleryMaster GetGallerydetail(int mGalleryID)
        {
            PhotoGalleryMaster obj = new PhotoGalleryMaster();
            obj = this.context.PhotoGalleryMasters.Where(x => x.GalleryID == mGalleryID).SingleOrDefault();
            return obj;
        }


    }


   



}