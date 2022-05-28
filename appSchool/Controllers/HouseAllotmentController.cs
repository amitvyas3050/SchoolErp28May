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

namespace appSchool.Controllers
{
    [NoCache]
    public class HouseAllotmentController : Controller
    {
         private UnitOfWork unitOfWork = new UnitOfWork();
       
         public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appStudentSession == 0)
            {
                return Redirect("~/");
            }

            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 19, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            if (objuser != null)
            {
                PermissionFlag._AddFlag = objuser.AddP;
                PermissionFlag._ModFlag = objuser.ModP;
                PermissionFlag._DelFlag = objuser.DelP;
            }
            else
            {
                PermissionFlag._AddFlag = false;
                PermissionFlag._ModFlag = false;
                PermissionFlag._DelFlag = false;
            }
            return PartialView("Index");
        }

         public ActionResult PartialHouseView(int PclassSetupID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["HouseAllotmentVD"] = PclassSetupID;
            return PartialView("ListHouseAllotmentView", new UnitOfWork().studentSessionService.GetAllStudentbyClassID(PclassSetupID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

         public ActionResult GetAllStudentListView(string mClassesID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             int newclassID = int.Parse(mClassesID);
             ViewData["HouseAllotmentVD"] = newclassID;
             return PartialView("ListHouseAllotmentView", new UnitOfWork().studentSessionService.GetAllStudentbyClassID(newclassID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }

         public ActionResult UpdateStudentHouseSelectAll(string pStudentIDs,  int pHouseID,  int pClassID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             try
             {

                 string[] StudentIDList = pStudentIDs.Split(',');
                 foreach (string StudentID in StudentIDList)
                 {
                     StudentSession objStudent = new StudentSession();
                     objStudent.StudentSessionID = int.Parse(StudentID);
                     objStudent.HouseID = pHouseID;

                     unitOfWork.studentSessionService.UpdateStudentHouse(objStudent);
                     unitOfWork.Save();

                 }


             }
             catch (Exception e)
             {
                 // updateValues.SetErrorText(product, e.Message);
             }

             int mClassAttendanceID = 0;

             if (mClassAttendanceID == 0)
             {
                 //  mClassAttendanceID = unitOfWork.attendanceClassService.GetAttendanceInfoByClassSetupIDAndDate(int.Parse(mClassesID), newDate);
             }
             //ViewData["AttendanceStudent"] = unitOfWork.attendanceStudentService.GetAttendanceStudentForGrid(mClassAttendanceID);
             //ViewData["ClassAttendanceID"] = mClassAttendanceID;
             ViewData["HouseAllotmentVD"] = pClassID;
             return PartialView("ListHouseAllotmentView", new UnitOfWork().studentSessionService.GetAllStudentbyClassID(pClassID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }



        [ValidateInput(false)]
         public ActionResult updateHouseAll(MVCxGridViewBatchUpdateValues<vStudentSession, int> updateValues, int PclassSetupID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }

            ViewData["HouseAllotmentVD"] = PclassSetupID;
            foreach (var product in updateValues.Insert)
            {
                if (updateValues.IsValid(product))
                    InsertProduct(product, updateValues);
            }
            foreach (var product in updateValues.Update)
            {
                if (updateValues.IsValid(product))
                    UpdateProduct(product, updateValues);
            }
            foreach (var productID in updateValues.DeleteKeys)
            {
                DeleteProduct(productID, updateValues);
            }

            return PartialView("ListHouseAllotmentView", new UnitOfWork().studentSessionService.GetAllStudentbyClassID(PclassSetupID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        protected void UpdateProduct(vStudentSession product, MVCxGridViewBatchUpdateValues<vStudentSession, int> updateValues)
        {
            try
            {
                unitOfWork.studentSessionService.UpdateStudentSession(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
        protected void DeleteProduct(int product, MVCxGridViewBatchUpdateValues<vStudentSession, int> updateValues)
        {
            try
            {
                unitOfWork.studentSessionService.Delete(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
        protected void InsertProduct(vStudentSession product, MVCxGridViewBatchUpdateValues<vStudentSession, int> updateValues)
        {
            try
            {
                unitOfWork.studentSessionService.InsertProduct(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
     



        //[HttpPost, ValidateInput(false)]
        //public ActionResult DeleteRoute(RouteMaster obj)
        //{
        //    try
        //    {
        //        unitOfWork.routeMasterService.Delete(obj);
        //        unitOfWork.Save();
        //    }
        //    catch (Exception e)
        //    {
        //        ViewData["EditError"] = e.Message;
        //    }
        //    return PartialView("ListRouteView", unitOfWork.routeMasterService.Get());
        //}


        public ActionResult HouseGridRowChange(int ID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("RouteTabs", ID);
        }

    }


}



    