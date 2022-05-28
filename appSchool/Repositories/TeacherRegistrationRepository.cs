using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Data;


namespace appSchool.Repositories
{
    public class TeacherRegistrationRepository : GenericRepository<Teacher>
    {
        public TeacherRegistrationRepository() : base(new dbSchoolAppEntities()) { }
        public TeacherRegistrationRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }

        public Teacher GetProfileInfo(int TeacherID)
        {
            Teacher obj = GetByID(TeacherID);
             return obj;
        }
        public List<modelTeacherRegistration> GetTeacherRegistrationGridData(byte mCompID, byte mBranchID)
        {
            List<modelTeacherRegistration> lst =
                (from xx in this.context.Teachers
                 select new modelTeacherRegistration()
                 {
                     TeacherID = xx.TeacherID,
                     EmployeeCode = xx.EmployeeCode,
                     DOJ = xx.DOJ,
                     FirstName = xx.FirstName,
                     LastName = xx.LastName,
                     FullName = xx.FirstName + " " + xx.LastName,


                 }
                 ).ToList<modelTeacherRegistration>();
            return lst;

        }
        public List<Teacher> GetTeacherRegistrationDataForList(byte mCompID, byte mBranchID)
        {
            List<Teacher> obj = new List<Teacher>();
            obj = this.context.Teachers.Where(x => x.CompID == @mCompID && x.BranchID == mBranchID).ToList();
            return obj;
        }

        public string GetFullNameOfTeacher(int TeacherID, byte mCompID, byte mBranchID)
        {
            string FullName = string.Empty;

            Teacher obj = new Teacher();
            obj = this.context.Teachers.Where(x => x.TeacherID == TeacherID && x.CompID == mCompID && x.BranchID == mBranchID).SingleOrDefault();

            if (obj != null)
                FullName = obj.FirstName + " " + obj.LastName;
            return FullName;
        }


        public List<modelTeacherShiftTimeUploadInfo> GetTeacherListForInOutTime()
        {
            List<modelTeacherShiftTimeUploadInfo> list = new List<modelTeacherShiftTimeUploadInfo>();
            string sql = "Select * From Teacher";
            DataTable dt = DB.ExecuteQuery(sql);

            foreach(DataRow dr in dt.Rows)
            {
                DateTime dtInTime = DateTime.Now;
                DateTime dtOutTime = DateTime.Now;
                modelTeacherShiftTimeUploadInfo objteach = new modelTeacherShiftTimeUploadInfo();

                objteach.EmployeeName = dr["FirstName"].ToString() + " "+ dr["LastName"].ToString();
                objteach.FatherName = dr["FatherName"].ToString();
                objteach.EmployeeCode = dr["EmployeeCode"].ToString();
                objteach.TeacherID = int.Parse(dr["TeacherID"].ToString());
                objteach.StartTime = (dr["StartTime"].ToString());
                string[] timearray = objteach.StartTime.Split(':');
                DateTime dateToUse = DateTime.Now;
                DateTime timeToUse = new DateTime(2018, 1, 1, int.Parse(timearray[0].ToString()), int.Parse(timearray[1].ToString()), 00); //10:15:30 AM
                objteach.InTime = timeToUse;

                objteach.EndTime = (dr["EndTime"].ToString());
                timearray = objteach.EndTime.Split(':');
                 dateToUse = DateTime.Now;
                 timeToUse = new DateTime(2018, 1, 1, int.Parse(timearray[0].ToString()), int.Parse(timearray[1].ToString()), 00); //10:15:30 AM
                objteach.OutTime = timeToUse;

                list.Add(objteach);
            }

           
            //list = this.context.Teachers.Where(x => x.TeacherID > 0).ToList();
            return list;
        }

        //public void AddNewTeacherRegistrationInfo(modelTeacherRegistration obj, byte UserID, int mSessionID)
        //{
        //    Teacher newObj = new Teacher()
        //    {
        //        EmployeeCode = obj.EmployeeCode,
        //        DOJ = obj.DOJ,
        //        FirstName = obj.FirstName,
        //        LastName = obj.LastName,
        //        UIDAdd = UserID,
        //        AddDate = DateTime.Now,
        //        SessionID = mSessionID,
        //        CompID = obj.CompID,
        //        BranchID = obj.BranchID

        //    };
        //    this.Insert(newObj);
        //}


        public void AddNewTeacherRegistrationInfo(Teacher obj,byte UserID,int mSessionID)
        {
           
                obj.UIDAdd=UserID;
                obj.AddDate=DateTime.Now;
                obj.SessionID=mSessionID;
                obj.CompID=obj.CompID;
                obj.BranchID=obj.BranchID;
          
            this.Insert(obj);
        }
        public void UpdateTeacherRegistrationInfo(modelTeacherRegistration obj,byte UserID, byte CompID, byte BranchID)
        {
            Teacher newObj = this.GetByID(obj.TeacherID);
            newObj.EmployeeCode = obj.EmployeeCode;
            newObj.DOJ = obj.DOJ;
            newObj.FirstName = obj.FirstName;
            newObj.LastName = obj.LastName;
            newObj.DesignationId = obj.DesignationID;
            newObj.UIDMod = UserID;
            newObj.ModDate = DateTime.Now;
            newObj.SessionID = UserID;
            newObj.CompID = CompID;
            newObj.BranchID = BranchID;
            this.Update(newObj);
        }
        public modelTeacherPersonalInfo GetPersonalInfo(int TeacherID)
        {
            modelTeacherPersonalInfo obj = (
                from xx in this.context.Teachers
                where xx.TeacherID == TeacherID
                select new modelTeacherPersonalInfo()
                {
                     Age=xx.Age,
                     AnniversaryDate=xx.AnniversaryDate,
                     BloodGroup=xx.BloodGroup,
                     DateOfBirth=xx.DOB,
                     DOJ=xx.DOJ,
                     EmailID=xx.EmailID,
                     EmployeeCode=xx.EmployeeCode,
                     FatherName=xx.FatherName,
                     FirstName=xx.FirstName,
                     Gender=xx.Gender,
                     Hostel=xx.Hostel,
                     HusbandWifeName=xx.HusbandWifeName,
                     IsClassTeacher=xx.IsClassTeacher,
                     LastName=xx.LastName,
                     LocalAddress=xx.LocalAddress,
                     MaritalStatus=xx.MaritalStatus,
                     MobileNo=xx.MobileNo,
                     MotherName=xx.MotherName,
                     ParmanentAddress=xx.ParmanentAddress,
                     PhoneNo=xx.PhoneNo,
                     Religion=xx.Religion,
                     TeacherID=xx.TeacherID,
                     Transport=xx.Transport,
                     AppImage = xx.AppImage,
                     AppImageName = xx.AppImageName,
                    
                }).FirstOrDefault<modelTeacherPersonalInfo>();
            return obj;
        }
        public Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;

        }

        
        public modelTeacherPhotoUploadInfo GetTeacherPhotoInfo(int TeacherID)
        {
            modelTeacherPhotoUploadInfo obj = (
                from xx in this.context.Teachers
                where xx.TeacherID == TeacherID
                select new modelTeacherPhotoUploadInfo()
                {
                    Age = xx.Age,
                    AnniversaryDate = xx.AnniversaryDate,
                    BloodGroup = xx.BloodGroup,
                    DateOfBirth = xx.DOB,
                    DOJ = xx.DOJ,
                    EmailID = xx.EmailID,
                    EmployeeCode = xx.EmployeeCode,
                    FatherName = xx.FatherName,
                    FirstName = xx.FirstName,
                    Gender = xx.Gender,
                    Hostel = xx.Hostel,
                    HusbandWifeName = xx.HusbandWifeName,
                    IsClassTeacher = xx.IsClassTeacher,
                    LastName = xx.LastName,
                    LocalAddress = xx.LocalAddress,
                    MaritalStatus = xx.MaritalStatus,
                    MobileNo = xx.MobileNo,
                    MotherName = xx.MotherName,
                    ParmanentAddress = xx.ParmanentAddress,
                    PhoneNo = xx.PhoneNo,
                    Religion = xx.Religion,
                    TeacherID = xx.TeacherID,
                    Transport = xx.Transport,
                    AppImage=xx.AppImage,
                    AppImageName=xx.AppImageName

                }).FirstOrDefault<modelTeacherPhotoUploadInfo>();
            return obj;
        }
        public modelTeacherSignatureUploadInfo GetTeacherSignatureInfo(int TeacherID)
        {
            modelTeacherSignatureUploadInfo obj = (
                from xx in this.context.Teachers
                where xx.TeacherID == TeacherID
                select new modelTeacherSignatureUploadInfo()
                {
                    TeacherID = xx.TeacherID,
                    SignatureImg = xx.SignatureImg
                
                }).FirstOrDefault<modelTeacherSignatureUploadInfo>();
            return obj;
        }
        public modelTeacherDocumentUploadInfo GetTeacherDocumentInfo(int teacherID)
        {



            modelTeacherDocumentUploadInfo obj =
             (from xx in this.context.Teachers
              where xx.TeacherID == teacherID
              select new modelTeacherDocumentUploadInfo()
              {
                  TeacherID = xx.TeacherID,
                  FirstName = xx.FirstName,
                  LastName = xx.LastName,
                  CompID = xx.CompID,
                  BranchID = xx.BranchID,
                  //SessionID = xx.SessionID,
              }).FirstOrDefault<modelTeacherDocumentUploadInfo>();

            return obj;

        }
        public void UpdatePersonalInfo(modelTeacherPersonalInfo obj)
        {
            try
            {
                Teacher newInfo = this.GetByID(obj.TeacherID);
                newInfo.EmployeeCode = obj.EmployeeCode;
                newInfo.Age = obj.Age;
                newInfo.AnniversaryDate = obj.AnniversaryDate;
                newInfo.BloodGroup = obj.BloodGroup;
                newInfo.DOB = obj.DateOfBirth;
                newInfo.DOJ = obj.DOJ;
                newInfo.EmailID = obj.EmailID;
                newInfo.FatherName = obj.FatherName;
                newInfo.FirstName = obj.FirstName;
                newInfo.Gender = obj.Gender;
                newInfo.Hostel = obj.Hostel;
                newInfo.HusbandWifeName = obj.HusbandWifeName;
                newInfo.LastName = obj.LastName;
                newInfo.LocalAddress = obj.LocalAddress;
                newInfo.MaritalStatus = obj.MaritalStatus;
                newInfo.MobileNo = obj.MobileNo;
                newInfo.MotherName = obj.MotherName;
                newInfo.ParmanentAddress = obj.ParmanentAddress;
                newInfo.PhoneNo = obj.PhoneNo;
                newInfo.Religion = obj.Religion;
                newInfo.Transport = obj.Transport;
                newInfo.IsClassTeacher = obj.IsClassTeacher;
               
                //if (obj.attachment.HasFile())
                //{
                //    ////save the file
                //    //string mPath="~/images
                //    //obj.attachment.SaveAs(obj.EnrollmentNo +"
                //    ////Send it as an attachment 
                //    //Attachment messageAttachment = new Attachment(Model.attachment.InputStream, Model.attachment.FileName);
                //}
                this.Update(newInfo);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Teacher GetSingleTeacher(int TacherID)
        {
            Teacher Obj = this.context.Teachers.Where(x => x.TeacherID == TacherID).SingleOrDefault();
            return Obj;
        }

        public void UpdateTeacherPhoto(Teacher obj)
        {
            try
            {
                Teacher newInfo = this.GetByID(obj.TeacherID);
                //newInfo.EnrollmentNo = obj.EnrollmentNo;
                //newInfo.EnrollmentDate = obj.EnrollmentDate;
                newInfo.AppImageName = obj.AppImageName;
                newInfo.AppImage = obj.AppImage;

                this.Update(newInfo);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void UpdateTeacherSignature(Teacher obj)
        {
            try
            {
                Teacher newInfo = this.GetByID(obj.TeacherID);
                newInfo.SignatureImg = obj.SignatureImg;

                this.Update(newInfo);
            }
            catch (Exception)
            {

                throw;
            }

        }



        internal void AddNewTeacherRegistrationInfo(modelRegistration obj)
        {
            throw new NotImplementedException();
        }

        #region  STAFF Registration

        public void AddNewStaffRegistrationInfo(Teacher obj, byte UserID, int mSessionID)
        {

            obj.UIDAdd = UserID;
            obj.AddDate = DateTime.Now;
            obj.SessionID = mSessionID;
            obj.CompID = obj.CompID;
            obj.BranchID = obj.BranchID;

            this.Insert(obj);
        }
        public void UpdateStaffRegistrationInfo(modelTeacherRegistration obj, byte UserID)
        {
            Teacher newObj = this.GetByID(obj.TeacherID);
            newObj.EmployeeCode = obj.EmployeeCode;
            newObj.DOJ = obj.DOJ;
            newObj.FirstName = obj.FirstName;
            newObj.LastName = obj.LastName;
            newObj.DesignationId = obj.DesignationID;
            newObj.UIDMod = UserID;
            newObj.ModDate = DateTime.Now;

            this.Update(newObj);
        }
        #endregion

        //public object GetRegistrationGridData()
        //{
        //    throw new NotImplementedException();
        //}
    }
       
    public static class ExtensionMethods
    {
        public static bool HasFile(this HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0) ? true : false;
        }

    }
    #region METADATA

    public class modelTeacherRegistration
    {
        [Key]
        public int TeacherID { get; set; }
        [Required(ErrorMessage = "EmployeeCode is required")]
        public string EmployeeCode { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> DOJ{ get; set; }

        [Required(ErrorMessage = "First Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int DesignationID { get; set; }
        public int SessionID { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }

    }
    public class modelTeacherPersonalInfo
    {
        [Key]
        public int TeacherID { get; set; }
        public string EmployeeCode { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> DOJ { get; set; }
        [Required(ErrorMessage = "First Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        public string FirstName { get; set; }
      
        public string LastName { get; set; }
      
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string PhoneNo { get; set; }
        public Nullable<byte> Age { get; set; }
        public string BloodGroup { get; set; }
      
        public string Gender { get; set; }
       
        public string Religion { get; set; }
      
        public string LocalAddress { get; set; }
       
        public string MaritalStatus { get; set; }
        public string EmailID { get; set; }
        public string ParmanentAddress { get; set; }
        public Nullable<bool> Transport { get; set; }
        public Nullable<bool> Hostel { get; set; }
        public Nullable<bool> IsClassTeacher { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public string HusbandWifeName { get; set; }
        public string MobileNo { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> AnniversaryDate { get; set; }
        public HttpPostedFileBase attachment { get; set; }
        public byte[] AppImage { get; set; }
        public string AppImageName { get; set; }
        public Bitmap StudentPhoto { get; set; }


    }

    public class modelTeacherPhotoUploadInfo
    {
        [Key]
        public int TeacherID { get; set; }
        public string EmployeeCode { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> DOJ { get; set; }
        [Required(ErrorMessage = "First Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string PhoneNo { get; set; }
        public Nullable<byte> Age { get; set; }
        public string BloodGroup { get; set; }

        public string Gender { get; set; }

        public string Religion { get; set; }

        public string LocalAddress { get; set; }

        public string MaritalStatus { get; set; }
        public string EmailID { get; set; }
        public string ParmanentAddress { get; set; }
        public Nullable<bool> Transport { get; set; }
        public Nullable<bool> Hostel { get; set; }
        public Nullable<bool> IsClassTeacher { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public string HusbandWifeName { get; set; }
        public string MobileNo { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> AnniversaryDate { get; set; }
        public HttpPostedFileBase attachment { get; set; }
        public byte[] AppImage { get; set; }
        public string AppImageName { get; set; }
        public Bitmap StudentPhoto { get; set; }


    }
    public class modelTeacherSignatureUploadInfo
    {
        [Key]
        public int TeacherID { get; set; }
        public string EmployeeCode { get; set; }
        public HttpPostedFileBase attachment { get; set; }
        public byte[] SignatureImg { get; set; }

    }

    public class modelTeacherDocumentUploadInfo
    {
        [Key]
        public int TeacherID { get; set; }

        public string DocumentName { get; set; }
        public string FileName { get; set; }
        public string Remark { get; set; }


        [Required(ErrorMessage = "First Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name can't be Blank!!"), StringLength(100, ErrorMessage = "Can't be more than 100 chars.")]
        public string LastName { get; set; }
        //  [Required (ErrorMessage="Birth data is mandatory")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        public HttpPostedFileBase attachment { get; set; }
        public int SessionID { get; set; }
        public byte CompID { get; set; }
        public byte BranchID { get; set; }


    }

    public class modelTeacherShiftTimeUploadInfo
    {
        [Key]
        public int TeacherID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string FatherName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }


        
    }

    #endregion


}
