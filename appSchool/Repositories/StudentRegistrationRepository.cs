using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
//using DevExpress.Xpo;


namespace appSchool.Repositories
{
    public class StudentRegistrationRepository : GenericRepository<StudentRegistration> 
    {
        public StudentRegistrationRepository() : base(new dbSchoolAppEntities()) { }
        public StudentRegistrationRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }
        public IEnumerable<string> GetNamesWithEnrollmentNo()
        {
            var lst = (from xx in this.context.StudentRegistrations select new{ FullName= xx.FirstName + " " + xx.LastName }).ToList();
            return (from xx in this.context.StudentRegistrations select xx.FirstName + " " + xx.LastName );
        }
        public StudentRegistration GetProfileInfo(int StudentID)
        {
            StudentRegistration obj = GetByID(StudentID);
            return obj;
        }
        public List<StudentRegistration> GetRegistrationGridData(byte mCompID, byte mBranchID)
        {
            bool mflag = false;
            List<StudentRegistration> lst = new List<StudentRegistration>();
            lst = this.context.StudentRegistrations.Where(x => x.StudentID > 0 && x.CompID == mCompID && x.TCGiven==mflag && x.BranchID == mBranchID).ToList();

            return lst;
 
        }
        public List<Session> GetSessionDetail()
        {
            List<Session> lst = new List<Session>();
            lst = this.context.Sessions.Where(x => x.SessionId > 0 ).ToList();

            return lst;

        }

        public List<StudentRegistration> GetStudentTCAllotListSessionWise(byte mCompID, byte mBranchID, byte mSessionID)
        {
            bool mflag = true;
            List<StudentRegistration> lst = new List<StudentRegistration>();
            lst = this.context.StudentRegistrations.Where(x => x.StudentID > 0 && x.CompID == mCompID && x.TCGiven == mflag && x.BranchID == mBranchID && x.TCSessionID == mSessionID).ToList();

            return lst;

        }
        
        public List<StudentRegistration> GetRegistrationGridDataWithTCAllotment(byte mCompID, byte mBranchID,byte mSessionID)
        {
            bool flag = true;
            List<StudentRegistration> lst = new List<StudentRegistration>();
            lst = this.context.StudentRegistrations.Where(x => x.StudentID > 0 && x.TCGiven==flag && x.CompID == mCompID && x.BranchID == mBranchID).ToList();

            return lst;

        }

        public void UpdateTCAllotment(vStudentSession Stud, byte CompID, byte BranchID, byte SessionID)
        {
            int TCNumber = 1;
            //TCNumber = DB.ExecuteScalarQuery("select Max(CAST(TCNumber AS Integer)) from dbo.StudentRegistration where BranchID=" + BranchID + " And CompID=" + CompID + " ");

            //TCNumber = TCNumber + 1;
            StudentRegistration editSession = this.GetByID(Stud.StudentID);
            if (editSession != null)
            {
                editSession.TCGiven = bool.Parse(Stud.TCGiven.ToString());
                //editSession.TCNumber = TCNumber.ToString();
                editSession.TCSessionID = SessionID;
                this.Update(editSession);
            }

        }
        public List<vStudentAllSessiondetail> GetStudentAllSessiondetailbyStudentID(int mStudentID, int mSessionID, byte CompID, byte mBranchID)
        {
           
            List<vStudentAllSessiondetail> lst = new List<vStudentAllSessiondetail>();
            lst = this.context.vStudentAllSessiondetails.Where(x => x.StudentID == mStudentID && x.CompID == CompID && x.BranchID == mBranchID).ToList();

            return lst;

        }

        public void UpdateTcAllotmentStudentWise(StudentRegistration Stud, byte CompID, byte BranchID,byte SessionID)
        {
            int TCNumber = 0;
            //TCNumber = DB.ExecuteScalarQuery("select Max(CAST(TCNumber AS Integer)) from dbo.StudentRegistration where BranchID=" + BranchID + " And CompID=" + CompID + " ");

            //TCNumber = TCNumber + 1;
            StudentRegistration editSession = this.GetByID(Stud.StudentID);
            if (editSession != null)
            {
                editSession.TCGiven = Stud.TCGiven;
                editSession.TCDate = Stud.TCDate;
                editSession.TCNumber = "0";
                editSession.TCSessionID = SessionID;
                this.Update(editSession);
            }

        }

        public List<modelRegistration> GetStudentDatawithFullName(int mSessionID, byte mCompID, byte mBranchID)
        {
            List<modelRegistration> lst =
                (from xx in this.context.StudentRegistrations
                 select new modelRegistration()
                 {
                     StudentID = xx.StudentID,
                     EnrollmentNo = xx.EnrollmentNo,
                     EnrollmentDate = xx.EnrollmentDate,
                     FirstName = xx.FirstName,
                     LastName = xx.LastName,
                     ClassID = xx.ClassID,
                     FullName=xx.FirstName +" "+ xx.LastName,
                     FatherName = xx.FatherName,
                     DateOfBirth = xx.DateOfBirth,
                     SMSMobileNo = xx.SMSMobileNo,
                     BranchID=xx.BranchID,
                     CompID=xx.CompID,
                     SessionID=xx.SessionID,
                     
                 }
                 ).Where(x=>x.CompID==mCompID && x.BranchID==mBranchID).ToList<modelRegistration>();

            return lst;

        }

        public int GetStudentUniqueID(modelRegistration obj)
        {
            int mUniqueStudentNo = 0;

            StudentRegistration objnew = this.context.StudentRegistrations.Where(x => x.EnrollmentNo == obj.EnrollmentNo && x.FirstName == obj.FirstName).FirstOrDefault();
            if (objnew != null)
            {
                mUniqueStudentNo = objnew.StudentID;
            }
            return mUniqueStudentNo;
        }

        public string GetFullNameOfStudent(int StudentID, byte mCompID, byte mBranchID)
        {
            string FullName = string.Empty;

            StudentRegistration obj = new StudentRegistration();
            obj = this.context.StudentRegistrations.Where(x => x.StudentID == StudentID && x.CompID == mCompID && x.BranchID == mBranchID).SingleOrDefault();

            if (obj != null)
                FullName = obj.FirstName + " " + obj.LastName;
            return FullName;
        }

       
     
        public void AddNewRegistrationInfo(StudentRegistration obj)
        {
           
            this.Insert(obj);
        }
        public void UpdateRegistrationInfo(modelRegistration obj, byte UserID)
        {
            StudentRegistration newObj = this.GetByID(obj.StudentID);
            newObj.EnrollmentNo = obj.EnrollmentNo;
            newObj.EnrollmentDate = obj.EnrollmentDate;
            newObj.FirstName = obj.FirstName;
            newObj.LastName = obj.LastName;
            newObj.ClassID = obj.ClassID;
            newObj.UIDMod = UserID;
            newObj.DateOfBirth = obj.DateOfBirth;
            newObj.FatherName = obj.FatherName;
            newObj.SMSMobileNo = obj.SMSMobileNo;
            newObj.ModDate = DateTime.Now;
            this.Update(newObj);
        }

        public modelStudentPersonalInfo GetPersonalInfo(int studentID)
        {
            modelStudentPersonalInfo obj = (
                from xx in this.context.StudentRegistrations
                where xx.StudentID == studentID
                select new modelStudentPersonalInfo()
                {
                    StudentID = xx.StudentID,
                    EnrollmentNo = xx.EnrollmentNo,
                    EnrollmentDate = xx.EnrollmentDate,
                    FirstName = xx.FirstName,
                    MiddleName=xx.MiddleName,
                    MotherTounge=xx.MotherTounge,
                    LastName = xx.LastName,
                    BloodGroup= xx.BloodGroup,
                    Caste=xx.Caste,
                    City=xx.City,
                    DateOfBirth= xx.DateOfBirth,
                    Gender=xx.Gender,
                    LocalAddress =xx.LocalAddress,
                    Nationality= xx.Nationality,
                    Religion= xx.Religion,
                    SMSMobileNo=xx.SMSMobileNo,
                    EnableSMSFeature  = xx.EnableSMSFeature,
                    HomePhoneNo=xx.HomePhoneNo,
                    AppImage=xx.AppImage,
                    AppImageName=xx.AppImageName,
                    CompID=xx.CompID,
                    BranchID=xx.BranchID,
                    SessionID=xx.SessionID,

                }).FirstOrDefault<modelStudentPersonalInfo>();
            return obj;
        }

        public  Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;

        }

        public void UpdatePersonalInfo(modelStudentPersonalInfo obj)
        {
            try
            {
                StudentRegistration newInfo = this.GetByID(obj.StudentID);
                //newInfo.EnrollmentNo = obj.EnrollmentNo;
                //newInfo.EnrollmentDate = obj.EnrollmentDate;
                newInfo.FirstName = obj.FirstName;
                newInfo.MiddleName = obj.MiddleName;
                newInfo.LastName = obj.LastName;
                newInfo.BloodGroup = obj.BloodGroup;
                newInfo.Caste = obj.Caste;
                newInfo.City = obj.City;
                newInfo.DateOfBirth = obj.DateOfBirth;
                newInfo.Gender = obj.Gender;
                newInfo.LocalAddress = obj.LocalAddress;
                newInfo.Nationality = obj.Nationality;
                newInfo.Religion = obj.Religion;
                newInfo.HomePhoneNo = obj.HomePhoneNo;
                newInfo.SMSMobileNo = obj.SMSMobileNo;
                newInfo.EnableSMSFeature  = obj.EnableSMSFeature;
                newInfo.MotherTounge = obj.MotherTounge;
             
                this.Update(newInfo);
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        public void UpdateStudentPhoto(StudentRegistration obj)
        {
            try
            {
                StudentRegistration newInfo = this.GetByID(obj.StudentID);
                //newInfo.EnrollmentNo = obj.EnrollmentNo;
                //newInfo.EnrollmentDate = obj.EnrollmentDate;
                newInfo.AppImageName = obj.AppImageName;
             
               
                this.Update(newInfo);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public modelGuardianInfo GetGuardianInfo(int studentID)
        {
            modelGuardianInfo obj = (
                from xx in this.context.StudentRegistrations
                where xx.StudentID == studentID
                select new modelGuardianInfo()
                {
                StudentID= xx.StudentID,
        FatherName=xx.FatherName,
        FatherDOB=xx.FatherDOB,
        AnniversaryDate = xx.AnniversaryDate,
        FQualification= xx.FQualification,
        FOccupation=xx.FOccupation,
        FatherEmailID=xx.FatherEmailID,
        FatherMobileNo=xx.FatherMobileNo,
        FIncome=xx.FIncome,
        FOfficeAddress=xx.FOfficeAddress,
        FOfficePhoneNo =xx.FOfficePhoneNo,
        MotherDOB =xx .MotherDOB,
        MotherEmailID =xx.MotherEmailID,
        MotherMobileNo =xx.MotherMobileNo,
        MotherName =xx.MotherName,
        MQualification =xx.MQualification,
        MIncome =xx.MIncome,
        MOccupation =xx.MOccupation,
        MOfficeAddress =xx.MOfficeAddress,
        MOfficePhoneNo =xx.MOfficePhoneNo,
        GMobileNo =xx.GMobileNo,
        GPhoneNo =xx.GPhoneNo,
        GuardianName =xx.GuardianName,
        GOccupation =xx.GOccupation,
        SessionID=xx.SessionID,
        BranchID=xx.BranchID,
        CompID=xx.CompID,

                }).FirstOrDefault<modelGuardianInfo>();
            return obj;
        }

        public modelStudentPhotoUploadInfo GetStudentPhotoInfo(int studentID)
        {
            modelStudentPhotoUploadInfo obj = (
                from xx in this.context.StudentRegistrations
                where xx.StudentID == studentID
                select new modelStudentPhotoUploadInfo()
                {
                    StudentID = xx.StudentID,
                    EnrollmentNo = xx.EnrollmentNo,
                    EnrollmentDate = xx.EnrollmentDate,
                    FirstName = xx.FirstName,
                    MiddleName = xx.MiddleName,
                    MotherTounge = xx.MotherTounge,
                    LastName = xx.LastName,
                    BloodGroup = xx.BloodGroup,
                    Caste = xx.Caste,
                    AppImage=xx.AppImage,
                    AppImageName=xx.AppImageName,
                    SessionID=xx.SessionID,
                    BranchID=xx.BranchID,
                    CompID=xx.CompID,

                }).FirstOrDefault<modelStudentPhotoUploadInfo>();
            return obj;
        }

        public modelStudentDocumentUploadInfo GetStudentDocumentInfo(int studentID)
        {
            modelStudentDocumentUploadInfo obj = (
                from xx in this.context.StudentRegistrations
                where xx.StudentID == studentID
                select new modelStudentDocumentUploadInfo()
                {
                    StudentID = xx.StudentID,
                    EnrollmentNo = xx.EnrollmentNo,
                   // EnrollmentDate = xx.EnrollmentDate,
                    FirstName = xx.FirstName,
                    MiddleName = xx.MiddleName,
                    LastName = xx.LastName,
                    SessionID=xx.SessionID,
                    BranchID=xx.BranchID,
                    CompID=xx.CompID,


                }).FirstOrDefault<modelStudentDocumentUploadInfo>();
            return obj;
        }

        public void UpdateGuardianInfo(modelGuardianInfo obj)
        {
            try
            {
                StudentRegistration newInfo = this.GetByID(obj.StudentID);
              
                newInfo.FatherName = obj.FatherName;
                newInfo.FatherDOB= obj.FatherDOB;
                newInfo.AnniversaryDate = obj.AnniversaryDate;
                newInfo.FatherEmailID = obj.FatherEmailID;
                newInfo.FatherMobileNo = obj.FatherMobileNo;
                newInfo.FIncome = obj.FIncome;
                newInfo.FOfficeAddress = obj.FOfficeAddress;
                newInfo.FOfficePhoneNo = obj.FOfficePhoneNo;
                newInfo.FQualification = obj.FQualification;
                newInfo.FOccupation = obj.FOccupation;

                newInfo.MotherName = obj.MotherName;
                newInfo.MotherDOB = obj.MotherDOB;
                newInfo.MotherEmailID = obj.MotherEmailID;
                newInfo.MotherMobileNo = obj.MotherMobileNo;
                newInfo.MIncome = obj.MIncome;
                newInfo.MOfficeAddress = obj.MOfficeAddress;
                newInfo.MOfficePhoneNo = obj.MOfficePhoneNo;
                newInfo.MQualification = obj.MQualification;
                newInfo.MOccupation = obj.MOccupation;

                newInfo.GuardianName = obj.GuardianName;
                newInfo.GPhoneNo = obj.GPhoneNo;
                newInfo.GMobileNo = obj.GMobileNo;
                newInfo.GOccupation = obj.GOccupation;
                newInfo.GEmailID = obj.GEmailID;
                newInfo.GAddress = obj.GAddress;

                this.Update(newInfo);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public int CheckDelete(int mID)
        {
            int ID = 0;
            ID = this.context.StudentSessions.Where(x => x.StudentID == mID).Count();
            return ID;
        }



    }
    //public static class ExtensionMethods
    //{
    //    public static bool HasFile(this HttpPostedFileBase file)
    //    {
    //        return (file != null && file.ContentLength > 0) ? true : false;
    //    }

    //}
    #region METADATA
   
    public partial class modelRegistration
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentID { get; set; }

        public string EnrollmentNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> EnrollmentDate { get; set; }
        [Required(ErrorMessage = "First Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        public string LastName { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<int> ClassID { get; set; }
        public string FullName { get; set; }
        public string Rating { get; set; }
        public string FatherName { get; set; }
        public string SMSMobileNo { get; set; }
        public int SessionID { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }
    }
    public class modelStudentPersonalInfo
    {
        [Key]
        public int StudentID { get; set; }
        public string EnrollmentNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> EnrollmentDate { get; set; }
        [Required(ErrorMessage = "First Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Last Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        public string LastName { get; set; }
      //  [Required (ErrorMessage="Birth data is mandatory")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string BloodGroup { get; set; }
       // [Required(ErrorMessage = "Gender is mandatory")]
        public string Gender { get; set; }
       // [Required]
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string City { get; set; }
        public string Caste { get; set; }
        //[Required, StringLength(500, ErrorMessage = "Can't be more than 500 chars.")]
        public string LocalAddress { get; set; }
        //[RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Invalid Phone Number!")]
        //[Required(ErrorMessage = "Contact Number- Home is mandatory")]
        public string HomePhoneNo { get; set; }
        public string MotherTounge { get; set; }
        public string SMSMobileNo { get; set; }
        public bool EnableSMSFeature { get; set; }
        public HttpPostedFileBase attachment { get; set; }
        public byte[] AppImage { get; set; }
       
        public string AppImageName { get; set; }
        public Bitmap StudentPhoto {get; set; }
        public int SessionID { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }

      
    }

    public class modelStudentPhotoUploadInfo
    {
        [Key]
        public int StudentID { get; set; }
        public string EnrollmentNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> EnrollmentDate { get; set; }
        [Required(ErrorMessage = "First Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Last Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        public string LastName { get; set; }
        //  [Required (ErrorMessage="Birth data is mandatory")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string BloodGroup { get; set; }
        // [Required(ErrorMessage = "Gender is mandatory")]
        public string Gender { get; set; }
        // [Required]
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string City { get; set; }
        public string Caste { get; set; }
        //[Required, StringLength(500, ErrorMessage = "Can't be more than 500 chars.")]
        public string LocalAddress { get; set; }
        //[RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Invalid Phone Number!")]
        //[Required(ErrorMessage = "Contact Number- Home is mandatory")]
        public string HomePhoneNo { get; set; }
        public string MotherTounge { get; set; }
        public string SMSMobileNo { get; set; }
        public bool EnableSMSFeature { get; set; }
        public HttpPostedFileBase attachment { get; set; }
        public byte[] AppImage { get; set; }
        public string AppImageName { get; set; }
        public int SessionID { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }

    }

    public class modelStudentDocumentUploadInfo
    {
        [Key]
        public int StudentID { get; set; }
        public string EnrollmentNo { get; set; }
        public string DocumentName { get; set; }
        public string FileName { get; set; }
        public string Remark { get; set; }

     
        [Required(ErrorMessage = "First Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Last Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        public string LastName { get; set; }
        //  [Required (ErrorMessage="Birth data is mandatory")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
     
        public HttpPostedFileBase attachment { get; set; }
        public int SessionID { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }


    }


    public class modelGuardianInfo {
        [Key]
        public int StudentID { get; set; }
        [Required(ErrorMessage = "Father Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        public string FatherName { get; set; }
      //  [Required(ErrorMessage = "Birth data is mandatory")]
        public Nullable<System.DateTime> FatherDOB { get; set; }
        public Nullable<System.DateTime> AnniversaryDate { get; set; }
        public string FQualification { get; set; }
        public string FatherEmailID { get; set; }
        public string FOccupation { get; set; }
      //  [Required(ErrorMessage = "Father Mobile No. is mandatory")]
        public string FatherMobileNo { get; set; }
        public decimal? FIncome { get; set; }
        public string FOfficeAddress { get; set; }
        public string FOfficePhoneNo { get; set; }
        public Nullable<System.DateTime> MotherDOB { get; set; }
        public string MotherEmailID { get; set; }
      //   [Required(ErrorMessage = "Mother Mobile No. is mandatory")]
        public string MotherMobileNo { get; set; }
      //   [Required(ErrorMessage = "Mother Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        public string MotherName { get; set; }
        public string MQualification { get; set; }
        public decimal? MIncome { get; set; }
        public string MOccupation { get; set; }
        public string MOfficeAddress { get; set; }
        public string MOfficePhoneNo { get; set; }
        public string GMobileNo { get; set; }
        public string GPhoneNo { get; set; }
        public string GuardianName { get; set; }
        public string GOccupation { get; set; }
        public string GEmailID { get; set; }
        public string GAddress { get; set; }
        public int SessionID { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }

    }
    public class modelStudentSelectGrid
    {
        public int StudentID { get; set; }
        public string EnrollmentNo { get; set; }
        public string FullName { get; set; }
        public string ClassName { get; set; }
        public int SessionID { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }
    }


 
    #endregion


}
