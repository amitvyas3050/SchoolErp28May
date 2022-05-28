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
    public class StudentRegistrationController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;

        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appStudentRegistration == 0)
            {
                return Redirect("~/");
            }
            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 55, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

        public ActionResult Registrations()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appStudentRegistration == 0)
            {
                return Redirect("~/");
            }
            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 55, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
            return PartialView("Registrations");
        }
        public ActionResult grdRegistredStudents(bool mShowFilter)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewBag.ShowFilter = mShowFilter;
            return PartialView("GridViewPartial", unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult ListRegistrations()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            
            return PartialView("GridViewPartial", unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
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
           // return PartialView("ListRegistrations", unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
            return PartialView("GridViewPartial", unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public string GetStudentFullName(int StudentID)
        {
            string FullName = string.Empty;

            FullName = unitOfWork.studentRegistrationService.GetFullNameOfStudent(StudentID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));


            return FullName;
        }

        public int CreateParentLogin(StudentRegistration objnew, string mParentLoginID, string mPassword)
        {
            int i = 0;

            SqlCommand cmd = new SqlCommand("Add_ParentLogin", _mConn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = _mTran;

            SqlParameter outPutParameter = new SqlParameter();
            outPutParameter.ParameterName = "@ParentLoginID";
            outPutParameter.SqlDbType = SqlDbType.Int;
            outPutParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outPutParameter);

            cmd.Parameters.AddWithValue("@StudentID ", objnew.StudentID);
            cmd.Parameters.AddWithValue("@ParentName", objnew.FatherName);
            cmd.Parameters.AddWithValue("@ParentUserName", mParentLoginID);
            cmd.Parameters.AddWithValue("@Password", mPassword);
            cmd.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmd.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            cmd.ExecuteNonQuery();
            string ParentLoginID = outPutParameter.Value.ToString();

            if (ParentLoginID != string.Empty && ParentLoginID != "" && ParentLoginID != null)
            {
                i = int.Parse(ParentLoginID);
            }
           
            return i;
        }

        public int CreateStudentLogin(StudentRegistration objnew, string mStudentLoginID, string mStudentPassword, int mParentID)
        {
            int i = 0;

            SqlCommand cmd = new SqlCommand("Add_StudentLogin", _mConn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = _mTran;

            SqlParameter outPutParameter = new SqlParameter();
            outPutParameter.ParameterName = "@StudentLoginID";
            outPutParameter.SqlDbType = SqlDbType.Int;
            outPutParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outPutParameter);

            cmd.Parameters.AddWithValue("@StudentID ", objnew.StudentID);
            cmd.Parameters.AddWithValue("@StudentUserName", mStudentLoginID);
            cmd.Parameters.AddWithValue("@Password", mStudentPassword);
            cmd.Parameters.AddWithValue("@ParentID", int.Parse(Session["CompID"].ToString()));
            cmd.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmd.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            cmd.ExecuteNonQuery();
            string ParentLoginID = outPutParameter.Value.ToString();

            if (ParentLoginID != string.Empty && ParentLoginID != "" && ParentLoginID != null)
            {
                i = int.Parse(ParentLoginID);
            }

            return i;
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewRegistration(StudentRegistration obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

            if (ModelState.IsValid)
            {
                try
                {
                    obj.CompID   = byte.Parse(Session["CompID"].ToString());
                    obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                    obj.UIDAdd   = byte.Parse(Session["UserID"].ToString());
                    obj.AddDate  = DateTime.Now;
                    obj.SessionID = int.Parse(Session["SessionID"].ToString());
                    obj.TransportRequired = false;
                    obj.EnableSMSFeature = false;
                    obj.HostelRequired = false;
                    obj.TCGiven = false;

                    unitOfWork.studentRegistrationService.AddNewRegistrationInfo(obj);
                    unitOfWork.Save();
                    if (obj.StudentID != null)
                    {

                        unitOfWork.studentSessionService.AddNewStudentinSession(obj);
                        unitOfWork.Save();
                        int i = 0; int newID = 0;
                        if (obj.FatherName.Length > 0)
                        {
                            string mparentUserName = obj.FatherName + obj.StudentID;
                            string mPassword = "abcd";

                             i = CreateParentLogin(obj, mparentUserName, mPassword);
                        }
                        if (i > 0)
                        {
                            string mStudentUserName = obj.FirstName + obj.LastName + obj.StudentID;
                            string mStudentPassword = "abcd";
                            newID = CreateStudentLogin(obj, mStudentUserName, mStudentPassword, i);
                        }
                       

                        if (newID>0 && i>0)
                            _mTran.Commit();
                        else
                            _mTran.Rollback();

                    }
                

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableHouse"] = obj;
            //return PartialView("ListRegistrations", unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));

            return PartialView("GridViewPartial", unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public void SaveUserLogForUpdate(modelRegistration obj)
        {
            SqlCommand cmdMaster = new SqlCommand("UPDATE_StudentRegistrationHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@StudentID", obj.StudentID);
            cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", false);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
            cmdMaster.ExecuteNonQuery();

        }
        public void SaveUserLogForDelete(modelRegistration obj)
        {
            SqlCommand cmdMaster = new SqlCommand("UPDATE_StudentRegistrationHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@StudentID", obj.StudentID);
            cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", true);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            cmdMaster.ExecuteNonQuery();

        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateRegistration(modelRegistration obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            if (ModelState.IsValid)
            {
                try
                {

                    obj.CompID = byte.Parse(Session["CompID"].ToString());
                    obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForUpdate(obj);
                        _mTran.Commit();
                    }
                    unitOfWork.studentRegistrationService.UpdateRegistrationInfo(obj, byte.Parse(Session["UserID"].ToString()));
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableHouse"] = obj;
            //return PartialView("ListRegistrations", unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));

            return PartialView("GridViewPartial", unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteRegistration(modelRegistration obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {
                int RowCount = unitOfWork.studentRegistrationService.CheckDelete(obj.StudentID);
                if (RowCount == 0)
                {
                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForDelete(obj);
                        _mTran.Commit();
                    }
                }
                if (RowCount == 0)
                {
                    unitOfWork.studentRegistrationService.Delete(obj);
                    unitOfWork.Save();
                }
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("GridViewPartial", unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
       

        [HttpPost]

        public JsonResult EditPersonaldetailbyClick(int mStudentID)
        {
            string Displaymsg = string.Empty;
            modelStudentPersonalInfo obj = unitOfWork.studentRegistrationService.GetPersonalInfo(mStudentID);

            
            
            if (obj == null)
            {
                Displaymsg = "Data Not Found";
            }
            else
            {
                Displaymsg = " ";
            }
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    Resultmsg = Displaymsg,
                    ResultData = cCommon.RenderRazorViewToString("PersonalInfoPartial", obj, ControllerContext, ViewData, TempData)
                }
            };

        }

        public JsonResult RefreshPersonaldetailAfterUpdate(int mStudentID)
        {
            string Displaymsg = string.Empty;

            List<StudentRegistration> objList = unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {

                    ResultData = cCommon.RenderRazorViewToString("GridViewPartial", objList, ControllerContext, ViewData, TempData)
                }
            };

        }

        public JsonResult EditParentdetailbyClick(int mStudentID)
        {
            // if (Session["UserID"] == null) { return Redirect("~/"); }

            modelGuardianInfo obj = unitOfWork.studentRegistrationService.GetGuardianInfo(mStudentID);

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    Resultmsg = "",
                    ResultData = cCommon.RenderRazorViewToString("GuardianPartial", obj, ControllerContext, ViewData, TempData)
                }

            };


        }

        public JsonResult EditPreviousDetail(int mStudentID)
        {
            //if (Session["UserID"] == null) { return Redirect("~/"); }
            StudentPreviousDetail obj = unitOfWork.studentPriviousDetailsService.GetStudentPriviousInfo(mStudentID);
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    Resultmsg = "",
                    ResultData = cCommon.RenderRazorViewToString("StudentPriviousPartial", obj, ControllerContext, ViewData, TempData)
                }

            };

        }

        public ActionResult EditStudentBackLockSessionDetailbyClick(int mStudentID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("ListStudentPrevioussessiondetail", unitOfWork.studentRegistrationService.GetStudentAllSessiondetailbyStudentID(mStudentID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));

        }

        public ActionResult EditUpLoadDocsbyClick(int mStudentID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            modelStudentDocumentUploadInfo obj = unitOfWork.studentRegistrationService.GetStudentDocumentInfo(mStudentID);
            return PartialView("UploadDocumentPartial", obj);
        }

        public ActionResult EditUpLoadPhotobyClick(int mStudentID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            modelStudentPhotoUploadInfo obj = unitOfWork.studentRegistrationService.GetStudentPhotoInfo(mStudentID);
            return PartialView("UploadPhotoPartial", obj);
        }




        [HttpPost]
        public JsonResult UpdateProfileInfoOld(modelStudentPersonalInfo obj)
        {
            string DisplayMsg = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.studentRegistrationService.UpdatePersonalInfo(obj);
                    unitOfWork.Save();

                    DisplayMsg = "Update Successfully !";

                    
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    DisplayMsg = e.Message;
                }
            }
            else
            {
                ViewData["EditError"] = "Please, correct all errors.";
                DisplayMsg = "Please, correct all errors.";
            }
            List<StudentRegistration> objList = unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    Resultmsg = DisplayMsg,
                    //ResultData = cCommon.RenderRazorViewToString("ListRegistrations", objList, ControllerContext, ViewData, TempData)
                    ResultData = cCommon.RenderRazorViewToString("GridViewPartial", objList, ControllerContext, ViewData, TempData)
                }
            };


        }


        [HttpPost]
        public ActionResult UpdateProfileInfo(modelStudentPersonalInfo obj)
        {
            //string DisplayMsg = string.Empty;
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        unitOfWork.studentRegistrationService.UpdatePersonalInfo(obj);
            //        unitOfWork.Save();

            //        DisplayMsg = "Update Successfully !";


            //    }
            //    catch (Exception e)
            //    {
            //        ViewData["EditError"] = e.Message;
            //        DisplayMsg = e.Message;
            //    }
            //}
            //else
            //{
            //    ViewData["EditError"] = "Please, correct all errors.";
            //    DisplayMsg = "Please, correct all errors.";
            //}
            //List<StudentRegistration> objList = unitOfWork.studentRegistrationService.GetRegistrationGridData(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            //return new JsonResult()
            //{
            //    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            //    Data = new
            //    {
            //        Resultmsg = DisplayMsg,
            //        //ResultData = cCommon.RenderRazorViewToString("ListRegistrations", objList, ControllerContext, ViewData, TempData)
            //        ResultData = cCommon.RenderRazorViewToString("GridViewPartial", objList, ControllerContext, ViewData, TempData)
            //    }
            //};

            obj.Caste = "castechange";

            //return PartialView("PersonalInfoPartial", obj);
            return View("Index");

        }


        [HttpPost]
        public JsonResult UpdateGuardianInfo(modelGuardianInfo obj)
        {
            //  if (Session["UserID"] == null) { return Redirect("~/"); }
            string DisplayMsg = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.studentRegistrationService.UpdateGuardianInfo(obj);
                    unitOfWork.Save();
                    DisplayMsg = "Update Successfully !";
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    DisplayMsg = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    Resultmsg = DisplayMsg,
                    ResultData = cCommon.RenderRazorViewToString("GuardianEditPartial", obj, ControllerContext, ViewData, TempData)
                }
            };

        }

        [HttpPost]
        public JsonResult StudentPriviousInfoSaveANDUpdate(StudentPreviousDetail obj)
        {
            //if (Session["UserID"] == null) { return Redirect("~/"); }
            string DisplayMsg = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {

                    int CheckRow = unitOfWork.studentPriviousDetailsService.CheckPreviousDatainTable(obj.StudentID);
                    if (CheckRow > 0)
                    {
                        obj.UIDMod = byte.Parse(Session["UserID"].ToString());
                        obj.ModDate = DateTime.Now;
                        obj.CompID = byte.Parse(Session["CompID"].ToString());
                        obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                        obj.SessionID = byte.Parse(Session["SessionID"].ToString());
                        unitOfWork.studentPriviousDetailsService.UpdateStudentPriviousDetail(obj);
                        unitOfWork.Save();
                        DisplayMsg = "Update Successfully";
                    }
                    else
                    {

                        obj.CompID = byte.Parse(Session["CompID"].ToString());
                        obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                        obj.SessionID = byte.Parse(Session["SessionID"].ToString());
                        obj.UIDAdd = byte.Parse(Session["UserID"].ToString());
                        obj.AddDate = DateTime.Now;
                        unitOfWork.studentPriviousDetailsService.Insert(obj);
                        unitOfWork.Save();
                    }

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                    DisplayMsg = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    Resultmsg = DisplayMsg,
                    ResultData = cCommon.RenderRazorViewToString("StudentPriviousEditPartial", obj, ControllerContext, ViewData, TempData)
                }
            };
            //return PartialView("StudentPriviousEditPartial", obj);

        }


        public ActionResult CallbacksImageUpload(IEnumerable<UploadedFile> ucCallbacks)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return null;
        }


        [HttpPost]
        public ActionResult UploadControlCallbackAction(modelStudentPhotoUploadInfo obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            UploadControlDemosHelper objup = new UploadControlDemosHelper();
            objup.mEnrollmentNo = obj.EnrollmentNo;
            objup.mStudentID = obj.StudentID;

            //  string ImageName = System.IO.Path.GetFileName(file.FileName);
            //string physicalPath = Server.MapPath("~/Images/students/" + ImageName);

            //// save image in folder
            //file.SaveAs(physicalPath);
            //RenderImage ren = new RenderImage();
            //ren.UploadImageToDB(file);
            //string name = obj1.FileName.Replace(obj1.UploadedFile.FileName, obj.EnrollmentNo);
            //obj1.FileName.Replace(obj1.FileName, obj.EnrollmentNo + ".jpg");

            //UploadControlExtension.GetUploadedFiles("uc", UploadControlDemosHelper.ValidationSettings, UploadControlDemosHelper.uc_FileUploadComplete);

            return null;

            // return    RedirectToAction("ApplicationImageView");
        }

        [HttpPost]
        public ActionResult UploadDocument(modelStudentDocumentUploadInfo obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            UploadControlDocument objup = new UploadControlDocument();
            objup.mEnrollmentNo = obj.EnrollmentNo;
            objup.mStudentID = obj.StudentID;
            objup.mDocumentName = obj.DocumentName;
            objup.mRemark = obj.Remark;
            objup.mUidAdd = int.Parse(Session["UserID"].ToString());
            objup.mSessionID = int.Parse(Session["SessionID"].ToString());
            objup.mCompID = byte.Parse(Session["CompID"].ToString());
            objup.mBranchID = byte.Parse(Session["branchID"].ToString());

            //  string ImageName = System.IO.Path.GetFileName(file.FileName);
            //string physicalPath = Server.MapPath("~/Images/students/" + ImageName);

            //// save image in folder
            //file.SaveAs(physicalPath);
            //RenderImage ren = new RenderImage();
            //ren.UploadImageToDB(file);
            //string name = obj1.FileName.Replace(obj1.UploadedFile.FileName, obj.EnrollmentNo);
            //obj1.FileName.Replace(obj1.FileName, obj.EnrollmentNo + ".jpg");

           // UploadControlExtension.GetUploadedFiles("ucDoc", UploadControlDocument.ValidationSettings, UploadControlDocument.uc_FileUploadComplete);

            return null;

            // return    RedirectToAction("ApplicationImageView");
        }




    }

    public class UploadControlDemosHelper
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

        //public static readonly DevExpress.Web.UploadControlValidationSettings UploadControlValidationSettings = new DevExpress.Web.UploadControlValidationSettings
        //{
        //    AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif", ".bmp", ".png", },
        //    MaxFileSize = 40971520,
        //};

        public static void uc_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            if (e.UploadedFile.IsValid)
            {


                string name = e.UploadedFile.FileName.Replace(e.UploadedFile.FileName, _StudentID.ToString() + ".png");

                string resultFilePath = HttpContext.Current.Request.MapPath(UploadDirectory + name);
                e.UploadedFile.SaveAs(resultFilePath, true);//Code Central Mode - Uncomment This Line

                Stream strm = e.UploadedFile.PostedFile.InputStream;
                //Guid uid = Guid.NewGuid();
                //if (e.UploadedFile.ContentLength <= 1048576)
                //{
                //    System.Drawing.Bitmap bmpUploadImage = new System.Drawing.Bitmap(strm);
                //    System.Drawing.Image objImage = cCommon.ScaleImage(bmpUploadImage, 150);
                //    objImage.Save(resultFilePath);

                //}
                var targetFile = resultFilePath;
                //cCommon.GenerateThumbnails(0.5, strm, targetFile);

                appSchool.Repositories.StudentRegistration img = new appSchool.Repositories.StudentRegistration();
                img.StudentID = _StudentID;
                img.AppImageName = name;

                //byte[] imageBytes = null;

                //imageBytes = e.UploadedFile.FileBytes;

                //img.AppImage = imageBytes;

                UnitOfWork obj = new UnitOfWork();

                obj.studentRegistrationService.UpdateStudentPhoto(img);
                obj.Save();

                IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                if (urlResolver != null)
                {
                    e.CallbackData = urlResolver.ResolveClientUrl("Successfully Uploaded. ");
                }
            }
        }





    }

    public class UploadControlDocument
    {
        public static string _EnrollmentNo = string.Empty;
        public static int _StudentID;
        public static string _DocumentName;
        public static string _FileName;
        public static string _Remark;
        public static int _UidAdd;
        public static int _SessionID;
        public static byte _CompID;
        public static byte _BranchID;




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


        public byte mCompID
        {
            set { _CompID = value; }
            get { return _CompID; }
        }

        public byte mBranchID
        {
            set { _BranchID = value; }
            get { return _BranchID; }
        }


        public const string UploadDirectory = "~/Images/studentDocuments";

        //public static readonly DevExpress.Web.UploadControlValidationSettings UploadControlValidationSettings = new DevExpress.Web.UploadControlValidationSettings
        //{
        //    AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif", ".bmp", ".png", },
        //    MaxFileSize = 50971520,
        //};

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

                e.UploadedFile.SaveAs(resultFilePath, true);//Code Central Mode - Uncomment This Line


                Stream strm = e.UploadedFile.PostedFile.InputStream;
                //Guid uid = Guid.NewGuid();
                //if (e.UploadedFile.ContentLength <= 1048576)
                //{
                //    System.Drawing.Bitmap bmpUploadImage = new System.Drawing.Bitmap(strm);
                //    System.Drawing.Image objImage = cCommon.ScaleImage(bmpUploadImage, 700);
                //    objImage.Save(resultFilePath);

                //}


                var targetFile = resultFilePath;
                cCommon.GenerateThumbnails(0.5, strm, targetFile);

                appSchool.Repositories.StudentDocumentDetail img = new appSchool.Repositories.StudentDocumentDetail();
                img.StudentID = _StudentID;
                img.DocumentName = _DocumentName;
                img.Remark = _Remark;
                img.FileName = name;
                img.UIDAdd = _UidAdd;
                img.AddDate = DateTime.Now;
                img.SessionID = _SessionID;
                img.CompID = _CompID;
                img.BranchID = _BranchID;

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

