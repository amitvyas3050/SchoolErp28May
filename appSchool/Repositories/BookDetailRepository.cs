using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class BookDetailRepository : GenericRepository<Lib_BookDetail>
    {
        public BookDetailRepository() : base(new dbSchoolAppEntities()) { }
        public BookDetailRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public List<Lib_BookDetail> GetBookDetailListByMasterID(int mBookAccessionID,byte mCompID, byte mBranchID)
        {
            List<Lib_BookDetail> obj = new List<Lib_BookDetail>();
            obj = this.context.Lib_BookDetail.Where(x => x.AccessionId == mBookAccessionID && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }
        public void UpdateBookPageCover(Lib_BookDetail obj)
        {
            try
            {
                Lib_BookDetail newInfo = this.GetByID(obj.BookId);
                //newInfo.EnrollmentNo = obj.EnrollmentNo;
                //newInfo.EnrollmentDate = obj.EnrollmentDate;
                newInfo.FrontCoverImage = obj.FrontCoverImage;
                newInfo.FrontCover = obj.FrontCover;

                this.Update(newInfo);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void AddALLStudentAttendance(Lib_BookDetail obj)
        {
            this.Insert(obj);
        }

        public void UpdateBookDetailForIsAvaible(Lib_BookDetail obj)
        {
            Lib_BookDetail objnew = this.GetByID(obj.BookId);
            if (objnew != null)
            {
                objnew.IsAvailable = obj.IsAvailable;    
            }
        }

        public void UpdateBookDetail(Lib_BookDetail obj)
        {
            Lib_BookDetail objnew = this.GetByID(obj.BookId);
            if (objnew != null)
            {
                objnew.BillDate = obj.BillDate;
                objnew.BillNo = obj.BillNo;
                objnew.BookNo = obj.BookNo;
                objnew.CallNo = obj.CallNo;
                objnew.Edition = obj.Edition;
                objnew.IsAvailable = obj.IsAvailable;
                objnew.Issuable = obj.Issuable;
                objnew.Pages = obj.Pages;
                objnew.Price = obj.Price;
                objnew.PublishYear = obj.PublishYear;
                objnew.RackId = obj.RackId;
                objnew.ShelfId = obj.ShelfId;

                this.Update(objnew);
            }

        }



        //public List<vAttendanceStudent> GetAttendanceStudentForGrid(int ClassAttendanceID, byte mCompID, byte mBranchID)
        //{
        //    List<vAttendanceStudent> objAS = new List<vAttendanceStudent>();
        //    objAS = this.context.vAttendanceStudents.Where(x => x.ClassAttendanceID == ClassAttendanceID && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
        //    return objAS;
        //}

        //public List<AttendanceStudent> GetAbsentStudentClasswise(int ClassAttendanceID)
        //{
        //    // StudentSession obj = this.context.StudentSessions.Where(i => i.ClassSetupID == ClassSetupID ).firstordefault();

        //    List<AttendanceStudent> obj1 = this.context.AttendanceStudents.SqlQuery("SELECT * FROM dbo.AttendanceStudent where ClassAttendanceID=" + ClassAttendanceID + " and Attendance='A'").ToList();
        //    return obj1;
        //}
        //public List<vAttendanceStudent> GetAbsentStudentAttendanceDatewise(DateTime mAttendanceDate)
        //{
        //    List<vAttendanceStudent> obj1 = this.context.vAttendanceStudents.Where(i => i.AttendanceDate == mAttendanceDate.Date && i.Attendance == "A").ToList();

        //    return obj1;
        //}

        //public void UpdateStudentAttendance(AttendanceStudent Stud)
        //{
        //    AttendanceStudent editSession = this.GetByID(Stud.StudentAttendanceID);
        //    if (editSession != null)
        //    {
        //        editSession.Description = Stud.Description;
        //        editSession.Attendance = Stud.Attendance;

        //        this.Update(editSession);
        //    }

        //}

    }

    public class modelBookDetail
    {
        [Key]
        public int BookId { get; set; }
        public int AccessionId { get; set; }
        public Nullable<int> BookNo { get; set; }
        public string FrontCoverImage { get; set; }
        public byte[] FrontCover { get; set; }
    }

}
