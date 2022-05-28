using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.Repositories;
using appSchool.ViewModels;
using System.Data.SqlClient;
using System.Data;

namespace appSchool.Controllers
{
        [NoCache]
    public class FeesTermMasterController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;
        //
        // GET: /DesignationMaster/

        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appFeesTerm == 0)
            {
                return Redirect("~/");
            }
            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 37, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
        public ActionResult PartialFeesTermView()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("GridViewPartial", unitOfWork.feeTermService.GetFeeTermList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["SessionID"].ToString())));
        }

         [HttpPost, ValidateInput(false)]
        public ActionResult AddNewFeeTerm(FeeTerm obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {
                    obj.SessionId = byte.Parse(Session["SessionID"].ToString());
                    obj.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    obj.AddDate = DateTime.Now;
                    obj.CompID = byte.Parse(Session["CompID"].ToString());
                    obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                    obj.FeeTermID = unitOfWork.feeTermService.GetFeeTermID(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));


                    unitOfWork.feeTermService.Insert(obj);
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
            return PartialView("GridViewPartial", unitOfWork.feeTermService.GetFeeTermList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["SessionID"].ToString())));
        }

         public void SaveUserLogForUpdate(FeeTerm obj)
         {
             SqlCommand cmdMaster = new SqlCommand("UPDATE_FeeTermHistory", _mConn);
             cmdMaster.CommandType = CommandType.StoredProcedure;
             cmdMaster.Transaction = _mTran;

             cmdMaster.Parameters.AddWithValue("@FeeTermID", obj.FeeTermID);
             cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
             cmdMaster.Parameters.AddWithValue("@IsDeleted", false);
             cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
             cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
             cmdMaster.ExecuteNonQuery();

         }
         public void SaveUserLogForDelete(FeeTerm obj)
         {
             SqlCommand cmdMaster = new SqlCommand("UPDATE_FeeTermHistory", _mConn);
             cmdMaster.CommandType = CommandType.StoredProcedure;
             cmdMaster.Transaction = _mTran;

             cmdMaster.Parameters.AddWithValue("@FeeTermID", obj.FeeTermID);
             cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
             cmdMaster.Parameters.AddWithValue("@IsDeleted", true);
             cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
             cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

             cmdMaster.ExecuteNonQuery();

         }



         [HttpPost, ValidateInput(false)]
         public ActionResult UpdateFeeTerm(FeeTerm obj)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             _mConn = DB.GetActiveConnection();
             _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
             if (ModelState.IsValid)
             {
                 try
                 {
                     obj.SessionId = byte.Parse(Session["SessionID"].ToString());
                     obj.UIDMod = byte.Parse(Session["UserID"].ToString());
                     obj.ModDate = DateTime.Now;
                     obj.CompID = byte.Parse(Session["CompID"].ToString());
                     obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                     if (obj.FineAmount == null)
                     {
                         obj.FineAmount = 0;
                     }
                     if (SettingMasterStaticClass._ManageHistory == true)
                     {
                         SaveUserLogForUpdate(obj);
                         _mTran.Commit();
                     }
                     unitOfWork.feeTermService.UpdateFeeTerm(obj);
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
             return PartialView("GridViewPartial", unitOfWork.feeTermService.GetFeeTermList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["SessionID"].ToString())));
         }

         [HttpPost, ValidateInput(false)]
         public ActionResult DeleteFeeTerm(FeeTerm obj)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             _mConn = DB.GetActiveConnection();
             _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
             try
             {
                 int RowCount = unitOfWork.feeTermService.CheckDelete(obj.FeeTermID);
                 if (RowCount == 0)
                 {
                     if (SettingMasterStaticClass._ManageHistory == true)
                     {
                         SaveUserLogForDelete(obj);
                         _mTran.Commit();
                     }
                     unitOfWork.feeTermService.Delete(obj);
                     unitOfWork.Save();
                 }
             }
             catch (Exception e)
             {
                 ViewData["EditError"] = e.Message;
             }
             return PartialView("GridViewPartial", unitOfWork.feeTermService.GetFeeTermList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["SessionID"].ToString())));
         }


        public ActionResult FeesTermMasterGridRowChange(int ID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("FeesTermTabs", ID);
        }

        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1;
            }
            return PartialFeesTermView();
        }

    }
}



    