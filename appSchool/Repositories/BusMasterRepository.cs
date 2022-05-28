using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class BusMasterRepository : GenericRepository<BusMaster> 
    {
        public BusMasterRepository() : base(new dbSchoolAppEntities()) { }
        public BusMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }
       

        public List<BusMaster> GetBusMasterGridData(byte mCompID, byte mBranchID)
        {

            List<BusMaster> obj = new List<BusMaster>();
            obj = this.context.BusMasters.Where(x => x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public void AddNewBusMasterInfo(BusMaster obj)
        {
            this.Insert(obj);
        }
        public void UpdateBusMasterInfo(BusMaster obj)
        {
            BusMaster newObj = this.GetByID(obj.BusID);
            newObj.BusNo = obj.BusNo;
            newObj.LorryNo = obj.LorryNo;
            newObj.EngineNo=obj.EngineNo;
            newObj.ChassisNo = obj.ChassisNo;
            newObj.UIDMod = obj.UIDMod;
            newObj.ModDate = obj.ModDate;

            this.Update(newObj);
        }

        public modelBusPersonalInfo GetPersonalInfo(int BusID)
        {
            modelBusPersonalInfo obj = (
                from xx in this.context.BusMasters
                where xx.BusID == BusID
                select new modelBusPersonalInfo()
                {
                    BusID = xx.BusID,
                     Address= xx.Address,
                   BusNo=xx.BusNo,
                   ChassisNo = xx.ChassisNo,
                    City=xx.City,
                    Colour = xx.Colour,
                    EngineNo= xx.EngineNo,    
                    InsuranceDate=xx.InsuranceDate,
                    IsSelf=xx.IsSelf,
                    MakeNo= xx.MakeNo,
                    MfgCompany=xx.MfgCompany,
                    MfgYear =xx.MfgYear,
                    MobileNo= xx.MobileNo,
                    Model= xx.Model,
                    LorryNo=xx.LorryNo,
                    OpeningKM=xx.OpeningKM,
                    OwnerID=xx.OwnerID,
                    OpeningBalance=xx.OpeningBalance,
                    PassDate=xx.PassDate,
                    RegValidDate=xx.RegValidDate,
                    RTOCardNo=xx.RTOCardNo,
                    RTOName=xx.RTOName,
                    RTOPass=xx.RTOPass,
                    RTOPassedWeight=xx.RTOPassedWeight,
                    VehicleClass=xx.VehicleClass,
                    
                    

                }).FirstOrDefault<modelBusPersonalInfo>();
            return obj;
       }
        public void UpdateBusProfileInfo(modelBusPersonalInfo obj)
        {
            try
            {
                BusMaster newInfo = this.GetByID(obj.BusID);
              
                newInfo.MfgCompany = obj.MfgCompany;
                newInfo.MfgYear = obj.MfgYear;
                newInfo.MakeNo = obj.MakeNo;
                newInfo.Model = obj.Model;
                newInfo.Colour = obj.Colour;
                newInfo.VehicleClass = obj.VehicleClass;

                newInfo.RTOCardNo = obj.RTOCardNo;
                newInfo.InsuranceDate = obj.InsuranceDate;
                newInfo.RTOName = obj.RTOName;
                newInfo.RTOPassedWeight = obj.RTOPassedWeight;
                newInfo.RegValidDate = obj.RegValidDate;

                newInfo.OpeningBalance = obj.OpeningBalance;
                newInfo.OpeningKM = obj.OpeningKM;
              
                this.Update(newInfo);
            }
            catch (Exception)
            {

                throw;
            }

        }




        //public List<InsuranceDetail> GetInsurenceDetailListByBusID(int mBusID, byte mCompID, byte mBranchID)
        //{
        //    List<InsuranceDetail> objInsDetail = new List<InsuranceDetail>();
        //    objInsDetail = this.context.InsuranceDetails.Where(x => x.VehicleID == mBusID && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
        //    return objInsDetail;
        //}

        //public List<PermitDetail> GetPermitDetailListByBusID(int mBusID, byte mCompID, byte mBranchID)
        //{
        //    List<PermitDetail> objInsDetail = new List<PermitDetail>();
        //    objInsDetail = this.context.PermitDetails.Where(x => x.VehicleID == mBusID && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
        //    return objInsDetail;
        //}

        //public List<TaxDetail> GetTaxDetailListByBusID(int mBusID, byte mCompID, byte mBranchID)
        //{
        //    List<TaxDetail> objInsDetail = new List<TaxDetail>();
        //    objInsDetail = this.context.TaxDetails.Where(x => x.VehicleID == mBusID && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
        //    return objInsDetail;
        //}

        //public List<FitnessDetail> GetFitnessDetailListByBusID(int mBusID, byte mCompID, byte mBranchID)
        //{
        //    List<FitnessDetail> objDetail = new List<FitnessDetail>();
        //    objDetail = this.context.FitnessDetails.Where(x => x.VehicleID == mBusID && x.CompID == mCompID && x.BranchID == mBranchID).ToList();
        //    return objDetail;
        //}


        public void AddInsuranceDetail(InsuranceDetail obj)
        {
            this.context.InsuranceDetails.Add(obj);
        }

        public void UpdateInsuranceDetail(InsuranceDetail obj)
        {
            InsuranceDetail objnew = this.context.InsuranceDetails.Where(x => x.InsuranceID == obj.InsuranceID && x.CompID==obj.CompID && x.BranchID==obj.BranchID).FirstOrDefault();
            if (objnew != null)
            {
                objnew.Address1 = obj.Address1;
                objnew.Address2 = obj.Address2;
                objnew.Address3 = obj.Address3;
                objnew.City = obj.City;
                objnew.ContactPerson = obj.ContactPerson;
                objnew.FromDate = obj.FromDate;
                objnew.InsuranceBranch = obj.InsuranceBranch;
                objnew.InsuranceCertificateNo = obj.InsuranceCertificateNo;
                objnew.InsuranceCompany = obj.InsuranceCompany;
                objnew.InsuranceDate = obj.InsuranceDate;
                objnew.MobileNo = obj.MobileNo;
                objnew.Phone = obj.Phone;
                objnew.PinCode = obj.PinCode;
                objnew.PolicyNo = obj.PolicyNo;
                objnew.ToDate = obj.ToDate;
                objnew.UIdMod = obj.UIdMod;
                objnew.ModDate = obj.ModDate;
            
            }


        }
      
    }
  
    #region METADATA
   
    //public class modelBusMaster
    //{
    //    [Key]
    //    public int BusID { get; set; }
    //    public int BusNo { get; set; }
    //    [Required(ErrorMessage = "Lorry No is required")]
    //    public string LorryNo { get; set; }
    //    public string EngineNo { get; set; }
    //    public string ChessisNo { get; set; }
    //    public byte CompID { get; set;}
    //    public byte BranchID { get; set; }
    //    public Nullable<byte> UIDAdd { get; set; }
    //    public Nullable<System.DateTime> AddDate { get; set; }
    //    public Nullable<byte> UIDMod { get; set; }
    //    public Nullable<System.DateTime> ModDate { get; set; }


    //}
    public class modelBusPersonalInfo
    {
        [Key]
        public int BusID{ get; set; }
        public int BusNo { get; set; }
        [Required(ErrorMessage = "Lorry No is required")]
        public string LorryNo { get; set; }
        public Nullable<int> OwnerID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string MobileNo { get; set; }
        public string EngineNo { get; set; }
        public string ChassisNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> PassDate { get; set; }
        public string RTOPass { get; set; }
        public bool IsSelf { get; set; }
        public string MfgCompany { get; set; }
        public string MfgYear { get; set; }
        public string MakeNo { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }
        public string VehicleClass { get; set; }
        public string RTOCardNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> InsuranceDate { get; set; }
        public string RTOName { get; set; }
        public Nullable<decimal> RTOPassedWeight { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> RegValidDate { get; set; }
        public Nullable<decimal> OpeningBalance { get; set; }
        public  Nullable<int> OpeningKM { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }

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
