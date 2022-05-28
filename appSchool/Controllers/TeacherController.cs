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
    public class TeacherController : Controller
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
        public ActionResult ListContectListView()
        {
            return PartialView("ListContectListView", unitOfWork.contectListService.GetContactList(byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewContect(ContactList obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    obj.CompID = byte.Parse(Session["CompID"].ToString());
                    obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                    unitOfWork.contectListService.Insert(obj);
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
            return PartialView("ListContectListView", unitOfWork.contectListService.GetContactList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        //public void SaveUserLogForUpdate(DepartmentMaster obj)
        //{
        //    SqlCommand cmdMaster = new SqlCommand("UPDATE_DepartmentMasterHistory", _mConn);
        //    cmdMaster.CommandType = CommandType.StoredProcedure;
        //    cmdMaster.Transaction = _mTran;

        //    cmdMaster.Parameters.AddWithValue("@DepartmentID", obj.DepartmentID);
        //    cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
        //    cmdMaster.Parameters.AddWithValue("@IsDeleted", false);
        //    cmdMaster.ExecuteNonQuery();

        //}
        //public void SaveUserLogForDelete(DepartmentMaster obj)
        //{
        //    SqlCommand cmdMaster = new SqlCommand("UPDATE_DepartmentMasterHistory", _mConn);
        //    cmdMaster.CommandType = CommandType.StoredProcedure;
        //    cmdMaster.Transaction = _mTran;

        //    cmdMaster.Parameters.AddWithValue("@DepartmentID", obj.DepartmentID);
        //    cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
        //    cmdMaster.Parameters.AddWithValue("@IsDeleted", true);

        //    cmdMaster.ExecuteNonQuery();

        //}





        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateContect(ContactList obj)
        {
            //_mConn = DB.GetActiveConnection();
            //_mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            if (ModelState.IsValid)
            {
                try
                {
                    //SaveUserLogForUpdate(obj);
                    //_mTran.Commit();
                    //obj.UIDMod = byte.Parse(Session["UserID"].ToString());
                    //obj.ModDate = DateTime.Now;
                    unitOfWork.contectListService.UpdateContectList(obj);
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
            return PartialView("ListContectListView", unitOfWork.contectListService.GetContactList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteContect(ContactList obj)
        {
            //_mConn = DB.GetActiveConnection();
            //_mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {
                //SaveUserLogForDelete(obj);
                //_mTran.Commit();
                unitOfWork.contectListService.Delete(obj);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("ListContectListView", unitOfWork.contectListService.GetContactList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public ActionResult ContectListGridRowChange(int ID)
        {
            return PartialView("ContectListGridRowChange", ID);
        }

        

    }
}
