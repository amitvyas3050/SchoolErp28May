using appSchool.Repositories;
using appSchool.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace appSchool.Controllers
{
        [NoCache]
    public class ExamsManagerController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)TopMenuModules.appExamsManager == 0)
            {


                return Redirect("~/");
            }

            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 98, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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


            //return View(new myModel() { currModule = appModule.appExamsManager });
            return View();
        }
        public ActionResult PartialGridExamCategory()
        {
            return PartialView("GridViewPartial", unitOfWork.examCategoryService.GetExamCategoryList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
        public ActionResult ExamCategoryView()
        {
            return PartialView("ExamCategory");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewExamCategory(ExamMaster objExamCategory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objExamCategory.CompID = byte.Parse(Session["CompID"].ToString());
                    objExamCategory.BranchID = byte.Parse(Session["BranchID"].ToString());

                    ExamMaster objcheck = unitOfWork.examCategoryService.CheckDuplicateExamName(objExamCategory);
                    if (objcheck == null)
                    {
                        unitOfWork.examCategoryService.Insert(objExamCategory);
                        unitOfWork.Save();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = objExamCategory;
            return PartialView("GridViewPartial", unitOfWork.examCategoryService.GetExamCategoryList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdateExamCategory(ExamMaster objExamCategory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objExamCategory.CompID = byte.Parse(Session["CompID"].ToString());
                    objExamCategory.BranchID = byte.Parse(Session["BranchID"].ToString());
                     ExamMaster objcheck = unitOfWork.examCategoryService.CheckDuplicateExamName(objExamCategory);
                     if (objcheck == null)
                     {
                         unitOfWork.examCategoryService.UpdateExamMaster(objExamCategory);
                         unitOfWork.Save();
                     }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = objExamCategory;
            return PartialView("GridViewPartial", unitOfWork.examCategoryService.GetExamCategoryList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteExamCategory(ExamMaster objExamCategory)
        {
            try
            {
                unitOfWork.examCategoryService.Delete(objExamCategory);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("GridViewPartial", unitOfWork.examCategoryService.GetExamCategoryList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1;
            }
            return PartialGridExamCategory();
        }
    }
}
