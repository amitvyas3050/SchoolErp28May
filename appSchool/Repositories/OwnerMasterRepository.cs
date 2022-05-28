using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class OwnerMasterRepository : GenericRepository<OwnerMaster> 
    {
        public OwnerMasterRepository() : base(new dbSchoolAppEntities()) { }
        public OwnerMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }
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
        public List<modelOwnerMaster> GetOwnerMasterGridData( byte mCompID, byte mBranchID )
        {
            List<modelOwnerMaster> lst =
                (from xx in this.context.OwnerMasters
                 select new modelOwnerMaster()
                 {
                     OwnerID = xx.OwnerID,
                     OwnerName = xx.OwnerName,
                     MobileNo1 = xx.MobileNo1,
                     CompanyName = xx.CompanyName,

                 }
                 ).Where(x=>x.CompID==mCompID && x.BranchID==mBranchID).ToList<modelOwnerMaster>();
            return lst;

        }
        public void AddNewOwnerMasterInfo(modelOwnerMaster obj)
        {
            OwnerMaster newObj = new OwnerMaster()
            {
                OwnerID = obj.OwnerID,
                OwnerName= obj.OwnerName,
                CompanyName = obj.CompanyName,
                MobileNo1=obj.MobileNo1,
                CompID=obj.CompID,
                BranchID=obj.BranchID
            };
            this.Insert(newObj);
        }
        public void UpdateOwnerMasterInfo(modelOwnerMaster obj)
        {
            OwnerMaster newObj = this.GetByID(obj.OwnerID);
            newObj.OwnerName = obj.OwnerName;
            newObj.MobileNo1 = obj.MobileNo1;
            newObj.CompanyName = obj.CompanyName;
            this.Update(newObj);
        }

        public modelOwnerPersonalInfo GetPersonalInfo(int OwnerID)
        {
            modelOwnerPersonalInfo obj = (
                from xx in this.context.OwnerMasters
                where xx.OwnerID == OwnerID
                select new modelOwnerPersonalInfo()
                {
                     OwnerID = xx.OwnerID,
                     OwnerName=xx.OwnerName,
                     LocalAddress= xx.LocalAddress,
                     PermanentAddress=xx.permanentAddress,
                     MobileNo1=xx.MobileNo1,
                     City = xx.City,
                     CompanyName=xx.CompanyName,
                     DOB = xx.DOB,
                     EmailID= xx.EmailID,    
                     FatherName=xx.FatherName,
                     FaxNo=xx.FatherName,
                     MobileNo2= xx.MobileNo2,
                     PanNo=xx.PanNo,
                     PhoneNo=xx.PhoneNo,
                     PinCode =xx.PinCode,
                     Remark= xx.Remark,
                   



                }).FirstOrDefault<modelOwnerPersonalInfo>();
            return obj;
       }
        public void UpdateOwnerProfileInfo(modelOwnerPersonalInfo obj)
        {
            try
            {
                OwnerMaster newInfo = this.GetByID(obj.OwnerID);
                //newInfo.EnrollmentNo = obj.EnrollmentNo;
                //newInfo.EnrollmentDate = obj.EnrollmentDate;
                
                newInfo.LocalAddress = obj.LocalAddress;
                
                newInfo.BloodGroup = obj.BloodGroup;
                newInfo.City = obj.City;
                newInfo.CompanyName= obj.CompanyName;
                newInfo.DOB = obj.DOB;
                newInfo.EmailID = obj.EmailID;
                
                newInfo.FatherName = obj.FatherName;               
                newInfo.FaxNo = obj.FaxNo;
                newInfo.LocalAddress = obj.LocalAddress;
                newInfo.MobileNo1 = obj.MobileNo1;
                newInfo.MobileNo2 = obj.MobileNo2;
                
                newInfo.PanNo = obj.PanNo;
                newInfo.permanentAddress = obj.PermanentAddress;
                newInfo.PhoneNo = obj.PhoneNo;
                newInfo.PinCode = obj.PinCode;
                newInfo.Remark = obj.Remark;
               
                
                this.Update(newInfo);
            }
            catch (Exception)
            {

                throw;
            }

        }

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

    public class modelOwnerMaster
    {
        [Key]
        public int OwnerID { get; set; }
        
        public string OwnerName { get; set; }
        [Required(ErrorMessage = "Mobile No is required")]
        public string MobileNo1 { get; set; }
        public string CompanyName { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:d}")]
        //public Nullable<System.DateTime> EnrollmentDate { get; set; }
        //[Required(ErrorMessage = "First Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        //public string FirstName { get; set; }
        //[Required(ErrorMessage = "Last Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        //public string LastName { get; set; }
       
        //public Nullable<int> ClassID { get; set; }
        //public string FullName { get; set; }
        //public string Rating { get; set; }
    }
    public class modelOwnerPersonalInfo
    {
        [Key]
        public int OwnerID{ get; set; }
        public string OwnerName { get; set; }
        public string FatherName { get; set; }
        [Required(ErrorMessage = "Mobile No is required")]
        public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string CompanyName { get; set; }
        public string LocalAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string City { get; set; }
        public string BloodGroup { get; set; }
        
        public string PinCode { get; set; }
        public string PhoneNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> DOB { get; set; }
        public string PanNo { get; set; }
        
        public string FaxNo { get; set; }
        public string EmailID { get; set; }
        public string Remark{ get; set; }
       

        //[Required(ErrorMessage = "First Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        //public string FirstName { get; set; }
        //public string MiddleName { get; set; }
        //[Required(ErrorMessage = "Last Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        //public string LastName { get; set; }
        //[Required (ErrorMessage="Birth data is mandatory")]
        //public Nullable<System.DateTime> DateOfBirth { get; set; }
        //public string BloodGroup { get; set; }
        //[Required(ErrorMessage = "Gender is mandatory")]
        //public string Gender { get; set; }
        //[Required]
        //public string Religion { get; set; }
        //public string Nationality { get; set; }
        //public string City { get; set; }
        //public string Caste { get; set; }
        //[Required, StringLength(500, ErrorMessage = "Can't be more than 500 chars.")]
        //public string LocalAddress { get; set; }
        ////[RegularExpression(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", ErrorMessage = "Invalid Phone Number!")]
        //[Required(ErrorMessage = "Contact Number- Home is mandatory")]
        //public string HomePhoneNo { get; set; }
        //public string MotherTounge { get; set; }
        //public HttpPostedFileBase attachment { get; set; }


      
    }
   
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
