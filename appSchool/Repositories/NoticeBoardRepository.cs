using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace appSchool.Repositories
{
    public class NoticeBoardRepository : GenericRepository<NoticeBoard>
    {
        public NoticeBoardRepository() : base(new dbSchoolAppEntities()) { }
        public NoticeBoardRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<NoticeBoard> GetNoticeBoardList(byte mCompID, byte mBranchID)
        {
            List<NoticeBoard> obj = new List<NoticeBoard>();
            obj = this.context.NoticeBoards.Where(x => x.NoticeID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).OrderByDescending(x => x.NoticeID).ToList();
            return obj;
        }

        public void AddNewNotice(NoticeBoard obj, byte UserID)
        {
            obj.UIDAdd = UserID;
            obj.AddDate = DateTime.Now;
            this.Insert(obj);
        }

        public void UpdateNotice(NoticeBoard obj, byte UserID)
        {
            NoticeBoard newObj = this.GetByID(obj.NoticeID);
            //newObj.NoticePerson = obj.NoticePerson;
            newObj.Notice = obj.Notice;
            newObj.IsActive = obj.IsActive;
            newObj.NoticeOrder = obj.NoticeOrder;
            //newObj.FromDate = obj.FromDate;
            //newObj.ToDate   = obj.ToDate;
            newObj.UIDMod = UserID;
            newObj.ModDate = DateTime.Now;
            this.Update(newObj);
        }


    }

    #region METADATA


    public partial class modelNoticeBoard
    {
        public int NoticeID { get; set; }
        [Required(ErrorMessage = "NoticePerson can't be Blank!!")]
        public string NoticePerson { get; set; }
        [Required(ErrorMessage = "Notice can't be Blank!!")]
        public string Notice { get; set; }
        [Required(ErrorMessage = "FromDate can't be Blank!!")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> FromDate { get; set; }
        [Required(ErrorMessage = "ToDate can't be Blank!!")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<byte> UIDAdd { get; set; }
        public Nullable<System.DateTime> AddDate { get; set; }
        public Nullable<byte> UIDMod { get; set; }
        public Nullable<System.DateTime> ModDate { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }
    }

   

 
  
    #endregion


}