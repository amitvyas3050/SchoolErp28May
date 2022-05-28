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
    public class SubjectLevel3MasterController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;

        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appSubjectLevelThree == 0)
            {
                return Redirect("~/");
            }


            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 84, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

            ViewData["IdL1_ForSubjectLevelThree"] = 0;
            return View();
        }



        public ActionResult GetSubjectLevelTwoListByClassID(string pIdL1)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            List<vSubjectLevelThreeByIDLOne> obj = new List<vSubjectLevelThreeByIDLOne>();

            obj = unitOfWork.subjectLevel3Service.GetAllsubjectsLevel3ByIDL1(int.Parse(pIdL1),byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString()));

            ViewData["IdL1_ForSubjectLevelThree"] = int.Parse(pIdL1);
            return PartialView("GridViewPartial", obj);
        }

        public ActionResult GetIDL2SubjectList(int mIdL1)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            List<SubjectLevelTwo> obj = new List<SubjectLevelTwo>();
            obj = unitOfWork.subjectLevel3Service.GetAllsubjectsNameByIDL1(mIdL1);

            return PartialView("GridViewPartial", obj);
        }

        public ActionResult ListsubjectLevel3MasterView(int pIdL1)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            List<vSubjectLevelThreeByIDLOne> obj = new List<vSubjectLevelThreeByIDLOne>();

            obj = unitOfWork.subjectLevel3Service.GetAllsubjectsLevel3ByIDL1(pIdL1, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

            ViewData["IdL1_ForSubjectLevelThree"] = pIdL1;
            return PartialView("GridViewPartial", obj);
        }

       

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewSubjectLevel3(SubjectLevelThree obj, int pIdL1)
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
                    unitOfWork.subjectLevel3Service.Insert(obj);
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
            return PartialView("GridViewPartial", unitOfWork.subjectLevel3Service.GetAllsubjectsLevel3ByIDL1(pIdL1, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
        #region
        public void SaveUserLogForUpdate(SubjectLevelThree obj)
        {
            SqlCommand cmdMaster = new SqlCommand("Update_SubjectLevelThreeHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@IdL3", obj.IdL3);
            cmdMaster.Parameters.AddWithValue("@ChangedBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", false);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
            cmdMaster.ExecuteNonQuery();

        }
        public void SaveUserLogForDelete(SubjectLevelThree obj)
        {
            SqlCommand cmdMaster = new SqlCommand("Update_SubjectLevelThreeHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@IdL3", obj.IdL3);
            cmdMaster.Parameters.AddWithValue("@ChangedBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", true);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            cmdMaster.ExecuteNonQuery();

        }
        #endregion




        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateSubjectLevel3(SubjectLevelThree obj, int pIdL1)
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
                    unitOfWork.subjectLevel3Service.UpdateSubL3Master(obj);
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
            return PartialView("GridViewPartial", unitOfWork.subjectLevel3Service.GetAllsubjectsLevel3ByIDL1(pIdL1, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteSubjectLevel3(SubjectLevelThree obj, int pIdL1)
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
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("GridViewPartial", unitOfWork.subjectLevel3Service.GetAllsubjectsLevel3ByIDL1(pIdL1, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


    }
}
