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
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
namespace appSchool.Controllers
{
    [NoCache]
    public class SendGeneralMessegeController : Controller
    {
         private UnitOfWork unitOfWork = new UnitOfWork();
         private SqlConnection _mConn;
         private SqlTransaction _mTran;

        
        
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }

            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 31, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

            modelSendSms objsmsmodel = unitOfWork.smsTemplateService.GetSendSMSModele();
            return View(objsmsmodel);
        }

        public static List<List<string>> Split(List<string> source)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 100)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }


        public ActionResult SendGeneralMessegeView()
        {
            if (Session["UserID"] == null ||(int)SubMenuModules.appSendGeneralMessage==0)
            {
                return Redirect("~/");
            }

            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 31, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            if (objuser != null)
            {
                PermissionFlag._AddFlag = objuser.AddP;
                PermissionFlag._ModFlag = objuser.ModP;
                PermissionFlag._DelFlag = objuser.DelP;
            }
            else
            {
                PermissionFlag._AddFlag = false;
                PermissionFlag._ModFlag = false;
                PermissionFlag._DelFlag = false;
            }
            return PartialView("SendGeneralMessegeView");
        }
         public ActionResult PartialSendGeneralMessegeView(string mClassSetup)
         {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (mClassSetup == "" && mClassSetup == string.Empty && mClassSetup == null)
            {
                mClassSetup = "-1";
            }

            return PartialView("ListSendGeneralMessegeView", unitOfWork.sendMessegeService.GetStudentSessionByClassSetupIDs(mClassSetup, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }
         public ActionResult PartialSendGeneralMessegeTemplateView()
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             return PartialView("SendGeneralMessegeTemplateView", unitOfWork.smsTemplateService.GetSMSTemplateListForGeneral(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }
         public ActionResult SMSTemplateGridRowChange(int ID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             return PartialView("SendGeneralMessegeTextView", ID);
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

         [HttpPost]
         public JsonResult SMSButtonClick(modelSendSms obj)
         {
             //if (Session["UserID"] == null) { return Redirect("~/"); }
             bool Result = false;
             string Errormsg = string.Empty;
             SettingMasterStaticClass._SaveSMSLog = true;

             _mConn = DB.GetActiveConnection();
             _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);


             #region ONETIME SMS

             if (ModelState.IsValid)
             {
                 if (obj.smsMobileNo == null )
                 {
                     Errormsg +="Please Select Mobile No.";
                     modelSendSms objsmsMobileNo = new modelSendSms();
                     objsmsMobileNo.includeName = true;
                     return new JsonResult()
                     {
                         JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                         Data = new
                         {
                             Resultmsg = Errormsg,
                             ResultData = RenderRazorViewToString("SendGeneralMessegeEditTextView", objsmsMobileNo, ControllerContext, ViewData, TempData)
                         }
                     };
                    
                 }


                 string mMobileNo = obj.smsMobileNo;

                 if (obj.smsCopy == true)
                 {
                     string TeacherMNo = unitOfWork.teacherRegistrationService.GetByID(obj.OrderedBy).MobileNo;
                     mMobileNo = mMobileNo + "," + TeacherMNo;
                 }
                 if (obj.smsAdminCopy == true) // smsAdminCopy Code Changes --- 28/may/2022
                {
                    SchoolMaster objschool = unitOfWork.schoolMasterService.GetSchoolSetup();
                    if(objschool.AdminSMSCopyNo != null)
                    {
                        if (objschool.AdminSMSCopyNo.Length > 9)
                            mMobileNo = mMobileNo + "," + objschool.AdminSMSCopyNo;
                    }

                } // smsAdminCopy Code Changes --- 28/may/2022

                if (obj.SMSLanguage == 2)
                 {
                     string NewMessage = obj.PrefixEnglish + " " + obj.smsTextEnglish;
                     if (obj.smsMobileNo != string.Empty)
                     {
                         //Errormsg = SendMesssageText((NewMessage.Trim()), mMobileNo);

                         Errormsg = unitOfWork.sendMessegeService.SendMesssageText((NewMessage.Trim()), mMobileNo, byte.Parse(Session["CompID"].ToString()));

                     }
                 }
                 else
                 {
                     string NewMessage = obj.PrefixHindi + " " + obj.smsTextHindi;
                     if (obj.smsMobileNo != string.Empty)
                     {
                         //Errormsg = SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), mMobileNo);

                         //Errormsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), mMobileNo, byte.Parse(Session["CompID"].ToString()));
                         Errormsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(NewMessage.Trim(), mMobileNo, byte.Parse(Session["CompID"].ToString()));
                     }
                 }

                 if (SettingMasterStaticClass._SaveSMSLog == true)
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

                     int newSMSLogMasterID = SaveSMSLogMaster("GeneralSMS", int.Parse(Session["SessionID"].ToString()), Message, obj.smsMobileNo, obj.OrderedBy, int.Parse(Session["SessionID"].ToString()));
                     if (newSMSLogMasterID > 0)
                     {
                         Result = true;
                     }
                     if (obj.smsStudentID == null)
                     {
                         obj.smsStudentID = string.Empty;
                     }
                     if (obj.smsStudentID.Trim() != null && obj.smsStudentID.Trim() != "" && obj.smsStudentID.Trim() != string.Empty)
                     {
                         Result = Result && SaveSMSLogDetail(obj, newSMSLogMasterID);
                     }

                     if (Result)
                     {
                         _mTran.Commit();
                         Errormsg += " SMSlog Inserted Successfully.";
                     }
                     else
                     {
                         _mTran.Rollback();
                         Errormsg += " SMSlog is not Inserted.there is some error.";
                     }
                     #endregion
                 }
             }
             else
             {
                 Errormsg += "ModelState is not Valid.";
             }
             #endregion

             modelSendSms objsms = new modelSendSms();
             objsms.includeName = true;


             return new JsonResult()
             {
                 JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                 Data = new{
                     Resultmsg = Errormsg,
                     ResultData = RenderRazorViewToString("SendGeneralMessegeEditTextView", objsms, ControllerContext, ViewData, TempData) 
                 }
             };
             
         }


         public static string RenderRazorViewToString(string viewName, object model, ControllerContext controllerContext, ViewDataDictionary viewData, TempDataDictionary tempData)
         {
             viewData.Model = model;
             using (var sw = new StringWriter())
             {
                 var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                 var viewContext = new ViewContext(controllerContext, viewResult.View, viewData, tempData, sw);
                 viewResult.View.Render(viewContext, sw);
                 viewResult.ViewEngine.ReleaseView(controllerContext, viewResult.View);
                 return sw.GetStringBuilder().ToString();
             }
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
         //public string GetTemplateMesssageText(int mTemplateID)
         //{




         //    return unitOfWork.smsTemplateService.GetByID(mTemplateID).TemplateMessage;
         //}

         public ActionResult GetTemplateMesssageText(int mTemplateID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             modelSendSms objsms = new modelSendSms();
             SMSTemplate objTemp = new SMSTemplate();
             objTemp = unitOfWork.smsTemplateService.GetByID(mTemplateID);
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
             return PartialView("SendGeneralMessegeTextView", objsms);

            // return unitOfWork.smsTemplateService.GetByID(mTemplateID).TemplateMessage;
         }



         public string  SendMesssageTextold(string  mMsg,string mMob)
         {
             string ReturnMsg = string.Empty;

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

                         switch (int.Parse(Session["CompID"].ToString()))
                         {
                             //------- 1. Nirmala Convent--------------
                             case 1:
                                 mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=nirmalconvent&pwd=school@eee&to=" + lst + "&sid=NcgiCJ&msg=" + mMsg + "&fl=0&gwid=2";
                                 break;
                             //----------------ST Mark-----------------
                             case 2:
                                 mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + lst + "&sid=STmark&msg=" + mMsg + "&fl=0&gwid=2";
                                 break;
                             //--------------KVJ------------------
                             case 3:
                                 mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=KVJIII&pwd=school@eee&to=" + lst + "&sid=KVJIII&msg=" + mMsg + "&fl=0&gwid=2";
                                 break;
                             //--------------St Xavier------------------
                             case 4:
                                 mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS&to=" + mMob + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2";
                                
                                 break;

                         }



                         HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                         HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                         System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                         string responseString = respStreamReader.ReadToEnd().ToString();

                         //ReturnMsg = responseString;

                         ReturnMsg = "Message Sent Successfully"; 
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
                         //--------------St Xavier------------------
                         case 4:
                             mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS&to=" + mMob + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2";
                             break;
                     }


                     HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                     HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                     System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                     string responseString = respStreamReader.ReadToEnd().ToString();


                     //ReturnMsg = responseString;
                     ReturnMsg = "Message Sent Successfully"; 
                     
                     respStreamReader.Close();
                     myResp.Close();

                 }

             }
             else
             {
                 ReturnMsg += " Message field is blank.";
             }
             return ReturnMsg;
         }

         public string  SendMesssageTextHindiold(string mMsg, string mMob)
         {
             string ReturnMsg = string.Empty;
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
                         switch (int.Parse(Session["CompID"].ToString()))
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
                         string responseString = respStreamReader.ReadToEnd().ToString();

                        // ReturnMsg = responseString;
                         ReturnMsg = "Message Sent Successfully"; 
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
                             mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS​&to=" + mMob + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                            

                             break;


                     }
                     
                     HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                     HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                     System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                     string responseString = respStreamReader.ReadToEnd().ToString();
                     //ReturnMsg = responseString;
                     ReturnMsg = "Message Sent Successfully"; 
                     respStreamReader.Close();
                     myResp.Close();

                 }
             }
             else
             {
                 ReturnMsg += " Message field is blank.";
             }
             return ReturnMsg;
         }


         public string convertstring(string unicodeString)
         {
             string res = String.Empty;

             for (int a = 0; a < unicodeString.Length; a = a + 2)
             {
                 string Char2Convert = unicodeString.Substring(a, 2);
                 int n = Convert.ToInt32(Char2Convert, 16);
                 char c = (char)n;
                 res += c.ToString();

             }

             return res;
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

        public ActionResult GetStudentListForSMS(string mClassesID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             ViewData["ClassSetupID"] = mClassesID;
             return PartialView("ListSendGeneralMessegeView", unitOfWork.sendMessegeService.GetStudentSessionByClassSetupIDs(mClassesID, int.Parse(Session["SessionID"].ToString()),byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
           
         }


    }



}



    