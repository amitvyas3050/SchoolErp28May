using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.ViewModels;
using appSchool.Repositories;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using System.Web.SessionState;

namespace appSchool.Controllers
{
        [NoCache]
    public class StudentListFeesStructureController : Controller
    {
        

        UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {


                return Redirect("~/");
            }

            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 11, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

      


        #region ListStudentFeesStructure

        public ActionResult StudentListFeesStructure ()
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }

            return PartialView("StudentListFeesStructure");
        }


        public ActionResult PartialGridListFeesStructure()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("GridStudentList", new UnitOfWork().vstudentListFromStudFeesStructureService.GetStudentListFromFeesStructure(int.Parse(Session["SessionID"].ToString()),byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }




        public ActionResult GetFeesTermForListEdit(int newStudentID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            int sessionID = int.Parse(Session["SessionID"].ToString());

            ViewData["FeesStructStudentID"] = newStudentID;
            return PartialView("GridFeesTermList", new UnitOfWork().vstudentListFromStudFeesStructureService.GetTermListByStudentID(newStudentID, sessionID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult PartialGridFeesTerm(int newStudentID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["FeesStructStudentID"] = newStudentID;

            return PartialView("GridFeesTermList", unitOfWork.vstudentListFromStudFeesStructureService.GetTermListByStudentID(newStudentID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }



        public ActionResult GetFeesHeadForListEdit(int newStudMasterID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            int sessionID = int.Parse(Session["SessionID"].ToString());

            ViewData["StudMasterIDForSFS"] = newStudMasterID;
            return PartialView("ListFeesStructureForEdit", unitOfWork.StudentFeesDetailService.GetStudentFeesDetailByStudMasterID(newStudMasterID, int.Parse(Session["SessionID"].ToString())));
        }

        public ActionResult PartialGridFeesHead(int newStudMasterID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["StudMasterIDForSFS"] = newStudMasterID;

            return PartialView("ListFeesStructureForEdit", unitOfWork.StudentFeesDetailService.GetStudentFeesDetailByStudMasterID(newStudMasterID, int.Parse(Session["SessionID"].ToString())));
        }


        public ActionResult UpdateFeesStructureEdit(MVCxGridViewBatchUpdateValues<vListFeesStructure, int> updateValues, int mFeesStructID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            try
            {
                        foreach (var product in updateValues.Insert)
                        {
                            if (updateValues.IsValid(product))
                                InsertStructure(product, updateValues,mFeesStructID);
                        }
                        foreach (var product in updateValues.Update)
                        {
                            if (updateValues.IsValid(product))
                                UpdateStructure(product, updateValues);
                        }
                        foreach (var productID in updateValues.DeleteKeys)
                        {
                            DeleteStructure(productID, updateValues);
                        }


                        decimal FeesAmount = 0;

                        FeesAmount = unitOfWork.feesStructureDetailService.GetTotalFeesAmountByClassID(mFeesStructID, int.Parse(Session["SessionID"].ToString()));

                        FeesStructureMaster objSM = new FeesStructureMaster();
                        objSM.FeeStructAmount = FeesAmount;
                        objSM.FeeStructID = mFeesStructID;
                        unitOfWork.feesStructureMasterService.UpdateFeesAmountMaster(objSM);
                        unitOfWork.Save();
          

            }
            catch (Exception e)
            {

            }


            ViewData["FeesStructID"] = mFeesStructID;
            return PartialView("ListFeesStructureForEdit",unitOfWork.feesStructureDetailService.GetFeeStructureDetailbyFeeStructID(mFeesStructID));
        }

        protected void UpdateStructure(vListFeesStructure product, MVCxGridViewBatchUpdateValues<vListFeesStructure, int> updateValues)
        {
            try
            {
                unitOfWork.feesStructureDetailService.UpdateFeesStructureDetail(product, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
        protected void DeleteStructure(int product, MVCxGridViewBatchUpdateValues<vListFeesStructure, int> updateValues)
        {
            try
            {
                unitOfWork.feesStructureDetailService.Delete(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
        protected void InsertStructure(vListFeesStructure product, MVCxGridViewBatchUpdateValues<vListFeesStructure, int> updateValues,int mfeeStructID)
        {
            try
            {
                unitOfWork.feesStructureDetailService.InsertFeesStructureDetail(product, mfeeStructID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }

        #endregion


    }
}
