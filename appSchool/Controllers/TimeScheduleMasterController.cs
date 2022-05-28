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
    public class TimeScheduleMasterController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;
       
       

        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appTimeSchedule == 0)
            {
                return Redirect("~/");
            }
            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 78, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
        public ActionResult PartialTimeScheduleMasterView()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            return PartialView("GridViewPartial", unitOfWork.timeScheduleMasterService.GetTimeScheduleMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

         [HttpPost, ValidateInput(false)]
        public ActionResult AddNewTimeSchedule(TimeSchedule obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            if (ModelState.IsValid)
            {
                try
                {
                    //obj.SessionId = byte.Parse(Session["SessionID"].ToString());
                   
                    obj.CompID = byte.Parse(Session["CompID"].ToString());
                    obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                    obj.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    obj.AddDate = DateTime.Now;   

                    unitOfWork.timeScheduleMasterService.Insert(obj);
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
            return PartialView("GridViewPartial", unitOfWork.timeScheduleMasterService.GetTimeScheduleMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

         public void SaveUserLogForUpdate(TimeSchedule obj)
         {

             //SqlCommand cmdMaster = new SqlCommand("UPDATE_FeeTermHistory", _mConn);
             //cmdMaster.CommandType = CommandType.StoredProcedure;
             //cmdMaster.Transaction = _mTran;
             //cmdMaster.Parameters.AddWithValue("@FeeTermID", obj.FeeTermID);
             //cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
             //cmdMaster.Parameters.AddWithValue("@IsDeleted", false);
             //cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
             //cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
             //cmdMaster.ExecuteNonQuery();

         }
         public void SaveUserLogForDelete(TimeSchedule obj)
         {
            
             //SqlCommand cmdMaster = new SqlCommand("UPDATE_FeeTermHistory", _mConn);
             //cmdMaster.CommandType = CommandType.StoredProcedure;
             //cmdMaster.Transaction = _mTran;

             //cmdMaster.Parameters.AddWithValue("@FeeTermID", obj.FeeTermID);
             //cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
             //cmdMaster.Parameters.AddWithValue("@IsDeleted", true);
             //cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
             //cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

             //cmdMaster.ExecuteNonQuery();

         }



         [HttpPost, ValidateInput(false)]
         public ActionResult UpdateTimeSchedule(TimeSchedule obj)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             //_mConn = DB.GetActiveConnection();
             //_mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
             if (ModelState.IsValid)
             {
                 try
                 {
                     //obj.SessionId = byte.Parse(Session["SessionID"].ToString());
                     obj.UIDMod = byte.Parse(Session["UserID"].ToString());
                     obj.ModDate = DateTime.Now;
                     obj.CompID = byte.Parse(Session["CompID"].ToString());
                     obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                    
                     unitOfWork.timeScheduleMasterService.UpdateTimeScheduleMaster(obj);
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
             return PartialView("GridViewPartial", unitOfWork.timeScheduleMasterService.GetTimeScheduleMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }

         [HttpPost, ValidateInput(false)]
         public ActionResult DeleteTimeSchedule(TimeSchedule obj)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             //_mConn = DB.GetActiveConnection();
             //_mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
             try
             {
                     //SaveUserLogForDelete(obj);
                     //_mTran.Commit();
                     unitOfWork.timeScheduleMasterService.Delete(obj);
                     unitOfWork.Save();
                 
             }
             catch (Exception e)
             {
                 ViewData["EditError"] = e.Message;
             }
             return PartialView("GridViewPartial", unitOfWork.timeScheduleMasterService.GetTimeScheduleMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }


         public ActionResult TimeScheduleMasterGridRowChange(int ID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("TimeScheduleMasterTabs", ID);
        }

        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1;
            }
            return PartialTimeScheduleMasterView();
        }
    }
}



    