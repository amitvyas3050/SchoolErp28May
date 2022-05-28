using appSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.ViewModels;
using System.Data.SqlClient;
using System.Data;

namespace appSchool.Controllers
{
    [NoCache]
    public class SMSTemplateController : Controller
    {
        //
        // GET: /SMSTemplate/

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
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 11, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

        public string GetTemplateMesssageText(int mTemplateID)
        {
         
            return unitOfWork.smsTemplateService.GetByID(mTemplateID).TemplateMessage;
        }
        public ActionResult PartialGridSMSTemplate()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("GridViewPartial", unitOfWork.smsTemplateService.GetSMSTemplateList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewSMSTemplate(SMSTemplate objSMSTemplate)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {
                    objSMSTemplate.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    objSMSTemplate.AddDate = DateTime.Now;
                    objSMSTemplate.CompID = byte.Parse(Session["CompID"].ToString());
                    objSMSTemplate.BranchID = byte.Parse(Session["BranchID"].ToString());

                    unitOfWork.smsTemplateService.Insert(objSMSTemplate);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = objSMSTemplate;
           // SMSTemplate obj = new SMSTemplate();
           // return PartialView("SMSTemplateForm",obj);

            return PartialView("GridViewPartial", unitOfWork.smsTemplateService.GetSMSTemplateList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));

        }

        public void SaveUserLogForUpdate(SMSTemplate objSMSTemplate)
        {
            SqlCommand cmdMaster = new SqlCommand("UPDATE_SMSTemplateHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@TemplateID", objSMSTemplate.TemplateID);
            cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", false);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
            cmdMaster.ExecuteNonQuery();

        }
        public void SaveUserLogForDelete(SMSTemplate objSMSTemplate)
        {
            SqlCommand cmdMaster = new SqlCommand("UPDATE_SMSTemplateHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@TemplateID", objSMSTemplate.TemplateID);
            cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", true);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            cmdMaster.ExecuteNonQuery();

        }




        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateSMSTemplate(SMSTemplate objSMSTemplate)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                _mConn = DB.GetActiveConnection();
                _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
                try
                {
                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForUpdate(objSMSTemplate);
                        _mTran.Commit();
                    }
                    objSMSTemplate.UIDMod = byte.Parse(Session["UserID"].ToString());
                    objSMSTemplate.ModDate = DateTime.Now;
                    objSMSTemplate.CompID = byte.Parse(Session["CompID"].ToString());
                    objSMSTemplate.BranchID = byte.Parse(Session["BranchID"].ToString());

                    unitOfWork.smsTemplateService.UpdateSMSTemplate(objSMSTemplate);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = objSMSTemplate;

            return PartialView("GridViewPartial", unitOfWork.smsTemplateService.GetSMSTemplateList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteSMSTemplate(SMSTemplate objSMSTemplate)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {
                if (SettingMasterStaticClass._ManageHistory == true)
                {
                    SaveUserLogForDelete(objSMSTemplate);
                    _mTran.Commit();
                }
                unitOfWork.smsTemplateService.Delete(objSMSTemplate);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("ListSMSTemplate", unitOfWork.smsTemplateService.GetSMSTemplateList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public ActionResult SMSTemplateGridRowChange(int ID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("TemplateEntryPartial", ID);
        }

        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1;
            }
            return PartialGridSMSTemplate();
        }

    }
}
