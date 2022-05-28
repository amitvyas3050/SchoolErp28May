using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class AttendanceStudentRepository : GenericRepository<AttendanceStudent> 
    {
        public AttendanceStudentRepository() : base(new dbSchoolAppEntities()) { }
        public AttendanceStudentRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public void AddALLStudentAttendance(AttendanceStudent obj)
        {

            this.Insert(obj);

            //StudentRegistration newObj = new StudentRegistration()
            //{
            //    EnrollmentNo = obj.EnrollmentNo,
            //    EnrollmentDate = obj.EnrollmentDate,
            //    FirstName = obj.FirstName,
            //    LastName = obj.LastName,
            //    ClassID = obj.ClassID,
            //};
           
        }

        public List<vAttendanceStudent> GetAttendanceStudentForGrid(int ClassAttendanceID,byte mCompID, byte mBranchID)
        {
            bool mflag = false;
            List<vAttendanceStudent> objAS = new List<vAttendanceStudent>();
            objAS = this.context.vAttendanceStudents.Where(x => x.ClassAttendanceID == ClassAttendanceID && x.TCGiven==mflag &&  x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return objAS;
        }

        public List<vStudentAttendanceDateWise> GetAbsentStudentClasswise(int ClassAttendanceID)
        {
            // StudentSession obj = this.context.StudentSessions.Where(i => i.ClassSetupID == ClassSetupID ).firstordefault();

            
            List<vStudentAttendanceDateWise> obj1 = this.context.vStudentAttendanceDateWises.SqlQuery("SELECT * FROM dbo.vStudentAttendanceDateWise where ClassAttendanceID=" + ClassAttendanceID + " and Attendance='A'").ToList();
            return obj1;
        }
        //public List<vAttendanceStudent> GetAbsentStudentAttendanceDatewise(DateTime mAttendanceDate, string mClassSetupID)
        //{
        //    List<vAttendanceStudent> obj1 = this.context.vAttendanceStudents.Where(i => i.AttendanceDate == mAttendanceDate.Date && i.ClassSetupID == int.Parse(mClassSetupID) && i.Attendance == "A").ToList();

        //    return obj1;
        //}

        public List<vAttendanceStudent> GetAbsentStudentAttendanceDatewise(DateTime mAttendanceDate, string mClassSetupID)
        {

            string sql = "SELECT * FROM dbo.vAttendanceStudent where ClassSetupID in (" + mClassSetupID + ") and AttendanceDate='" + mAttendanceDate.Date + "'  and Attendance='A'  "; //old 29/10/2021
            //string sql = "SELECT * FROM dbo.vAttendanceStudent where ClassSetupID in (" + mClassSetupID + ") and AttendanceDate='2021-10-08'  and Attendance='A'  "; // only testing 
            List<vAttendanceStudent> obj = this.context.vAttendanceStudents.SqlQuery(sql).ToList();



            
           // List<vAttendanceStudent> obj = new List<vAttendanceStudent>();
           //string[] CountClassID = mClassSetupID.Split(',');
            // for (int i = 0; i < CountClassID.Length; i++)
           // {
           //     int mClassetupID = int.Parse(CountClassID[i]);
           //     List<vAttendanceStudent> obj2 = new List<vAttendanceStudent>();


           //     obj2 = GetStudentAbsetTemplist(mAttendanceDate, mClassetupID);

           //     foreach()


           //     vAttendanceStudent objAttend = this.context.vAttendanceStudents.Where(x => x.ClassSetupID == mClassetupID && x.AttendanceDate == mAttendanceDate && x.Attendance == "A").FirstOrDefault();



           //     if (objAttend != null)
           //         obj.Add(objAttend);

           // }

           


            return obj;

        }


        public List<vAttendanceOnlineClass> GetOnlineAbsentStudentAttendanceDatewise(DateTime mAttendanceDate, string mClassSetupID)
        {
            string attendancestring = mAttendanceDate.ToString("dd/MM/yyyy");
            string sql = "SELECT * FROM dbo.vAttendanceOnlineClass where SectionID in (" + mClassSetupID + ") and Convert(varchar(50), AttendanceDate, 103) ='" + attendancestring + "'    "; //old 29/10/2021
            //string sql = "SELECT * FROM dbo.vAttendanceStudent where ClassSetupID in (" + mClassSetupID + ") and AttendanceDate='2021-10-08'  and Attendance='A'  "; // only testing 
            List<vAttendanceOnlineClass> obj = this.context.vAttendanceOnlineClasses.SqlQuery(sql).ToList();
            return obj;
        }


        public List<vAttendanceStudent> GetStudentAbsetTemplist(DateTime mAttendanceDate, int mClassetupID)
        {
            List<vAttendanceStudent> obj = new List<vAttendanceStudent>();
            obj = this.context.vAttendanceStudents.Where(x => x.ClassSetupID == mClassetupID && x.AttendanceDate == mAttendanceDate && x.Attendance == "A").ToList();

            return obj;
        }


        public void UpdateStudentAttendance(AttendanceStudent Stud)
        {
            AttendanceStudent editSession = this.GetByID(Stud.StudentAttendanceID);
            if (editSession != null)
            {
                editSession.Description = Stud.Description;
                editSession.Attendance = Stud.Attendance;

                this.Update(editSession);
            }

        }

        //public int CheckRowInAttendance

        //public IEnumerable<string> GetNamesWithEnrollmentNo()
        //{
        //    var lst = (from xx in this.context.StudentRegistrations select new{ FullName= xx.FirstName + " " + xx.LastName }).ToList();
        //    return (from xx in this.context.StudentRegistrations select xx.FirstName + " " + xx.LastName );
        //}
        //public StudentRegistration GetProfileInfo(int StudentID)
        //{
        //    StudentRegistration obj = GetByID(StudentID);
        //    return obj;
        //}
        //public List<modelRegistration> GetRegistrationGridData()
        //{
        //    List<modelRegistration> lst =
        //        (from xx in this.context.StudentRegistrations 
        //         select new modelRegistration(){
        //             StudentID = xx.StudentID,
        //             EnrollmentNo = xx.EnrollmentNo,
        //             EnrollmentDate = xx.EnrollmentDate,
        //             FirstName = xx.FirstName,
        //             LastName = xx.LastName,
        //             ClassID = xx.ClassID,
        //             FullName=xx.FirstName +" "+ xx.LastName,
        //             Rating = "*****",
                    
        //            }
        //         ).ToList<modelRegistration>();
        //    return lst;
 
        //}
        //public void AddNewRegistrationInfo(modelRegistration obj)
        //{
        //    StudentRegistration newObj = new StudentRegistration() { 
        //        EnrollmentNo= obj.EnrollmentNo,
        //        EnrollmentDate=obj.EnrollmentDate,
        //        FirstName=obj.FirstName,
        //        LastName=obj.LastName,
        //        ClassID=obj.ClassID,
        //    };
        //    this.Insert(newObj);
        //}
        //public void UpdateRegistrationInfo(modelRegistration obj)
        //{
        //    StudentRegistration newObj = this.GetByID(obj.StudentID);
        //    newObj.EnrollmentNo = obj.EnrollmentNo;
        //    newObj.EnrollmentDate = obj.EnrollmentDate;
        //    newObj.FirstName = obj.FirstName;
        //    newObj.LastName = obj.LastName;
        //    newObj.ClassID = obj.ClassID;
        //    this.Update(newObj);
        //}

        //public modelStudentPersonalInfo GetPersonalInfo(int studentID)
        //{
        //    modelStudentPersonalInfo obj = (
        //        from xx in this.context.StudentRegistrations
        //        where xx.StudentID == studentID
        //        select new modelStudentPersonalInfo()
        //        {
        //            StudentID = xx.StudentID,
        //            EnrollmentNo = xx.EnrollmentNo,
        //            EnrollmentDate = xx.EnrollmentDate,
        //            FirstName = xx.FirstName,
        //            MiddleName=xx.MiddleName,
        //            MotherTounge=xx.MotherTounge,
        //            LastName = xx.LastName,
        //            BloodGroup= xx.BloodGroup,
        //            Caste=xx.Caste,
        //            City=xx.City,
        //            DateOfBirth= xx.DateOfBirth,
        //            Gender=xx.Gender,
        //            LocalAddress =xx.LocalAddress,
        //            Nationality= xx.Nationality,
        //            Religion= xx.Religion,
        //            SMSMobileNo=xx.SMSMobileNo,
        //            HomePhoneNo=xx.HomePhoneNo
        //        }).FirstOrDefault<modelStudentPersonalInfo>();
        //    return obj;
        //}
        //public void UpdatePersonalInfo(modelStudentPersonalInfo obj)
        //{
        //    try
        //    {
        //        StudentRegistration newInfo = this.GetByID(obj.StudentID);
        //        //newInfo.EnrollmentNo = obj.EnrollmentNo;
        //        //newInfo.EnrollmentDate = obj.EnrollmentDate;
        //        newInfo.FirstName = obj.FirstName;
        //        newInfo.MiddleName = obj.MiddleName;
        //        newInfo.LastName = obj.LastName;
        //        newInfo.BloodGroup = obj.BloodGroup;
        //        newInfo.Caste = obj.Caste;
        //        newInfo.City = obj.City;
        //        newInfo.DateOfBirth = obj.DateOfBirth;
        //        newInfo.Gender = obj.Gender;
        //        newInfo.LocalAddress = obj.LocalAddress;
        //        newInfo.Nationality = obj.Nationality;
        //        newInfo.Religion = obj.Religion;
        //        newInfo.HomePhoneNo = obj.HomePhoneNo;
        //        newInfo.SMSMobileNo = obj.SMSMobileNo;
            
        //        newInfo.MotherTounge = obj.MotherTounge;
        //        if (obj.attachment.HasFile())
        //        {
        //            ////save the file
        //            //string mPath="~/images
        //            //obj.attachment.SaveAs(obj.EnrollmentNo +"
        //            ////Send it as an attachment 
        //            //Attachment messageAttachment = new Attachment(Model.attachment.InputStream, Model.attachment.FileName);
        //        }
        //        this.Update(newInfo);
        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
            
        //}

        //public modelGuardianInfo GetGuardianInfo(int studentID)
        //{
        //    modelGuardianInfo obj = (
        //        from xx in this.context.StudentRegistrations
        //        where xx.StudentID == studentID
        //        select new modelGuardianInfo()
        //        {
        //        StudentID= xx.StudentID,
        //FatherName=xx.FatherName,
        //FatherDOB=xx.FatherDOB,
        //FQualification= xx.FQualification,
        //FatherEmailID=xx.FatherEmailID,
        //FatherMobileNo=xx.FatherMobileNo,
        //FIncome=xx.FIncome,
        //FOfficeAddress=xx.FOfficeAddress,
        //FOfficePhoneNo =xx.FOfficePhoneNo,
        //MotherDOB =xx .MotherDOB,
        //MotherEmailID =xx.MotherEmailID,
        //MotherMobileNo =xx.MotherMobileNo,
        //MotherName =xx.MotherName,
        //MQualification =xx.MQualification,
        //MIncome =xx.MIncome,
        //MOccupation =xx.MOccupation,
        //MOfficeAddress =xx.MOfficeAddress,
        //MOfficePhoneNo =xx.MOfficePhoneNo,
        //GMobileNo =xx.GMobileNo,
        //GPhoneNo =xx.GPhoneNo,
        //GuardianName =xx.GuardianName,
        //GOccupation =xx.GOccupation

        //        }).FirstOrDefault<modelGuardianInfo>();
        //    return obj;
        //}
        //public void UpdateGuardianInfo(modelGuardianInfo obj)
        //{
        //    try
        //    {
        //        StudentRegistration newInfo = this.GetByID(obj.StudentID);
        //        //newInfo.EnrollmentNo = obj.EnrollmentNo;
        //        //newInfo.EnrollmentDate = obj.EnrollmentDate;
        //        newInfo.FatherName = obj.FatherName;
        //        newInfo.FatherDOB= obj.FatherDOB;
        //        newInfo.FatherEmailID = obj.FatherEmailID;
        //        newInfo.FatherMobileNo = obj.FatherMobileNo;
        //        newInfo.FIncome = obj.FIncome;
        //        newInfo.FOfficeAddress = obj.FOfficeAddress;
        //        newInfo.FOfficePhoneNo = obj.FOfficePhoneNo;
        //        newInfo.FQualification = obj.FQualification;
        //        newInfo.FOccupation = obj.FOccupation;

        //        newInfo.MotherName = obj.MotherName;
        //        newInfo.MotherDOB = obj.MotherDOB;
        //        newInfo.MotherEmailID = obj.MotherEmailID;
        //        newInfo.MotherMobileNo = obj.MotherMobileNo;
        //        newInfo.MIncome = obj.MIncome;
        //        newInfo.MOfficeAddress = obj.MOfficeAddress;
        //        newInfo.MOfficePhoneNo = obj.MOfficePhoneNo;
        //        newInfo.MQualification = obj.MQualification;
        //        newInfo.MOccupation = obj.MOccupation;

        //        newInfo.GuardianName = obj.GuardianName;
        //        newInfo.GPhoneNo = obj.GPhoneNo;
        //        newInfo.GMobileNo = obj.GMobileNo;
        //        newInfo.GOccupation = obj.GOccupation;
        //        newInfo.GEmailID = obj.GEmailID;
        //        newInfo.GAddress = obj.GAddress;
                




        //        this.Update(newInfo);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}


    }
    //public static class ExtensionMethods
    //{
    //    public static bool HasFile(this HttpPostedFileBase file)
    //    {
    //        return (file != null && file.ContentLength > 0) ? true : false;
    //    }

    //}
    #region METADATA
   
    //public class modelRegistration
    //{
    //    [Key]
    //    public long StudentID { get; set; }
    //    [Required(ErrorMessage = "Enrollment No is required")]
    //    public string EnrollmentNo { get; set; }
    //    [DataType(DataType.Date)]
    //    [DisplayFormat(DataFormatString = "{0:d}")]
    //    public Nullable<System.DateTime> EnrollmentDate { get; set; }
    //    [Required(ErrorMessage = "First Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
    //    public string FirstName { get; set; }
    //    [Required(ErrorMessage = "Last Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
    //    public string LastName { get; set; }
       
    //    public Nullable<int> ClassID { get; set; }
    //    public string FullName { get; set; }
    //    public string Rating { get; set; }
    //}
    //public class modelStudentPersonalInfo
    //{
    //    [Key]
    //    public long StudentID { get; set; }
    //    public string EnrollmentNo { get; set; }
    //    [DataType(DataType.Date)]
    //    [DisplayFormat(DataFormatString = "{0:d}")]
    //    public Nullable<System.DateTime> EnrollmentDate { get; set; }
    //    [Required(ErrorMessage = "First Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
    //    public string FirstName { get; set; }
    //    public string MiddleName { get; set; }
    //    [Required(ErrorMessage = "Last Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
    //    public string LastName { get; set; }
    //    [Required (ErrorMessage="Birth data is mandatory")]
    //    public Nullable<System.DateTime> DateOfBirth { get; set; }
    //    public string BloodGroup { get; set; }
    //    [Required(ErrorMessage = "Gender is mandatory")]
    //    public string Gender { get; set; }
    //    [Required]
    //    public string Religion { get; set; }
    //    public string Nationality { get; set; }
    //    public string City { get; set; }
    //    public string Caste { get; set; }
    //    [Required, StringLength(500, ErrorMessage = "Can't be more than 500 chars.")]
    //    public string LocalAddress { get; set; }
    //    //[RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Invalid Phone Number!")]
    //    [Required(ErrorMessage = "Contact Number- Home is mandatory")]
    //    public string HomePhoneNo { get; set; }
    //    public string MotherTounge { get; set; }
    //    public string SMSMobileNo { get; set; }
    //    public HttpPostedFileBase attachment { get; set; }


      
    //}
    //public class modelGuardianInfo {
    //    [Key]
    //    public long StudentID { get; set; }
    //    [Required(ErrorMessage = "Father Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
    //    public string FatherName { get; set; }
    //    [Required(ErrorMessage = "Birth data is mandatory")]
    //    public Nullable<System.DateTime> FatherDOB { get; set; }
    //    public string FQualification { get; set; }
    //    public string FatherEmailID { get; set; }
    //    public string FOccupation { get; set; }
    //    [Required(ErrorMessage = "Father Mobile No. is mandatory")]
    //    public string FatherMobileNo { get; set; }
    //    public decimal? FIncome { get; set; }
    //    public string FOfficeAddress { get; set; }
    //    public string FOfficePhoneNo { get; set; }
    //    public Nullable<System.DateTime> MotherDOB { get; set; }
    //    public string MotherEmailID { get; set; }
    //     [Required(ErrorMessage = "Mother Mobile No. is mandatory")]
    //    public string MotherMobileNo { get; set; }
    //     [Required(ErrorMessage = "Mother Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
    //    public string MotherName { get; set; }
    //    public string MQualification { get; set; }
    //    public decimal? MIncome { get; set; }
    //    public string MOccupation { get; set; }
    //    public string MOfficeAddress { get; set; }
    //    public string MOfficePhoneNo { get; set; }
    //    public string GMobileNo { get; set; }
    //    public string GPhoneNo { get; set; }
    //    public string GuardianName { get; set; }
    //    public string GOccupation { get; set; }
    //    public string GEmailID { get; set; }
    //    public string GAddress { get; set; }
      

    //}
    //public class modelStudentSelectGrid
    //{
    //    public int StudentID { get; set; }
    //    public string EnrollmentNo { get; set; }
    //    public string FullName { get; set; }
    //    public string ClassName { get; set; }
       
    //}


    //[MetadataType(typeof(StudentRegistrationMetadata))]
    //public partial class StudentRegistration
    //{
    //}
    //public class StudentRegistrationMetadata
    //{

    //    [Key]
    //    public int StudentID { get; set; }
    //    [Required(ErrorMessage = "Enrollment No is required")]
    //    public string EnrollmentNo { get; set; }
    //    [DataType(DataType.Date)]
    //    [DisplayFormat(DataFormatString = "{0:d}")]
    //    public Nullable<System.DateTime> EnrollmentDate { get; set; }
    //    [Required(ErrorMessage = "First Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
    //    public string FirstName { get; set; }
    //    public string MiddleName { get; set; }
    //    public string LastName { get; set; }
    //    [Display(Name = "IdentityCardNo.")]
    //    public string ICardNo { get; set; }
    //    public string Gender { get; set; }
    //    public Nullable<System.DateTime> DateOfBirth { get; set; }
    //    public string City { get; set; }
    //    public string Nationality { get; set; }
    //    public string MotherTounge { get; set; }
    //    public int ClassID { get; set; }
    //    [Required, StringLength(500, ErrorMessage = "Can't be more than 500 chars.")]
    //    public string LocalAddress { get; set; }
    //    [StringLength(500, MinimumLength = 1)]
    //    public string parmanentAddress { get; set; }
    //    public string Caste { get; set; }
    //    public string BloodGroup { get; set; }
    //    [Required]
    //    public string Religion { get; set; }
    //    public bool TransportRequired { get; set; }
    //    public bool HostelRequired { get; set; }
    //    public bool EnableSMSFeature { get; set; }
    //    public bool TCGiven { get; set; }
    //    public string TCNumber { get; set; }
    //    [DataType(DataType.Date)]
    //    public DateTime? TCDate { get; set; }

    //    [Required(ErrorMessage = "MobileNo is required")]
    //    [DataType(DataType.PhoneNumber)]
    //    public string FatherMobileNo { get; set; }
    //    [Required(ErrorMessage = "MobileNo is required")]
    //    [DataType(DataType.PhoneNumber)]
    //    public string MotherMobileNo { get; set; }

    //    public string FOccupation { get; set; }
    //    public string MOccupation { get; set; }
    //    [DataType(DataType.Date)]
    //    public DateTime? FatherDOB { get; set; }
    //    [DataType(DataType.Date)]
    //    public DateTime? MotherDOB { get; set; }

    //    [RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
    //    public decimal FIncome { get; set; }
    //    [RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
    //    public decimal MIncome { get; set; }

    //    [Required(ErrorMessage = "Please enter your email address")]
    //    [DataType(DataType.EmailAddress)]
    //    [Display(Name = "Email address")]
    //    [MaxLength(50)]
    //    [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
    //    public string FatherEmailID { get; set; }
    //    [DataType(DataType.EmailAddress)]
    //    [Display(Name = "Email address")]
    //    [MaxLength(50)]
    //    [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
    //    public string MotherEmailID { get; set; }

    //    [RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Invalid Phone Number!")]
    //    public string FOfficePhoneNo { get; set; }
    //    [RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Invalid Phone Number!")]
    //    public string MOfficePhoneNo { get; set; }
    //    [RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Invalid Phone Number!")]
    //    public string HomePhoneNo { get; set; }
    //    [DataType(DataType.EmailAddress)]
    //    [MaxLength(50)]
    //    [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
    //    public string GEmailID { get; set; }
    //    [RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Invalid Phone Number!")]
    //    public string GPhoneNo { get; set; }
    //    [RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Invalid Phone Number!")]
    //    public string GmobileNo { get; set; }


    //}
    #endregion


}
