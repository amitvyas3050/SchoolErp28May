using appSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.ViewModels;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;


namespace appSchool.Controllers
{
        [NoCache]
    public class ClassesController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;

          
        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appClasses == 0)
            {
                return Redirect("~/");
            }

            //MenuID of Classes=11
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

        public ActionResult PartialGridClasses()
        {
            return PartialView("GridViewPartial", unitOfWork.ClassService.GetClassList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewClass(Class objClass)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objClass.CompID = byte.Parse(Session["CompID"].ToString());
                    objClass.BranchID = byte.Parse(Session["BranchID"].ToString());
                    objClass.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    objClass.AddDate = DateTime.Now;

                    unitOfWork.ClassService.AddNewClass(objClass);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = objClass;
            return PartialView("GridViewPartial", unitOfWork.ClassService.GetClassList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public void SaveUserLogForUpdate(Class obj)
        {
            SqlCommand cmdMaster = new SqlCommand("Update_ClassHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@ClassId", obj.ClassID);
            cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", false);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
            cmdMaster.ExecuteNonQuery();
            
        }
        public void SaveUserLogForDelete(Class obj)
        {
            SqlCommand cmdMaster = new SqlCommand("Update_ClassHistory", _mConn);
            cmdMaster.CommandType = CommandType.StoredProcedure;
            cmdMaster.Transaction = _mTran;

            cmdMaster.Parameters.AddWithValue("@ClassId", obj.ClassID);
            cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@IsDeleted", true);
            cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

            cmdMaster.ExecuteNonQuery();
         
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateClass(Class objClass)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
          
            if (ModelState.IsValid)
            {
                try
                {
                    objClass.ModDate = DateTime.Now;
                    objClass.UIDMod =byte.Parse(Session["UserID"].ToString());

                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForUpdate(objClass);
                    }
                   unitOfWork.ClassService.UpdateClass(objClass);
                   unitOfWork.Save();

                   _mTran.Commit();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = objClass;
            return PartialView("GridViewPartial", unitOfWork.ClassService.GetClassList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
 
        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteClass(Class objClass)
        {
           
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

            try
            {
                int RowsCount = unitOfWork.ClassService.CheckClassDelete(objClass.ClassID);
                if (RowsCount == 0)
                {
                    if (SettingMasterStaticClass._ManageHistory == true)
                    {
                        SaveUserLogForDelete(objClass);
                        _mTran.Commit();
                    }
                }
                #region Deletel Old Method
                if (RowsCount == 0)
                {
                    unitOfWork.ClassService.DeleteClass(objClass);
                    unitOfWork.Save();
                }
                else
                {
                }
                #endregion

            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("GridViewPartial", unitOfWork.ClassService.GetClassList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public ActionResult ClassGridRowChange(int RegID)
        {
            return PartialView("ClassTabs", RegID);
        }

        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1;
            }
            return PartialGridClasses();
        }

    }
}
