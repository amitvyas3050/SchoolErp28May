using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class FlyerVoiceMasterRepository : GenericRepository<FlyerVoiceMaster> 
    {
        public FlyerVoiceMasterRepository() : base(new dbSchoolAppEntities()) { }
        public FlyerVoiceMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<FlyerVoiceMaster> GetFlyerVoiceMasterList(byte mCompID, byte mBranchID)
        {
            List<FlyerVoiceMaster> obj = new List<FlyerVoiceMaster>();
            obj = this.context.FlyerVoiceMasters.Where(x => x.FlyerVoiceID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }


        // public void AddNewNewsEvent(NewsEventMaster obj)
        //{

        //    this.Insert(new NewsEventMaster() { EventTitle = obj.EventTitle, EventDescription = obj.EventDescription, CategoryType = obj.CategoryType, EFromDate = obj.EFromDate, EToDate = obj.EToDate, UIDAdd = obj.UIDAdd, AddDate = obj.AddDate, PublishDate=obj.PublishDate, CompID = obj.CompID, BranchID = obj.BranchID, });
        //    return;
        //}

        public void AddNewFlyerVoiceMaster(FlyerVoiceMaster obj)
         {
             this.Insert(new FlyerVoiceMaster() { FlyerVoiceName = obj.FlyerVoiceName, Isactive = obj.Isactive,  CompID = obj.CompID, BranchID = obj.BranchID, });
             return;
         }

        public void UpdateFlyerVoiceMaster(FlyerVoiceMaster obj)
        {
            FlyerVoiceMaster c = this.GetByID(obj.FlyerVoiceID);

            c.FlyerVoiceName = obj.FlyerVoiceName;
             c.Isactive = obj.Isactive;
            
            this.Update(c);
            return;
        }
        public void DeleteFlyerVoiceMaster(FlyerVoiceMaster obj)
        {
            FlyerVoiceMaster c = this.GetByID(obj.FlyerVoiceID);
            c.FlyerVoiceName = obj.FlyerVoiceName;
            c.Isactive = obj.Isactive;


            this.Delete(c);
            return;
        }

        public int CheckEventDelete(int mFlyerVoiceID)
        {
            int ID = 0;
            ID = this.context.FlyerVoiceMasters.Where(x => x.FlyerVoiceID == mFlyerVoiceID).Count();
            return ID;
        }


        public void UpdateVoiceActive(FlyerVoiceMaster objFSDetail, byte CompID, byte BranchID)
        {
            FlyerVoiceMaster editFStructDetail = this.GetByID(objFSDetail.FlyerVoiceID);
            if (editFStructDetail != null)
            {
                editFStructDetail.Isactive = objFSDetail.Isactive;
               

                this.Update(editFStructDetail);
            }

        }



        public modelFlyerVoiceMasterinfo GetFlyerVoiceMasterInfo(int FlyerVoiceID)
        {
            modelFlyerVoiceMasterinfo obj = (
                from xx in this.context.FlyerVoiceMasters
                where xx.FlyerVoiceID == FlyerVoiceID
                select new modelFlyerVoiceMasterinfo()
                {
                    FlyerVoiceID = xx.FlyerVoiceID,
                    FlyerVoiceName = xx.FlyerVoiceName,
                    BranchID = xx.BranchID,
                    CompID = xx.CompID,

                }).FirstOrDefault<modelFlyerVoiceMasterinfo>();
            return obj;
        }


        public void UpdateFlyerVoiceMasterInfo(modelFlyerVoiceMasterinfo obj)
        {
            try
            {
                FlyerVoiceMaster newInfo = this.GetByID(obj.FlyerVoiceID);

                newInfo.FlyerVoiceName = obj.FlyerVoiceName;
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


        public modelFlyerVoiceMasterPhotoUploadInfo GetFlyerVoiceMasterPhotoInfo(int FlyerVoiceID)
        {
            modelFlyerVoiceMasterPhotoUploadInfo obj = (
                from xx in this.context.FlyerVoiceMasters
                where xx.FlyerVoiceID == FlyerVoiceID
                select new modelFlyerVoiceMasterPhotoUploadInfo()
                {
                    FlyerVoiceID = xx.FlyerVoiceID,
                    //EventTitle = xx.EventTitle,
                    FlyerVoiceName = xx.FlyerVoiceName,

                    ImageName = xx.FileName,
                    BranchID = xx.BranchID,
                    CompID = xx.CompID,


                }).FirstOrDefault<modelFlyerVoiceMasterPhotoUploadInfo>();
            return obj;
        }


        public void UpdateFlyerVoiceMasterPhoto(FlyerVoiceMaster obj)
        {
            try
            {
                FlyerVoiceMaster newInfo = this.GetByID(obj.FlyerVoiceID);

                newInfo.FileName = obj.FileName;


                this.Update(newInfo);
            }
            catch (Exception)
            {

                throw;
            }

        }


    }



    #region METADATA

    public class modelFlyerVoiceMasterinfo 
     {
        [Key]
        public int FlyerVoiceID { get; set; }
        public string FlyerVoiceName { get; set; }
        public string ImageName { get; set; }
        public byte UIDAdd { get; set; }
        public System.DateTime AddDate { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
    }

    public class modelFlyerVoiceMasterPhotoUploadInfo
    {
        [Key]
        public int FlyerVoiceID { get; set; }

        public string FlyerVoiceName { get; set; }
        
        public string ImageName { get; set; }
        public byte UIDAdd { get; set; }
        public byte BranchID { get; set; }
        public byte CompID { get; set; }
       
    }
    #endregion

}
