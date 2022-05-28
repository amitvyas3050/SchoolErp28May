using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.Repositories;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using System.Web.SessionState;
using System.Net;
using appSchool.ViewModels;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace appSchool.Controllers
{

    [NoCache]
    public class SendSMSOnlineAbsenteesController : Controller
    {
         private UnitOfWork unitOfWork = new UnitOfWork();
         private SqlConnection _mConn;
         private SqlTransaction _mTran;

         //public override string Name { get { return "Editing"; } }


        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appSendSMSOnlineAbsentees == 0)
            {
                return Redirect("~/");
            }

            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 25, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            //if (objuser != null)
            //{
            //    PermissionFlag._AddFlag = objuser.AddP;
            //    PermissionFlag._ModFlag = objuser.ModP;
            //    PermissionFlag._DelFlag = objuser.DelP;
            //}
            //else
            //{
            //    PermissionFlag._AddFlag = false;
            //    PermissionFlag._ModFlag = false;
            //    PermissionFlag._DelFlag = false;
            //}
            ViewData["AttendanceDate"] = DateTime.Now;
            return View();
        }

        public ActionResult PartialSendSMSOnlineAbsenteesView(DateTime mAttendanceDate, string mClassSetupID)
        {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             ViewData["ClassSetupID"] = mClassSetupID;
             List<vAttendanceOnlineClass> obj = new List<vAttendanceOnlineClass>();
             obj = new UnitOfWork().attendanceOnlineClassservices.GetOnlineAbsentStudentAttendanceDatewise(mAttendanceDate, mClassSetupID);

            return PartialView("ListSendSMSOnlineAbsenteesView", obj);

        }
         public ActionResult PartialSendSMSOnlineAbsenteesTemplateView()
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             return PartialView("SendSMSOnlineAbsenteesTemplateView", unitOfWork.smsTemplateService.GetSMSTemplateforSMSType(1, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }
         public ActionResult SMSTemplateGridRowChange(int ID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             return PartialView("SendSMSOnlineAbsenteesTextView", ID);
         }

         public ActionResult GetAllOnlineAbsenteesforGrid(DateTime mAttendanceDate, string mClassesID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             ViewData["ClassSetupID"] = mClassesID;
             List<vAttendanceOnlineClass> obj = new List<vAttendanceOnlineClass>();
             obj = new UnitOfWork().attendanceOnlineClassservices.GetOnlineAbsentStudentAttendanceDatewise(mAttendanceDate, mClassesID);
             ViewData["AttendanceDate"] = mAttendanceDate;

             return PartialView("ListSendSMSOnlineAbsenteesView", obj);
         }

         public ActionResult GetStudentListForSMS(string mClassesID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             ViewData["ClassSetupID"] = mClassesID;
             return PartialView("ListSendGeneralMessegeView", unitOfWork.sendMessegeService.GetStudentSessionByClassSetupIDs(mClassesID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));

         }

         public int SaveSMSLogMaster(String SMSModal, int mSessionID, string mSMSText, string mSMSMobileNo, int mOrderBy, int mUIDAdd)
         {
             
             int i = 0;


             SqlCommand cmdMaster = new SqlCommand("Add_SMSLogMaster", _mConn);
             cmdMaster.CommandType = CommandType.StoredProcedure;
             cmdMaster.Transaction = _mTran;
             //cmdMaster.Parameters.Clear();

             cmdMaster.Parameters.AddWithValue("@SessionID", mSessionID);
             cmdMaster.Parameters.AddWithValue("@SMSText", mSMSText);
             if (mSMSMobileNo != null)
                 cmdMaster.Parameters.AddWithValue("@SMSMobileNo", mSMSMobileNo);
             else
                 cmdMaster.Parameters.AddWithValue("@SMSMobileNo", string.Empty);
             cmdMaster.Parameters.AddWithValue("@OrderBy", mOrderBy);
             cmdMaster.Parameters.AddWithValue("@SMSModal", SMSModal);
             cmdMaster.Parameters.AddWithValue("@UIDAdd", mUIDAdd);


             SqlParameter outPutParameter = new SqlParameter();
             outPutParameter.ParameterName = "@SMSLogID";
             outPutParameter.SqlDbType = SqlDbType.Int;
             outPutParameter.Direction = ParameterDirection.Output;
             cmdMaster.Parameters.Add(outPutParameter);

             cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
             cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

             cmdMaster.ExecuteNonQuery();
             string SMSLogMasterID = outPutParameter.Value.ToString();

             if (SMSLogMasterID != string.Empty || SMSLogMasterID != "" || SMSLogMasterID!=null)
             {
                 i = int.Parse(SMSLogMasterID);
             }




             return i;

         }

         private bool SaveSMSLogDetail(modelSendSms obj, int SMSLogID)
         {
             int i = 1;
             bool _DetailSaved = false;
             string Message = string.Empty;
             StringBuilder mQuery = new StringBuilder();
             mQuery.Append("INSERT INTO dbo.SMSLogDetail( SMSLogID,StudentID,MobileNo,SMSText,SessionID,CompID,BranchID)");
             string StudnetID = obj.smsStudentID;
             string[] StudIDs = StudnetID.Split(',');
             if (obj.SMSLanguage == 2)
             {
                 Message = obj.PrefixEnglish + " " + obj.smsTextEnglish;
             }
             else
             {
                 Message = obj.PrefixHindi + " " + obj.smsTextHindi;
             }

             foreach (string StudID in StudIDs)
             {
                 int PStudentID = int.Parse(StudID);

                 if (PStudentID > 0)
                 {
                     mQuery.Append("SELECT " + SMSLogID + "," + PStudentID + ",'0','" + Message + "'," + int.Parse(Session["SessionID"].ToString()) + "," + int.Parse(Session["CompID"].ToString()) + "," + int.Parse(Session["BranchID"].ToString()));

                     if (i < StudIDs.Count())
                     {
                         mQuery.Append(" UNION ALL ");
                         i++;
                     }
                 }
             }
             SqlCommand cmdDetail = new SqlCommand("Execute_SQL", _mConn);
             cmdDetail.Transaction = _mTran;
             cmdDetail.CommandType = CommandType.StoredProcedure;
             cmdDetail.Parameters.Add("@MQuery", SqlDbType.VarChar).Value = mQuery.ToString();
             int recs = cmdDetail.ExecuteNonQuery();

             _DetailSaved = (recs > 0) ? true : false;
             return _DetailSaved;

         }

         private bool SaveSMSLogDetailStudentWise(int SMSLogID, int mStudentID, string mMobileNo, string mSMSText)
         {

             bool _DetailSaved = false;
             StringBuilder mQuery = new StringBuilder();
             mQuery.Append("INSERT INTO dbo.SMSLogDetail( SMSLogID,StudentID,MobileNo,SMSText,SessionID,CompID,BranchID)");
             mQuery.Append("Select " + SMSLogID + "," + mStudentID + ",'" + mMobileNo + "','" + mSMSText + "'," + int.Parse(Session["SessionID"].ToString()) + "," + int.Parse(Session["CompID"].ToString()) + "," + int.Parse(Session["BranchID"].ToString()));

             SqlCommand cmdDetail = new SqlCommand("Execute_SQL", _mConn);
             cmdDetail.CommandType = CommandType.StoredProcedure;
             cmdDetail.Parameters.Add("@MQuery", SqlDbType.VarChar).Value = mQuery.ToString();
             int recs = cmdDetail.ExecuteNonQuery();

             _DetailSaved = (recs > 0) ? true : false;
             return _DetailSaved;

         }




         [HttpPost]
         public JsonResult SMSButtonClick(modelSendSms obj)
         {

             bool Result = false;
             string ErrorMsg = string.Empty;
             bool ResultSMSDetail = false;
             int newSMSLogMasterID = 0;
             string apitemplateid = "1207164209013384973";
             _mConn = DB.GetActiveConnection();
             _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

             if (ModelState.IsValid)
             {

                 if (obj.includeName == true)
                 {
                     //if (SettingMasterStaticClass._SaveSMSLog == true)
                     if (true) // default insert
                     {
                         #region LogMaster
                         string Message = string.Empty;
                         if (obj.SMSLanguage == 2)
                         {
                             Message = obj.PrefixEnglish + " " + obj.smsTextEnglish;
                         }
                         else
                         {
                             Message = obj.PrefixHindi + " " + obj.smsTextHindi;
                         }

                         newSMSLogMasterID = SaveSMSLogMaster("OnlineAbsenteesSMS", int.Parse(Session["SessionID"].ToString()), Message, obj.smsMobileNo, obj.OrderedBy, int.Parse(Session["SessionID"].ToString()));
                         if (newSMSLogMasterID > 0)
                         {
                             Result = true;
                             if (Result)
                                 _mTran.Commit();
                             else
                                 _mTran.Rollback();
                         }
                         #endregion
                     }
                     #region include Name and Class
                     if (obj.smsStudentID == string.Empty || obj.smsStudentID == null)
                     {
                         ErrorMsg += "Please Select Students.";
                         return new JsonResult() { JsonRequestBehavior = JsonRequestBehavior.AllowGet, 
                             Data = new{
                                 Resultmsg = ErrorMsg,
                                 ResultData = cCommon.RenderRazorViewToString("OnlineSendSMSAbsenteesEditTextView", obj, ControllerContext, ViewData, TempData) 
                             } };

                     }
                     string mStudnetID = string.Empty;
                     mStudnetID=obj.smsStudentID;

                     string[] bits = mStudnetID.Split(',');

                     string smsTextEnglish = obj.smsTextEnglish;
                     string smsTextHindi = obj.smsTextHindi;
                     string PrefixEnglish = obj.PrefixEnglish;
                     string PrefixHindi = obj.PrefixHindi;

                     string AbsentSubjects = string.Empty;
                     foreach (string bit in bits)
                     {
                         int StudentIDforSearch = int.Parse(bit);

                         string NewMessage = string.Empty;
                         vStudentSession objStudReg = new vStudentSession();
                         objStudReg = unitOfWork.sendMessegeService.GetStudentForSendingSMSbyStudentID(StudentIDforSearch, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                         AbsentSubjects = unitOfWork.attendanceOnlineClassservices.GetStudentAbsentSubjectNameListbyStudID(StudentIDforSearch, obj.ScheduledDate);

                         if (objStudReg.SMSInHindi == false)
                         {
                             if (obj.smsTextEnglish != string.Empty && obj.smsTextEnglish != "" && obj.smsTextEnglish != null)
                             {
                                 obj.smsTextEnglish = smsTextEnglish;
                                 obj.smsTextEnglish = obj.smsTextEnglish.Replace("$Name$", objStudReg.FullName);
                                 obj.smsTextEnglish = obj.smsTextEnglish.Replace("$Class$", objStudReg.ClassDescription);
                                 obj.smsTextEnglish = obj.smsTextEnglish.Replace("$Date$", obj.ScheduledDate.ToString("dd/MMM/yy"));
                                 obj.smsTextEnglish = obj.smsTextEnglish.Replace("$Subject$", AbsentSubjects);
                             }

                             if (obj.PrefixEnglish != string.Empty && obj.PrefixEnglish != "" && obj.PrefixEnglish != null)
                             {
                                 obj.PrefixEnglish = PrefixEnglish;
                                 obj.PrefixEnglish = obj.PrefixEnglish.Replace("$Name$", objStudReg.FullName);
                                 obj.PrefixEnglish = obj.PrefixEnglish.Replace("$Class$", objStudReg.ClassDescription);
                                 obj.PrefixEnglish = obj.PrefixEnglish.Replace("$Date$", obj.ScheduledDate.ToString("dd/MMM/yy"));
                                 obj.PrefixEnglish = obj.PrefixEnglish.Replace("$Subject$", AbsentSubjects);
                             }
                             NewMessage = obj.PrefixEnglish + " " + obj.smsTextEnglish;

                             if (objStudReg.SMSMobileNo != string.Empty)
                             {
                                 ErrorMsg = SendMesssageText(NewMessage.Trim(), objStudReg.SMSMobileNo, byte.Parse(Session["CompID"].ToString()), apitemplateid); // new for online attendance 14jan2022

                                 //ErrorMsg = unitOfWork.sendMessegeService.SendMesssageText((NewMessage.Trim()), objStudReg.SMSMobileNo, byte.Parse(Session["CompID"].ToString())); // new code 2021
                                 if (newSMSLogMasterID > 0 && SettingMasterStaticClass._SaveSMSLog==true)
                                 {
                                     ResultSMSDetail = SaveSMSLogDetailStudentWise(newSMSLogMasterID, StudentIDforSearch, objStudReg.SMSMobileNo, NewMessage);

                                 }
                             }
                         }
                         if (objStudReg.SMSInHindi == true)
                         {
                             if (obj.smsTextHindi != string.Empty && obj.smsTextHindi != "" && obj.smsTextHindi != null)
                             {
                                 obj.smsTextHindi = smsTextHindi;
                                 obj.smsTextHindi = obj.smsTextHindi.Replace("$Name$", objStudReg.FullName);
                                 obj.smsTextHindi = obj.smsTextHindi.Replace("$Class$", objStudReg.ClassDescription);
                                 obj.smsTextHindi = obj.smsTextHindi.Replace("$Date$", obj.ScheduledDate.ToString("dd/MMM/yy"));
                                 obj.smsTextHindi = obj.smsTextHindi.Replace("$Subject$", AbsentSubjects);
                             }

                             if (obj.PrefixHindi != string.Empty && obj.PrefixHindi != "" && obj.PrefixHindi != null)
                             {
                                 obj.PrefixHindi = PrefixHindi;
                                 obj.PrefixHindi = obj.PrefixHindi.Replace("$Name$", objStudReg.FullName);
                                 obj.PrefixHindi = obj.PrefixHindi.Replace("$Class$", objStudReg.ClassDescription);
                                 obj.PrefixHindi = obj.PrefixHindi.Replace("$Date$", obj.ScheduledDate.ToString("dd/MMM/yy"));
                                 obj.PrefixHindi = obj.PrefixHindi.Replace("$Subject$", AbsentSubjects);
                             }
                             NewMessage = obj.PrefixHindi + " " + obj.smsTextHindi;

                             if (objStudReg.SMSMobileNo != string.Empty)
                             {
                                 ErrorMsg = SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), objStudReg.SMSMobileNo, byte.Parse(Session["CompID"].ToString()), apitemplateid); // new for online attendance 14jan2022

                                //ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), objStudReg.SMSMobileNo, byte.Parse(Session["CompID"].ToString()));
                                 
                                 //ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(NewMessage.Trim(), objStudReg.SMSMobileNo, byte.Parse(Session["CompID"].ToString())); // new code 2021

                                 if (newSMSLogMasterID > 0 && SettingMasterStaticClass._SaveSMSLog == true)
                                 {
                                     ResultSMSDetail = SaveSMSLogDetailStudentWise(newSMSLogMasterID, StudentIDforSearch, objStudReg.SMSMobileNo, NewMessage);

                                 }
                             }

                         }

                     }


                     #endregion
                 }
                 else
                 {
                     #region OneTime
                     if (obj.smsMobileNo == null || obj.smsMobileNo==string.Empty)
                     {
                         ErrorMsg += "Please Select Mobile No.";
                         return new JsonResult()
                         {
                             JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                             Data = new
                             {
                                 Resultmsg = ErrorMsg,
                                 ResultData = cCommon.RenderRazorViewToString("OnlineSendSMSAbsenteesEditTextView", obj, ControllerContext, ViewData, TempData)
                             }
                         };
                     }

                     string mMobileNo =  obj.smsMobileNo;

                     if (obj.smsCopy == true)
                     {
                         string TeacherMNo = unitOfWork.teacherRegistrationService.GetByID(obj.OrderedBy).MobileNo;

                         mMobileNo = mMobileNo + "," + TeacherMNo;

                     }


                     if (obj.SMSLanguage == 2)
                     {
                         string NewMessage = obj.PrefixEnglish + " " + obj.smsTextEnglish;

                         if (obj.smsMobileNo != string.Empty)
                         {
                             ErrorMsg = SendMesssageText(NewMessage.Trim(), mMobileNo, byte.Parse(Session["CompID"].ToString()), apitemplateid); // new for online attendance 14jan2022
                             
                             //ErrorMsg = unitOfWork.sendMessegeService.SendMesssageText((NewMessage.Trim()), mMobileNo, byte.Parse(Session["CompID"].ToString())); // new code
                         }
                     }
                     else
                     {
                         string NewMessage = obj.PrefixHindi + " " + obj.smsTextHindi;

                         if (obj.smsMobileNo != string.Empty)
                         {
                             ErrorMsg = SendMesssageTextHindi(NewMessage.Trim(), mMobileNo, byte.Parse(Session["CompID"].ToString()), apitemplateid); // new for online attendance 14jan2022);
                            // ErrorMsg=SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), mMobileNo);
                             //ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), mMobileNo, byte.Parse(Session["CompID"].ToString()));
                             //ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(NewMessage.Trim(), mMobileNo, byte.Parse(Session["CompID"].ToString())); // new code 2021
                         }
                     }

                     #endregion
                     if(SettingMasterStaticClass._SaveSMSLog == true)
                     {
                         #region Save SMS Log in Database
                         string Message = string.Empty;
                         if (obj.SMSLanguage == 2)
                         {
                             Message = obj.PrefixEnglish + " " + obj.smsTextEnglish;
                         }
                         else
                         {
                             Message = obj.PrefixHindi + " " + obj.smsTextHindi;
                         }

                         newSMSLogMasterID = SaveSMSLogMaster("OnlineAbsenteesSMS", int.Parse(Session["SessionID"].ToString()), Message, obj.smsMobileNo, obj.OrderedBy, int.Parse(Session["SessionID"].ToString()));
                         if (newSMSLogMasterID > 0)
                         {
                             Result = true;
                         }
                         Result = Result && SaveSMSLogDetail(obj, newSMSLogMasterID);


                         if (Result)
                             _mTran.Commit();
                         else
                             _mTran.Rollback();

                         #endregion
                     }
                 }

             }
             else
             {
                 ErrorMsg = "ModelState is not Valid.";
             }

             return new JsonResult()
             {
                 JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                 Data = new
                 {
                     Resultmsg = ErrorMsg,
                     ResultData = cCommon.RenderRazorViewToString("SendSMSOnlineAbsenteesEditTextView", obj, ControllerContext, ViewData, TempData)
                 }
             };

         }

         public string ConvertUnicodeStringToHexString(string unicodeString)
         {
             string hex = "";
             if (unicodeString.Trim() != string.Empty && unicodeString.Trim() != "" && unicodeString.Trim() != null)
             {
                 foreach (char c in unicodeString)
                 {
                     int tmp = c;
                     hex += String.Format("{0:x4}", (uint)System.Convert.ToUInt32(tmp.ToString()));
                 }
             }
             return hex;
         }

        

         public ActionResult GetTemplateMesssageText(int mTemplateID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             modelSendSms objsms = new modelSendSms();
             SMSTemplate objTemp = new SMSTemplate();
             //objTemp = unitOfWork.smsTemplateService.GetByID(mTemplateID);
             IEnumerable<SMSTemplate> listsmstmp = unitOfWork.smsTemplateService.GetSMSTemplateforSMSType(1, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
             if (listsmstmp != null)
             {
                 listsmstmp = listsmstmp.OrderByDescending(x => x.TemplateID).ToList();
                 foreach (SMSTemplate objsmstmp  in listsmstmp)
                 {
                     objTemp = objsmstmp;
                     break;
                 }
             }
             
             objsms.PrefixEnglish = objTemp.PreFix;
             objsms.smsTextEnglish = objTemp.TemplateMessage;
             objsms.PrefixHindi = objTemp.PreFixH;
             objsms.smsTextHindi = objTemp.TemplateMessageH;
             if (SettingMasterStaticClass._IncludeNameInSMS == true)
             {
                 objsms.includeName = true;
             }
             else
             {
                 objsms.includeName = false;
             }

             return PartialView("SendSMSOnlineAbsenteesTextView", objsms);

             // return unitOfWork.smsTemplateService.GetByID(mTemplateID).TemplateMessage;
         }

         public ActionResult PartialClassSetupView()
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             return PartialView("ListClassSetupPartial", unitOfWork.classSetupService.GetClassSetupList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }

        public ActionResult ListPartialClassSetup()
        {
            return PartialView("ListClassGridLookupPartial", unitOfWork.classSetupService.GetClassSetupList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
        public string SendMesssageText(string mMsg, string mMob, byte mCompID, string mapitemplateid)
         {
             string ReturnMsg = string.Empty;

             if (mMob != null)
             {
                 if (mMsg.Trim() != string.Empty && mMsg.Trim() != "" && mMsg.Trim() != null && mMob.Trim() != string.Empty && mMob.Trim() != "" && mMob.Trim() != null)
                 {

                     string[] LstMob = mMob.Split(',');
                     string lst = string.Empty;
                     if (LstMob.Length > 100)
                     {
                         int m = 0, n = 99;


                         for (int i = 0; i <= LstMob.Length / 100; i++)
                         {
                             lst = string.Empty;
                             for (int j = m; j <= n; j++)
                             {
                                 lst += LstMob[j] + ",";

                             }

                             lst = lst.TrimEnd(',');
                             m += 100;
                             if (n + 100 > LstMob.Length)
                                 n = LstMob.Length - 1;
                             else
                                 n += 100;

                             string mApi = string.Empty;

                             switch (byte.Parse(mCompID.ToString()))
                             {
                                 //------- 1. Nirmala Convent--------------new api 16/04/2021
                                 case 1:
                                     //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=nirmalconvent&pwd=school@eee&to=" + lst + "&sid=NcgiCJ&msg=" + mMsg + "&fl=0&gwid=2";
                                     mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=4d4e5bf2-a6e3-4dc8-8e04-1cb84287fc20&senderid=NcgiCJ&channel=2&DCS=0&flashsms=0&number=" + lst + "&text=" + mMsg + "&route=31";
                                     break;
                                 //----------------ST Mark-----------------
                                 case 2:
                                     //    mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + lst + "&sid=STmark&msg=" + mMsg + "&fl=0&gwid=2";
                                     mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=367801e0-6818-4746-a728-477e4c1b738a&senderid=SMCJHS&channel=2&DCS=0&flashsms=0&number=" + lst + "&text=" + mMsg + "&dlttemplateid=" + mapitemplateid + "&route=31";
                                     break;
                                 //--------------KVJ------------------
                                 case 3:
                                     mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=KVJIII&pwd=school@eee&to=" + lst + "&sid=KVJIII&msg=" + mMsg + "&fl=0&gwid=2";
                                     break;
                                 //--------------St Xavier------------------
                                 case 4:
                                     //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS&to=" + lst + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2";
                                     mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=DXepx9rFKkWNDyr00r66VQ&senderid=SXCJHS&channel=2&DCS=0&flashsms=0&number=" + lst + "&text=" + mMsg + "&route=31";
                                     break;

                                 case 5:
                                     //-- St Joseph--//
                                     mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=8P2PEDg6vkaTSA1mPQSQNA&senderid=JOSEPH&channel=2&DCS=0&flashsms=0&number= " + lst + "&text=" + mMsg + "&route=31";
                                     break;
                                 case 6:
                                     //-- St lawrence--//
                                     mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=FLZm2OsB2UWlil9w4bYpQQ&senderid=SLSMEH&channel=2&DCS=0&flashsms=0&number= " + lst + "&text=" + mMsg + "&route=31";
                                     break;
                                 case 7:
                                     //-- Shanti Dham-//
                                     mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=BTOYuKN8cUiCZm78x7ixzw&senderid=SHANTI&channel=2&DCS=0&flashsms=0&number= " + lst + "&text=" + mMsg + "&route=31";
                                     break;
                             }



                             HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                             HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                             System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                             string responseString = respStreamReader.ReadToEnd().ToString();

                             //ReturnMsg = responseString;

                             ReturnMsg = "Message sent successfully by portal. API Potal Response : " + responseString;
                             respStreamReader.Close();
                             myResp.Close();
                         }
                     }
                     else
                     {
                         string mApi = string.Empty;
                         switch (byte.Parse(mCompID.ToString()))
                         {
                             //------- 1. Nirmala Convent--------------
                             case 1:
                                 //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=nirmalconvent&pwd=school@eee&to=" + mMob + "&sid=NcgiCJ&msg=" + mMsg + "&fl=0&gwid=2";
                                 mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=4d4e5bf2-a6e3-4dc8-8e04-1cb84287fc20&senderid=NcgiCJ&channel=2&DCS=0&flashsms=0&number=91" + mMob + "&text=" + mMsg + "&route=31";
                                 //mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=366191e0-6818-4746-a728-477e4c1b738a&senderid=STmark&channel=2&DCS=0&flashsms=0&number=91" + mMob + "&text=" + mMsg + "&route=31";

                                 break;
                             case 2:
                                 // mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + mMob + "&sid=STmark&msg=" + mMsg + "&fl=0&gwid=2";
                                 mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=367801e0-6818-4746-a728-477e4c1b738a&senderid=SMCJHS&channel=2&DCS=0&flashsms=0&number=91" + mMob + "&text=" + mMsg + "&dlttemplateid=" + mapitemplateid + "&route=31";
                                 break;
                             case 3:
                                 mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=KVJIII&pwd=school@eee&to=" + mMob + "&sid=KVJIII&msg=" + mMsg + "&fl=0&gwid=2";
                                 break;
                             //--------------St Xavier------------------
                             case 4:
                                 //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS&to=" + mMob + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2";
                                 mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=DXepx9rFKkWNDyr00r66VQ&senderid=SXCJHS&channel=2&DCS=0&flashsms=0&number=91" + mMob + "&text=" + mMsg + "&route=31";
                                 break;
                             case 5:
                                 //-- St Joseph--//
                                 mApi = "http://login.smsgatewayhub.com/api/mt/SendSMS?APIKey=8P2PEDg6vkaTSA1mPQSQNA&senderid=JOSEPH&channel=2&DCS=0&flashsms=0&number=" + mMob + "&text=" + mMsg + "&route=13 ";
                                 //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=joseph1&pwd=497993&to=" + mMob + "&sid=JOSEPH&msg=" + mMsg + "&fl=0&gwid=2";
                                 break;
                             case 6:
                                 //-- St lawrence--//
                                 mApi = "http://login.smsgatewayhub.com/api/mt/SendSMS?APIKey=FLZm2OsB2UWlil9w4bYpQQ&senderid=SLSMEH&channel=2&DCS=0&flashsms=0&number=" + mMob + "&text=" + mMsg + "&route=13 ";

                                 break;
                             case 7:
                                 //-- Shanti Dham-//
                                 mApi = "http://login.smsgatewayhub.com/api/mt/SendSMS?APIKey=BTOYuKN8cUiCZm78x7ixzw&senderid=SHANTI&channel=2&DCS=0&flashsms=0&number=" + mMob + "&text=" + mMsg + "&route=13 ";
                                 break;
                         }


                         HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                         HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                         System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                         string responseString = respStreamReader.ReadToEnd().ToString();


                         //ReturnMsg = responseString;
                         ReturnMsg = "Message sent successfully by portal. API Potal Response : " + responseString;

                         respStreamReader.Close();
                         myResp.Close();

                     }

                 }
                 else
                 {
                     ReturnMsg += " Message field is blank.";
                 }
             }

             return ReturnMsg;
         }

         public string SendMesssageTextHindi(string mMsg, string mMob, byte mCompID, string mapitemplateid)
         {
             string ReturnMsg = string.Empty;
             if (mMob != null)
             {
                 if (mMsg.Trim() != string.Empty && mMsg.Trim() != "" && mMsg.Trim() != null && mMob.Trim() != string.Empty && mMob.Trim() != "" && mMob.Trim() != null)
                 {

                     string[] LstMob = mMob.Split(',');
                     string lst = string.Empty;
                     if (LstMob.Length > 100)
                     {
                         int m = 0, n = 99;


                         for (int i = 0; i <= LstMob.Length / 100; i++)
                         {
                             lst = string.Empty;
                             for (int j = m; j <= n; j++)
                             {
                                 lst += LstMob[j] + ",";
                             }

                             lst = lst.TrimEnd(',');
                             m += 100;
                             if (n + 100 > LstMob.Length)
                                 n = LstMob.Length - 1;
                             else
                                 n += 100;

                             string mApi = string.Empty;
                             switch (byte.Parse(mCompID.ToString()))
                             {
                                 //------- 1. Nirmala Convent--------------
                                 case 1:
                                     //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=nirmalconvent&pwd=school@eee&to=" + lst + "&sid=NcgiCJ&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                     mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=4d4e5bf2-a6e3-4dc8-8e04-1cb84287fc20&senderid=NcgiCJ&channel=2&DCS=8&flashsms=0&number=" + lst + "&text=" + mMsg + "&route=1";
                                     break;
                                 case 2:
                                     // mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + lst + "&sid=STmark&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                     mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=367801e0-6818-4746-a728-477e4c1b738a&senderid=SMCJHS&channel=2&DCS=8&flashsms=0&number=" + lst + "&text=" + mMsg + "&dlttemplateid=" + mapitemplateid + "&route=1";
                                     break;
                                 case 3:
                                     mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=KVJIII&pwd=school@eee&to=" + lst + "&sid=KVJIII&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                     break;
                                 //--------------St Xavier------------------
                                 case 4:
                                     //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS&to=" + lst + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                     mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=DXepx9rFKkWNDyr00r66VQ&senderid=SXCJHS&channel=2&DCS=8&flashsms=0&number=" + lst + "&text=" + mMsg + "&route=1";
                                     break;

                                 case 5:
                                     //-- St Joseph--////Id-joseph1////pwd-497993
                                     mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=joseph1&pwd=497993&to=" + lst + "&sid=JOSEPH&msg=" + mMsg + "&fl=0&gwid=2&dc=8";

                                     break;
                                 case 6:
                                     //-- St Lawrence--
                                     mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=slsmeh&pwd=slsmeh@123&to=" + lst + "&sid=SLSMEH&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                     break;
                                 case 7:
                                     //-- Shanti Dham--

                                     mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Shantidham&pwd=530050&to=" + lst + "&sid=SHANTI&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                     break;
                             }

                             HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                             HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                             System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                             string responseString = respStreamReader.ReadToEnd().ToString();

                             // ReturnMsg = responseString;
                             ReturnMsg = "Message sent successfully by portal. API Potal Response : " + responseString;
                             respStreamReader.Close();
                             myResp.Close();
                         }

                     }
                     else
                     {
                         string mApi = string.Empty;
                         switch (byte.Parse(mCompID.ToString()))
                         {
                             //------- 1. Nirmala Convent--------------
                             case 1:
                                 //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=nirmalconvent&pwd=school@eee&to=" + mMob + "&sid=NcgiCJ&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                 mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=4d4e5bf2-a6e3-4dc8-8e04-1cb84287fc20&senderid=NcgiCJ&channel=2&DCS=8&flashsms=0&number=91" + mMob + "&text=" + mMsg + "&route=1";
                                 break;
                             case 2:
                                 //------- 2 StMark--------------
                                 //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + mMob + "&sid=STmark&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                 mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=367801e0-6818-4746-a728-477e4c1b738a&senderid=SMCJHS&channel=2&DCS=8&flashsms=0&number=91" + mMob + "&text=" + mMsg + "&dlttemplateid=" + mapitemplateid + "&route=1";
                                 break;
                             case 3:
                                 //------- 3 KVJII--------------
                                 mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=KVJIII&pwd=school@eee&to=" + mMob + "&sid=KVJIII&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                 break;
                             //--------------St Xavier------------------
                             case 4:
                                 //mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS&to=" + mMob + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                 mApi = "http://www.smsgatewayhub.com/api/mt/SendSMS?APIKey=DXepx9rFKkWNDyr00r66VQ&senderid=SXCJHS&channel=2&DCS=8&flashsms=0&number=91" + mMob + "&text=" + mMsg + "&route=1";
                                 break;
                             //--------------St Joseph--////Id-joseph1////pwd-497993
                             case 5:
                                 mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=joseph1&pwd=497993&to=" + mMob + "&sid=JOSEPH&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                 break;
                             case 7:
                                 mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Shantidham&pwd=530050&to=" + mMob + "&sid=SHANTI&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                 break;

                         }

                         HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                         HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                         System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                         string responseString = respStreamReader.ReadToEnd().ToString();
                         //ReturnMsg = responseString;
                         ReturnMsg = "Message sent successfully by portal. API Potal Response : " + responseString;
                         respStreamReader.Close();
                         myResp.Close();

                     }
                 }
                 else
                 {
                     ReturnMsg += " Message field is blank.";
                 }
             }


             return ReturnMsg;
         }


    }



}



    