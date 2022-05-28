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
    public class SendMessegeWishesController : Controller
    {
         private UnitOfWork unitOfWork = new UnitOfWork();
         private SqlConnection _mConn;
         private SqlTransaction _mTran;

         public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appSendMessageWishes == 0)
            {
                return Redirect("~/");
            }

            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 27,byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString()));
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
            ViewData["ClassSetupID"] = string.Empty;
            return View();
        }
         public ActionResult PartialSendMessegeWishesView(string mClassSetup)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (mClassSetup == "" && mClassSetup == string.Empty && mClassSetup == null)
            {
                mClassSetup = "-1";
            }

            ViewData["ClassSetupID"] = mClassSetup;
            int mWishType = int.Parse(mClassSetup.ToString());
            return PartialView("ListSendMessegeWishesView", unitOfWork.sendMessegeService.GetStudentListforSMSWishes(mWishType, int.Parse(Session["SessionID"].ToString()),byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));

               
        }
         public ActionResult PartialSendMessegeWishesTemplateView()
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             return PartialView("SendMessegeWishesTemplateView", unitOfWork.smsTemplateService.GetSMSTemplateListForWishesStudent(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }
         public ActionResult SMSTemplateGridRowChange(int ID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             return PartialView("SendMessegeWishesTextView", ID);
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

             if (SMSLogMasterID != string.Empty && SMSLogMasterID != "" && SMSLogMasterID != null)
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
             //if (Session["UserID"] == null) { return Redirect("~/"); }
             bool Result = false;
             string ErrorMsg = string.Empty;
             SettingMasterStaticClass._SaveSMSLog = true;
             bool ResultSMSDetail = false;
             int newSMSLogMasterID = 0;

             _mConn = DB.GetActiveConnection();
             _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

             if (ModelState.IsValid)
             {
                 if (obj.includeName == true)
                 {
                     if (SettingMasterStaticClass._SaveSMSLog == true)
                     {
                         #region LogMaster
                         string Message = string.Empty;
                         if (obj.SMSLanguage == 2)
                         { Message = obj.PrefixEnglish + " " + obj.smsTextEnglish; }
                         else
                         { Message = obj.PrefixHindi + " " + obj.smsTextHindi; }
                         newSMSLogMasterID = SaveSMSLogMaster("WishesSMS", int.Parse(Session["SessionID"].ToString()), Message, obj.smsMobileNo, obj.OrderedBy, int.Parse(Session["SessionID"].ToString()));
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

                     if (obj.smsStudentID == null || obj.smsStudentID==string.Empty)
                     {
                         ErrorMsg += "Please Select Students.";
                         return new JsonResult()
                         {
                             JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                             Data = new{
                                 Resultmsg = ErrorMsg,
                                 ResultData = cCommon.RenderRazorViewToString("SendMessegeWishesEditTextView", obj, ControllerContext, ViewData, TempData)
                             }
                         };

                     }

                     string mStudnetID = obj.smsStudentID;

                     string[] bits = mStudnetID.Split(',');

                     string smsTextEnglish = obj.smsTextEnglish;
                     string smsTextHindi = obj.smsTextHindi;
                     string PrefixEnglish = obj.PrefixEnglish;
                     string PrefixHindi = obj.PrefixHindi;


                     foreach (string bit in bits)
                     {
                         int StudentIDforSearch = int.Parse(bit);

                         string NewMessage = string.Empty;
                         vStudentSession objStudReg = new vStudentSession();
                         StudentRegistration objParentInfo = new StudentRegistration();
                         if (obj.TemplateType == 1)
                         {
                             objStudReg = unitOfWork.sendMessegeService.GetStudentForSendingSMSbyStudentID(StudentIDforSearch, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                         }
                         else
                         {
                             objStudReg = unitOfWork.sendMessegeService.GetStudentForSendingSMSbyStudentID(StudentIDforSearch, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                             objParentInfo = unitOfWork.studentRegistrationService.GetByID(StudentIDforSearch);
                         }

                         if (objStudReg.SMSInHindi == false)
                         {

                             if (obj.TemplateType == 1)
                             {
                                 #region Student Birthday
                                 if (obj.smsTextEnglish != string.Empty && obj.smsTextEnglish != "" && obj.smsTextEnglish != null)
                                 {
                                     obj.smsTextEnglish = smsTextEnglish;
                                     obj.smsTextEnglish = obj.smsTextEnglish.Replace("$Name$", objStudReg.FullName);
                                     obj.smsTextEnglish = obj.smsTextEnglish.Replace("$Class$", objStudReg.ClassDescription);
                                 }
                                 if (obj.PrefixEnglish != string.Empty && obj.PrefixEnglish != "" && obj.PrefixEnglish != null)
                                 {
                                     obj.PrefixEnglish = PrefixEnglish;
                                     obj.PrefixEnglish = obj.PrefixEnglish.Replace("$Name$", objStudReg.FullName);
                                     obj.PrefixEnglish = obj.PrefixEnglish.Replace("$Class$", objStudReg.ClassDescription);
                                 }

                                 NewMessage = obj.PrefixEnglish + " " + obj.smsTextEnglish;

                                 if (objStudReg.SMSMobileNo != string.Empty)
                                 {
                                     //ErrorMsg=SendMesssageText(NewMessage.Trim(), objStudReg.SMSMobileNo);

                                     ErrorMsg = unitOfWork.sendMessegeService.SendMesssageText(NewMessage.Trim(), objStudReg.SMSMobileNo,  byte.Parse(Session["CompID"].ToString()));
                                     if (newSMSLogMasterID > 0 && SettingMasterStaticClass._SaveSMSLog==true)
                                     {
                                         ResultSMSDetail = SaveSMSLogDetailStudentWise(newSMSLogMasterID, StudentIDforSearch, objStudReg.SMSMobileNo, NewMessage);

                                     }
                                 }
                                 #endregion
                             }
                             else if (obj.TemplateType == 2)
                             {
                                 #region Father Birthday
                                 if (obj.smsTextEnglish != string.Empty && obj.smsTextEnglish != "" && obj.smsTextEnglish != null)
                                 {
                                     obj.smsTextEnglish = smsTextEnglish;
                                     obj.smsTextEnglish = obj.smsTextEnglish.Replace("$Name$", objParentInfo.FatherName);
                                     obj.smsTextEnglish = obj.smsTextEnglish.Replace("$Class$", "");
                                 }
                                 if (obj.PrefixEnglish != string.Empty && obj.PrefixEnglish != "" && obj.PrefixEnglish != null)
                                 {
                                     obj.PrefixEnglish = PrefixEnglish;
                                     obj.PrefixEnglish = obj.PrefixEnglish.Replace("$Name$", objParentInfo.FatherName);
                                     obj.PrefixEnglish = obj.PrefixEnglish.Replace("$Class$", "");
                                 }
                                 NewMessage = obj.PrefixEnglish + " " + obj.smsTextEnglish;

                                 if (objParentInfo.SMSMobileNo != string.Empty)
                                 {
                                     //ErrorMsg=SendMesssageText(NewMessage.Trim(), objParentInfo.SMSMobileNo);

                                     ErrorMsg = unitOfWork.sendMessegeService.SendMesssageText(NewMessage.Trim(), objParentInfo.SMSMobileNo,  byte.Parse(Session["CompID"].ToString()));
                                     if (newSMSLogMasterID > 0 && SettingMasterStaticClass._SaveSMSLog == true)
                                     {
                                         ResultSMSDetail = SaveSMSLogDetailStudentWise(newSMSLogMasterID, StudentIDforSearch, objStudReg.SMSMobileNo, NewMessage);

                                     }
                                 }

                                 #endregion
                             }
                             else if (obj.TemplateType == 3)
                             {
                                 #region Mother Birthday
                                 if (obj.smsTextEnglish != string.Empty && obj.smsTextEnglish != "" && obj.smsTextEnglish != null)
                                 {
                                     obj.smsTextEnglish = smsTextEnglish;
                                     obj.smsTextEnglish = obj.smsTextEnglish.Replace("$Name$", objParentInfo.MotherName);
                                     obj.smsTextEnglish = obj.smsTextEnglish.Replace("$Class$", "");
                                 }
                                 if (obj.PrefixEnglish != string.Empty && obj.PrefixEnglish != "" && obj.PrefixEnglish != null)
                                 {
                                     obj.PrefixEnglish = PrefixEnglish;
                                     obj.PrefixEnglish = obj.PrefixEnglish.Replace("$Name$", objParentInfo.MotherName);
                                     obj.PrefixEnglish = obj.PrefixEnglish.Replace("$Class$", "");
                                 }
                                 NewMessage = obj.PrefixEnglish + " " + obj.smsTextEnglish;

                                 if (objParentInfo.SMSMobileNo != string.Empty)
                                 {
                                     //ErrorMsg=SendMesssageText(NewMessage.Trim(), objParentInfo.SMSMobileNo);

                                     ErrorMsg = unitOfWork.sendMessegeService.SendMesssageText(NewMessage.Trim(), objParentInfo.SMSMobileNo,  byte.Parse(Session["CompID"].ToString()));
                                     if (newSMSLogMasterID > 0 && SettingMasterStaticClass._SaveSMSLog == true)
                                     {
                                         ResultSMSDetail = SaveSMSLogDetailStudentWise(newSMSLogMasterID, StudentIDforSearch, objStudReg.SMSMobileNo, NewMessage);

                                     }
                                 }
                                 #endregion
                             }
                             else if (obj.TemplateType == 4)
                             {
                                 #region Parent's Anniversary
                                 if (obj.smsTextEnglish != string.Empty && obj.smsTextEnglish != "" && obj.smsTextEnglish != null)
                                 {
                                     obj.smsTextEnglish = smsTextEnglish;
                                     obj.smsTextEnglish = obj.smsTextEnglish.Replace("$Name$", objParentInfo.FatherName + "," + objParentInfo.MotherName);
                                     obj.smsTextEnglish = obj.smsTextEnglish.Replace("$Class$", "");
                                 }
                                 if (obj.PrefixEnglish != string.Empty && obj.PrefixEnglish != "" && obj.PrefixEnglish != null)
                                 {
                                     obj.PrefixEnglish = PrefixEnglish;
                                     obj.PrefixEnglish = obj.PrefixEnglish.Replace("$Name$", objParentInfo.FatherName + "," + objParentInfo.MotherName);
                                     obj.PrefixEnglish = obj.PrefixEnglish.Replace("$Class$", "");
                                 }
                                 NewMessage = obj.PrefixEnglish + " " + obj.smsTextEnglish;

                                 if (objParentInfo.SMSMobileNo != string.Empty)
                                 {
                                     //ErrorMsg=SendMesssageText(NewMessage.Trim(), objParentInfo.SMSMobileNo);

                                     ErrorMsg = unitOfWork.sendMessegeService.SendMesssageText(NewMessage.Trim(), objParentInfo.SMSMobileNo,  byte.Parse(Session["CompID"].ToString()));

                                     if (newSMSLogMasterID > 0 && SettingMasterStaticClass._SaveSMSLog == true)
                                     {
                                         ResultSMSDetail = SaveSMSLogDetailStudentWise(newSMSLogMasterID, StudentIDforSearch, objStudReg.SMSMobileNo, NewMessage);

                                     }
                                 }
                                 #endregion

                             }

                         }
                         if (objStudReg.SMSInHindi == true)
                         {

                             if (obj.TemplateType == 1)
                             {
                                 #region Student Birthday
                                 if (obj.smsTextHindi != string.Empty && obj.smsTextHindi != "" && obj.smsTextHindi != null)
                                 {
                                     obj.smsTextHindi = smsTextHindi;
                                     obj.smsTextHindi = obj.smsTextHindi.Replace("$Name$", objStudReg.FullName);
                                     obj.smsTextHindi = obj.smsTextHindi.Replace("$Class$", objStudReg.ClassDescription);
                                 }
                                 if (obj.PrefixHindi != string.Empty && obj.PrefixHindi != "" && obj.PrefixHindi != null)
                                 {
                                     obj.PrefixHindi = PrefixHindi;
                                     obj.PrefixHindi = obj.PrefixHindi.Replace("$Name$", objStudReg.FullName);
                                     obj.PrefixHindi = obj.PrefixHindi.Replace("$Class$", objStudReg.ClassDescription);
                                 }
                                 NewMessage = obj.PrefixHindi + " " + obj.smsTextHindi;
                                 if (objStudReg.SMSMobileNo != string.Empty)
                                 {
                                     //ErrorMsg=SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), objStudReg.SMSMobileNo);

                                     //ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), objStudReg.SMSMobileNo, byte.Parse(Session["CompID"].ToString()));
                                     ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(NewMessage.Trim(), objStudReg.SMSMobileNo,  byte.Parse(Session["CompID"].ToString()));
                                     if (newSMSLogMasterID > 0 && SettingMasterStaticClass._SaveSMSLog == true)
                                     {
                                         ResultSMSDetail = SaveSMSLogDetailStudentWise(newSMSLogMasterID, StudentIDforSearch, objStudReg.SMSMobileNo, NewMessage);
                                     }
                                 }
                                 #endregion
                             }
                             else if (obj.TemplateType == 2)
                             {
                                 #region Father Birthday
                                 if (obj.smsTextHindi != string.Empty && obj.smsTextHindi != "" && obj.smsTextHindi != null)
                                 {
                                     obj.smsTextHindi = smsTextHindi;
                                     obj.smsTextHindi = obj.smsTextHindi.Replace("$Name$", objParentInfo.FatherName);
                                     obj.smsTextHindi = obj.smsTextHindi.Replace("$Class$", "");
                                 }
                                 if (obj.PrefixHindi != string.Empty && obj.PrefixHindi != "" && obj.PrefixHindi != null)
                                 {
                                     obj.PrefixHindi = PrefixHindi;
                                     obj.PrefixHindi = obj.PrefixHindi.Replace("$Name$", objParentInfo.FatherName);
                                     obj.PrefixHindi = obj.PrefixHindi.Replace("$Class$", "");
                                 }
                                 NewMessage = obj.PrefixHindi + " " + obj.smsTextHindi;
                                 if (objParentInfo.SMSMobileNo != string.Empty)
                                 {
                                     //ErrorMsg = SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), objParentInfo.SMSMobileNo);

                                     //ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), objParentInfo.SMSMobileNo, byte.Parse(Session["CompID"].ToString()));
                                     ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(NewMessage.Trim(), objParentInfo.SMSMobileNo, byte.Parse(Session["CompID"].ToString()));
                                     if (newSMSLogMasterID > 0 && SettingMasterStaticClass._SaveSMSLog == true)
                                     {
                                         ResultSMSDetail = SaveSMSLogDetailStudentWise(newSMSLogMasterID, StudentIDforSearch, objStudReg.SMSMobileNo, NewMessage);
                                     }
                                 }
                                 #endregion
                             }
                             else if (obj.TemplateType == 3)
                             {
                                 #region Mother Birthday
                                 if (obj.smsTextHindi != string.Empty && obj.smsTextHindi != "" && obj.smsTextHindi != null)
                                 {
                                     obj.smsTextHindi = smsTextHindi;
                                     obj.smsTextHindi = obj.smsTextHindi.Replace("$Name$", objParentInfo.MotherName);
                                     obj.smsTextHindi = obj.smsTextHindi.Replace("$Class$", "");
                                 }
                                 if (obj.PrefixHindi != string.Empty && obj.PrefixHindi != "" && obj.PrefixHindi != null)
                                 {
                                     obj.PrefixHindi = PrefixHindi;
                                     obj.PrefixHindi = obj.PrefixHindi.Replace("$Name$", objParentInfo.MotherName);
                                     obj.PrefixHindi = obj.PrefixHindi.Replace("$Class$", "");
                                 }
                                 NewMessage = obj.PrefixHindi + " " + obj.smsTextHindi;
                                 if (objParentInfo.SMSMobileNo != string.Empty)
                                 {
                                     //ErrorMsg=SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), objParentInfo.SMSMobileNo);

                                     //ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), objParentInfo.SMSMobileNo, byte.Parse(Session["CompID"].ToString()));
                                     ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(NewMessage.Trim(), objParentInfo.SMSMobileNo,  byte.Parse(Session["CompID"].ToString()));
                                     if (newSMSLogMasterID > 0 && SettingMasterStaticClass._SaveSMSLog == true)
                                     {
                                         ResultSMSDetail = SaveSMSLogDetailStudentWise(newSMSLogMasterID, StudentIDforSearch, objStudReg.SMSMobileNo, NewMessage);
                                     }
                                 }
                                 #endregion
                             }
                             else if (obj.TemplateType == 4)
                             {
                                 #region Parents's Anniversary
                                 if (obj.smsTextHindi != string.Empty && obj.smsTextHindi != "" && obj.smsTextHindi != null)
                                 {
                                     obj.smsTextHindi = smsTextHindi;
                                     obj.smsTextHindi = obj.smsTextHindi.Replace("$Name$", objParentInfo.FatherName + ", " + objParentInfo.MotherName);
                                     obj.smsTextHindi = obj.smsTextHindi.Replace("$Class$", "");
                                 }
                                 if (obj.PrefixHindi != string.Empty && obj.PrefixHindi != "" && obj.PrefixHindi != null)
                                 {
                                     obj.PrefixHindi = PrefixHindi;
                                     obj.PrefixHindi = obj.PrefixHindi.Replace("$Name$", objParentInfo.FatherName + ", " + objParentInfo.MotherName);
                                     obj.PrefixHindi = obj.PrefixHindi.Replace("$Class$", "");
                                 }
                                 NewMessage = obj.PrefixHindi + " " + obj.smsTextHindi;
                                 if (objParentInfo.SMSMobileNo != string.Empty)
                                 {
                                     //ErrorMsg=SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), objParentInfo.SMSMobileNo);

                                     //ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), objParentInfo.SMSMobileNo, byte.Parse(Session["CompID"].ToString()));
                                     ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(NewMessage.Trim(), objParentInfo.SMSMobileNo, byte.Parse(Session["CompID"].ToString()));
                                     if (newSMSLogMasterID > 0 && SettingMasterStaticClass._SaveSMSLog == true)
                                     {
                                         ResultSMSDetail = SaveSMSLogDetailStudentWise(newSMSLogMasterID, StudentIDforSearch, objStudReg.SMSMobileNo, NewMessage);
                                     }
                                 }
                                 #endregion
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
                         return new JsonResult() { JsonRequestBehavior = JsonRequestBehavior.AllowGet, 
                             Data = new {
                                 Resultmsg = ErrorMsg,
                                 ResultData = cCommon.RenderRazorViewToString("SendMessegeWishesEditTextView", obj, ControllerContext, ViewData, TempData)
                             } };
                     }
                     string mMobileNo = obj.smsMobileNo;
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
                            // ErrorMsg=SendMesssageText(NewMessage.Trim(), mMobileNo);

                             ErrorMsg = unitOfWork.sendMessegeService.SendMesssageText((NewMessage.Trim()), mMobileNo, byte.Parse(Session["CompID"].ToString()));
                         }
                     }
                     else
                     {
                         string NewMessage = obj.PrefixHindi + " " + obj.smsTextHindi;

                         if (obj.smsMobileNo != string.Empty)
                         {
                             //ErrorMsg=SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), mMobileNo);

                             //ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), mMobileNo, byte.Parse(Session["CompID"].ToString()));
                             ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(NewMessage.Trim(), mMobileNo, byte.Parse(Session["CompID"].ToString()));
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

                         newSMSLogMasterID = SaveSMSLogMaster("WishesSMS", int.Parse(Session["SessionID"].ToString()), Message, obj.smsMobileNo, obj.OrderedBy, int.Parse(Session["SessionID"].ToString()));
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

           
             obj.TemplateType = obj.TemplateType;
             return new JsonResult()
             {
                 JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                 Data = new
                 {
                     Resultmsg = ErrorMsg,
                     ResultData = cCommon.RenderRazorViewToString("SendMessegeWishesEditTextView", obj, ControllerContext, ViewData, TempData)
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
             return PartialView("SendMessegeWishesTextView", objsms);

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
                             //----2. St. Marks College------------------
                         case 2:
                             mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=Stmark&pwd=Welcome@123&to=" + mMob + "&sid=STmark&msg=" + mMsg + "&fl=0&gwid=2";
                             break;
                         //----2. St. KVJII College------------------
                         case 3:
                             mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=KVJIII&pwd=school@eee&to=" + mMob + "&sid=KVJIII&msg=" + mMsg + "&fl=0&gwid=2";
                             break;
                         //----2. St. SXCJHS College------------------
                         case 4:
                             mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS&to=" + mMob + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2";
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
                             case 4:
                                 mApi = "http://login.smsgatewayhub.com/smsapi/pushsms.aspx?user=SXCJHS&pwd=SXCJHS​&to=" + lst + "&sid=SXCJHS&msg=" + mMsg + "&fl=0&gwid=2&dc=8";
                                 break;
                         }
                          
                          

                         HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(mApi);
                         HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();
                         System.IO.StreamReader respStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
                         string responseString = respStreamReader.ReadToEnd();
                         //ReturnMsg = responseString;
                         ReturnMsg = "Message sent successfully.";
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
                     ReturnMsg = "Message sent successfully.";
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

         public ActionResult GetStudentListForSMS(string mClassesID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             ViewData["ClassSetupID"] = mClassesID;
             int mWishType = int.Parse(mClassesID.ToString());
             return PartialView("ListSendMessegeWishesView", unitOfWork.sendMessegeService.GetStudentListforSMSWishes(mWishType, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
           
         }


    }



}



    