using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class BookMasterRepository : GenericRepository<Lib_BookMaster>
    {
        public BookMasterRepository() : base(new dbSchoolAppEntities()) { }
        public BookMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public void AddBookMaster(Lib_BookMaster obj)
        {
            this.Insert(obj);
        }

        public List<Lib_BookMaster> GetBookMasterDataForGrid(byte mCompID, byte mBranchID)
        {
            List<Lib_BookMaster> objAS = new List<Lib_BookMaster>();
            objAS = this.context.Lib_BookMaster.Where(x => x.AccessionId >0 && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return objAS;
        }


        public void UpdateBookMaster(Lib_BookMaster obj)
        {
            Lib_BookMaster objnew = this.GetByID(obj.AccessionId);
            if (objnew != null)
            {
                objnew.AccessionNo = obj.AccessionNo;
                objnew.Author1 = obj.Author1;
                objnew.Author2 = obj.Author2;
                objnew.Author3 = obj.Author3;
                objnew.Author4 = obj.Author4;
                objnew.BookTitle = obj.BookTitle;
                objnew.ClassificationNo = obj.ClassificationNo;
                objnew.GrantedBy = obj.GrantedBy;
                objnew.ISBNNo = obj.ISBNNo;
                objnew.ISSNNo = obj.ISSNNo;
                objnew.Medium = obj.Medium;
                objnew.ModDate = obj.ModDate;
                objnew.Publisher = obj.Publisher;
                objnew.Source = obj.Source;
                objnew.Subject = obj.Subject;
                objnew.Volume = obj.Volume;
                objnew.WebUrl = obj.WebUrl;

                this.Update(objnew);
            }

        }

    }
   

}
