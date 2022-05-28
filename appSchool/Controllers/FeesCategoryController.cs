using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.ViewModels;
using appSchool.Repositories;
using appSchool.ChartModels;
using System.Data.SqlClient;
using System.Data;

namespace appSchool.Controllers
{
    //[NoCache]
    public class FeesCategoryController : Controller
    {
        //
        // GET: /FeesManager/
        UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;


       
        #region FeesCategory

        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }

            UserPermission objuser = new UserPermission();
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
        public ActionResult FeeCategories()
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }

            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 11, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
            return PartialView("FeeCategories");
        }
        public ActionResult ListFeeCategoryPartial()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("ListFeeCategoryPartial", unitOfWork.feesCategoryService.GetFeesCategoryList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewFeeCategory(FeesCategory obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {
                    obj.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    obj.AddDate = DateTime.Now;
                    obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                    obj.CompID = byte.Parse(Session["CompID"].ToString());

                    unitOfWork.feesCategoryService.Insert(obj);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = obj;
            return PartialView("ListFeeCategoryPartial", unitOfWork.feesCategoryService.GetFeesCategoryList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public void SaveUserLogForUpdate(FeesCategory obj)
        {
            SqlCommand cmdMaster = new SqlCommand("UPDATE_FeesCategoryHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@CategoryID", obj.CategoryID);
            cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", false);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
            cmdMaster.ExecuteNonQuery();

        }
        public void SaveUserLogForDelete(FeesCategory obj)
        {
            SqlCommand cmdMaster = new SqlCommand("UPDATE_FeesCategoryHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@CategoryID", obj.CategoryID);
            cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", true);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            cmdMaster.ExecuteNonQuery();

        }




        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateFeeCategory(FeesCategory obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                _mConn = DB.GetActiveConnection();
                _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
                try
                {
                    obj.UIDMod = byte.Parse(Session["UserID"].ToString());
                    obj.ModDate = DateTime.Now;
                    obj.CompID = byte.Parse(Session["CompID"].ToString());
                    obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForUpdate(obj);
                        _mTran.Commit();
                    }
                    unitOfWork.feesCategoryService.UpdateFeesCategory(obj);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = obj;
            return PartialView("ListFeeCategoryPartial", unitOfWork.feesCategoryService.GetFeesCategoryList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteFeeCategory(FeesCategory obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {
                if (SettingMasterStaticClass._ManageHistory == true)
                {
                    SaveUserLogForDelete(obj);
                    _mTran.Commit();
                }
                unitOfWork.feesCategoryService.Delete(obj);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("ListFeeCategoryPartial", unitOfWork.feesCategoryService.GetFeesCategoryList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        #endregion




        #region InstalmentMaster
        public ActionResult InstalmentMaster()
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }

            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 11, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
            return PartialView("InstalmentMaster");

        }

        public ActionResult ListInstalmentMasterPartial()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("ListInstalmentMasterPartial", unitOfWork.instalmentMasterService.GetInstalmentMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewInstalmentMaster(InstalmentMaster obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {
                   
                    unitOfWork.instalmentMasterService.Insert(obj);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = obj;
            return PartialView("ListInstalmentMasterPartial", unitOfWork.instalmentMasterService.GetInstalmentMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateInstalmentMaster(InstalmentMaster obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.instalmentMasterService.Update(obj);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = obj;
            return PartialView("ListInstalmentMasterPartial", unitOfWork.instalmentMasterService.GetInstalmentMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteInstalmentMaster(InstalmentMaster obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            try
            {
                unitOfWork.instalmentMasterService.Delete(obj);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("ListInstalmentMasterPartial", unitOfWork.instalmentMasterService.GetInstalmentMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
        #endregion



        public ActionResult AccountMasterView()
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }
            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 11, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
            return PartialView("AccountMaster");

        }
        public ActionResult ListAccountMaster()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("ListAccountMaster", unitOfWork.accountMasterService.GetAccountMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
        public ActionResult PartialGridAccountMaster()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("ListAccountMaster", unitOfWork.accountMasterService.GetAccountMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewAccountMaster(AccountMaster obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.accountMasterService.Insert(obj);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = obj;
            return PartialView("ListAccountMaster", unitOfWork.accountMasterService.GetAccountMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateAccountMaster(AccountMaster obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.accountMasterService.Update(obj);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = obj;
            return PartialView("ListAccountMaster", unitOfWork.accountMasterService.GetAccountMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteAccountMaster(AccountMaster obj)  
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            try
            {
                unitOfWork.accountMasterService.Delete(obj);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("ListAccountMaster", unitOfWork.accountMasterService.GetAccountMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        [HttpGet]
        public ActionResult FeeSummaryQuarterwiseChart()
        {
            ChartBarViewsDemoOptions options = new ChartBarViewsDemoOptions();
            options.View = DevExpress.XtraCharts.ViewType.Bar;
            options.ShowLabels = true;
            ViewData[ChartDemoHelper.OptionsKey] = options;
            return PartialView("FeeSummaryQuarterwiseChart", unitOfWork.vfeeSummaryQuaterwiseChartService.GetFeeSummaryQuarterwiseForChart(int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

    }
}
