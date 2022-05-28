using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;

namespace appSchool.Repositories
{
    public class PhotoGallerdetailRepository : GenericRepository<PhotoGalleryDetail> 
    {
        public PhotoGallerdetailRepository() : base(new dbSchoolAppEntities()) { }
        public PhotoGallerdetailRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<PhotoGalleryDetail> GetPhotoGalleryDetailList(byte mCompID, byte mBranchID)
        {
            List<PhotoGalleryDetail> obj = new List<PhotoGalleryDetail>();
            obj = this.context.PhotoGalleryDetails.Where(x => x.GalleryDetailID > 0).OrderByDescending(x => x.GalleryDetailID).ToList();
            return obj;
        }

        public List<PhotoGalleryDetail> GetPhotoGalleryDetailList(int mGalleryID)
        {
            List<PhotoGalleryDetail> obj = new List<PhotoGalleryDetail>();
            obj = this.context.PhotoGalleryDetails.Where(x => x.GalleryID == mGalleryID).OrderBy(x => x.GalleryDetailID).ToList();
            return obj;
        }

        public void UpdatephotoOrder(PhotoGalleryDetail objFSDetail, byte CompID, byte BranchID)
        {
            PhotoGalleryDetail editFStructDetail = this.GetByID(objFSDetail.GalleryDetailID);
            if (editFStructDetail != null)
            {
                editFStructDetail.PhotoOrder = objFSDetail.PhotoOrder;
                //editFStructDetail.is = objFSDetail.FeeHeadID;
                //editFStructDetail.FeeTermID = objFSDetail.FeeTermID;
                //editFStructDetail.CompID = CompID;
                //editFStructDetail.BranchID = BranchID;

                this.Update(editFStructDetail);
            }

        }

       

    }








}
