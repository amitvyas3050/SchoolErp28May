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
    public class EmployeeSMSAbsenteesController : Controller
    {
         private UnitOfWork unitOfWork = new UnitOfWork();
         private SqlConnection _mConn;
         private SqlTransaction _mTran;

         //public override string Name { get { return "Editing"; } }


         public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appSendSMSAbsentees == 0)
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


         public ActionResult PartialSendSMSAbsenteesView(DateTime mAttendanceDate)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }

             ViewData["AttendanceDate"] = mAttendanceDate;
             List<vEmployeeattendancelist> list = new List<vEmployeeattendancelist>();
            list = new UnitOfWork().employeeAttendanceDailyservices.GetEmployeAbsentPresentListByAttendanceDate(mAttendanceDate);

            return PartialView("ListSendSMSAbsenteesView", list);

        }
         public ActionResult PartialSendSMSAbsenteesTemplateView()
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             return PartialView("ListTemplateGridLookupPartial", unitOfWork.smsTemplateService.GetSMSTemplateforSMSType(1, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }
         public ActionResult SMSTemplateGridRowChange(int ID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             return PartialView("SendSMSAbsenteesTextView", ID);
         }

         public ActionResult GetAllAbsenteesforGrid(DateTime mAttendanceDate)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }

             List<vEmployeeattendancelist> obj = new List<vEmployeeattendancelist>();
             obj = new UnitOfWork().employeeAttendanceDailyservices.GetEmployeAbsentPresentListByAttendanceDate(mAttendanceDate);
             ViewData["AttendanceDate"] = mAttendanceDate;

             return PartialView("ListSendSMSAbsenteesView", obj);
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

         private bool SaveSMSLogDetail(modelEmployeeSendSms obj, int SMSLogID)
         {
             int i = 1;
             bool _DetailSaved = false;
             string Message = string.Empty;
             StringBuilder mQuery = new StringBuilder();
             mQuery.Append("INSERT INTO dbo.SMSLogDetail( SMSLogID,StudentID,MobileNo,SMSText,SessionID,CompID,BranchID)");
             string StudnetID = obj.smsTeacherID;
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
         public JsonResult SMSButtonClick(modelEmployeeSendSms obj)
         {

             bool Result = false;
             string ErrorMsg = string.Empty;
             bool ResultSMSDetail = false;
             int newSMSLogMasterID = 0;
             SettingMasterStaticClass._SaveSMSLog = true;
             _mConn = DB.GetActiveConnection();
             _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

             //if (ModelState.IsValid)
             if (true)
             {

                 if (obj.includeName == true)
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

                         newSMSLogMasterID = SaveSMSLogMaster("EmployeeAbsenteesSMS", int.Parse(Session["SessionID"].ToString()), Message, obj.smsMobileNo, obj.OrderedBy, int.Parse(Session["SessionID"].ToString()));
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
                     if (obj.smsTeacherID == string.Empty || obj.smsTeacherID == null)
                     {
                         ErrorMsg += "Please Select Employee.";
                         return new JsonResult() { JsonRequestBehavior = JsonRequestBehavior.AllowGet, 
                             Data = new{
                                 Resultmsg = ErrorMsg,
                                 ResultData = cCommon.RenderRazorViewToString("SendSMSAbsenteesEditTextView", obj, ControllerContext, ViewData, TempData) 
                             } };

                     }
                     string mTeacherID = string.Empty;
                     mTeacherID = obj.smsTeacherID;

                     string[] bits = mTeacherID.Split(',');

                     string smsTextEnglish = obj.smsTextEnglish;
                     string smsTextHindi = obj.smsTextHindi;
                     string PrefixEnglish = obj.PrefixEnglish;
                     string PrefixHindi = obj.PrefixHindi;


                     foreach (string bit in bits)
                     {
                         int TeacherIDforSearch = int.Parse(bit);

                         string NewMessage = string.Empty;
                         Teacher objTeach = new Teacher();
                         objTeach = unitOfWork.teacherRegistrationService.GetSingleTeacher(TeacherIDforSearch);

                         if (obj.SMSLanguage == 2) // english
                         {
                             if (obj.smsTextEnglish != string.Empty && obj.smsTextEnglish != "" && obj.smsTextEnglish != null)
                             {
                                 obj.smsTextEnglish = smsTextEnglish;
                                 obj.smsTextEnglish = obj.smsTextEnglish.Replace("$Name$", objTeach.FirstName + " " + objTeach.LastName);
                                 obj.smsTextEnglish = obj.smsTextEnglish.Replace("$EmpCode$", objTeach.EmployeeCode);
                                 obj.smsTextEnglish = obj.smsTextEnglish.Replace("$Date$", obj.ScheduledDate.ToString("dd/MMM/yy"));
                             }

                             if (obj.PrefixEnglish != string.Empty && obj.PrefixEnglish != "" && obj.PrefixEnglish != null)
                             {
                                 obj.PrefixEnglish = PrefixEnglish;
                                 obj.PrefixEnglish = obj.PrefixEnglish.Replace("$Name$", objTeach.FirstName + " " + objTeach.LastName);
                                 obj.PrefixEnglish = obj.PrefixEnglish.Replace("$EmpCode$", objTeach.EmployeeCode);
                                 obj.PrefixEnglish = obj.PrefixEnglish.Replace("$Date$", obj.ScheduledDate.ToString("dd/MMM/yy"));
                             }
                             NewMessage = obj.PrefixEnglish + " " + obj.smsTextEnglish;

                             if (objTeach.MobileNo != string.Empty)
                             {
                                 // ErrorMsg = SendMesssageText(NewMessage.Trim(), objStudReg.SMSMobileNo);

                                 ErrorMsg = unitOfWork.sendMessegeService.SendMesssageText((NewMessage.Trim()), objTeach.MobileNo, byte.Parse(Session["CompID"].ToString()));
                                 if (newSMSLogMasterID > 0 && SettingMasterStaticClass._SaveSMSLog==true)
                                 {
                                     ResultSMSDetail = SaveSMSLogDetailStudentWise(newSMSLogMasterID, TeacherIDforSearch, objTeach.MobileNo, NewMessage);

                                 }
                             }
                         }
                         if (obj.SMSLanguage == 1) //Hindi
                         {
                             if (obj.smsTextHindi != string.Empty && obj.smsTextHindi != "" && obj.smsTextHindi != null)
                             {
                                 obj.smsTextHindi = smsTextHindi;
                                 obj.smsTextHindi = obj.smsTextHindi.Replace("$Name$", objTeach.FirstName + " " + objTeach.LastName);
                                 obj.smsTextHindi = obj.smsTextHindi.Replace("$EmpCode$", objTeach.EmployeeCode);
                                 obj.smsTextHindi = obj.smsTextHindi.Replace("$Date$", obj.ScheduledDate.ToString("dd/MMM/yy"));
                             }

                             if (obj.PrefixHindi != string.Empty && obj.PrefixHindi != "" && obj.PrefixHindi != null)
                             {
                                 obj.PrefixHindi = PrefixHindi;
                                 obj.PrefixHindi = obj.PrefixHindi.Replace("$Name$", objTeach.FirstName + " " + objTeach.LastName);
                                 obj.PrefixHindi = obj.PrefixHindi.Replace("$EmpCode$", objTeach.EmployeeCode);
                                 obj.PrefixHindi = obj.PrefixHindi.Replace("$Date$", obj.ScheduledDate.ToString("dd/MMM/yy"));
                             }
                             NewMessage = obj.PrefixHindi + " " + obj.smsTextHindi;

                             if (objTeach.MobileNo != string.Empty)
                             {
                                 //ErrorMsg = SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), objStudReg.SMSMobileNo);

                                 //ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), objTeach.MobileNo, byte.Parse(Session["CompID"].ToString()));
                                 ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(NewMessage.Trim(), objTeach.MobileNo, byte.Parse(Session["CompID"].ToString()));
                                 if (newSMSLogMasterID > 0 && SettingMasterStaticClass._SaveSMSLog == true)
                                 {
                                     ResultSMSDetail = SaveSMSLogDetailStudentWise(newSMSLogMasterID, TeacherIDforSearch, objTeach.MobileNo, NewMessage);

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
                                 ResultData = cCommon.RenderRazorViewToString("SendSMSAbsenteesEditTextView", obj, ControllerContext, ViewData, TempData)
                             }
                         };
                     }

                     string mMobileNo =  obj.smsMobileNo;

                     


                     if (obj.SMSLanguage == 2)
                     {
                         string NewMessage = obj.PrefixEnglish + " " + obj.smsTextEnglish;

                         if (obj.smsMobileNo != string.Empty)
                         {
                             //ErrorMsg=SendMesssageText(NewMessage.Trim(), mMobileNo);
                             
                             ErrorMsg = unitOfWork.sendMessegeService.SendMesssageText((NewMessage.Trim()), mMobileNo, byte.Parse(Session["CompID"].ToString()));
                         }
                     }
                     else
                     {
                         string NewMessage = obj.PrefixHindi + " " + obj.smsTextHindi;

                         if (obj.smsMobileNo != string.Empty)
                         {
                            // ErrorMsg=SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), mMobileNo);
                             //ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(ConvertUnicodeStringToHexString(NewMessage.Trim()), mMobileNo, byte.Parse(Session["CompID"].ToString()));
                             ErrorMsg = unitOfWork.sendMessegeService.SendMesssageTextHindi(NewMessage.Trim(), mMobileNo, byte.Parse(Session["CompID"].ToString()));
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

                          newSMSLogMasterID = SaveSMSLogMaster("EmployeeAbsenteesSMS", int.Parse(Session["SessionID"].ToString()), Message, obj.smsMobileNo, obj.OrderedBy, int.Parse(Session["SessionID"].ToString()));
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
                     ResultData = cCommon.RenderRazorViewToString("SendSMSAbsenteesEditTextView", obj, ControllerContext, ViewData, TempData)
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
             modelEmployeeSendSms objsms = new modelEmployeeSendSms();
             SMSTemplate objTemp = new SMSTemplate();
             objTemp = unitOfWork.smsTemplateService.GetSMSTemplateEmployeeAbsent(mTemplateID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

             return PartialView("SendSMSAbsenteesTextView", objsms);

             // return unitOfWork.smsTemplateService.GetByID(mTemplateID).TemplateMessage;
         }

         public ActionResult PartialClassSetupView()
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             return PartialView("ListClassSetupPartial", unitOfWork.classSetupService.GetClassSetupList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }

         

    }



}



    