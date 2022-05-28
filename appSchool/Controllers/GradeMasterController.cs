using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.Repositories;
using appSchool.ViewModels;

namespace appSchool.Controllers
{
        [NoCache]
    public class GradeMasterController : Controller
    {
         private UnitOfWork unitOfWork = new UnitOfWork();
        //
         // GET: /GradeMaster/

         public ActionResult Index()
         {
            if (Session["UserID"] == null || (int)SubMenuModules.appGradeMaster == 0)
            {
                return Redirect("~/");
            }
          
            return View();
        }
         public ActionResult PartialGradeView()
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("GridViewPartial", unitOfWork.gradeMasterService.GetGradeMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["CompID"].ToString())));
        }

         [HttpPost, ValidateInput(false)]
         public ActionResult AddNewGrade(GradeMaster obj)
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

                    unitOfWork.gradeMasterService.Insert(obj);
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
            return PartialView("GridViewPartial", unitOfWork.gradeMasterService.GetGradeMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
         public ActionResult UpdateGrade(GradeMaster obj)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.gradeMasterService.UpdateGradeMaster(obj,byte.Parse(Session["UserID"].ToString()));
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
            return PartialView("GridViewPartial", unitOfWork.gradeMasterService.GetGradeMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult DeleteGrade(GradeMaster obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            try
            {
                unitOfWork.gradeMasterService.Delete(obj);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            return PartialView("GridViewPartial", unitOfWork.gradeMasterService.GetGradeMasterList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public ActionResult GradeMasterGridRowChange(int ID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("GradeTabs", ID);
        }

        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1;
            }
            return PartialGradeView();
        }
    }
}



    