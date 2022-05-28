using appSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.ViewModels;
using System.Data;
using System.Data.SqlClient;

namespace appSchool.Controllers
{
    [NoCache]
    public class SectionsController : Controller
    {
        
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;


        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appSections == 0)
            {
                return Redirect("~/");
            }
            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 12, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
        //public ActionResult PartialGridSections()
        //{
        //    if (Session["UserID"] == null) { return Redirect("~/"); }
        //    return PartialView("ListSections", unitOfWork.SectionRepositoryViewModel.GetSectionList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        //}


        public ActionResult PartialGridSections()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("GridViewPartial", unitOfWork.SectionRepositoryViewModel.GetSectionList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        //public void SaveUserLogForDelete(Class obj)
        //{
        //    SqlCommand cmdMaster = new SqlCommand("Update_ClassHistory", _mConn);
        //    cmdMaster.CommandType = CommandType.StoredProcedure;
        //    cmdMaster.Transaction = _mTran;

        //    cmdMaster.Parameters.AddWithValue("@ClassId", obj.ClassID);
        //    cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
        //    cmdMaster.Parameters.AddWithValue("@IsDeleted", true);

        //    cmdMaster.ExecuteNonQuery();

        //}


        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewSection(Section objClass)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {
                    objClass.BranchID = byte.Parse(Session["BranchID"].ToString());
                    objClass.CompID = byte.Parse(Session["CompID"].ToString());
                    unitOfWork.SectionRepositoryViewModel.AddNewSection(objClass,byte.Parse(Session["UserID"].ToString()));
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
            return PartialView("GridViewPartial", unitOfWork.SectionRepositoryViewModel.GetSectionList(byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateSection(Section   objClass)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.SectionRepositoryViewModel.UpdateSection(objClass,byte.Parse(Session["UserID"].ToString()));
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
            return PartialView("GridViewPartial", unitOfWork.SectionRepositoryViewModel.GetSectionList(byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteSection(Section objClass)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);

            try
            {
                int RowsCount = unitOfWork.SectionRepositoryViewModel.CheckDelete(objClass.SectionID);
                if (RowsCount == 0)
                {
                   // SaveUserLogForDelete(objClass);
                    _mTran.Commit();

                }
                if (RowsCount == 0)
                {
                unitOfWork.SectionRepositoryViewModel.DeleteSection(objClass);
                unitOfWork.Save();
                }
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("GridViewPartial", unitOfWork.SectionRepositoryViewModel.GetSectionList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public ActionResult SectionGridRowChange(int ID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("SectionTabs", ID);
        }

        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1;
            }
            return PartialGridSections();
        }


    }
}
