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
    public class LeftAndRightPanelController : Controller
    {

        private UnitOfWork unitOfWork = new UnitOfWork();


        public ActionResult ReturnLeftPanelView(int moduleID)
        {
            if (Session["UserID"] == null )
            {
                return Redirect("~/");
            }
            string moduleName = string.Empty;


            List<vUserRoleModulePermission> listmodulefeature = unitOfWork.appFeatureservices.GetAllModuleAndFeatureListByRoleIDOrderbyIndex(moduleID, int.Parse(Session["UserRoleID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

            AppModule objmodule = unitOfWork.appModuleservices.GetAppModuleName(moduleID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

            if (objmodule != null)
            {
                moduleName = objmodule.MenuText;
            }

            ViewData["ModuleName"] = moduleName;
            return PartialView("LeftPanelPartial", listmodulefeature);

            //return PartialView("LeftPanelPartial");


        }

        public ActionResult ReturnRightPanelView()
        {
            if (Session["UserID"] == null )
            {
                return Redirect("~/");
            }

            int userRoleID = int.Parse(Session["UserRoleID"].ToString());
            List<RoleModule> listuserrole = unitOfWork.roleModuleservices.GetRoleModuleList(userRoleID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

            
            List<AppModule> listappmodule = unitOfWork.appModuleservices.GetAppModuleList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

            List<RoleModulePermission> listrolemodulePermission = new List<RoleModulePermission>();

            foreach(RoleModule objrole in listuserrole)
            {
                RoleModulePermission objrolmodule = new RoleModulePermission();
                objrolmodule.Id = objrole.Id;
                objrolmodule.MenuName = listappmodule.Where(x => x.Id == objrole.ModuleId).SingleOrDefault().MenuText; 
                objrolmodule.NavUrl = objrole.NavUrl;
                objrolmodule.IconName = listappmodule.Where(x => x.Id == objrole.ModuleId).SingleOrDefault().MenuIcon;
                listrolemodulePermission.Add(objrolmodule);
            }





            return PartialView("RightPanelPartial", listrolemodulePermission);

            //return PartialView("LeftPanelPartial");


        }




    }
}
