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
using appSchool.Model;

namespace appSchool.Controllers
{
    [NoCache]
    public class SendReligionMessegeController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;



        public static List<List<string>> Split(List<string> source)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 100)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
        public ActionResult Index()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (Session["UserID"] == null || (int)SubMenuModules.appSendReligionMessage == 0)
            {
                return Redirect("~/");
            }
            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 24, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
            return View();
        }
        public ActionResult PartialSendReligionMessegeView(string mClassSetup, string mReligion)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (mClassSetup == "" || mClassSetup == string.Empty || mClassSetup == null)
            {
                mClassSetup = "-1";
            }

            return PartialView("ListSendReligionMessegeView", unitOfWork.sendMessegeService.GetStudentSessionByClassNReligions(mClassSetup, mReligion, int.Parse(Session["SessionID"].ToString())));

        }
        public ActionResult PartialSendReligionMessegeTemplateView()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("SendReligionMessegeTemplateView", unitOfWork.smsTemplateService.GetSMSTemplateListForGeneral(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
        public ActionResult SMSTemplateGridRowChange(int ID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("SendReligionMessegeTextView", ID);
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

            if (SMSLogMasterID != string.Empty || SMSLogMasterID != "" || SMSLogMasterID != null)
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
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

            if (ModelState.IsValid)
            {
                #region  Send SMS

                if (obj.smsMobileNo == null || obj.smsMobileNo == string.Empty)
                {
                    Errormsg += "Please Select Student.";
                    return new JsonResult()
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = new
                        {
                            Resultmsg = Errormsg,
                            ResultData = cCommon.RenderRazorViewToString("SendReligionMessegeEditTextView", obj, ControllerContext, ViewData, TempData)
                        }
                    };
                }
                string mMobileNo = string.Empty;
                if (obj.smsMobileNo != null)
                    mMobileNo = obj.smsMobileNo;
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
                        //Errormsg = SendMesssageText(NewMessage.Trim(), mMobileNo);
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
                        Errormsg = unitOfWork.sendMessegeService.SendMesssageTextHindi((NewMessage.Trim()), mMobileNo, byte.Parse(Session["CompID"].ToString()));
                    }
                }
                #endregion

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

                    int newSMSLogMasterID = SaveSMSLogMaster("ReligionSMS", int.Parse(Session["SessionID"].ToString()), Message, obj.smsMobileNo, obj.OrderedBy, int.Parse(Session["SessionID"].ToString()));
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
            else
            {
                Errormsg = "ModelState is not Valid.";
            }

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    Resultmsg = Errormsg,
                    ResultData = cCommon.RenderRazorViewToString("SendReligionMessegeEditTextView", obj, ControllerContext, ViewData, TempData)
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

        public string SendMesssageTextold(string mMsg, string mMob)
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


                    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                    HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                    System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                    string responseString = respStreamReader.ReadToEnd();
                    ReturnMsg = responseString;
                    respStreamReader.Close();
                    myResp.Close();

                }
            }
            return ReturnMsg;
        }

        public string SendMesssageTextHindiold(string mMsg, string mMob)
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
                            case 4:
                                mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS​&to=" + lst + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                break;
                        }



                        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                        System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                        string responseString = respStreamReader.ReadToEnd();
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
                            mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=nirmalconvent&pwd=school@eee&to=" + mMob + "&sid=NcgiCJ&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                            break;
                        case 2:
                            mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + mMob + "&sid=STmark&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                            break;
                        case 3:
                            mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=KVJIII&pwd=school@eee&to=" + mMob + "&sid=KVJIII&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                            break;
                        case 4:
                            mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS​&to=" + lst + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                            break;
                    }



                    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                    HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                    System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                    string responseString = respStreamReader.ReadToEnd();
                    //ReturnMsg = responseString;
                    ReturnMsg = "Message Sent Successfully";
                    respStreamReader.Close();
                    myResp.Close();

                }
            }
            return ReturnMsg;
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

        public ActionResult ListPartialReligion()
        {
            return PartialView("ListReligionGridLookupPartial", ReligionModel.GetReligionList());
        }

        public ActionResult GetStudentListForSMS(string mClassesID, string mReligion)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["ClassSetupID"] = mClassesID;
            ViewData["Religion"] = mReligion;
            return PartialView("ListSendReligionMessegeView", unitOfWork.sendMessegeService.GetStudentSessionByClassNReligions(mClassesID, mReligion, int.Parse(Session["SessionID"].ToString())));

        }


    }



}



