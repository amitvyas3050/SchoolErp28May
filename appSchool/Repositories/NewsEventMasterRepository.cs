using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class NewsEventMasterRepository : GenericRepository<NewsEventMaster> 
    {
        public NewsEventMasterRepository() : base(new dbSchoolAppEntities()) { }
        public NewsEventMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<NewsEventMaster> GetNewsEventMasterList(byte mCompID, byte mBranchID)
        {
            List<NewsEventMaster> obj = new List<NewsEventMaster>();
            obj = this.context.NewsEventMasters.Where(x => x.EventID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }


        // public void AddNewNewsEvent(NewsEventMaster obj)
        //{

        //    this.Insert(new NewsEventMaster() { EventTitle = obj.EventTitle, EventDescription = obj.EventDescription, CategoryType = obj.CategoryType, EFromDate = obj.EFromDate, EToDate = obj.EToDate, UIDAdd = obj.UIDAdd, AddDate = obj.AddDate, PublishDate=obj.PublishDate, CompID = obj.CompID, BranchID = obj.BranchID, });
        //    return;
        //}

         public void AddNewNewsEventMaster(NewsEventMaster obj)
         {
             this.Insert(new NewsEventMaster() { EventTitle = obj.EventTitle, EventDescription = obj.EventDescription, CategoryType = obj.CategoryType, EFromDate = obj.EFromDate, EToDate = obj.EToDate, UIDAdd = obj.UIDAdd, AddDate = obj.AddDate, PublishDate = obj.PublishDate, CompID = obj.CompID, BranchID = obj.BranchID, });
             return;
         }

        public void UpdateNewsEventMaster(NewsEventMaster obj)
        {
            NewsEventMaster c = this.GetByID(obj.EventID);
            c.EventTitle = obj.EventTitle;
            c.EventDescription = obj.EventDescription;
            c.CategoryType = obj.CategoryType;
            c.EFromDate = obj.EFromDate;
            c.EToDate = obj.EToDate;
            
            this.Update(c);
            return;
        }
        public void DeleteNewsEventMaster(NewsEventMaster obj)
        {
            NewsEventMaster c = this.GetByID(obj.EventID);
            c.EventTitle = obj.EventTitle;
            c.EventDescription = obj.EventDescription;
            c.CategoryType = obj.CategoryType;

            c.EFromDate = obj.EFromDate;
            c.EFromDate = obj.EFromDate;
            


            this.Delete(c);
            return;
        }

        public int CheckEventDelete(int mEventID)
        {
            int ID = 0;
            ID = this.context.NewsEventMasters.Where(x => x.EventID == mEventID).Count();
            return ID;
        }


        public modelNewsEventinfo GetNewsEventInfo(int EventID)
        {
            modelNewsEventinfo obj = (
                from xx in this.context.NewsEventMasters
                where xx.EventID == EventID
                select new modelNewsEventinfo()
                {
                    EventID = xx.EventID,
                    EventTitle = xx.EventTitle,
                    EventDescription = xx.EventDescription,
                    CategoryType = xx.CategoryType,
                    PublishDate = xx.PublishDate,
                    EFromDate = xx.EFromDate,

                    EToDate = xx.EToDate,
                   
                    BranchID = xx.BranchID,
                    CompID = xx.CompID,

                }).FirstOrDefault<modelNewsEventinfo>();
            return obj;
        }


        public void UpdateNewsEventInfo(modelNewsEventinfo obj)
        {
            try
            {
                NewsEventMaster newInfo = this.GetByID(obj.EventID);
                newInfo.EventTitle = obj.EventTitle;
                newInfo.EventDescription = obj.EventDescription;
                newInfo.CategoryType = obj.CategoryType;
                newInfo.EFromDate = obj.EFromDate;
                newInfo.EToDate = obj.EToDate;
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


        public modelNewsEventPhotoUploadInfo GetEventPhotoInfo(int EventID)
        {
            modelNewsEventPhotoUploadInfo obj = (
                from xx in this.context.NewsEventMasters
                where xx.EventID == EventID
                select new modelNewsEventPhotoUploadInfo()
                {
                    EventID = xx.EventID,
                    EventTitle = xx.EventTitle,
                    EventDescription = xx.EventDescription,
                    CategoryType = xx.CategoryType,
                    PublishDate = xx.PublishDate,
                    EFromDate = xx.EFromDate,
                    EToDate = xx.EToDate,
                    ImageName = xx.ImageName,
                    BranchID = xx.BranchID,
                    CompID = xx.CompID,
                   
                   
                }).FirstOrDefault<modelNewsEventPhotoUploadInfo>();
            return obj;
        }


        public void UpdateEventPhoto(NewsEventMaster obj)
        {
            try
            {
                NewsEventMaster newInfo = this.GetByID(obj.EventID);
                
                newInfo.ImageName = obj.ImageName;


                this.Update(newInfo);
            }
            catch (Exception)
            {

                throw;
            }

        }


    }



    #region METADATA

    public class modelNewsEventinfo 
     {
        [Key]
        public int EventID { get; set; }
        public string EventTitle { get; set; }
        public string EventDescription { get; set; }
        public string CategoryType { get; set; }
        public System.DateTime PublishDate { get; set; }
        public System.DateTime EFromDate { get; set; }
        public System.DateTime EToDate { get; set; }
        public string ImageName { get; set; }
        public byte UIDAdd { get; set; }
        public System.DateTime AddDate { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
    }

    public class modelNewsEventPhotoUploadInfo
    {
        [Key]
        public int EventID { get; set; }
        public string EventTitle { get; set; }
        public string EventDescription { get; set; }
        public string CategoryType { get; set; }
        public System.DateTime PublishDate { get; set; }
        public System.DateTime EFromDate { get; set; }
        public System.DateTime EToDate { get; set; }
        public string ImageName { get; set; }
        public byte UIDAdd { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
       
    }
    #endregion

}
