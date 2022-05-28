using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using System.IO;
using appSchool.Model;
using DevExpress.Web.ASPxHtmlEditor;


namespace appSchool.Repositories
{
    public class LetterHeadMasterRepository : GenericRepository<LetterHeadMaster>
    {
        public LetterHeadMasterRepository() : base(new dbSchoolAppEntities()) { }
        public LetterHeadMasterRepository(dbSchoolAppEntities dbContext) : base(dbContext) { }



        public void InsertData(LetterHeadMaster obj)
        {
            this.Insert(obj);
        }


        public List<LetterHeadMaster> GetLetterHedMasterList(byte mCompID, byte mBranchID)
        {
            List<LetterHeadMaster> obj = new List<LetterHeadMaster>();
            obj = this.context.LetterHeadMasters.Where(x => x.LetterHeadID > 0 && x.CompID == mCompID && x.BranchID == mBranchID).OrderBy(x => x.LetterHeadID).ToList();
            return obj;
        }


        public void AddNewLetterHeadMaster(LetterHeadMaster obj)
        {
            this.Insert(new LetterHeadMaster() { LetterHeadName = obj.LetterHeadName,LetterHeadDesc= obj.LetterHeadDesc, LetterHeadOrder = obj.LetterHeadOrder, IsActive = obj.IsActive, FromDate = obj.FromDate, ToDate = obj.ToDate, CompID = obj.CompID, BranchID = obj.BranchID, });
            return;
        }

        public void UpdateLetterHeadMaster(LetterHeadMaster obj)
        {
            LetterHeadMaster c = this.GetByID(obj.LetterHeadID);
            c.LetterHeadName = obj.LetterHeadName;
            c.LetterHeadDesc = obj.LetterHeadDesc;
            c.LetterHeadOrder = obj.LetterHeadOrder;
            c.IsActive = obj.IsActive;
            //c.FromDate = obj.FromDate;
            //c.ToDate = obj.ToDate;
            c.IsActive = obj.IsActive;

            this.Update(c);
            return;
        }

        public void UpdateLetterHeadDescCription(LetterHeadMaster obj)
        {
            LetterHeadMaster c = this.GetByID(obj.LetterHeadID);
           
            c.LetterHeadDesc = obj.LetterHeadDesc;
           

            this.Update(c);
            return;
        }


        public void DeleteLetterHeadMaster(LetterHeadMaster obj)
        {
            LetterHeadMaster c = this.GetByID(obj.LetterHeadID);
            c.LetterHeadName = obj.LetterHeadName;
            c.LetterHeadOrder = obj.LetterHeadOrder;
            c.FromDate = obj.FromDate;
            c.ToDate = obj.ToDate;
            c.IsActive = obj.IsActive;


            this.Delete(c);
            return;
        }

        public int CheckThoughtDelete(int mThoughtID)
        {
            int ID = 0;
            ID = this.context.ThoughtMasters.Where(x => x.ThoughtID == mThoughtID).Count();
            return ID;
        }



    }





    public class HtmlEditorFeaturesDemosHelper {
        public const string ImagesDirectory = "~/Content/HtmlEditor/Images/";
        public const string ThumbnailsDirectory = "~/Content/HtmlEditor/Thumbnails/";
        public const string UploadDirectory = "~/Content/HtmlEditor/UploadFiles/";
        public const string HtmlLocation = "~/Content/HtmlEditor/DemoHtml/";

        //public static readonly UploadControlValidationSettings ImageUploadValidationSettings = new UploadControlValidationSettings {
        //    AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif", ".png" },
        //    MaxFileSize = 4000000
        //};

        //static HtmlEditorFileSaveSettings fileSaveSettings;
        //public static HtmlEditorFileSaveSettings FileSaveSettings {
        //    get {
        //        if(fileSaveSettings == null) {
        //            fileSaveSettings = new HtmlEditorFileSaveSettings();
        //            fileSaveSettings.FileSystemSettings.UploadFolder = ImagesDirectory + "Upload/";
        //        }
        //        return fileSaveSettings;
        //    }
        //}

        static MVCxHtmlEditorImageSelectorSettings imageSelectorSettings;
        public static HtmlEditorImageSelectorSettings ImageSelectorSettings {
            get {
                if(imageSelectorSettings == null) {
                    imageSelectorSettings = new MVCxHtmlEditorImageSelectorSettings();
                    SetHtmlEditorImageSelectorSettings(imageSelectorSettings);
                }
                return imageSelectorSettings;
            }
        }
        public static MVCxHtmlEditorImageSelectorSettings SetHtmlEditorImageSelectorSettings(MVCxHtmlEditorImageSelectorSettings settingsImageSelector) {
            settingsImageSelector.UploadCallbackRouteValues = new { Controller = "Features", Action = "FeaturesImageSelectorUpload" };

            settingsImageSelector.Enabled = true;
            settingsImageSelector.CommonSettings.RootFolder = ImagesDirectory;
            settingsImageSelector.CommonSettings.ThumbnailFolder = ThumbnailsDirectory;
            settingsImageSelector.CommonSettings.AllowedFileExtensions = new string[] { ".jpg", ".jpeg", ".jpe", ".gif", ".png" };
            settingsImageSelector.EditingSettings.AllowCreate = true;
            settingsImageSelector.EditingSettings.AllowDelete = true;
            settingsImageSelector.EditingSettings.AllowMove = true;
            settingsImageSelector.EditingSettings.AllowRename = true;
            settingsImageSelector.UploadSettings.Enabled = true;
            settingsImageSelector.FoldersSettings.ShowLockedFolderIcons = true;

            settingsImageSelector.PermissionSettings.AccessRules.Add(
                new FileManagerFolderAccessRule {
                    Path = "",
                    Upload = Rights.Deny
                });
            return settingsImageSelector;
        }

        public static string GeHtmlContentByFileName(string fileName) {
            return System.IO.File.ReadAllText(System.Web.HttpContext.Current.Request.MapPath(string.Format("{0}{1}", HtmlLocation, fileName)));
        }
        public static string GeHtmlContentByFileName(string fileName, bool demoPageIsInRoot) {
            string result = GeHtmlContentByFileName(fileName);
            return demoPageIsInRoot ? result : result.Replace("Content/", "../Content/");
        }
    }






}