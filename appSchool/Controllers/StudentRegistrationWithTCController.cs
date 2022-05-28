using appSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System.Web.UI;
using appSchool.ViewModels;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace appSchool.Controllers
{
    [NoCache]
    public class StudentRegistrationWithTCController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;

        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appRegistrationsWithTC == 0)
            {
                return Redirect("~/");
            }
            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 109, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
            return PartialView("Index");
        }
        public ActionResult grdRegistredStudents(bool mShowFilter)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewBag.ShowFilter = mShowFilter;
            return PartialView("ListRegistrationsWithTC", unitOfWork.studentRegistrationService.GetRegistrationGridDataWithTCAllotment(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["SessionID"].ToString())));
        }

        public ActionResult grdStudentsDocumentsParial(int mStudentID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("GridDocumentListPartial", unitOfWork.studentDocumentService.GetStudentDetailListByStudentID(mStudentID));
        }

        public ActionResult RefreshRegistrationGrid(bool mShowFilter)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewBag.ShowFilter = mShowFilter;
            return PartialView("ListRegistrationsWithTC", unitOfWork.studentRegistrationService.GetRegistrationGridDataWithTCAllotment(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["SessionID"].ToString())));
        }

       


        [HttpPost, ValidateInput(false)]
     
        public void SaveUserLogForUpdate(modelRegistration obj)
        {
     
        }
        public void SaveUserLogForDelete(modelRegistration obj)
        {

        }

        [HttpPost, ValidateInput(false)]

        public ActionResult StudentGridRowChange(int RegID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("RegistrationTabs", RegID);
        }

        [HttpPost]

        public ActionResult CallbacksImageUpload(IEnumerable<UploadedFile> ucCallbacks)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return null;
        }


        public ActionResult GetStudentTCListSessionWise(bool mShowFilter, byte mSessionID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewBag.ShowFilter = mShowFilter;

            return PartialView("ListRegistrationsWithTC", unitOfWork.studentRegistrationService.GetStudentTCAllotListSessionWise(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), mSessionID));
        }

        [HttpPost]
        public ActionResult UploadControlCallbackAction(modelStudentPhotoUploadInfo obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            UploadControlDemosHelpers objup = new UploadControlDemosHelpers();
            objup.mEnrollmentNo = obj.EnrollmentNo;
            objup.mStudentID = obj.StudentID;
       
            return null;

        }

        [HttpPost]
        public ActionResult UploadDocument(modelStudentDocumentUploadInfo obj)
        {
 
            return null;
       
        }

        [HttpPost]
        public ActionResult StudentPriviousInfoSaveANDUpdate(StudentPreviousDetail obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {

                    int CheckRow = unitOfWork.studentPriviousDetailsService.CheckPreviousDatainTable(obj.StudentID);
                    if (CheckRow > 0)
                    {

                        unitOfWork.studentPriviousDetailsService.UpdateStudentPriviousDetail(obj);
                        unitOfWork.Save();

                    }
                    else
                    {
                        unitOfWork.studentPriviousDetailsService.Insert(obj);
                        unitOfWork.Save();
                    }

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("StudentPriviousEditPartial", obj);

        }



    }

    public class UploadControlDemosHelpers
    {
        public static string _EnrollmentNo = string.Empty;
        public static int _StudentID;

        public string mEnrollmentNo
        {
            set { _EnrollmentNo = value; }
            get { return _EnrollmentNo; }
        }
        public int mStudentID
        {
            set { _StudentID = value; }
            get { return _StudentID; }
        }

        public const string UploadDirectory = "~/Images/students/";

        public static void uc_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                string name = e.UploadedFile.FileName.Replace(e.UploadedFile.FileName, _EnrollmentNo + ".png");
               
                string resultFilePath = HttpContext.Current.Request.MapPath(UploadDirectory + name);
                e.UploadedFile.SaveAs(resultFilePath, true);//Code Central Mode - Uncomment This Line

     
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                if (urlResolver != null)
                {
                    e.CallbackData = urlResolver.ResolveClientUrl("Successfully Uploaded. ");
                }
            }
        }
    }

    public class UploadControlDocumentt
    {
        public static string _EnrollmentNo = string.Empty;
        public static int _StudentID;
        public static string _DocumentName;
        public static string _FileName;
        public static string _Remark;
        public static int _UidAdd;
        public static int _SessionID;



        public string mEnrollmentNo
        {
            set { _EnrollmentNo = value; }
            get { return _EnrollmentNo; }
        }
        public int mStudentID
        {
            set { _StudentID = value; }
            get { return _StudentID; }
        }
        public string mDocumentName
        {
            set { _DocumentName = value; }
            get { return _DocumentName; }
        }

        public string mFileName
        {
            set { _FileName = value; }
            get { return _FileName; }
        }
        public string mRemark
        {
            set { _Remark = value; }
            get { return _Remark; }
        }
        public int mUidAdd
        {
            set { _UidAdd = value; }
            get { return _UidAdd; }
        }

        public int mSessionID
        {
            set { _SessionID = value; }
            get { return _SessionID; }
        }

        public const string UploadDirectory = "~/Images/studentDocuments";

      
        public static void uc_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {
                //string name = e.UploadedFile.FileName.Replace(e.UploadedFile.FileName, _EnrollmentNo + ".png");
                  string subfolder = _StudentID.ToString();
                  string name = e.UploadedFile.FileName;
                  string path = HttpContext.Current.Request.MapPath(UploadDirectory);
                if (Directory.Exists(path))
                {
                    Directory.CreateDirectory(path + "\\" + subfolder);
                }

                string resultFilePath = path + "\\" + subfolder + "\\" + name;

                e.UploadedFile.SaveAs(resultFilePath,true);//Code Central Mode - Uncomment This Line

                appSchool.Repositories.StudentDocumentDetail img = new appSchool.Repositories.StudentDocumentDetail();
                img.StudentID = _StudentID;
                img.DocumentName = _DocumentName;
                img.Remark = _Remark;
                img.FileName = name;
                img.UIDAdd = _UidAdd;
                img.AddDate = DateTime.Now;
                img.SessionID = _SessionID;

                UnitOfWork obj = new UnitOfWork();

                obj.studentDocumentService.Insert(img);
                obj.Save();

                IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                if (urlResolver != null)
                {
                    e.CallbackData = urlResolver.ResolveClientUrl("Successfully Uploaded. ");
                }
            }
        }





    }



}

