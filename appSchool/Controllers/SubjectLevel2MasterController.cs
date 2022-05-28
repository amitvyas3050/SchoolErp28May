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
    public class SubjectLevel2MasterController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;

        public ActionResult Index()
        {

            if (Session["UserID"] == null || (int)SubMenuModules.appSubjectLevelTwo == 0)
            {
                return Redirect("~/");
            }


            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 83,byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString()));
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
        public ActionResult ListsubjectLevel2MasterView()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("GridViewPartial", unitOfWork.subjectLevel2Service.GetSubjectLevel2List(byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewSubjectLevel2(SubjectLevelTwo obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {
                    obj.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    obj.AddDate = DateTime.Now;
                    obj.CompID=byte.Parse(Session["CompID"].ToString());
                    obj.BranchID=byte.Parse(Session["BranchID"].ToString());

                    unitOfWork.subjectLevel2Service.Insert(obj);
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
            return PartialView("GridViewPartial", unitOfWork.subjectLevel2Service.GetSubjectLevel2List(byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }
        #region
        public void SaveUserLogForUpdate(SubjectLevelTwo obj)
        {
            SqlCommand cmdMaster = new SqlCommand("UPDATE_SubjectLevelTwoHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@IdL2", obj.IdL2);
            cmdMaster.Parameters.AddWithValue("@ChangedBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", false);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
            cmdMaster.ExecuteNonQuery();

        }
        public void SaveUserLogForDelete(SubjectLevelTwo obj)
        {
            SqlCommand cmdMaster = new SqlCommand("UPDATE_SubjectLevelTwoHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@IdL2", obj.IdL2);
            cmdMaster.Parameters.AddWithValue("@ChangedBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", true);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            cmdMaster.ExecuteNonQuery();

        }
        #endregion




        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateSubjectLevel2(SubjectLevelTwo obj)
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
                      obj.CompID=byte.Parse(Session["CompID"].ToString());
                    obj.BranchID=byte.Parse(Session["BranchID"].ToString());

                    unitOfWork.subjectLevel2Service.UpdateSubL2Master(obj);
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
            return PartialView("GridViewPartial", unitOfWork.subjectLevel2Service.GetSubjectLevel2List(byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteSubjectLevel2(SubjectLevelTwo obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {
                int CheckID=unitOfWork.subjectLevel2Service.CheckSubjectLevelTwoDelete(obj);
                if(CheckID==0)
                {
                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForDelete(obj);
                        _mTran.Commit();
                    }
                    unitOfWork.subjectLevel2Service.Delete(obj);
                    unitOfWork.Save();
                }
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("GridViewPartial", unitOfWork.subjectLevel2Service.GetSubjectLevel2List(byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1;
            }
            return ListsubjectLevel2MasterView();
        }
    }
}
