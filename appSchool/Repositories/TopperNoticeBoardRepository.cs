using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class TopperNoticeBoardRepository : GenericRepository<TopperNoticeBoard> 
    {
        public TopperNoticeBoardRepository() : base(new dbSchoolAppEntities()) { }
        public TopperNoticeBoardRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }


        public List<TopperNoticeBoard> GetTopperNoticeBoardList(byte mCompID, byte mBranchID)
        {
            List<TopperNoticeBoard> obj = new List<TopperNoticeBoard>();
            obj = this.context.TopperNoticeBoards.Where(x => x.TNoticeID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }


        public List<vStudentTopperList>GetTopperList(byte mCompID, byte mBranchID)
        {
            List<vStudentTopperList> obj = new List<vStudentTopperList>();
            obj = this.context.vStudentTopperLists.Where(x => x.TNoticeID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public StudentRegistration GetTopperStudentID(int mStudentID, byte mCompID, byte mBranchID)
        {
            StudentRegistration obj = new StudentRegistration();
            obj = this.context.StudentRegistrations.Where(x => x.StudentID == mStudentID  &&   x.CompID == mCompID && x.BranchID == mBranchID).SingleOrDefault();
            return obj;
        }




        public void AddNewTopperNoticeBoard(TopperNoticeBoard obj) 
        {
            this.Insert(new TopperNoticeBoard() { 
                StudentID = obj.StudentID,
                                                  
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                FatherName = obj.FatherName,
                ClassID = obj.ClassID,
                AppImageName = obj.StudentID + ".png",
                //Gender = obj.Gender,
                
                UIDAdd = obj.UIDAdd, 
                AddDate = obj.AddDate, 
                CompID = obj.CompID, 
                BranchID = obj.BranchID,  });
            return;
        }


      

        public void UpdateTopperNoticeBoard(TopperNoticeBoard obj)
        {
            TopperNoticeBoard T = this.GetByID(obj.TNoticeID);
            T.FatherName = obj.FatherName;
            T.FirstName = obj.FirstName; T.LastName = obj.LastName; T.ClassID = obj.ClassID;
            T.Percentage = obj.Percentage;  T.IsTopper = obj.IsTopper;
            T.TopperOrder = obj.TopperOrder; T.RankPoint = obj.RankPoint;
            T.Session = obj.Session;
            //T.RankPoint = obj.RankPoint;
            //T.Gender = obj.Gender; 
            T.UIDMod = obj.UIDMod;
            T.ModDate = obj.ModDate;
            this.Update(T);
            return;
        }


        public void UpdateTopperStudent(TopperNoticeBoard obj)
        {
            TopperNoticeBoard T = this.GetByID(obj.TNoticeID);

            T.RankPoint = obj.RankPoint;
            T.Percentage = obj.Percentage;
            T.IsTopper = obj.IsTopper;
             T.ModDate = obj.ModDate;
            this.Update(T);
            return;
        }






        public void DeleteTopperNoticeBoard(TopperNoticeBoard obj)
        {
            TopperNoticeBoard T = this.GetByID(obj.TNoticeID);
            T.StudentID = obj.StudentID; T.FatherName = obj.FatherName;
            T.FirstName = obj.FirstName; T.LastName = obj.LastName; T.ClassID = obj.ClassID; 
            T.Gender = obj.Gender; T.Session = obj.Session;
            T.FromDate = obj.FromDate;
            T.ToDate = obj.ToDate;
            T.UIDMod = obj.UIDMod;
            T.ModDate = obj.ModDate;
            this.Delete(T);
            return;
        }

        public int CheckClassDelete(int mClassID)
        {
            int ID = 0;
            ID = this.context.ClassSetups.Where(x => x.ClassID == mClassID).Count();
            return ID;
        }

        public List<TopperNoticeBoard> GetTopperStudentList(byte mCompID, byte mBranchID)
        {
            List<TopperNoticeBoard> obj = new List<TopperNoticeBoard>();
            obj = this.context.TopperNoticeBoards.Where(x => x.TNoticeID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }


        public List<vStudentTopperList> GetOldTopperStudentList(byte mCompID, byte mBranchID)
        {
            List<vStudentTopperList> obj = new List<vStudentTopperList>();
            obj = this.context.vStudentTopperLists.Where(x => x.TNoticeID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public void UpdateNewsEventInfo(modelTopperNoticeinfo obj)
        {
            try
            {
                //TopperNoticeBoard newInfo = this.GetByID(obj.EventID);
                //newInfo.EventTitle = obj.EventTitle;
                //newInfo.EventDescription = obj.EventDescription;
                //newInfo.CategoryType = obj.CategoryType;
                //newInfo.EFromDate = obj.EFromDate;
                //newInfo.EToDate = obj.EToDate;
                //newInfo.PublishDate = obj.PublishDate;
                //newInfo.UIDAdd = obj.UIDAdd;
                //newInfo.AddDate = obj.AddDate;
                //newInfo.BranchID = obj.BranchID;
                //newInfo.CompID = obj.CompID;


                //this.Update(newInfo);
            }
            catch (Exception)
            {

                throw;
            }

        }


        public modelTopperNoticeinfoPhotoUploadInfo GetopperPhotoInfo(int TNoticeID)
        {
            modelTopperNoticeinfoPhotoUploadInfo obj = (
                from xx in this.context.TopperNoticeBoards
                where xx.TNoticeID == TNoticeID
                select new modelTopperNoticeinfoPhotoUploadInfo()
                {
                    TNoticeID = xx.TNoticeID,
                    StudentID = xx.StudentID,
                    FirstName = xx.FirstName,
                    LastName = xx.LastName,
                    //ClassID = xx.ClassID,
                    //Session = xx.Session,
                    ImageName = xx.AppImageName,
                    BranchID = xx.BranchID,
                    CompID = xx.CompID,


                }).FirstOrDefault<modelTopperNoticeinfoPhotoUploadInfo>();
            return obj;
        }


        public void UpdateTopperPhoto(TopperNoticeBoard obj)
        {
            try
            {
                TopperNoticeBoard newInfo = this.GetByID(obj.TNoticeID);

                newInfo.AppImageName = obj.AppImageName;


                this.Update(newInfo);
            }
            catch (Exception)
            {

                throw;
            }

        }


    }



    #region METADATA

    public class modelTopperNoticeinfo
    {
        [Key]
        public int TNoticeID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<byte> ClassID { get; set; }
      
        public string Session { get; set; }
        public string Gender { get; set; }
        public string ImageName { get; set; }
        public string Remark { get; set; }
        public string RankPoint { get; set; }
        public string Percentage { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<byte> UIDAdd { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<byte> UIDMod { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }
    }

    public class modelTopperNoticeinfoPhotoUploadInfo
    {
        [Key]
        public int TNoticeID { get; set; }
        public Nullable<int> StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<byte> ClassID { get; set; }
      
        public string ImageName { get; set; }
        public string Remark { get; set; }
    
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<byte> UIDMod { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }

    }
    #endregion




}
