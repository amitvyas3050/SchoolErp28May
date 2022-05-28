using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class FlyerMasterRepository : GenericRepository<FlyerMaster> 
    {
        public FlyerMasterRepository() : base(new dbSchoolAppEntities()) { }
        public FlyerMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<FlyerMaster> GetFlyerMasterList(byte mCompID, byte mBranchID)
        {
            List<FlyerMaster> obj = new List<FlyerMaster>();
            obj = this.context.FlyerMasters.Where(x => x.FlyerID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }


        // public void AddNewNewsEvent(NewsEventMaster obj)
        //{

        //    this.Insert(new NewsEventMaster() { EventTitle = obj.EventTitle, EventDescription = obj.EventDescription, CategoryType = obj.CategoryType, EFromDate = obj.EFromDate, EToDate = obj.EToDate, UIDAdd = obj.UIDAdd, AddDate = obj.AddDate, PublishDate=obj.PublishDate, CompID = obj.CompID, BranchID = obj.BranchID, });
        //    return;
        //}

        public void AddNewFlyerMaster(FlyerMaster obj)
         {
             this.Insert(new FlyerMaster() { FlyerName = obj.FlyerName,FlyerOrder=obj.FlyerOrder, IsActive = obj.IsActive, FromDate = obj.FromDate, ToDate = obj.ToDate, CompID = obj.CompID, BranchID = obj.BranchID, });
             return;
         }

        public void UpdateFlyerMaster(FlyerMaster obj)
        {
            FlyerMaster c = this.GetByID(obj.FlyerID);

            c.FlyerName = obj.FlyerName;
            c.FromDate = obj.FromDate;
            c.ToDate = obj.ToDate;
            c.IsActive = obj.IsActive;
            c.FlyerOrder=obj.FlyerOrder;
            
            this.Update(c);
            return;
        }
        public void DeleteFlyerMaster(FlyerMaster obj)
        {
            FlyerMaster c = this.GetByID(obj.FlyerID);
            c.FlyerName = obj.FlyerName;
            c.IsActive = obj.IsActive;


            this.Delete(c);
            return;
        }

        public int CheckEventDelete(int mFlyerID)
        {
            int ID = 0;
            ID = this.context.FlyerMasters.Where(x => x.FlyerID == mFlyerID).Count();
            return ID;
        }


        public modelFlyerMasterinfo GetFlyerMasterInfo(int FlyerID)
        {
            modelFlyerMasterinfo obj = (
                from xx in this.context.FlyerMasters
                where xx.FlyerID == FlyerID
                select new modelFlyerMasterinfo()
                {
                    FlyerID = xx.FlyerID,
                    FlyerName = xx.FlyerName,
                    BranchID = xx.BranchID,
                    CompID = xx.CompID,

                }).FirstOrDefault<modelFlyerMasterinfo>();
            return obj;
        }


        //public void UpdateAchievementInfo(modelAchievementinfo obj)
        //{
        //    try
        //    {
        //        Achievement newInfo = this.GetByID(obj.AchievementID);

        //        newInfo.AchievementDescription = obj.AchievementDescription;
        //        //newInfo.CategoryType = obj.CategoryType;
        //        //newInfo.EFromDate = obj.EFromDate;
        //        //newInfo.EToDate = obj.EToDate;
        //        //newInfo.PublishDate = obj.PublishDate;
        //        //newInfo.UIDAdd = obj.UIDAdd;
        //        //newInfo.AddDate = obj.AddDate;
        //        //newInfo.BranchID = obj.BranchID;
        //        //newInfo.CompID = obj.CompID;

               
        //        this.Update(newInfo);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}


        public modelFlyerMasterPhotoUploadInfo GetFlyerMasterPhotoInfo(int FlyerID)
        {
            modelFlyerMasterPhotoUploadInfo obj = (
                from xx in this.context.FlyerMasters
                where xx.FlyerID == FlyerID
                select new modelFlyerMasterPhotoUploadInfo()
                {
                    FlyerID = xx.FlyerID,
                    //EventTitle = xx.EventTitle,
                    FlyerName = xx.FlyerName,
                    ImageName = xx.ImageName,
                    BranchID = xx.BranchID,
                    CompID = xx.CompID,


                }).FirstOrDefault<modelFlyerMasterPhotoUploadInfo>();
            return obj;
        }


        public void UpdateFlyerMasterPhoto(FlyerMaster obj)
        {
            try
            {
                FlyerMaster newInfo = this.GetByID(obj.FlyerID);
                
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

    public class modelFlyerMasterinfo 
     {
        [Key]
        public int FlyerID { get; set; }
        public string FlyerName { get; set; }
        public string ImageName { get; set; }
        public byte UIDAdd { get; set; }
        public System.DateTime AddDate { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
    }

    public class modelFlyerMasterPhotoUploadInfo
    {
        [Key]
        public int FlyerID { get; set; }

        public string FlyerName { get; set; }        
        public string ImageName { get; set; }
        public byte UIDAdd { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
       
    }
    #endregion

}
