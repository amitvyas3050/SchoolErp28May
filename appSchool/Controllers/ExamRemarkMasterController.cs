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
    public class ExamRemarkMasterController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        //private SqlConnection _mConn;
        //private SqlTransaction _mTran;

        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appExamRemark == 0)
            {
                return Redirect("~/");
            }


            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 81, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
        public ActionResult ListExamRemarkMasterView()
        {
            return PartialView("GridViewPartial", unitOfWork.examRemarkMasterService.GetExamRemarkMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewExamRemark(ExamRemarkMaster obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    obj.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    obj.AddDate = DateTime.Now;
                    obj.CompID=byte.Parse(Session["CompID"].ToString());
                    obj.BranchID= byte.Parse(Session["BranchID"].ToString());

                    unitOfWork.examRemarkMasterService.Insert(obj);
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
            return PartialView("GridViewPartial", unitOfWork.examRemarkMasterService.GetExamRemarkMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public void SaveUserLogForUpdate(ExamRemarkMaster obj)
        {
            //SqlCommand cmdMaster = new SqlCommand("UPDATE_DepartmentMasterHistory", _mConn);
            //cmdMaster.CommandType = CommandType.StoredProcedure;
            //cmdMaster.Transaction = _mTran;

            //cmdMaster.Parameters.AddWithValue("@DepartmentID", obj.DepartmentID);
            //cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            //cmdMaster.Parameters.AddWithValue("@IsDeleted", false);
            //cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            //cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
            //cmdMaster.ExecuteNonQuery();

        }
        public void SaveUserLogForDelete(ExamRemarkMaster obj)
        {
            //SqlCommand cmdMaster = new SqlCommand("UPDATE_DepartmentMasterHistory", _mConn);
            //cmdMaster.CommandType = CommandType.StoredProcedure;
            //cmdMaster.Transaction = _mTran;

            //cmdMaster.Parameters.AddWithValue("@DepartmentID", obj.DepartmentID);
            //cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            //cmdMaster.Parameters.AddWithValue("@IsDeleted", true);
            //cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            //cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            //cmdMaster.ExecuteNonQuery();

        }





        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateExamRemark(ExamRemarkMaster obj)
        {
            //_mConn = DB.GetActiveConnection();
            //_mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            if (ModelState.IsValid)
            {
                try
                {
                    obj.UIDMod = byte.Parse(Session["UserID"].ToString());
                    obj.ModDate = DateTime.Now;
                    obj.CompID = byte.Parse(Session["CompID"].ToString());
                    obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        //SaveUserLogForUpdate(obj);
                        //_mTran.Commit();
                    }
                    unitOfWork.examRemarkMasterService.UpdateExamRemarkMaster(obj);
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
            return PartialView("GridViewPartial", unitOfWork.examRemarkMasterService.GetExamRemarkMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteExamRemark(ExamRemarkMaster obj)
        {
            //_mConn = DB.GetActiveConnection();
            //_mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {
                if (SettingMasterStaticClass._ManageHistory == true)
                {
                    //SaveUserLogForDelete(obj);
                    //_mTran.Commit();
                }
                unitOfWork.examRemarkMasterService.Delete(obj);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("GridViewPartial", unitOfWork.examRemarkMasterService.GetExamRemarkMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public ActionResult ExamRemarkMasterGridRowChange(int ID)
        {
            return PartialView("ExamRemarkMasterGridRowChange", ID);
        }


        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1;
            }
            return ListExamRemarkMasterView();
        }


    }
}
