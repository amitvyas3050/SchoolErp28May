using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class AchievementRepository : GenericRepository<Achievement> 
    {
        public AchievementRepository() : base(new dbSchoolAppEntities()) { }
        public AchievementRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<Achievement> GetAchievementList(byte mCompID, byte mBranchID)
        {
            List<Achievement> obj = new List<Achievement>();
            obj = this.context.Achievements.Where(x => x.AchievementID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }


        // public void AddNewNewsEvent(NewsEventMaster obj)
        //{

        //    this.Insert(new NewsEventMaster() { EventTitle = obj.EventTitle, EventDescription = obj.EventDescription, CategoryType = obj.CategoryType, EFromDate = obj.EFromDate, EToDate = obj.EToDate, UIDAdd = obj.UIDAdd, AddDate = obj.AddDate, PublishDate=obj.PublishDate, CompID = obj.CompID, BranchID = obj.BranchID, });
        //    return;
        //}

        public void AddNewAchievement(Achievement obj)
         {
             this.Insert(new Achievement() { AchievementDescription = obj.AchievementDescription,AchievementTitle =obj.AchievementTitle, Isactive =obj.Isactive, Order = obj.Order,  UIDAdd = obj.UIDAdd, AddDate = obj.AddDate,  CompID = obj.CompID, BranchID = obj.BranchID, });
             return;
         }

        public void UpdateAchievement(Achievement obj)
        {
            Achievement c = this.GetByID(obj.AchievementID);

            c.AchievementDescription = obj.AchievementDescription;
            c.Order = obj.Order; c.Isactive = obj.Isactive;
            c.AchievementTitle = obj.AchievementTitle;
            this.Update(c);
            return;
        }
        public void DeleteAchievement(Achievement obj)
        {
            Achievement c = this.GetByID(obj.AchievementID);
            c.AchievementDescription = obj.AchievementDescription;
            c.Isactive = obj.Isactive;
            c.AchievementTitle = obj.AchievementTitle;

            this.Delete(c);
            return;
        }

        public int CheckEventDelete(int mAchievementID)
        {
            int ID = 0;
            ID = this.context.Achievements.Where(x => x.AchievementID == mAchievementID).Count();
            return ID;
        }


        public modelAchievementinfo GetAchievementInfo(int AchievementID)
        {
            modelAchievementinfo obj = (
                from xx in this.context.Achievements
                where xx.AchievementID == AchievementID
                select new modelAchievementinfo()
                {
                    AchievementID = xx.AchievementID,
                    
                    AchievementDescription = xx.AchievementDescription,
                   
                   
                    BranchID = xx.BranchID,
                    CompID = xx.CompID,

                }).FirstOrDefault<modelAchievementinfo>();
            return obj;
        }


        public void UpdateAchievementInfo(modelAchievementinfo obj)
        {
            try
            {
                Achievement newInfo = this.GetByID(obj.AchievementID);

                newInfo.AchievementDescription = obj.AchievementDescription;
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


        public modelAchievementPhotoUploadInfo GetAchievementPhotoInfo(int AchievementID)
        {
            modelAchievementPhotoUploadInfo obj = (
                from xx in this.context.Achievements
                where xx.AchievementID == AchievementID
                select new modelAchievementPhotoUploadInfo()
                {
                    AchievementID = xx.AchievementID,
                    //EventTitle = xx.EventTitle,
                    AchievementDescription = xx.AchievementDescription,
                   
                    ImageName = xx.ImageName,
                    BranchID = xx.BranchID,
                    CompID = xx.CompID,


                }).FirstOrDefault<modelAchievementPhotoUploadInfo>();
            return obj;
        }


        public void UpdateAchievementPhoto(Achievement obj)
        {
            try
            {
                Achievement newInfo = this.GetByID(obj.AchievementID);
                
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

    public class modelAchievementinfo 
     {
        [Key]
        public int AchievementID { get; set; }
        public string AchievementDescription { get; set; }
        public string ImageName { get; set; }
        public byte UIDAdd { get; set; }
        public System.DateTime AddDate { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
    }

    public class modelAchievementPhotoUploadInfo
    {
        [Key]
        public int AchievementID { get; set; }

        public string AchievementDescription { get; set; }
        
        public string ImageName { get; set; }
        public byte UIDAdd { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
       
    }
    #endregion

}
