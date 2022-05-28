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
    public class RollNoAllotmentController : Controller
    {
         private UnitOfWork unitOfWork = new UnitOfWork();
       
         public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appRollNumberAllotment == 0)
            {
                return Redirect("~/");
            }

            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 110, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

         public ActionResult PartialRollNoView(int PclassSetupID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["ClassSetupID"] = PclassSetupID;
            return PartialView("ListRollNoAllotmentView", new UnitOfWork().studentSessionService.GetAllStudentbyClassSetupID(PclassSetupID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

         public ActionResult GetAllStudentListView(string mClassesID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             int newclassSetupID = int.Parse(mClassesID);
             ViewData["ClassSetupID"] = newclassSetupID;
             return PartialView("ListRollNoAllotmentView", new UnitOfWork().studentSessionService.GetAllStudentbyClassSetupID(newclassSetupID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }

         public ActionResult UpdateStudentRollNoSelectAll( string argRollNoID, string argClassID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             int pRollNoID = 0;
             int pClassID = 0;
             try
             {
                 if (argRollNoID != null)
                     pRollNoID = int.Parse(argRollNoID);
                 if (argClassID != null)
                     pClassID = int.Parse(argClassID);


                 List<vStudentSession> objStudentSessionList = unitOfWork.studentSessionService.GetAllStudentbyClassSetupID(pClassID, int.Parse(Session["SessionID"].ToString()),byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                 int mRollNo = 0;
                 if (pRollNoID > 0)
                 {
                     mRollNo = pRollNoID;
                 }

                 foreach (vStudentSession objStudentSession in objStudentSessionList)
                 {
                     StudentSession objStudent = new StudentSession();
                     objStudent.StudentSessionID = objStudentSession.StudentSessionID;
                     objStudent.RollNo = pRollNoID;

                     unitOfWork.studentSessionService.UpdateStudentRollNo(objStudent);
                     unitOfWork.Save();
                     pRollNoID = pRollNoID + 1;

                 }


             }
             catch (Exception e)
             {
                 // updateValues.SetErrorText(product, e.Message);
             }
          
             ViewData["ClassSetupID"] = pClassID;
             return PartialView("ListRollNoAllotmentView", new UnitOfWork().studentSessionService.GetAllStudentbyClassSetupID(pClassID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }



        [ValidateInput(false)]
         public ActionResult updateRollNoAll(MVCxGridViewBatchUpdateValues<vStudentSession, int> updateValues, int PclassSetupID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["ClassSetupID"] = PclassSetupID;
            foreach (var product in updateValues.Insert)
            {
                //if (updateValues.IsValid(product))
                //    InsertProduct(product, updateValues);
            }
            foreach (var product in updateValues.Update)
            {
                bool res = false;
                res = unitOfWork.studentSessionService.CheckDuplicateRollNoClassWise(int.Parse(product.RollNo.ToString()),PclassSetupID, int.Parse(Session["SessionID"].ToString()));
                if (res == false)
                {
                    if (updateValues.IsValid(product))
                        UpdateProduct(product, updateValues);
                }
            }
            foreach (var productID in updateValues.DeleteKeys)
            {
               // DeleteProduct(productID, updateValues);
            }

            return PartialView("ListRollNoAllotmentView", new UnitOfWork().studentSessionService.GetAllStudentbyClassSetupID(PclassSetupID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

       

        protected void UpdateProduct(vStudentSession product, MVCxGridViewBatchUpdateValues<vStudentSession, int> updateValues)
        {
            try
            {
                unitOfWork.studentSessionService.UpdateStudentRollNoByVStudentSession(product);
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
     


        public ActionResult RollNoGridRowChange(int ID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("RouteTabs", ID);
        }

    }


}



    