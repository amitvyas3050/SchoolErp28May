using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;

namespace appSchool.Repositories
{
    public class BookIssueSubmitRepository : GenericRepository<Lib_BookIssueSubmit>
    {
        public BookIssueSubmitRepository() : base(new dbSchoolAppEntities()) { }
        public BookIssueSubmitRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public void AddBookIssueByStudent(Lib_BookIssueSubmit obj)
        {
            this.Insert(obj);
        }


        public List<vStudentListForLibrary> GetStudentListForLibrary(byte mCompID, byte mBranchID, byte mSessionID)
        {
            List<vStudentListForLibrary> obj = new List<vStudentListForLibrary>();
            obj = this.context.vStudentListForLibraries.Where(x => x.CompID == mCompID && x.BranchID == mBranchID && x.SessionID==mSessionID).OrderBy(x => x.FullName).ToList();
            return obj;  
        }


        public vStudentListForLibrary GetStudentDetailByStudentID(int mStudentID, byte mCompID, byte mBranchID , byte mSessionID)
        {
            vStudentListForLibrary obj = new vStudentListForLibrary();
            obj = this.context.vStudentListForLibraries.Where(x => x.StudentID == mStudentID && x.CompID == mCompID && x.BranchID == mBranchID && x.SessionID==mSessionID).SingleOrDefault();
            return obj;
        }


        public List<vBookIssueSubmitDetail> GetTotalSubmitedBooksByStudent(int mStudentID, byte mCompID, byte mBranchID)
        {
            string mMemberType = "Student";
            List<vBookIssueSubmitDetail> obj = new List<vBookIssueSubmitDetail>();
            obj = this.context.vBookIssueSubmitDetails.Where(x => x.MemberID == mStudentID && x.MemberType == mMemberType && x.IsSubmitted == true && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public List<vBookIssueSubmitDetail> GetTotalIssueBooksByStudent(int mStudentID, byte mCompID, byte mBranchID)
        {
            string mMemberType = "Student";
            List<vBookIssueSubmitDetail> obj = new List<vBookIssueSubmitDetail>();
            obj = this.context.vBookIssueSubmitDetails.Where(x => x.MemberID == mStudentID && x.MemberType == mMemberType && x.IsSubmitted == false && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public List<vBookMasterDetailList> GetBookDetailListForIssueToStudent(byte mCompID, byte mBranchID)
        {
            List<vBookMasterDetailList> obj = new List<vBookMasterDetailList>();
            obj = this.context.vBookMasterDetailLists.Where(x => x.Issuable == true && x.IsAvailable==true  && x.CompID == mCompID && x.BranchID == mBranchID  ).ToList();
            return obj;
        }

        public vBookMasterDetailList GetBookDetailByBookId(int mBookId, byte mCompID, byte mBranchID)
        {
            vBookMasterDetailList obj = new vBookMasterDetailList();
            obj = this.context.vBookMasterDetailLists.Where(  x =>x.BookId==mBookId && x.Issuable == true && x.IsAvailable==true && x.CompID == mCompID && x.BranchID == mBranchID).FirstOrDefault();
            return obj;
        }


        public bool CheckTotalIssueBookForStudent(int mStudentID, byte mCompID, byte mBranchID)
        {
            bool res = false;
            int count = 0;
            count = this.context.Lib_BookIssueSubmit.Where(x => x.MemberID == mStudentID).Count();
            if (count < 5)
            {
                res = true;
            }
            return res;
        }

       

        public void UpdateData(vBookIssueSubmitDetail obj, byte UserID)
        {
            Lib_BookIssueSubmit objnew = this.GetByID(obj.IssueID);
            if (objnew != null)
            {
                objnew.IsSubmitted = obj.IsSubmitted;
                objnew.SubmitDate = obj.SubmitDate;
                objnew.Fine = obj.Fine;
                objnew.Remark = obj.Remark;
                objnew.ModDate = DateTime.Now;
                objnew.UIDMod = UserID;

                this.Update(objnew);
            }
        }


            //public List<DriverMaster> GetDriverMasterList(byte mCompID, byte mBranchID)
            //{
            //    List<DriverMaster> obj = new List<DriverMaster>();
            //    obj = this.context.DriverMasters.Where(i => i.DriverId > 0 && i.CompID == mCompID && i.BranchID == mBranchID).OrderBy(i => i.Dname).ToList();

            //    return obj;
            //}

            //public List<DriverMaster> GetOnlyCleanerList(byte mCompID, byte mBranchID)
            //{
            //    string mType="Cleaner";

            //    List<DriverMaster> obj = new List<DriverMaster>();
            //    obj = this.context.DriverMasters.Where(i => i.DriverId > 0 && i.EmpType ==mType && i.CompID == mCompID && i.BranchID == mBranchID).OrderBy(i => i.Dname).ToList();

            //    return obj;
            //}

            //public List<DriverMaster> GetOnlyDriverList(byte mCompID, byte mBranchID)
            //{
            //    string mType = "Driver";

            //    List<DriverMaster> obj = new List<DriverMaster>();
            //    obj = this.context.DriverMasters.Where(i => i.DriverId > 0 && i.EmpType == mType && i.CompID == mCompID && i.BranchID == mBranchID).OrderBy(i => i.Dname).ToList();

            //    return obj;
            //}


            //public void AddNewDriverMaster(DriverMaster obj)
            //{
            //    Insert(obj);
            //    return;
            //}
            //public void UpdateDriverMaster(DriverMaster obj)
            //{
            //    DriverMaster objNew = this.GetByID(obj.DriverId);
            //    if (objNew != null)
            //    {
            //        objNew.Address = obj.Address;
            //        objNew.Birthdate = obj.Birthdate;
            //        objNew.City = obj.City;
            //        objNew.CityID = obj.CityID;
            //        objNew.Dname= obj.Dname;
            //        objNew.DrEmailID = obj.DrEmailID;
            //        objNew.DrMobileNo = obj.DrMobileNo;
            //        objNew.DrMobileNo2 = obj.DrMobileNo2;
            //        objNew.EmpType = obj.EmpType;
            //        objNew.Gender = obj.Gender;
            //        objNew.IsActive = obj.IsActive;
            //        objNew.Licence = obj.Licence;
            //        objNew.LorryNo = obj.LorryNo;
            //        objNew.PAN = obj.PAN;
            //        objNew.Phone = obj.Phone;
            //        objNew.PinCode = obj.PinCode;
            //        objNew.RefAddress = obj.RefAddress;
            //        objNew.RefBy = obj.RefBy;
            //        objNew.RefMobileNo = obj.RefMobileNo;
            //        objNew.Remark = obj.Remark;
            //        objNew.RTO = obj.RTO;
            //        objNew.StateId = obj.StateId;

            //        objNew.uploadflag = obj.uploadflag;
            //        objNew.Validupto = obj.Validupto;
            //        objNew.UIDMod = obj.UIDMod;
            //        objNew.ModDate = DateTime.Now;

            //        Update(objNew);
            //    }
            //    return;
            //}




        }
     







}
