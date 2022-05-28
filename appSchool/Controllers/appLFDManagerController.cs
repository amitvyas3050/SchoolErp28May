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
        
    public class appLFDManagerController : Controller
    {
       

          
        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appClasses == 0)
            {
                return Redirect("~/");
            }
         
            return View();
        }

       

    }
}
