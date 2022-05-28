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
    public class ListFeesStructureController : Controller
    {
        //
        // GET: /FeesStructure/


        UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {


                return Redirect("~/");
            }

            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 11, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
            return View();
        }

       

        #region ListFeesStructure

        public ActionResult ListFeesStructure()
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }

            return PartialView("ListFeesStructure");
        }
        public ActionResult PartialGridListFeesStructure()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            return PartialView("GridListFeesStructure", new UnitOfWork().vlistFeesStructureService.GetFeesStructureList(byte.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
        public ActionResult GetFeesStructureForEdit(int newFeesStructID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            int sessionID = int.Parse(Session["SessionID"].ToString());

            ViewData["FeesStructID"] = newFeesStructID;
            return PartialView("ListFeesStructureForEdit", new UnitOfWork().vlistFeesStructureService.GetFeesStructureByFeeStructID(newFeesStructID,sessionID));
        }
        public ActionResult PartialFeeStructureForEdit( int mFeesStructID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }


            return PartialView("ListFeesStructureForEdit", unitOfWork.feesStructureDetailService.GetFeeStructureDetailbyFeeStructID(mFeesStructID));
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
