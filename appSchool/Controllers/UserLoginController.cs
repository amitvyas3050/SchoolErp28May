using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.Model;
using appSchool.ViewModels;
using appSchool.Repositories;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using System.Threading;
//using WebMatrix.WebData;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Web.Script.Serialization;
using System.Data;
using System.IO;
using System.Web.Hosting;

namespace appSchool.Controllers
{
     

    [NoCache]
    public class UserLoginController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public static string PR_EmailID = string.Empty;
        public static string PR_Password = string.Empty;
        public static string PR_Host = string.Empty;
        public static int PR_Port = 0;
        public static bool IsSMS_Facility = false;

        IPLogMasterDemo objLog = null;

        public ActionResult Index()
        {
          
            return View("Index",new ModelUserLogin());
        }
        
        public ActionResult LogOff()
        {
            //if (Session["IPAddressID"] != null)
            //{
            //    int ID = int.Parse(Session["IPAddressID"].ToString());
            //    bool res = UpdateLogoutDateTime(ID);
            //}
           
            Session.Clear();
            Session.Abandon();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now);

            return Redirect("~/");




        }

        public bool UpdateLogoutDateTime(int ID)
        {
            bool res = false;

            string sql = "Update IPLogMaster Set LogOutDateTime=GetDate() where ID=" + ID + "";
            SqlCommand cmd = new SqlCommand(sql, DB.GetActiveConnection());
            int i = DB.ExecuteNoResult(cmd);
            if (i > 0)
            {
                res = true;
            }
            return res;
        }

        private void CheckLogin()
        {
            string dtCheck = string.Empty;
            string sql = string.Empty;
            dtCheck = "2022-07-31";
            if (DateTime.Now.Date >= DateTime.Parse(dtCheck).Date)
            {
                sql = "disable trigger trReadOnly_tblEvents on [dbo].[Session] ";
                DB.ExecuteQuery(sql);
                sql = "update dbo.[session] set MkRd=1 ";
                DB.ExecuteQuery(sql);
                sql = " enable trigger trReadOnly_tblEvents on [dbo].[Session] ";
                DB.ExecuteQuery(sql);
            }
        }

        public static void CheckLoginByEncryptFile(string mCompID)
        {
            string dtCheck = string.Empty;
            string sql = string.Empty;
            string mdt = DateTime.Now.Date.Year.ToString() + "-" + DateTime.Now.Date.Month.ToString() + "-" + DateTime.Now.Date.Day.ToString();

            if (DateTime.Parse(mdt) >= GetEncryptValidDatevalue(mCompID))
            {
                sql = " disable trigger trReadOnly_tblEvents on [dbo].[Session] ";
                DB.ExecuteQueryNoResult(sql);
                sql = "update dbo.[session] set MkRd=1 ";
                DB.ExecuteQueryNoResult(sql);
                sql = " enable trigger trReadOnly_tblEvents on [dbo].[Session] ";
                DB.ExecuteQueryNoResult(sql);
                
            }

        }

        private static DateTime GetEncryptValidDatevalue(string CompID)
        {
            DateTime ValidDate = DateTime.Parse("2010-07-31");
            string FileValue = string.Empty;
            try
            {
                var fileContents = System.IO.File.ReadAllText(HostingEnvironment.MapPath(@"~/Content/validdateencrypt.txt"));
                //using (StreamReader rd = new StreamReader(Server.MapPathvaliddateencrypt.txt"))
                //{
                //    FileValue = rd.ReadLine();
                //}
                string DecryptValue = cCommon.Decrypt(fileContents, cCommon.EncryptPwdkey);

                string CheckCompID = DecryptValue.Split(';')[0].ToString();
                CheckCompID = CheckCompID.Split('=')[1];

                if (CheckCompID != CompID)
                    return ValidDate;

                ValidDate = DateTime.Parse(DecryptValue.Split(';')[1].ToString());


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
            }
            return ValidDate;
        }

        [HttpPost]
        public ActionResult UserLoginCheck(ModelUserLogin obj)
        {
            string Message = string.Empty;

            
            bool MachineActivationFlag = false;

            
           
             
           

            //if (!Request.IsAjaxRequest())
            //{ // Theme changing
            //    ModelState.Clear();
            //    return View("Index", obj);
            //}

            // Intentionally pauses server-side processing,
            // to demonstrate the Loading Panel functionality.
            Thread.Sleep(1000);
            if (ModelState.IsValid)
            {
                UserLogin objuser=new UserLogin();

                objuser = new UnitOfWork().userLoginService.IsValid(obj);
                if (objuser!=null)
                {
                    Session["UserID"] = objuser.UserID;
                    
                    //Session["SessionID"] = obj.SessionID;
                    int mUserRoleId = 0;
                    if (objuser.RoleId != null)
                    {
                        mUserRoleId = objuser.RoleId.Value;
                        Session["UserRoleID"] = mUserRoleId;
                    }
                    else
                        Session["UserRoleID"] = mUserRoleId;

                    Session["SessionID"] = obj.SessionID = unitOfWork.academicYearService.GetCurrentSessionByFlag().SessionId;
                    Session["BranchID"] = obj.BranchID;
                    Session["BranchName"] = unitOfWork.branchService.GetBranchName(byte.Parse(obj.BranchID.ToString()));
                    Session["UserName"] = obj.UserName;
                    Session["SessionName"] = unitOfWork.academicYearService.GetByID(Session["SessionID"]).SessionName;
                    Session["FullName"] = objuser.FullName;
                    Session["IsAdmin"] = objuser.IsAdmin;
                    Session["CompID"] = objuser.CompID;
                    Session["UserRoleName"] = string.Empty;
                    Session["SchoolName"] = string.Empty;

                    string UserRollName = string.Empty; string SchoolName = string.Empty;
                    UserRole objrole = unitOfWork.userRoleservices.GetUserRoleName(mUserRoleId, objuser.CompID, objuser.BranchID);
                    if(objrole != null)
                    {
                        Session["UserRoleName"] = objrole.Name;
                    }
                    SchoolMaster objschooldetail = unitOfWork.schoolMasterService.GetSchoolSetup();
                    if (objschooldetail != null)
                    {
                        Session["SchoolName"] = objschooldetail.SchoolName;
                    }

                    
                    string mCompID = Session["CompID"].ToString();
                    CheckLoginByEncryptFile(mCompID);


                    Session objsession = unitOfWork.academicYearService.GetByID(obj.SessionID);
                    if (objsession.MkRd == true)
                    {
                        return View("Index", obj);
                    }


                    //MachineActivation objMA = new MachineActivation();
                    //MachineActivationFlag = objMA.IsValidMachine(int.Parse(Session["CompID"].ToString()));
                    //if (MachineActivationFlag == false)
                    //{
                    //    Message = "Invalid machin.first registrerd it";
                    //    ViewData["USerMessage"] = Message;
                    //    return View("Index", obj);
                    //}
                    SchoolSetupStaticClass._ThemesName = objuser.ThemesName;
                    if (objuser.ThemesName != null)
                    {
                        DevExpressHelper.Theme = objuser.ThemesName;
                    }
                    SchoolMaster objschool = new SchoolMaster();
                    objschool = unitOfWork.schoolMasterService.GetSchoolSetup();

                    if (objschool != null)
                    {
                        SchoolSetupStaticClass._SchoolName = objschool.SchoolName;
                        SchoolSetupStaticClass._Address1 = objschool.Address1;
                        SchoolSetupStaticClass._Address2 = objschool.Address2;
                        SchoolSetupStaticClass._BusFacility = objschool.BusFacility;
                        SchoolSetupStaticClass._EmailID = objschool.EmailID;
                        SchoolSetupStaticClass._City = objschool.City;
                        SchoolSetupStaticClass._HostelFacility = objschool.HostelFacility;
                        SchoolSetupStaticClass._MessFacility = objschool.MessFacility;
                        SchoolSetupStaticClass._PhoneNo1 = objschool.PhoneNo1;
                        SchoolSetupStaticClass._PhoneNo2 = objschool.PhoneNo2;
                        SchoolSetupStaticClass._RegistrationNo = objschool.RegistrationNo;
                        SchoolSetupStaticClass._Remark = objschool.Remark;
                        SchoolSetupStaticClass._State = objschool.State;

                    }

                    SettingMaster objSetting = new SettingMaster();
                    objSetting = unitOfWork.SettingMasterService.GetSettingMaster();

                    if (objschool != null)
                    {
                        SettingMasterStaticClass._IncludeNameInSMS = objSetting.IncludeNameInSMS;
                       // SettingMasterStaticClass._ManageHistory = objSetting.ManageHistory;
                       // SettingMasterStaticClass._SaveSMSLog = objSetting.SaveSMSLog;
                    }

                    //objLog = new IPLogMasterDemo();
                    //InsertIPLogMaster();
                    //objLog.BranchID = 1;
                    //objLog.CompID = 1;
                    //objLog.PortalName = "MainPortal";
                    //objLog.SessionID = Session.SessionID;
                    //objLog.UserID = int.Parse(Session["UserID"].ToString());
                    //bool reslog = objLog.AddIPLogMaster();
                    //if (reslog)
                    //{
                    //    Session["IPAddressID"] = objLog.ID;
                    //}
                    


                    return RedirectToAction("Index", "Menu");

                    //return RedirectToAction("Index", "ClassSetup");

                }
                else
                {
                    Message = "Wrong User ID or Password";
                    ViewData["USerMessage"] = Message;
                    return View("Index", obj);
                }

                //object redirectActionName = "Index";
                //return PartialView("Index");
            }
            else
                return PartialView("Index", obj);
          
        }

        public void InsertIPLogMaster()
        {
            string visitorIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(visitorIPAddress))
            {
                visitorIPAddress = Request.ServerVariables["REMOTE_ADDR"];
            }
            if (visitorIPAddress == "::1")
            {
                visitorIPAddress = "127.0.0.1";
            }

            Location objlst = new Location();
            objlst = GetLocation(visitorIPAddress);

            if (objlst != null)
            {
                objLog.Country = objlst.CountryName.ToString() + ", " + objlst.RegionName.ToString() + ", " + objlst.CityName.ToString();
                objLog.IPAddress = visitorIPAddress.ToString();
            }

            try
            {
                IPHostEntry ip = Dns.GetHostEntry(objLog.IPAddress);
                objLog.HostName = ip.HostName;
            }
            catch (SocketException se)
            {
                objLog.HostName = "Not Found";
            }
            catch (Exception ex)
            {
            }

        }

        private Location GetLocation(string strIPAddress)
        {
            Location location = new Location();

            // account - http://www.ipinfodb.com/login.php    id=jngacollege   pass- jnga#1234
            string APIKey = "77e3b372612e2523a0fa24e87874b5029bd860e01bac49d2070bad23e85c3907";
            string url = string.Format("http://api.ipinfodb.com/v3/ip-city/?key={0}&ip={1}&format=json", APIKey, strIPAddress);

            try
            {
                using (WebClient client = new WebClient())
                {
                    string json = client.DownloadString(url);
                    location = new JavaScriptSerializer().Deserialize<Location>(json);
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {

                }

            }
            return location;
        }

        public ActionResult ForgotPasswordView()
        {

            return PartialView("ForgotPasswordView");
        }

        public JsonResult RecoverUserNameAndPassword(string MobileNo, string EmailID, int mType)
        {
            string Errormsg = string.Empty;
            string UserName = string.Empty;
            string Password = string.Empty;
           
         

            try
            {

                SettingMaster objSet = new SettingMaster();
                objSet = unitOfWork.userLoginService.GetPortalUserInfo();

                if (objSet != null)
                {
                    IsSMS_Facility = objSet.IsSMS_Facility;
                    PR_EmailID = objSet.PR_EmailID;
                    PR_Password = objSet.PR_Password;
                    PR_Host = objSet.PR_Host;
                    PR_Port = int.Parse(objSet.PR_Port.ToString());
                }

                if (mType == 1)
                {
                    UserLogin objuser = new UserLogin();
                    objuser = unitOfWork.userLoginService.CheckUserLoginByEmailID(EmailID);

                    if (objuser != null)
                    {
                        UserName = objuser.UserName;
                        Password = objuser.Password;
                    }

                    if (!string.IsNullOrEmpty(Password))
                    {
                        MailMessage mm = new MailMessage(PR_EmailID.Trim(), EmailID);
                        mm.Subject = "Password Recovery";
                        mm.Body = string.Format("Hi {0},<br /><br />Your password is {1}.<br /><br />Thank You.", UserName, Password);
                        mm.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = PR_Host.Trim();
                        smtp.EnableSsl = true;

                        NetworkCredential NetworkCred = new NetworkCredential();
                        NetworkCred.UserName = PR_EmailID.Trim();
                        NetworkCred.Password = PR_Password.Trim(); ;
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = PR_Port;
                        smtp.Send(mm);

                        
                        Errormsg = "Password has been sent to your email address.";
                        


                    }
                    else
                    {
                        Errormsg = "This email address does not match in records.";
                    }

                }


                if (mType == 2)
                {
                    UserLogin objusrlog = new UserLogin();
                    objusrlog = unitOfWork.userLoginService.CheckUserLoginByMobNo(MobileNo);

                    if (objusrlog != null)
                    {
                        UserName = objusrlog.UserName;
                        Password = objusrlog.Password;

                        string SMS = string.Empty;

                        SMS = "Hi Your Username is " + UserName + " and password  " + Password + " Thank you";

                        string mApi = string.Empty;
                        switch (objusrlog.CompID)
                        {
                            //------- 1. Nirmala Convent--------------
                            case 1:
                                mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=nirmalconvent&pwd=school@eee&to=" + MobileNo + "&sid=NcgiCJ&msg=" + SMS + "&fl=0&gwid=2";
                                break;
                            case 2:
                                mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + MobileNo + "&sid=STmark&msg=" + SMS + "&fl=0&gwid=2";
                                break;
                            case 3:
                                mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=KVJIII&pwd=school@eee&to=" + MobileNo + "&sid=KVJIII&msg=" + SMS + "&fl=0&gwid=2";
                                break;
                            //--------------St Xavier------------------
                            case 4:
                                mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS&to=" + MobileNo + "&sid=SXCJHS&msg=" + SMS + "&fl=0&gwid=2";

                                break;
                        }
                        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                        string responseString = respStreamReader.ReadToEnd();
                        respStreamReader.Close();
                        myResp.Close();


                        Errormsg = "Password has been sent to your MobileNo";

                    }
                    else
                    {
                        Errormsg = "This MobileNo does not match in records";
                    }


                }

            }
            catch (Exception ex)
            {

            }
                 



            

            return Json( new { Displaymsg = Errormsg }, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult ChangeTheme()
       {
            return PartialView("ThemePartial", new myModel() { currTheme = new ThemesModel() { Name = "Glass", Theme = CommonThemes.Glass } });
       }

        public ActionResult SchoolImageGallery()
        {
            string ImageSourceFolder = "~/Images/SchoolGallery";
            return PartialView("ImageGalleryPartial", ImageSourceFolder);
        }

        public ActionResult SchoolImageGalleryPartial()
        {
            string ImageSourceFolder = "~/Images/SchoolGallery";
            return PartialView("ImageGalleryPartial", ImageSourceFolder);
        }

        [HttpPost]
        public ActionResult PostTheme([ModelBinder(typeof(MyEditorsBinder))] ThemesModel themeCollection)
        {

            myModel mdl = new myModel() { currModule = appModule.Home, currTheme = themeCollection };
            return View("Index", mdl);
        }

        public ActionResult GridViewPartialView()
        {
            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            return PartialView("GridViewPartialView");
        }



    
    }


    public sealed class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnResultExecuting(filterContext);
        }
    }


    public class CommonDemoHelper
    {
        static Action<TextBoxSettings> textBoxSettingsMethod;
        static Action<DateEditSettings> dateEditSettingsMethod;
        static Action<LabelSettings> labelSettingsMethod;
        static Action<LabelSettings> wideLabelSettingsMethod;
        static Action<SpinEditSettings> spinEditSettingsMethod;
        static Action<MemoSettings> memoSettingsMethod;

        public static Action<TextBoxSettings> TextBoxSettingsMethod
        {
            get
            {
                if (textBoxSettingsMethod == null)
                    textBoxSettingsMethod = CreateTextBoxSettingsMethod();
                return textBoxSettingsMethod;
            }
        }
        public static Action<DateEditSettings> DateEditSettingsMethod
        {
            get
            {
                if (dateEditSettingsMethod == null)
                    dateEditSettingsMethod = CreateDateEditSettingsMethod();
                return dateEditSettingsMethod;
            }
        }
        public static Action<LabelSettings> LabelSettingsMethod
        {
            get
            {
                if (labelSettingsMethod == null)
                    labelSettingsMethod = CreateLabelSettingsMethod();
                return labelSettingsMethod;
            }
        }
        public static Action<LabelSettings> WideLabelSettingsMethod
        {
            get
            {
                if (wideLabelSettingsMethod == null)
                    wideLabelSettingsMethod = CreateWideLabelSettingsMethod();
                return wideLabelSettingsMethod;
            }
        }
        public static Action<SpinEditSettings> SpinEditSettingsMethod
        {
            get
            {
                if (spinEditSettingsMethod == null)
                    spinEditSettingsMethod = CreateSpinEditSettingsMethod();
                return spinEditSettingsMethod;
            }
        }
        public static Action<MemoSettings> MemoSettingsMethod
        {
            get
            {
                if (memoSettingsMethod == null)
                    memoSettingsMethod = CreateMemoSettingsMethod();
                return memoSettingsMethod;
            }
        }

        static Action<TextBoxSettings> CreateTextBoxSettingsMethod()
        {
            return settings =>
            {
                settings.ControlStyle.CssClass = "editor";
                settings.ShowModelErrors = true;
                settings.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithText;
            };
        }
        static Action<DateEditSettings> CreateDateEditSettingsMethod()
        {
            return settings =>
            {
                settings.ControlStyle.CssClass = "editor";
                settings.ShowModelErrors = true;
                settings.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithText;
            };
        }
        static Action<LabelSettings> CreateLabelSettingsMethod()
        {
            return settings => { settings.ControlStyle.CssClass = "label"; };
        }
        static Action<LabelSettings> CreateWideLabelSettingsMethod()
        {
            return settings => { settings.ControlStyle.CssClass = "wideLabel"; };
        }
        static Action<SpinEditSettings> CreateSpinEditSettingsMethod()
        {
            return settings =>
            {
                settings.ControlStyle.CssClass = "editor";
                settings.ShowModelErrors = true;
                settings.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithText;

                settings.Properties.NumberType = SpinEditNumberType.Float;
                settings.Properties.Increment = 0.1M;
                settings.Properties.LargeIncrement = 1;
                settings.Properties.SpinButtons.ShowLargeIncrementButtons = true;
            };
        }
        static Action<MemoSettings> CreateMemoSettingsMethod()
        {
            return settings =>
            {
                settings.Height = 50;
                settings.ControlStyle.CssClass = "editor";
                settings.ShowModelErrors = true;
                settings.Properties.ValidationSettings.ErrorDisplayMode = ErrorDisplayMode.ImageWithText;
            };
        }
    }

    public class Location
    {
        public string IPAddress { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TimeZone { get; set; }
    }

    public class IPLogMasterDemo
    {
        private SqlConnection _mConn;

        public SqlConnection MConn
        {
            get { return _mConn; }
            set { _mConn = value; }
        }
        private SqlTransaction _mTran;

        public SqlTransaction MTran
        {
            get { return _mTran; }
            set { _mTran = value; }
        }

        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _IPAddress;

        public string IPAddress
        {
            get { return _IPAddress; }
            set { _IPAddress = value; }
        }
        private string _HostName;

        public string HostName
        {
            get { return _HostName; }
            set { _HostName = value; }
        }
        private DateTime _LoginDateTime;

        public DateTime LoginDateTime
        {
            get { return _LoginDateTime; }
            set { _LoginDateTime = value; }
        }
        private DateTime _LogoutDateTime;

        public DateTime LogoutDateTime
        {
            get { return _LogoutDateTime; }
            set { _LogoutDateTime = value; }
        }
        private int _UserID;

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        private string _PortalName;

        public string PortalName
        {
            get { return _PortalName; }
            set { _PortalName = value; }
        }
        private string _Country;

        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        private string _SessionID;

        public string SessionID
        {
            get { return _SessionID; }
            set { _SessionID = value; }
        }

        private int _BranchID;
        public int BranchID
        {
            get { return _BranchID; }
            set { _BranchID = value; }
        }

        private int _CompID;
        public int CompID
        {
            get { return _CompID; }
            set { _CompID = value; }
        }




        public IPLogMasterDemo()
        {
            _ID = 0;
            _IPAddress = _Country = _PortalName = _HostName = string.Empty;


        }

        public bool AddIPLogMaster()
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction();

            bool res = false;

            SqlCommand cmd = new SqlCommand("Add_IPLogMaster", _mConn);
            cmd.Transaction = _mTran;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter param = cmd.Parameters.Add("@ID", SqlDbType.Int);
            param.Direction = ParameterDirection.InputOutput;
            param.Value = 0;

            cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = this._IPAddress;
            cmd.Parameters.Add("@HostName", SqlDbType.VarChar).Value = this._HostName;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = this._UserID;
            cmd.Parameters.Add("@PortalName", SqlDbType.VarChar).Value = this._PortalName;
            cmd.Parameters.Add("@Country", SqlDbType.VarChar).Value = this._Country;
            cmd.Parameters.Add("@SessionID", SqlDbType.VarChar).Value = this._SessionID;
            cmd.Parameters.Add("@BranchID", SqlDbType.TinyInt).Value = this._BranchID;
            cmd.Parameters.Add("@CompID", SqlDbType.TinyInt).Value = this._CompID;

            cmd.ExecuteNonQuery();
            int _ID = int.Parse(param.Value.ToString());
            if (_ID > 0)
            {
                ID = _ID;
                res = true;
                _mTran.Commit();
            }
            else
            {
                _mTran.Rollback();
            }
            return res;
        }

    }


}