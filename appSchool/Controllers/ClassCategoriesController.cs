using appSchool.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.ViewModels;

namespace appSchool.Controllers
{
        [NoCache]
    public class ClassCategoriesController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appClassCategories == 0)
            {
                return Redirect("~/");
            }

            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 13, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
        public ActionResult PartialGridClassCategories()
        {
            return PartialView("GridViewPartial", unitOfWork.classCategoryRepositoryViewModel.GetClassCategoryList(byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewClassCategory(ClassCategory obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    obj.CompID=byte.Parse(Session["CompID"].ToString());
                    obj.BranchID = byte.Parse(Session["BranchID"].ToString());
                    obj.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    obj.AddDate = DateTime.Now;
                    
                    unitOfWork.classCategoryRepositoryViewModel.AddNewCategory(obj);
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
            return PartialView("GridViewPartial", unitOfWork.classCategoryRepositoryViewModel.GetClassCategoryList(byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateClassCategory(ClassCategory obj)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    obj.UIDMod = byte.Parse(Session["UserID"].ToString());
                    obj.ModDate = DateTime.Now;
                    unitOfWork.classCategoryRepositoryViewModel.UpdateCategory(obj);
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
            return PartialView("GridViewPartial", unitOfWork.classCategoryRepositoryViewModel.GetClassCategoryList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteClassCategory(ClassCategory obj)
        {
            try
            {
                int RowsCount = unitOfWork.classCategoryRepositoryViewModel.CheckDelete(obj.ClassCategoryID);
                if (RowsCount == 0)
                {
                    unitOfWork.classCategoryRepositoryViewModel.DeleteCategory(obj);
                    unitOfWork.Save();
                }
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("GridViewPartial", unitOfWork.classCategoryRepositoryViewModel.GetClassCategoryList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public ActionResult ClassCatagoryGridRowChange(int ID)
        {
            return PartialView("ClassCategoryTabs", ID);
        }

        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1;
            }
            return PartialGridClassCategories();
        }


    }
}
