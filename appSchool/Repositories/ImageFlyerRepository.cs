using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class ImageFlyerRepository : GenericRepository<ImageFlyer> 
    {
        public ImageFlyerRepository() : base(new dbSchoolAppEntities()) { }
        public ImageFlyerRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<ImageFlyer> GetImageFlyerList(byte mCompID, byte mBranchID)
        {
            List<ImageFlyer> obj = new List<ImageFlyer>();
            obj = this.context.ImageFlyers.Where(x => x.FlyerID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).OrderBy(x => x.OrderNo).ToList();
            return obj;
        }


        // public void AddNewNewsEvent(NewsEventMaster obj)
        //{

        //    this.Insert(new NewsEventMaster() { EventTitle = obj.EventTitle, EventDescription = obj.EventDescription, CategoryType = obj.CategoryType, EFromDate = obj.EFromDate, EToDate = obj.EToDate, UIDAdd = obj.UIDAdd, AddDate = obj.AddDate, PublishDate=obj.PublishDate, CompID = obj.CompID, BranchID = obj.BranchID, });
        //    return;
        //}

        public void AddNewImageFlyer(ImageFlyer obj)
         {
             this.Insert(new ImageFlyer() { FlyerName = obj.FlyerName,IsActive=obj.IsActive,FlyerTime = obj.FlyerTime * 1000, OrderNo = obj.OrderNo,  CompID = obj.CompID, BranchID = obj.BranchID, });
             return;
         }

        public void UpdateImageFlyer(ImageFlyer obj)
        {
            ImageFlyer c = this.GetByID(obj.FlyerID);

            c.FlyerName = obj.FlyerName;
            c.OrderNo = obj.OrderNo;
            c.IsActive = obj.IsActive;
            c.FlyerTime = obj.FlyerTime;
            
            this.Update(c);
            return;
        }
        public void DeleteImageFlyer(ImageFlyer obj)
        {
            ImageFlyer c = this.GetByID(obj.FlyerID);
            c.FlyerName = obj.FlyerName;
            c.OrderNo = obj.OrderNo;
            
            this.Delete(c);
            return;
        }

        public int CheckEventDelete(int mImageFlyerID)
        {
            int ID = 0;
            ID = this.context.ImageFlyers.Where(x => x.FlyerID == mImageFlyerID).Count();
            return ID;
        }


        public modelImageFlyerinfo GetImageFlyerInfo(int ImageFlyerID)
        {
            modelImageFlyerinfo obj = (
                from xx in this.context.ImageFlyers
                where xx.FlyerID == ImageFlyerID
                select new modelImageFlyerinfo()
                {
                    ImageFlyerID = xx.FlyerID,

                    ImageFlyerName = xx.FlyerName,


                    BranchID = xx.BranchID,
                    CompID = xx.CompID,

                }).FirstOrDefault<modelImageFlyerinfo>();
            return obj;
        }


        public void UpdateImageFlyerInfo(modelImageFlyerinfo obj)
        {
            try
            {
                ImageFlyer newInfo = this.GetByID(obj.ImageFlyerID);

                newInfo.FlyerName = obj.ImageFlyerName;


                //newInfo.CategoryType = obj.CategoryType;
                //newInfo.EFromDate = obj.EFromDate;
                //newInfo.EToDate = obj.EToDate;
                //newInfo.PublishDate = obj.PublishDate;
                //newInfo.UIDAdd = obj.UIDAdd;
                //newInfo.AddDate = obj.AddDate;
                //newInfo.BranchID = obj.BranchID;
                //newInfo.CompID = obj.CompID;


                this.Update(newInfo);
            }
            catch (Exception)
            {

                throw;
            }

        }


        public modelImageFlyerPhotoUploadInfo GetImageFlyerPhotoInfo(int ImageFlyerID)
        {
            modelImageFlyerPhotoUploadInfo obj = (
                from xx in this.context.ImageFlyers
                where xx.FlyerID == ImageFlyerID
                select new modelImageFlyerPhotoUploadInfo()
                {
                    ImageFlyerID = xx.FlyerID,
                    //EventTitle = xx.EventTitle,
                    ImageFlyerName = xx.FlyerName,
                   
                    ImageName = xx.ImageName,
                    BranchID = xx.BranchID,
                    CompID = xx.CompID,


                }).FirstOrDefault<modelImageFlyerPhotoUploadInfo>();
            return obj;
        }


        public void UpdateImageFlyerPhoto(ImageFlyer obj)
        {
            try
            {
                ImageFlyer newInfo = this.GetByID(obj.FlyerID);

                newInfo.ImageName = obj.ImageName;
                newInfo.ImagePath = obj.ImagePath;

                this.Update(newInfo);
            }
            catch (Exception)
            {

                throw;
            }

        }


    }



    #region METADATA

    public class modelImageFlyerinfo 
     {
        [Key]
        public int ImageFlyerID { get; set; }
        public string ImageFlyerName { get; set; }
        
        public byte UIDAdd { get; set; }
        public System.DateTime AddDate { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
    }

    public class modelImageFlyerPhotoUploadInfo
    {
        [Key]
        public int ImageFlyerID { get; set; }

        public string ImageFlyerName { get; set; }
        
        public string ImageName { get; set; }
        public byte UIDAdd { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
       
    }
    #endregion

}
