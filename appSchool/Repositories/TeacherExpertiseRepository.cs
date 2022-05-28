using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;


namespace appSchool.Repositories
{
    public class TeacherExpertiseRepository : GenericRepository<TeacherExpertise>
    {
        public TeacherExpertiseRepository() : base(new dbSchoolAppEntities()) { }
        public TeacherExpertiseRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }

        //public Teacher GetProfileInfo(int TeacherID)
        //{
        //    Teacher obj = GetByID(TeacherID);
        //     return obj;
        //}
        public List<TeacherExpertise> GetTeacherExpertiseGridData(int mTeacherID)
        {
            List<TeacherExpertise> objTQ = this.context.TeacherExpertises.Where(x => x.TeacherID == mTeacherID).ToList();
           
            return objTQ;
        }


        public TeacherExpertise GetTeacherExpertiseMasterByTeacherIDandSessionID(int mTeacherID, int mSessionID)
        {
            //DateTime mAttendanceDate = obj.AttendanceDate.Date;
            TeacherExpertise obj1 = this.context.TeacherExpertises.Where(x => x.TeacherID == mTeacherID && x.SessionID == mSessionID).FirstOrDefault();
            return obj1;
        }






        //public List<TeacherQualification> GetTeacherQualificationMasterByTeacherIDandSessionID(int mTeacherID, int mSessionID)
        //{
        //    List<TeacherQualification> obj = new List<TeacherQualification>();
        //    obj = this.context.TeacherQualifications.Where(i => i.TeacherID == mTeacherID && i.SessionID == mSessionID).ToList();
        //    return obj;
        //}

        public List<TeacherExpertise> GetTeacherExpertiseDetailbyTeacherID(int mTeacherID)
        {
            List<TeacherExpertise> obj = new List<TeacherExpertise>();
            obj = this.context.TeacherExpertises.Where(i => i.TeacherID == mTeacherID).ToList();
            return obj;
        }

        public void UpdateTeacherExpertiseInfo(TeacherExpertise obj, byte UserID)
        {
            TeacherExpertise newObj = this.GetByID(obj.TeacherID);
            newObj.SessionID = obj.SessionID;
            newObj.Designation = obj.Designation;
            newObj.Description = obj.Description;
            newObj.Organization = obj.Organization;
            newObj.Salary = obj.Salary;
            newObj.FromDate = obj.FromDate;
            newObj.ToDate = obj.ToDate;
            newObj.UIDMod = UserID;
            //newObj.ModDate = DateTime.Now;

            this.Update(newObj);
        }


        public void InsertTex(TeacherExpertise objTexMaster, int mTeacherID, int mSessionID)
        {
            TeacherExpertise editTexDetail = new TeacherExpertise();
            if (editTexDetail != null)
            {
                editTexDetail.TeacherID = mTeacherID;
                editTexDetail.Designation = objTexMaster.Designation;
                editTexDetail.Description = objTexMaster.Description;
                editTexDetail.SessionID = mSessionID;
                editTexDetail.Organization = objTexMaster.Organization;
                editTexDetail.Salary = objTexMaster.Salary;
                editTexDetail.ID = objTexMaster.ID;
                this.Insert(editTexDetail);
            }

        }



        public void InsertTeacherExpertiseDetail(TeacherExpertise objTexDetail, int mTeacherID, int mSessionID)
        {

           

                this.Insert(objTexDetail);
           

        }


        public void UpdateTeacherExpertiseDetail(TeacherExpertise objTexDetail)
        {
            TeacherExpertise editTexDetail = this.GetByID(objTexDetail.ID);
            if (editTexDetail != null)
            {
                editTexDetail.Description = objTexDetail.Description;
                editTexDetail.Designation = objTexDetail.Designation;
                editTexDetail.FromDate = objTexDetail.FromDate;
                editTexDetail.ToDate = objTexDetail.ToDate;
                editTexDetail.Salary = objTexDetail.Salary;
                editTexDetail.Organization = objTexDetail.Organization;
              


                this.Update(editTexDetail);
            }

        }

        public void DeleteTeacherExpertiseDetail(TeacherExpertise objTexDetail)
        {
            TeacherExpertise editSession = this.GetByID(objTexDetail.TeacherID);
            if (editSession != null)
            {
                this.Delete(editSession);
            }

        }


        #region 123
        //public List<TeacherExpertise> GetTeacherExpertiesGridData(int TeacherID)
        //{
        //    List<TeacherExpertise> lstEx =
        //        (from xx in this.context.TeacherExpertises
        //         select new TeacherExpertise() 
        //         { 
        //             TeacherID = xx.TeacherID,
        //             Organization = xx.Organization,
        //             FromDate= xx.FromDate,
        //             ToDate=xx.ToDate,
        //             Description=xx.Description,
        //             Designation=xx.Designation,
        //             Salary=xx.Salary,
                                  
                 
                 
                 
        //         }).Where(x=>x.TeacherID==TeacherID).ToList<TeacherExpertise>();
        //    return lstEx;
        
        
        //}



        //public void AddNewTeacherRegistrationInfo(modelTeacherRegistration obj,byte UserID,int mSessionID)
        //{
        //    Teacher newObj = new Teacher()
        //    {
        //        EmployeeCode = obj.EmployeeCode,
        //        DOJ = obj.DOJ,
        //        FirstName = obj.FirstName,
        //        LastName = obj.LastName,
        //        UIDAdd=UserID,
        //        AddDate=DateTime.Now,
        //        SessionID=mSessionID
                
        //    };
        //    this.Insert(newObj);
        //}
        
        
        //public void UpdateTeacherQualificationInfo(TeacherQualification obj,byte UserID)
        //{
        //    TeacherQualification newObj = this.GetByID(obj.TeacherID);
        //    newObj.SessionID = obj.SessionID;
        //    newObj.DegreeName = obj.DegreeName;
        //    newObj.Percentage = obj.Percentage;
        //    newObj.University = obj.University;
        //    newObj.UIDMod = UserID;
        //    newObj.ModDate = DateTime.Now;
           
        //    this.Update(newObj);
        //}

        //public modelTeacherPersonalInfo GetPersonalInfo(int TeacherID)
        //{
        //    modelTeacherPersonalInfo obj = (
        //        from xx in this.context.Teachers
        //        where xx.TeacherID == TeacherID
        //        select new modelTeacherPersonalInfo()
        //        {
        //             Age=xx.Age,
        //             AnniversaryDate=xx.AnniversaryDate,
        //             BloodGroup=xx.BloodGroup,
        //             DateOfBirth=xx.DOB,
        //             DOJ=xx.DOJ,
        //             EmailID=xx.EmailID,
        //             EmployeeCode=xx.EmployeeCode,
        //             FatherName=xx.FatherName,
        //             FirstName=xx.FirstName,
        //             Gender=xx.Gender,
        //             Hostel=xx.Hostel,
        //             HusbandWifeName=xx.HusbandWifeName,
        //             IsClassTeacher=xx.IsClassTeacher,
        //             LastName=xx.LastName,
        //             LocalAddress=xx.LocalAddress,
        //             MaritalStatus=xx.MaritalStatus,
        //             MobileNo=xx.MobileNo,
        //             MotherName=xx.MotherName,
        //             ParmanentAddress=xx.ParmanentAddress,
        //             PhoneNo=xx.PhoneNo,
        //             Religion=xx.Religion,
        //             TeacherID=xx.TeacherID,
        //             Transport=xx.Transport,
        //             AppImage = xx.AppImage,
        //             AppImageName = xx.AppImageName,
                    
        //        }).FirstOrDefault<modelTeacherPersonalInfo>();
        //    return obj;
        //}
        //public Bitmap ByteToImage(byte[] blob)
        //{
        //    MemoryStream mStream = new MemoryStream();
        //    byte[] pData = blob;
        //    mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
        //    Bitmap bm = new Bitmap(mStream, false);
        //    mStream.Dispose();
        //    return bm;

        //}



        //public modelTeacherPhotoUploadInfo GetTeacherPhotoInfo(int TeacherID)
        //{
        //    modelTeacherPhotoUploadInfo obj = (
        //        from xx in this.context.Teachers
        //        where xx.TeacherID == TeacherID
        //        select new modelTeacherPhotoUploadInfo()
        //        {
        //            Age = xx.Age,
        //            AnniversaryDate = xx.AnniversaryDate,
        //            BloodGroup = xx.BloodGroup,
        //            DateOfBirth = xx.DOB,
        //            DOJ = xx.DOJ,
        //            EmailID = xx.EmailID,
        //            EmployeeCode = xx.EmployeeCode,
        //            FatherName = xx.FatherName,
        //            FirstName = xx.FirstName,
        //            Gender = xx.Gender,
        //            Hostel = xx.Hostel,
        //            HusbandWifeName = xx.HusbandWifeName,
        //            IsClassTeacher = xx.IsClassTeacher,
        //            LastName = xx.LastName,
        //            LocalAddress = xx.LocalAddress,
        //            MaritalStatus = xx.MaritalStatus,
        //            MobileNo = xx.MobileNo,
        //            MotherName = xx.MotherName,
        //            ParmanentAddress = xx.ParmanentAddress,
        //            PhoneNo = xx.PhoneNo,
        //            Religion = xx.Religion,
        //            TeacherID = xx.TeacherID,
        //            Transport = xx.Transport,

        //        }).FirstOrDefault<modelTeacherPhotoUploadInfo>();
        //    return obj;
        //}



        ////public void UpdatePersonalInfo(modelTeacherPersonalInfo obj)
        ////{
        ////    try
        ////    {
        ////        Teacher newInfo = this.GetByID(obj.TeacherID);
        ////        newInfo.EmployeeCode = obj.EmployeeCode;
        ////        newInfo.Age = obj.Age;
        ////        newInfo.AnniversaryDate = obj.AnniversaryDate;
        ////        newInfo.BloodGroup = obj.BloodGroup;
        ////        newInfo.DOB = obj.DateOfBirth;
        ////        newInfo.DOJ = obj.DOJ;
        ////        newInfo.EmailID = obj.EmailID;
        ////        newInfo.FatherName = obj.FatherName;
        ////        newInfo.FirstName = obj.FirstName;
        ////        newInfo.Gender = obj.Gender;
        ////        newInfo.Hostel = obj.Hostel;
        ////        newInfo.HusbandWifeName = obj.HusbandWifeName;
        ////        newInfo.LastName = obj.LastName;
        ////        newInfo.LocalAddress = obj.LocalAddress;
        ////        newInfo.MaritalStatus = obj.MaritalStatus;
        ////        newInfo.MobileNo = obj.MobileNo;
        ////        newInfo.MotherName = obj.MotherName;
        ////        newInfo.ParmanentAddress = obj.ParmanentAddress;
        ////        newInfo.PhoneNo = obj.PhoneNo;
        ////        newInfo.Religion = obj.Religion;
        ////        newInfo.Transport = obj.Transport;
        ////        newInfo.IsClassTeacher = obj.IsClassTeacher;
               
        ////        //if (obj.attachment.HasFile())
        ////        //{
        ////        //    ////save the file
        ////        //    //string mPath="~/images
        ////        //    //obj.attachment.SaveAs(obj.EnrollmentNo +"
        ////        //    ////Send it as an attachment 
        ////        //    //Attachment messageAttachment = new Attachment(Model.attachment.InputStream, Model.attachment.FileName);
        ////        //}
        ////        this.Update(newInfo);
        ////    }
        ////    catch (Exception)
        ////    {

        ////        throw;
        ////    }

        ////}

        //public void UpdateTeacherPhoto(Teacher obj)
        //{
        //    try
        //    {
        //        Teacher newInfo = this.GetByID(obj.TeacherID);
        //        //newInfo.EnrollmentNo = obj.EnrollmentNo;
        //        //newInfo.EnrollmentDate = obj.EnrollmentDate;
        //        newInfo.AppImageName = obj.AppImageName;
        //        newInfo.AppImage = obj.AppImage;

        //        this.Update(newInfo);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}

        //internal void AddNewTeacherRegistrationInfo(modelRegistration obj)
        //{
        //    throw new NotImplementedException();
        //}
  #endregion
        //public object GetRegistrationGridData()
        //{
        //    throw new NotImplementedException();
        //}

        //public StudentRegistration GetTeacherQualificationGridData(int Model)
        //{
        //    throw new NotImplementedException();
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

    //public class modelTeacherRegistration
    //{
    //    [Key]
    //    public int TeacherID { get; set; }
    //    [Required(ErrorMessage = "EmployeeCode is required")]
    //    public string EmployeeCode { get; set; }
    //    [DataType(DataType.Date)]
    //    [DisplayFormat(DataFormatString = "{0:d}")]
    //    public Nullable<System.DateTime> DOJ{ get; set; }

    //    [Required(ErrorMessage = "First Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
    //    public string FirstName { get; set; }
    //    [Required(ErrorMessage = "Last Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
    //    public string LastName { get; set; }
    //    public string FullName { get; set; }
    //    public int SessionID { get; set; }

    //}
    //public class modelTeacherPersonalInfo
    //{
    //    [Key]
    //    public int TeacherID { get; set; }
    //    public string EmployeeCode { get; set; }
    //    [DataType(DataType.Date)]
    //    [DisplayFormat(DataFormatString = "{0:d}")]
    //    public Nullable<System.DateTime> DOJ { get; set; }
    //    [Required(ErrorMessage = "First Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
    //    public string FirstName { get; set; }
      
    //    public string LastName { get; set; }
      
    //    public Nullable<System.DateTime> DateOfBirth { get; set; }
    //    public string PhoneNo { get; set; }
    //    public Nullable<byte> Age { get; set; }
    //    public string BloodGroup { get; set; }
      
    //    public string Gender { get; set; }
       
    //    public string Religion { get; set; }
      
    //    public string LocalAddress { get; set; }
       
    //    public string MaritalStatus { get; set; }
    //    public string EmailID { get; set; }
    //    public string ParmanentAddress { get; set; }
    //    public Nullable<bool> Transport { get; set; }
    //    public Nullable<bool> Hostel { get; set; }
    //    public Nullable<bool> IsClassTeacher { get; set; }
    //    public string MotherName { get; set; }
    //    public string FatherName { get; set; }
    //    public string HusbandWifeName { get; set; }
    //    public string MobileNo { get; set; }
    //    [DisplayFormat(DataFormatString = "{0:d}")]
    //    public Nullable<System.DateTime> AnniversaryDate { get; set; }
    //    public HttpPostedFileBase attachment { get; set; }
    //    public byte[] AppImage { get; set; }
    //    public string AppImageName { get; set; }
    //    public Bitmap StudentPhoto { get; set; }


    //}

    //public class modelTeacherPhotoUploadInfo
    //{
    //    [Key]
    //    public int TeacherID { get; set; }
    //    public string EmployeeCode { get; set; }
    //    [DataType(DataType.Date)]
    //    [DisplayFormat(DataFormatString = "{0:d}")]
    //    public Nullable<System.DateTime> DOJ { get; set; }
    //    [Required(ErrorMessage = "First Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
    //    public string FirstName { get; set; }

    //    public string LastName { get; set; }

    //    public Nullable<System.DateTime> DateOfBirth { get; set; }
    //    public string PhoneNo { get; set; }
    //    public Nullable<byte> Age { get; set; }
    //    public string BloodGroup { get; set; }

    //    public string Gender { get; set; }

    //    public string Religion { get; set; }

    //    public string LocalAddress { get; set; }

    //    public string MaritalStatus { get; set; }
    //    public string EmailID { get; set; }
    //    public string ParmanentAddress { get; set; }
    //    public Nullable<bool> Transport { get; set; }
    //    public Nullable<bool> Hostel { get; set; }
    //    public Nullable<bool> IsClassTeacher { get; set; }
    //    public string MotherName { get; set; }
    //    public string FatherName { get; set; }
    //    public string HusbandWifeName { get; set; }
    //    public string MobileNo { get; set; }
    //    [DisplayFormat(DataFormatString = "{0:d}")]
    //    public Nullable<System.DateTime> AnniversaryDate { get; set; }
    //    public HttpPostedFileBase attachment { get; set; }
    //    public byte[] AppImage { get; set; }
    //    public string AppImageName { get; set; }
    //    public Bitmap StudentPhoto { get; set; }


    //}

    #endregion


}
