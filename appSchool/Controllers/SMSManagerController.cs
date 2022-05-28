using appSchool.Repositories;
using appSchool.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace appSchool.Controllers
{
    [NoCache]
    public class SMSManagerController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;



        public ActionResult Index()
        {

            if (Session["UserID"] == null || (int)TopMenuModules.appSMSManager == 0)
            {


                return Redirect("~/");
            }

            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 102, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
            return View(new myModel() { currModule = appModule.appSMSManager });
        }

        public ActionResult SMSTypes()
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }
            return PartialView("SMSTypes");
        }

        //private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult SMSTypesView()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("SMSTypes", unitOfWork.SMSTypeService.GetSMSTypeList());
        }
        public ActionResult PartialGridSMSTypes()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("ListSMSType", unitOfWork.SMSTypeService.GetSMSTypeList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewSMSType(SMSType objSMSType)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {
                    objSMSType.CompID = byte.Parse(Session["CompID"].ToString());
                    objSMSType.BranchID = byte.Parse(Session["BranchID"].ToString());
                    objSMSType.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    objSMSType.AddDate = DateTime.Now;

                    unitOfWork.SMSTypeService.Insert(objSMSType);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = objSMSType;
            return PartialView("ListSMSType", unitOfWork.SMSTypeService.GetSMSTypeList());
        }

        public void SaveUserLogForUpdate(SMSType objSMSType)
        {
            SqlCommand cmdMaster = new SqlCommand("UPDATE_SMSTypeHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@SMSTypeID", objSMSType.SMSTypeID);
            cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", false);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
            cmdMaster.ExecuteNonQuery();

        }
        public void SaveUserLogForDelete(SMSType objSMSType)
        {
            SqlCommand cmdMaster = new SqlCommand("UPDATE_SMSTypeHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@SMSTypeID", objSMSType.SMSTypeID);
            cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", true);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            cmdMaster.ExecuteNonQuery();

        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateSMSType(SMSType objSMSType)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            if (ModelState.IsValid)
            {
                try
                {
                    objSMSType.UIDMod = byte.Parse(Session["UserID"].ToString());
                    objSMSType.ModDate = DateTime.Now;
                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForUpdate(objSMSType);
                        _mTran.Commit();
                    }
                    unitOfWork.SMSTypeService.UpdateSMSType(objSMSType);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = objSMSType;
            return PartialView("ListSMSType", unitOfWork.SMSTypeService.GetSMSTypeList());
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteSMSType(SMSType objSMSType)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {
                if (SettingMasterStaticClass._ManageHistory == true)
                {
                    SaveUserLogForDelete(objSMSType);
                    _mTran.Commit();
                }
                unitOfWork.SMSTypeService.Delete(objSMSType);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("ListSMSType", unitOfWork.SMSTypeService.GetSMSTypeList());
        }


        public ActionResult SMSTypeGridRowChange(int ID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("SMSTypeTabs", ID);
        }

    }
}
