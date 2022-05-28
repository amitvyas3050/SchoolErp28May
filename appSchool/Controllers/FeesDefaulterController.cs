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
using System.Collections;
namespace appSchool.Controllers
{
    [NoCache]
    public class FeesDefaulterController : Controller
    {
         private UnitOfWork unitOfWork = new UnitOfWork();
         private SqlConnection _mConn;
         private SqlTransaction _mTran;

         //public override string Name { get { return "Editing"; } }


         public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appFeesDefaulterSMS==0)
            {
                return Redirect("~/");
            } 
             
            // UserPermission objuser = new UserPermission();
            // objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 57, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
          //  ViewData["AttendanceDate"] = DateTime.Now;
            return View();
        }

         public ActionResult PartialSendSMSAbsenteesView()
        {
            List<vStudentFeesReminder> objFeesReminder = new List<vStudentFeesReminder>();
            objFeesReminder = unitOfWork.VstudentFeesReminderService.GetFeesReminderBySessionWise(int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

            return PartialView("ListSendSMSDefaulterView", objFeesReminder);
        }
         public ActionResult PartialSendSMSAbsenteesTemplateView()
         {
             return PartialView("SendSMSDefaulterTemplateView", unitOfWork.smsTemplateService.GetSMSTemplateforSMSType(7, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }
         public ActionResult SMSTemplateGridRowChange(int ID)
         {
             return PartialView("SendSMSDefaulterTextView", ID);
         }

         public ActionResult GetAllAbsenteesforGrid()
         {
             List<vStudentFeesReminder> objFeesReminder = new List<vStudentFeesReminder>();
             objFeesReminder = unitOfWork.VstudentFeesReminderService.GetFeesReminderBySessionWise(int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
             //ViewData["FeesReminderDate"] = mSessionID;

             return PartialView("ListSendSMSDefaulterView", objFeesReminder);
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

             if (SMSLogMasterID != string.Empty && SMSLogMasterID != "" && SMSLogMasterID!=null)
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
         public string[] RemoveDuplicates(string[] myList)
         {
             ArrayList newList = new ArrayList();

             foreach (string str in myList)
                 if (!newList.Contains(str))
                     newList.Add(str);
             return (string[])newList.ToArray(typeof(string));
         }

         [HttpPost]
         public JsonResult SMSButtonClick(modelSendSms obj)
         {
             obj.includeName = true;
             bool Result = false;
             string ErrorMsg = string.Empty;
             SettingMasterStaticClass._SaveSMSLog = true;
             bool ResultSMSDetail = false;
             int newSMSLogMasterID = 0;

             _mConn = DB.GetActiveConnection();
             _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

             if (ModelState.IsValid)
             {
                 if (SettingMasterStaticClass._SaveSMSLog == true)
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

                     newSMSLogMasterID = SaveSMSLogMaster("FeesDefaulterSMS", int.Parse(Session["SessionID"].ToString()), Message, obj.smsMobileNo, obj.OrderedBy, int.Parse(Session["SessionID"].ToString()));
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
                 #region SMS Sent
                 if (obj.smsMobileNo == null || obj.smsMobileNo == string.Empty)
                 {
                     ErrorMsg += "Please Select Mobile No.";
                     return new JsonResult()
                     {
                         JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                         Data = new
                         {
                             Resultmsg = ErrorMsg,
                             ResultData = cCommon.RenderRazorViewToString("SendSMSDefaulterEditTextView", obj, ControllerContext, ViewData, TempData)
                         }
                     };
                 }
                 string mStudnetID = obj.smsMobileNo;

                 string[] bits = mStudnetID.Split(',');
                 string[] StudIDs = RemoveDuplicates(bits);

                 string smsTextEnglish = obj.smsTextEnglish;
                 string smsTextHindi = obj.smsTextHindi;
                 string PrefixEnglish = obj.PrefixEnglish;
                 string PrefixHindi = obj.PrefixHindi;

                 int duplicateStudent = 0;
                 foreach (string bit in StudIDs)
                 {
                     duplicateStudent = int.Parse(bit);
                     int StudentIDforSearch = int.Parse(bit);

                     string NewMessage = string.Empty;
                     string TermsMsg = string.Empty;
                     double FineAmount = 0.00;
                     double TotalFees = 0.00; int TermID = 0;
                     vTotalFeesReminder objStudReg = new vTotalFeesReminder();

                     List<vTotalFeesReminder> objStudSMS = new List<vTotalFeesReminder>();
                     objStudSMS = unitOfWork.VstudentFeesReminderService.GetTotalFeesReminderStudentwise(int.Parse(Session["SessionID"].ToString()), StudentIDforSearch, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                     
                     foreach (vTotalFeesReminder objnew in objStudSMS)
                     {
                         objStudReg = objnew;
                         TermsMsg += objnew.FeeTermType + " Rs. " + objnew.FeesAmount + ",";
                         if (objnew.Fine != null)
                             FineAmount += double.Parse(objnew.Fine.ToString());
                         else
                             FineAmount = 0;
                         TotalFees += double.Parse(objnew.FeesAmount.ToString());

                         TermID++;

                     }
                     //9425910166

                     if (int.Parse(Session["CompID"].ToString()) == 5 || int.Parse(Session["CompID"].ToString()) == 6)
                     {
                         if (TermID == 2) FineAmount = FineAmount + 50;

                         if (TermID == 3) FineAmount = FineAmount + 150;

                         if (TermID == 4) FineAmount = FineAmount + 300;

                         TermID = 0;

                     }
                     

                     //Session["CompID"].ToString();
                     TermsMsg = TermsMsg.TrimEnd(',');
                     if (objStudReg.SMSInHindi == false)
                     {
                         if (obj.smsTextEnglish != string.Empty && obj.smsTextEnglish != "" && obj.smsTextEnglish != null)
                         {
                             obj.smsTextEnglish = smsTextEnglish;
                             obj.smsTextEnglish = obj.smsTextEnglish.Replace("$Name$", objStudReg.FullName);
                             obj.smsTextEnglish = obj.smsTextEnglish.Replace("$TotalFee$",TotalFees.ToString());
                             obj.smsTextEnglish = obj.smsTextEnglish.Replace("$Fine$", FineAmount.ToString());
                             obj.smsTextEnglish = obj.smsTextEnglish.Replace("$Class$", objStudReg.ClassDescription);
                             obj.smsTextEnglish = obj.smsTextEnglish.Replace("$Term$", TermsMsg);

                         }
                         if (obj.PrefixEnglish != string.Empty && obj.PrefixEnglish != "" && obj.PrefixEnglish != null)
                         {
                             obj.PrefixEnglish = PrefixEnglish;
                             obj.PrefixEnglish = obj.PrefixEnglish.Replace("$Name$", objStudReg.FullName);
                             obj.PrefixEnglish = obj.PrefixEnglish.Replace("$TotalFee$", TotalFees.ToString());
                             obj.PrefixEnglish = obj.PrefixEnglish.Replace("$Fine$", FineAmount.ToString());
                             obj.PrefixEnglish = obj.PrefixEnglish.Replace("$Class$", objStudReg.ClassDescription);
                             obj.PrefixEnglish = obj.PrefixEnglish.Replace("$Term$", TermsMsg);
                         }

                         NewMessage = obj.PrefixEnglish + " " + obj.smsTextEnglish;

                         if (objStudReg.SMSMobileNo != string.Empty)
                         {
                             ErrorMsg = unitOfWork.sendMessegeService.SendMesssageText(NewMessage.Trim(), objStudReg.SMSMobileNo, byte.Parse(Session["CompID"].ToString()));
                             if (newSMSLogMasterID > 0 && SettingMasterStaticClass._SaveSMSLog==true)
                             {
                                 ResultSMSDetail = SaveSMSLogDetailStudentWise(newSMSLogMasterID, StudentIDforSearch, objStudReg.SMSMobileNo, NewMessage);

                             }
                         }
                     }
                     else
                     {
                         if (obj.smsTextHindi != string.Empty && obj.smsTextHindi != "" && obj.smsTextHindi != null)
                         {
                             obj.smsTextHindi = smsTextHindi;
                             obj.smsTextHindi = obj.smsTextHindi.Replace("$Name$", objStudReg.FullName);
                             obj.smsTextHindi = obj.smsTextHindi.Replace("$TotalFee$", TotalFees.ToString());
                             obj.smsTextHindi = obj.smsTextHindi.Replace("$Fine$", FineAmount.ToString());
                             obj.smsTextHindi = obj.smsTextHindi.Replace("$Class$", objStudReg.ClassDescription);
                             obj.smsTextHindi = obj.smsTextHindi.Replace("$Term$", TermsMsg);
                         }

                         if (obj.PrefixHindi != string.Empty && obj.PrefixHindi != "" && obj.PrefixHindi != null)
                         {
                             obj.PrefixHindi = PrefixHindi;
                             obj.PrefixHindi = obj.PrefixHindi.Replace("$Name$", objStudReg.FullName);
                             obj.PrefixHindi = obj.PrefixHindi.Replace("$TotalFee$", TotalFees.ToString());
                             obj.PrefixHindi = obj.PrefixHindi.Replace("$Fine$", FineAmount.ToString());
                             obj.PrefixHindi = obj.PrefixHindi.Replace("$Class$", objStudReg.ClassDescription);
                             obj.PrefixHindi = obj.PrefixHindi.Replace("$Term$", TermsMsg);
                         }
                         NewMessage = obj.PrefixHindi + "  " + obj.smsTextHindi;

                         if (objStudReg.SMSMobileNo != string.Empty)
                         {
                             //ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), objStudReg.SMSMobileNo, byte.Parse(Session["CompID"].ToString()));
                             ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(NewMessage.Trim(), objStudReg.SMSMobileNo, byte.Parse(Session["CompID"].ToString()));
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
                 ErrorMsg += "ModelState is Not Valid.";
             }

             return new JsonResult()
             {
                 JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                 Data = new
                 {
                     Resultmsg = ErrorMsg,
                     ResultData = cCommon.RenderRazorViewToString("SendSMSDefaulterEditTextView", obj, ControllerContext, ViewData, TempData)
                 }
             };
         }

         public string ConvertUnicodeStringToHexString(string unicodeString)
         {
             string hex = "";
             foreach (char c in unicodeString)
             {
                 int tmp = c;
                 hex += String.Format("{0:x4}", (uint)System.Convert.ToUInt32(tmp.ToString()));
             }
             return hex;
         }

         public ActionResult GetTemplateMesssageText(int mTemplateID)
         {
             modelSendSms objsms = new modelSendSms();
             SMSTemplate objTemp = new SMSTemplate();
             objTemp = unitOfWork.smsTemplateService.GetByID(mTemplateID);
             if (objTemp != null)
             {
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
             }
             return PartialView("SendSMSDefaulterTextView", objsms);

         }

         public string  SendMesssageTextold(string  mMsg,string mMob)
         {
             string ReturnMsg = string.Empty;

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
                     m += 100;
                     if (n + 100 > LstMob.Length)
                         n = LstMob.Length;
                     else
                         n += 100;

                     string mApi = string.Empty;
                     switch (int.Parse(Session["CompID"].ToString()))
                     {
                         //------- 1. Nirmala Convent--------------
                         case 1:
                             mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=nirmalconvent&pwd=school@eee&to=" + lst + "&sid=NcgiCJ&msg=" + mMsg + "&fl=0&gwid=2";
                             break;
                         case 2:
                             mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + lst + "&sid=STmark&msg=" + mMsg + "&fl=0&gwid=2";
                             break;
                         case 3:
                             mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=KVJIII&pwd=school@eee&to=" + lst + "&sid=KVJIII&msg=" + mMsg + "&fl=0&gwid=2";
                             break;
                         case 4:
                             mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS&to=" + mMob + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2";
                             break;
                             
                     }
                      

                     HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                     HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                     System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                     string responseString = respStreamReader.ReadToEnd();
                     ReturnMsg = responseString;
                     ReturnMsg = "Message sent successfully ";
                     respStreamReader.Close();
                     myResp.Close();
                 }
             }
             else
             {

                 string mApi = string.Empty;
                 switch (int.Parse(Session["CompID"].ToString()))
                 {
                     //------- 1. Nirmala Convent--------------
                     case 1:
                         mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=nirmalconvent&pwd=school@eee&to=" + mMob + "&sid=NcgiCJ&msg=" + mMsg + "&fl=0&gwid=2";
                         break;
                     case 2:
                         mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + mMob + "&sid=STmark&msg=" + mMsg + "&fl=0&gwid=2";
                         break;
                     case 3:
                         mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=KVJIII&pwd=school@eee&to=" + mMob + "&sid=KVJIII&msg=" + mMsg + "&fl=0&gwid=2";
                         break;
                     case 4:
                         mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS&to=" + mMob + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2";
                         break;

                 }

                  

                 HttpWebRequest myReq =
              (HttpWebRequest)WebRequest.Create(mApi);
                 HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                 System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                 string responseString = respStreamReader.ReadToEnd();
                 ReturnMsg = responseString;
                 ReturnMsg = "Message sent successfully ";
                 respStreamReader.Close();
                 myResp.Close();
             }
             return ReturnMsg;
         }
        
         public string SendMesssageTextHindiold(string mMsg, string mMob)
         {
             string ReturnMsg = string.Empty;
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
                     m += 100;
                     if (n + 100 > LstMob.Length)
                         n = LstMob.Length;
                     else
                         n += 100;

                     string mApi = string.Empty;
                     switch(int.Parse(Session["CompID"].ToString()))
                     {
                         //------- 1. Nirmala Convent--------------
                         case 1:
                             mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=nirmalconvent&pwd=school@eee&to=" + lst + "&sid=NcgiCJ&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                             break;
                         case 2:
                             mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + lst + "&sid=STmark&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                             break;
                         case 3:
                             mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=KVJIII&pwd=school@eee&to=" + lst + "&sid=KVJIII&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                             break;
                         //--------------St Xavier------------------
                         case 4:
                             mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS​&to=" + lst + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                             break;

                     }
                      

                     HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                     HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                     System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                     string responseString = respStreamReader.ReadToEnd();
                     ReturnMsg = "Message sent successfully ";
                     respStreamReader.Close();
                     myResp.Close();
                 }

             }
             else
             {
                 string mApi = string.Empty;
                 switch (int.Parse(Session["CompID"].ToString()))
                 {
                     //------- 1. Nirmala Convent--------------
                     case 1:
                         mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=nirmalconvent&pwd=school@eee&to=" + mMob + "&sid=NcgiCJ&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                         break;
                     case 2:
                         mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + mMob + "&sid=STmark&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                         break;
                     case 3:
                         mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=KVJIII&pwd=school@eee&to=" + mMob + "&sid=KVJIII&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                         break;
                     //--------------St Xavier------------------
                     case 4:
                         mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS​&to=" + lst + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                         break;
                 }
                  

                 HttpWebRequest myReq =
              (HttpWebRequest)WebRequest.Create(mApi);
                 HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                 System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                 string responseString = respStreamReader.ReadToEnd();
                 ReturnMsg = "Message sent successfully ";
                 respStreamReader.Close();
                 myResp.Close();
             }

             return ReturnMsg;

         }


    }



}



    