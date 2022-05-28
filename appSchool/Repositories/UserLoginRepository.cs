using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace appSchool.Repositories
{
    public class UserLoginRepository : GenericRepository<UserLogin> 
    {
        public UserLoginRepository() : base(new dbSchoolAppEntities()) { }
        public UserLoginRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }

        //public IEnumerable<listItem> GetAcademicyearforSelect()
        //{
        //    List<listItem> lst = new List<listItem>();
        //    lst.Add(new listItem() { Value = -1, Description = "(None)" });
        //    lst.AddRange(
        //        from xx in this.context.Sessions
        //        select new listItem() { Value = xx.SessionId, Description = xx.SessionName }
        //        );
        //    return lst;
        //}

        
        //public void AddNewAcademicyear(Session obj) 
        //{
        //    this.Insert(obj);
        //    return;
        //}
        //public void UpdateAcademicyear(Session obj)
        //{
        //    Session c = this.GetByID(obj.SessionId);
        //    c.SessionName = obj.SessionName;
        //    this.Update(c);
        //    return;
        //}
        //public void DeleteAcademicyear(Session obj)
        //{
        //    this.Delete(obj);
        //    return;
        //}
        //public Session GetCurrentSession()
        //{
        //    //DateTime currDT=DateTime.Now;
        //    //Session s= this.Get(). .Where(x => x.StartDate.Value >= currDT && x.EndDate.Value <= currDT).FirstOrDefault();
        //    return this.GetByID(2);
        //}


        public List<UserLogin> GetUserLoginList(byte mCompID, byte mBranchID)
        {
            List<UserLogin> obj = new List<UserLogin>();
            obj = this.context.UserLogins.Where(x => x.CompID == mCompID && x.BranchID == mBranchID).ToList();
            if (obj == null)
            {
                List<UserLogin> objnew = new List<UserLogin>();
                return objnew;
            }
            else
                return obj;
        }

        public UserLogin IsValidOld(ModelUserLogin objmodel, byte mCompID)
        {
          
            UserLogin objLog = new UserLogin();

            objLog = this.context.UserLogins.Where(i => i.UserName == objmodel.UserName && i.Password == objmodel.Password && i.BranchID == objmodel.BranchID && i.CompID == mCompID).SingleOrDefault();
          
            return objLog;
        }
        public UserLogin IsValid(ModelUserLogin objmodel)
        {

            UserLogin objLog = new UserLogin();

            objLog = this.context.UserLogins.Where(i => i.UserName == objmodel.UserName && i.Password == objmodel.Password && i.BranchID == objmodel.BranchID).SingleOrDefault();

            return objLog;
        }

        public List<UserLogin> GetUserRegistrationGridData(byte mCompID, byte mBranchID)
        {


            List<UserLogin> lst = new List<UserLogin>();
            lst = this.context.UserLogins.Where(x => x.UserID > 0 && x.CompID==mCompID && x.BranchID==mBranchID ).ToList();

            return lst;

        }



        public void AddNewRegistrationUser(UserLogin obj)
        {
            this.Insert(obj);
        }


        public void UpdateUserRegistration(UserLogin obj, byte CompID, byte BranchID)
        {
            UserLogin newObj = this.GetByID(obj.UserID);

            //string sql = "Update UserLogin set FullName='" + obj.FullName + "' , UserName='" + obj.UserName + "' , Password='" + obj.Password + "', IsAdmin='" + obj.IsAdmin + "' " +
            //            " Where UserID= " + obj.UserID + " And CompID=" + CompID + " And BranchID=" + BranchID + " ";

            //DB.ExecuteQueryNoResult(sql);

            newObj.UserName = obj.UserName;
            newObj.IsAdmin = obj.IsAdmin;
            newObj.FullName = obj.FullName;
            newObj.Password = obj.Password;

            this.Update(newObj);
            return;

        
        }
        
        
        public modelUserPhotoUploadInfo GetUserPhotoInfo(int userID)
        {
            modelUserPhotoUploadInfo obj = (
                from xx in this.context.UserLogins
                where xx.UserID == userID
                select new modelUserPhotoUploadInfo()
                {
                    UserID = xx.UserID,
                    UserName = xx.UserName,
                    AppImage=xx.AppImage

                }).FirstOrDefault<modelUserPhotoUploadInfo>();
            return obj;
        }


        public void UpdateUserPhoto(UserLogin obj)
        {
            try
            {
                UserLogin newInfo = this.GetByID(obj.UserName);
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


        public SettingMaster GetPortalUserInfo()
        {
            SettingMaster obj = new SettingMaster();
            obj = this.context.SettingMasters.Where(x => x.SettingID > 0).SingleOrDefault();
            return obj;
        }

        public UserLogin CheckUserLoginByEmailID(string mEmailID)
        {
            UserLogin obj = new UserLogin();
            obj = this.context.UserLogins.Where(x => x.EmailID == mEmailID).SingleOrDefault();
            return obj;
        }

        public UserLogin CheckUserLoginByMobNo(string mMobNo)
        {
            UserLogin obj = new UserLogin();
            obj = this.context.UserLogins.Where(x => x.MobileNo == mMobNo).SingleOrDefault();
            return obj;
        }


    }

    #region Academicyear
   
    public class ModelUserLogin
    {
        [Key]
        public int UserID { get; set; } 
      
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Please Enter your User Name")]
        [StringLength(50, ErrorMessage = "Can't be more than 50 chars.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter your Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
         [Required(ErrorMessage = "Please Select Session")]
         [Display(Name = "Session")]
        public int SessionID { get; set; }
         [Required(ErrorMessage = "Please Select Branch")]
         [Display(Name = "Branch")]
         public int BranchID { get; set; } 



    }

    public class modelUserPhotoUploadInfo
    {
        [Key]
        public int UserID { get; set; }

        [Display(Name = "UserID")]
        [Required(ErrorMessage = "Please Enter your User Name")]
        [StringLength(50, ErrorMessage = "Can't be more than 50 chars.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter your Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Select Session")]
        [Display(Name = "Session")]
        public int SessionID { get; set; }
        public byte[] AppImage { get; set; }

    }


    #endregion


}
