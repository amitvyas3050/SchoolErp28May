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
    public class SubjectLevel1MasterController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;

        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appSubjectLevelOne == 0)
            {
                return Redirect("~/");
            }


            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 16, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
        public ActionResult ListSubjectLevel1View()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("GridViewPartial", unitOfWork.subjectLevelService.GetSubjectLevel1List(byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewSubject(SubjectLevelOne obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {
                    obj.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    obj.AddDate = DateTime.Now;
                    obj.CompID = byte.Parse(Session["CompID"].ToString());
                    obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                    unitOfWork.subjectLevelService.Insert(obj);
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
            return PartialView("GridViewPartial", unitOfWork.subjectLevelService.GetSubjectLevel1List(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public void SaveUserLogForUpdate(SubjectLevelOne obj)
        {
            SqlCommand cmdMaster = new SqlCommand("UPDATE_SubjectLevelOneHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@IdL1", obj.IdL1);
            cmdMaster.Parameters.AddWithValue("@ChangedBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", false);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
            cmdMaster.ExecuteNonQuery();

        }
        public void SaveUserLogForDelete(SubjectLevelOne obj)
        {
            SqlCommand cmdMaster = new SqlCommand("UPDATE_SubjectLevelOneHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@IdL1", obj.IdL1);
            cmdMaster.Parameters.AddWithValue("@ChangedBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", true);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
            cmdMaster.ExecuteNonQuery();

        }





        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateSubject(SubjectLevelOne obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            if (ModelState.IsValid)
            {
                try
                {
                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForUpdate(obj);
                        _mTran.Commit();
                    }
                    obj.UIDMod = byte.Parse(Session["UserID"].ToString());
                    obj.ModDate = DateTime.Now;
                    unitOfWork.subjectLevelService.UpdateSubjectLevel1(obj);
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
            return PartialView("GridViewPartial", unitOfWork.subjectLevelService.GetSubjectLevel1List(byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteSubject(SubjectLevelOne obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {

                int RowsCount = unitOfWork.subjectLevelService.CheckSubjectevelOneDelete(obj.IdL1);
                if (RowsCount == 0)
                {
                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForDelete(obj);
                        _mTran.Commit();
                    }
                    unitOfWork.subjectLevelService.Delete(obj);
                    unitOfWork.Save();
                }
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("GridViewPartial", unitOfWork.subjectLevelService.GetSubjectLevel1List(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public ActionResult SubjectLevel1MasterGridRowChange(int ID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("SubjectLevel1MasterGridRowChange", ID);
        }

        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1;
            }
            return ListSubjectLevel1View();
        }

    }
}
