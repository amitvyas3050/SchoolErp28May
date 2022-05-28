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
        //[NoCache]
    public class ClassSetupController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;



        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appClassSetup == 0)
            {
                return Redirect("~/");
            }
            ////MenuID of Classes=11
            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 14, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

            
            Session["CompID"] = 1;
            Session["BranchID"] = 1;
            Session["SessionID"] = 7;
            Session["UserID"] = 1;
            //return PartialView("ClassSetupMain");

            return View();
        }

        

        public ActionResult ListPartialClassSetup()
        {
            return PartialView("GridViewPartial", unitOfWork.classSetupService.GetClassSetupList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewClassSetup(ClassSetup obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    obj.CompID=byte.Parse(Session["CompID"].ToString());
                    obj.BranchID= byte.Parse(Session["BranchID"].ToString());
                    obj.UIDAdd= byte.Parse(Session["UserID"].ToString());
                    obj.AddDate=DateTime.Now;

                    unitOfWork.classSetupService.AddNewClassSetup(obj);
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
            return PartialView("GridViewPartial", unitOfWork.classSetupService.GetClassSetupList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public void SaveUserLogForUpdate(ClassSetup obj)
        {
            SqlCommand cmdMaster = new SqlCommand("Update_ClassSetupHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@ClassSetupID", obj.ClassSetupID);
            cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", false);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
            cmdMaster.ExecuteNonQuery();

        }
        public void SaveUserLogForDelete(ClassSetup obj)
        {
            SqlCommand cmdMaster = new SqlCommand("Update_ClassSetupHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@ClassSetupID", obj.ClassSetupID);
            cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", true);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            cmdMaster.ExecuteNonQuery();

        }




        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateClassSetup(ClassSetup obj)
        {

            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

            if (ModelState.IsValid)
            {
                try
                {
                    obj.CompID = byte.Parse(Session["CompID"].ToString());
                    obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForUpdate(obj);
                        _mTran.Commit();
                    }
                    unitOfWork.classSetupService.UpdateClassSetup(obj, byte.Parse(Session["UserID"].ToString()));
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
            return PartialView("GridViewPartial", unitOfWork.classSetupService.GetClassSetupList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteClassSetup(ClassSetup objClass)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {
                int RowsCount = unitOfWork.classSetupService.CheckDelete(objClass.ClassSetupID);
                if (RowsCount == 0)
                {
                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForDelete(objClass);
                        _mTran.Commit();
                    }
                }
                if (RowsCount == 0)
                {
                    unitOfWork.classSetupService.Delete(objClass);
                    unitOfWork.Save();

                }
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("GridViewPartial", unitOfWork.classSetupService.GetClassSetupList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        

        [ValidateAntiForgeryToken]
        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
            { 
                //SafeExecute(() => PerformDelete());
                //string vartem = 1;
            }
            return ListPartialClassSetup();
        }
    }
}
