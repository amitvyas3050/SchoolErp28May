using appSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.ViewModels;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Text;
using appSchool.Model;
using System.IO;
using DevExpress.Web.Mvc;
using DevExpress.Web;


namespace appSchool.Controllers
{
        [NoCache]
    public class FlyerVoiceMasterController : Controller
    {
        public static readonly object RootFolder = "~/Images/FlyerVoice";
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;


        public ActionResult FlyerVoiceMasterView()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appFlyerVoice == 0)
            {
                return Redirect("~/");
            }

            //MenuID of Classes=11
            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 121, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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



            return PartialView("Index", RootFolder);
        }

        //public ActionResult PartialGridFlyerVoiceMaster(bool mShowFilter)
        //{
        //     if (Session["UserID"] == null) { return Redirect("~/"); }
        //     ViewBag.ShowFilter = mShowFilter;
        //     return PartialView("ListFlyerVoiceMaster", unitOfWork.flyervoicemasterService.GetFlyerVoiceMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        //}

        //[ValidateInput(false)]
        public ActionResult VoiceGalleryPartial()
        {
            //   ArtsFileSystemProvider obj=new ArtsFileSystemProvider(RootFolder);
            return PartialView("VoiceGalleryPartial", RootFolder);
        }
        //public FileStreamResult DownloadFiles()
        //{
        //    return FileManagerExtension.DownloadFiles("FileManager", (string)RootFolder);
        //}


        [HttpPost, ValidateInput(false)]

        public ActionResult EventGridRowChange(int RegID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            return PartialView("AchievementTabs", RegID);
        }

        public ActionResult RefreshNewsEventGrid(bool mShowFilter)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewBag.ShowFilter = mShowFilter;
            return PartialView("ListNewsEventMaster", unitOfWork.NewsEventMasterService.GetNewsEventMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public void SaveUserLogForUpdate(FlyerVoiceMaster obj)
        {
            SqlCommand cmdMaster = new SqlCommand("Update_ClassHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            //cmdMaster.Parameters.AddWithValue("@ClassId", obj.ClassID);
            //cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            //cmdMaster.Parameters.AddWithValue("@IsDeleted", false);
            //cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            //cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
            //cmdMaster.ExecuteNonQuery();
            
        }
        public void SaveUserLogForDelete(FlyerVoiceMaster obj)
        {
            SqlCommand cmdMaster = new SqlCommand("Update_ClassHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            //cmdMaster.Parameters.AddWithValue("@ClassId", obj.ClassID);
            //cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            //cmdMaster.Parameters.AddWithValue("@IsDeleted", true);
            //cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            //cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            cmdMaster.ExecuteNonQuery();
         
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewFlyerVoiceMaster(FlyerVoiceMaster objNews)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            if (ModelState.IsValid)
            {
                try
                {
                    objNews.CompID = byte.Parse(Session["CompID"].ToString());
                    objNews.BranchID = byte.Parse(Session["BranchID"].ToString());
                    
                    
                   
                    unitOfWork.flyervoicemasterService.AddNewFlyerVoiceMaster(objNews);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please Fill all Field, & correct all errors.";
            ViewData["EditableClass"] = objNews;
            return PartialView("ListFlyerVoiceMaster", unitOfWork.flyervoicemasterService.GetFlyerVoiceMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateFlyerVoiceMaster(FlyerVoiceMaster objNews)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
          
            if (ModelState.IsValid)
            {
                try
                {

                    objNews.CompID = byte.Parse(Session["CompID"].ToString());
                    objNews.BranchID = byte.Parse(Session["BranchID"].ToString());
                   
                    

                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForUpdate(objNews);
                    }
                    unitOfWork.flyervoicemasterService.UpdateFlyerVoiceMaster(objNews);
                   unitOfWork.Save();

                   _mTran.Commit();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please Fill all Field, & correct all errors.";
            ViewData["EditableClass"] = objNews;
            return PartialView("ListFlyerVoiceMaster", unitOfWork.flyervoicemasterService.GetFlyerVoiceMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
 
        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteAchievement(FlyerVoiceMaster objNews)
        {
            //if (Session["UserID"] == null) { return Redirect("~/"); }
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

            try
            {
                int RowsCount = unitOfWork.achievementService.CheckEventDelete(objNews.FlyerVoiceID);
                if (RowsCount == 0)
                {
                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForDelete(objNews);
                        _mTran.Commit();
                    }
                }



                #region Delete Old Method
                if (RowsCount == 0)
                {
                    unitOfWork.flyervoicemasterService.DeleteFlyerVoiceMaster(objNews);
                    unitOfWork.Save();
                }
                else
                {
                }
                #endregion

            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("ListFlyerVoiceMaster", unitOfWork.flyervoicemasterService.GetFlyerVoiceMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult UpdateVoiceFile(string PFlyerVoice)
        {
            string ErrorMsg = string.Empty;
            //string FolderPath = "~/FlyerVoice/Voice";
            int i = 1;
            bool mResult = false;
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction();


            string FolderName = string.Empty;
            FolderName = "Voice";


            string NewFolderName = "~/Images/FlyerVoice/" + FolderName;
            //List<ImageList> objlst = new List<ImageList>();
            string PathName = System.Web.HttpContext.Current.Request.MapPath(NewFolderName);
            if (Directory.Exists(PathName))
            {
                string[] FileList = Directory.GetFiles(PathName);

                if (FileList.Length > 0)
                {
                    string sqldelete = "delete  From FlyerVoiceMaster";
                    SqlCommand cmddelete = new SqlCommand(sqldelete, _mConn);
                    cmddelete.Transaction = _mTran;
                    cmddelete.CommandType = CommandType.Text;
                    int recdelete = cmddelete.ExecuteNonQuery();

                    StringBuilder mQuery = new StringBuilder();
                    mQuery.Append(" Insert INTO FlyerVoiceMaster (FlyerVoiceName, FileName,Isactive,CompID,BranchID) ");
                    foreach (string s in FileList)
                    {
                        string Newfile = Path.GetFileName(s);
                        mQuery.Append("SELECT '" + Newfile + "' ,'" + Newfile + "'," + 0 + ", " + byte.Parse(Session["CompID"].ToString()) + ", " + byte.Parse(Session["BranchID"].ToString()) + "  ");
                        if (i < FileList.Count())
                        {
                            mQuery.Append(" Union All ");
                            i++;
                        }

                    }

                    SqlCommand cmdDetail = new SqlCommand("Execute_SQL", _mConn);
                    cmdDetail.Transaction = _mTran;
                    cmdDetail.CommandType = CommandType.StoredProcedure;
                    cmdDetail.Parameters.Add("@MQuery", SqlDbType.VarChar).Value = mQuery.ToString();
                    int recs = cmdDetail.ExecuteNonQuery();

                    mResult = (recs > 0) ? true : false;
                    if (mResult)
                    {
                        _mTran.Commit();
                    }
                    else
                    {
                        _mTran.Rollback();
                    }

                    ErrorMsg = "Data inserted successfully";
                }
                else
                {
                    ErrorMsg = "Images Not Found";
                }


            }
            else
            {
                ErrorMsg = "Path not Exists.";
            }


            return PartialView("ListFlyerVoiceMaster", unitOfWork.flyervoicemasterService.GetFlyerVoiceMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
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

        public ActionResult UpdatePartialFileActivation(MVCxGridViewBatchUpdateValues<FlyerVoiceMaster, int> updateValues)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            try
            {
                foreach (var product in updateValues.Update)
                {
                    if (updateValues.IsValid(product))
                        UpdateStructure(product, updateValues);
                }
            }
            catch (Exception ex)
            {

            }
            return PartialView("ListFlyerVoiceMaster", unitOfWork.flyervoicemasterService.GetFlyerVoiceMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        protected void UpdateStructure(FlyerVoiceMaster product, MVCxGridViewBatchUpdateValues<FlyerVoiceMaster, int> updateValues)
        {
            try
            {
                unitOfWork.flyervoicemasterService.UpdateVoiceActive(product, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }



        

    }


        public class FileManagerDemoVoiceHelper
        {
            static ArtsFileSystemProvider artsFileSystemProvider;

            public static readonly object RootFolder = "~/Content/FileManager/Files";
            public static readonly object ImagesRootFolder = "~/Content/FileManager/Files/Images";
            public static readonly string[] AllowedFileExtensions = new string[] {
            ".jpg", ".jpeg", ".gif", ".rtf", ".txt", ".avi", ".png", ".mp3", ".xml", ".doc", ".pdf"
        };
            public static FileManagerFeaturesDemoOptions FeaturesDemoOptions
            {
                get
                {
                    if (HttpContext.Current.Session["FeaturesDemoOptions"] == null)
                        HttpContext.Current.Session["FeaturesDemoOptions"] = new FileManagerFeaturesDemoOptions();
                    return (FileManagerFeaturesDemoOptions)HttpContext.Current.Session["FeaturesDemoOptions"];
                }
                set { HttpContext.Current.Session["FeaturesDemoOptions"] = value; }
            }
            public static ArtsFileSystemProvider ArtsFileSystemProvider
            {
                get
                {
                    if (artsFileSystemProvider == null)
                        artsFileSystemProvider = new ArtsFileSystemProvider(string.Empty);
                    return artsFileSystemProvider;
                }
            }

            public static void ValidateSiteEdit(FileManagerActionEventArgsBase e)
            {
                e.Cancel = Utils.IsSiteMode;
                //   e.ErrorText = Utils.GetReadOnlyMessageText();
            }
            public static List<SelectListItem> GetSecurityRoles()
            {
                return new List<SelectListItem>() {
                new SelectListItem() { Text = "Default User", Value = SecurityVoiceRole.DefaultUser.ToString(), Selected = true },
                new SelectListItem() { Text = "Document Manager", Value = SecurityVoiceRole.DocumentManager.ToString() },
                new SelectListItem() { Text = "Media Moderator", Value = SecurityVoiceRole.MediaModerator.ToString() },
                new SelectListItem() { Text = "Administrator", Value = SecurityVoiceRole.Administrator.ToString() }
            };
            }
            public static List<SelectListItem> GetFileListViewModes()
            {
                return new List<SelectListItem>() {
                new SelectListItem() { Text = FileListView.Thumbnails.ToString(), Value = FileListView.Thumbnails.ToString(), Selected = true },
                new SelectListItem() { Text = FileListView.Details.ToString(), Value = FileListView.Details.ToString() }
            };
            }
        }

        public class FileManagerFeaturesVoiceDemoOptions
        {
            FileManagerSettingsEditing settingsEditing;
            FileManagerSettingsToolbar settingsToolbar;
            FileManagerSettingsFolders settingsFolders;
            MVCxFileManagerSettingsUpload settingsUpload;

            public FileManagerFeaturesVoiceDemoOptions()
            {
                this.settingsEditing = new FileManagerSettingsEditing(null)
                {
                    AllowCreate = true,
                    AllowMove = true,
                    AllowDelete = true,
                    AllowRename = true,
                    AllowCopy = true
                };
                this.settingsToolbar = new FileManagerSettingsToolbar(null)
                {
                    ShowPath = true,
                    ShowRefreshButton = true,
                    ShowFilterBox = true,
                    ShowDownloadButton = false
                };
                this.settingsFolders = new FileManagerSettingsFolders(null)
                {
                    Visible = true,
                    EnableCallBacks = false,
                    ShowFolderIcons = true,
                    ShowLockedFolderIcons = true
                };
                this.settingsUpload = new MVCxFileManagerSettingsUpload();
                this.settingsUpload.Enabled = true;
                this.settingsUpload.AdvancedModeSettings.EnableMultiSelect = true;
            }

            public FileManagerSettingsEditing SettingsEditing { get { return settingsEditing; } }
            public FileManagerSettingsToolbar SettingsToolbar { get { return settingsToolbar; } }
            public FileManagerSettingsFolders SettingsFolders { get { return settingsFolders; } }
            public MVCxFileManagerSettingsUpload SettingsUpload { get { return settingsUpload; } }
        }

        public class FileManagerDetailsViewModeDemoVoiceOptions
        {
            public FileManagerDetailsViewModeDemoVoiceOptions()
            {
                AllowColumnResize = true;
                AllowColumnDragDrop = true;
                AllowColumnSort = true;
                ShowHeaderFilterButton = false;
            }

            public bool AllowColumnResize { get; set; }
            public bool AllowColumnDragDrop { get; set; }
            public bool AllowColumnSort { get; set; }
            public bool ShowHeaderFilterButton { get; set; }
        }

        public enum SecurityVoiceRole { DefaultUser, DocumentManager, MediaModerator, Administrator }
}
