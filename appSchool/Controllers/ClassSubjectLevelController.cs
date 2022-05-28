using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.Repositories;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using System.Web.SessionState;
using appSchool.ViewModels;
using System.Data.SqlClient;
using System.Data;

namespace appSchool.Controllers
{
    [NoCache]
    public class ClassSubjectLevelController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        
        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appClassSubjectLevel == 0)
            {
                return Redirect("~/");
            }

            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 80, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

        public ActionResult PartialClassSubjectLevelView()
        {
            return PartialView("GridViewPartial", new UnitOfWork().ClassService.GetClassList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

 
        [ValidateInput(false)]
        public ActionResult UpdateClassSubjectLevelAll(MVCxGridViewBatchUpdateValues<Class, int> updateValues)
        {
            foreach (var product in updateValues.Update)
            {
                if (updateValues.IsValid(product))
                    UpdateProduct(product, updateValues);
            }

            return PartialView("GridViewPartial", new UnitOfWork().ClassService.GetClassList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        protected void UpdateProduct(Class product, MVCxGridViewBatchUpdateValues<Class, int> updateValues)
        {
            try
            {
                unitOfWork.ClassService.UpdateSubjectLevel(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
                ViewData["EditError"] = e.Message;
            }
        }
       


        public ActionResult ClassSubjectLevelGridRowChange(int ID)
        {
            return PartialView("RouteTabs", ID);
        }


        public ActionResult GridViewCustomActionPartial(string customAction)
        {
            if (customAction == "delete")
            {
                //SafeExecute(() => PerformDelete());
                //string vartem = 1;
            }
            return PartialClassSubjectLevelView();
        }

    }


}



    