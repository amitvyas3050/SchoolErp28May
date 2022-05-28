using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class AttendanceClassRepository : GenericRepository<AttendanceClass> 
    {
        public AttendanceClassRepository() : base(new dbSchoolAppEntities()) { }
        public AttendanceClassRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }

        public int GetAttendanceInfo(AttendanceClass obj)
        {
            int id=0;
            DateTime mAttendanceDate = obj.AttendanceDate.Date;

          AttendanceClass obj1 = this.context.AttendanceClasses.Where(x => x.ClassSetupID == obj.ClassSetupID && x.AttendanceDate == mAttendanceDate && x.CompID==obj.CompID && x.BranchID==obj.BranchID).FirstOrDefault();
          if (obj1 != null)
          {
              id = obj1.ClassAttendanceID;
          }
            return id;
        }

        public int GetAttendanceInfoByClassSetupIDAndDate(int ClassSetupID , DateTime mDate, byte SessionID, byte mCompID, byte mBranchID)
        {
            int id = 0;
            DateTime mAttendanceDate = mDate.Date;
            
            AttendanceClass obj1 = this.context.AttendanceClasses.Where(x => x.ClassSetupID == ClassSetupID && x.SessionID==SessionID && x.AttendanceDate == mAttendanceDate && x.CompID==mCompID && x.BranchID ==mBranchID).FirstOrDefault();
            if (obj1 != null)
            {
                id = obj1.ClassAttendanceID;
            }
            return id;
        }

        public List<vAttendanceClass> GetALLAttendance(int ClassSetupID, byte mSessionID, byte mBranchID, byte mCompID)
        {
            List<vAttendanceClass> obj1 = this.context.vAttendanceClasses.SqlQuery("SELECT * FROM dbo.vAttendanceClass where ClassSetupID=" + ClassSetupID + " AND SessionID="+ mSessionID +" AND BranchID="+ mBranchID+" AND CompID="+mCompID).ToList();
            return obj1;
        }
        public List<vAttendanceClass> GetALLAttendanceByDate(int ClassSetupID, DateTime AttnDate, byte mSessionID, byte mBranchID, byte mCompID)
        {

            DateTime AtandanceDAte = DateTime.Parse(AttnDate.ToString());

            //DateTime Atandance = //DateTime.Parse(AttnDate);

            List<vAttendanceClass> obj1 = new List<vAttendanceClass>();
            //List<vAttendanceClass> obj1 = this.context.vAttendanceClasses.SqlQuery("SELECT * FROM dbo.vAttendanceClass where ClassSetupID=" + ClassSetupID + " And AttendanceDate='" + AtandanceDAte + "' AND SessionID=" + mSessionID + " AND BranchID=" + mBranchID + " AND CompID=" + mCompID).ToList();
            obj1 = this.context.vAttendanceClasses.Where(x => x.ClassSetupID == ClassSetupID && x.AttendanceDate == AttnDate && x.SessionID == mSessionID && x.CompID == mCompID && x.BranchID == mBranchID).ToList();

            return obj1;
        }


        public List<vAttendanceClass> GetALLAttendanceDatewise(byte mSessionID, byte mBranchID, byte mCompID)
        {

            List<vAttendanceClass> obj1 = this.context.vAttendanceClasses.SqlQuery("SELECT * FROM dbo.vAttendanceClass Where AND SessionID="+ mSessionID +" AND BranchID="+ mBranchID+" AND CompID="+mCompID).ToList();
            return obj1;
        }



    }
  
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
