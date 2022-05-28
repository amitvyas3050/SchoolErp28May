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
    public class FeesStructureController : Controller
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

        #region AddFeesStructure

        public ActionResult AddFeesStructure()
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }
            return PartialView("AddFeesStructure");
        }

        public string GetClassDescription(string mClassSetupID)
        {

            string[] bits = mClassSetupID.Split(',');
            string mNewClassID = string.Empty;
            string ErrorMsg = string.Empty;
            string dupclass=string.Empty;
            foreach (string bit in bits)
            {
                int mFeesStructID = unitOfWork.feesStructureMasterService.GetClassExist(int.Parse(bit),int.Parse(Session["SessionID"].ToString()),byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString()));
                string className = string.Empty;
                int classIDforname = int.Parse(bit);
                className = unitOfWork.classSetupService.GetClassNameByClassID(classIDforname);
                if (dupclass == string.Empty)
                    dupclass = className;
                else
                    dupclass = dupclass + "," + className;
              
                if (mFeesStructID > 0)
                {
                    ViewData["EditError"] = "Class " + dupclass + " All Ready Exist";
                    ErrorMsg = "Class " + dupclass + " All Ready Exist";
                }
                else
                {
                    if (mNewClassID == string.Empty)
                        mNewClassID = bit;
                    else
                        mNewClassID = mNewClassID + "," + bit;
                  // ErrorMsg = dupclass;
                }
            }
            ViewData["FeesClasses"] = mNewClassID;


           // return PartialView("ListAllFeesTerms", (new UnitOfWork().feesHeadService.GetTermHeadForFeeStructure()));

            return ErrorMsg;
        }

        public string GetClassName(string mClassSetupID)
        {

            string[] bits = mClassSetupID.Split(',');
            string mNewClassID = string.Empty;
            string ErrorMsg = string.Empty;
            string dupclass = string.Empty;
            foreach (string bit in bits)
            {
                //int mFeesStructID = unitOfWork.feesStructureMasterService.GetClassExist(int.Parse(bit), int.Parse(Session["SessionID"].ToString()));
                string className = string.Empty;
                int classIDforname = int.Parse(bit);
                className = unitOfWork.classSetupService.GetClassNameByClassID(classIDforname);
                if (dupclass == string.Empty)
                    dupclass = className;
                else
                    dupclass = dupclass + "," + className;

               
                    if (mNewClassID == string.Empty)
                        mNewClassID = bit;
                    else
                        mNewClassID = mNewClassID + "," + bit;
                     ErrorMsg = dupclass;
               
            }

            // return PartialView("ListAllFeesTerms", (new UnitOfWork().feesHeadService.GetTermHeadForFeeStructure()));

            return ErrorMsg;
        }

        public ActionResult GetFeesTermHeadGridData(string mClassesID, string mTermID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["FeesClasses"] = mClassesID;
            ViewData["FeesTerms"] = mTermID;

            List<VFeesCompulsory> obj = new List<VFeesCompulsory>();
            return PartialView("ListAllFeesTerms", new UnitOfWork().feesHeadService.GetTermHeadForFeeStructure(mTermID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["SessionID"].ToString())));
        }

        public ActionResult MultiSelectPartial()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("TermMultiSelectPartial");
        }


        public ActionResult PartialFeeTermMultiSelect()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("TermMultiSelectPartial", unitOfWork.feeTermService.GetFeeTermList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["SessionID"].ToString())));
        }
    
        public ActionResult PartialFeeTermView(VFeesCompulsory obj, string mClasses, string mTermIds)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("ListAllFeesTerms", new UnitOfWork().feesHeadService.GetTermHeadForFeeStructure(mTermIds, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["SessionID"].ToString())));
          //   List<VFeesCompulsory> obj = new List<VFeesCompulsory>();
          //  return PartialView("ListAllFeesTerms",obj );
        }
        [ValidateInput(false)]
        public ActionResult updateFeesStructure(MVCxGridViewBatchUpdateValues<VFeesCompulsory, int> updateValues, string mClasses)
        {
           
            if (Session["SessionID"] == null)
            {

                return Redirect("~/");
            }

            if (mClasses == string.Empty || mClasses == "")
            {
                return RedirectToAction("Index", "FeesStructure");
            }

            if (updateValues.Insert.Count == 0 && updateValues.Update.Count == 0)
            {
                return RedirectToAction("Index", "FeesStructure");
            }


            try
            {
                string[] bits = mClasses.Split(',');
                foreach (string bit in bits)
                {

                    FeesStructureMaster objSM = new FeesStructureMaster();
                    objSM.FeeStructSessionID = int.Parse(Session["SessionID"].ToString());
                    objSM.FeesStructClassID = int.Parse(bit);
                    objSM.CompID = byte.Parse(Session["CompID"].ToString());
                    objSM.BranchID = byte.Parse(Session["BranchID"].ToString());

                    int mFeesStructDupleecate = unitOfWork.feesStructureMasterService.GetFeesStructID(objSM);
                    if (mFeesStructDupleecate == 0)
                    {
                        unitOfWork.feesStructureMasterService.Insert(objSM);
                        unitOfWork.Save();

                        int mFeesStructID = unitOfWork.feesStructureMasterService.GetFeesStructID(objSM);

                        decimal FeesAmount = 0;

                        foreach (var product in updateValues.Insert)
                        {
                            if (updateValues.IsValid(product))
                            {
                                FeesAmount += decimal.Parse(product.DefaultAmount.ToString());
                                InsertProduct(product, updateValues, mFeesStructID, int.Parse(bit), int.Parse(Session["SessionID"].ToString()));
                            }
                        }
                        foreach (var product in updateValues.Update)
                        {
                            if (updateValues.IsValid(product))
                            {
                                FeesAmount += decimal.Parse(product.DefaultAmount.ToString());
                                InsertProduct(product, updateValues, mFeesStructID, int.Parse(bit), int.Parse(Session["SessionID"].ToString()));
                            }
                        }
                        objSM.FeeStructAmount = FeesAmount;
                        objSM.FeeStructID = mFeesStructID;
                        unitOfWork.feesStructureMasterService.UpdateFeesAmountMaster(objSM);
                        unitOfWork.Save();
                    }
                }
            }
            catch (Exception e)
            {


            }

            List<VFeesCompulsory> obj = new List<VFeesCompulsory>();
            ViewData["FeesClasses"] ="";
            return RedirectToAction("index", "FeesStructure");
        }
        protected void InsertProduct(VFeesCompulsory product, MVCxGridViewBatchUpdateValues<VFeesCompulsory, int> updateValues,int mFeesStructID,int mFeesClassID,int mStructSessionID)
        {
            try
            {
                unitOfWork.feesStructureDetailService.InsertProduct(product, mFeesStructID, mFeesClassID, mStructSessionID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }


        public ActionResult ListPartialClassSetup()
        {
            return PartialView("ListClassGridLookupPartial", unitOfWork.ClassService.GetClassList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult ListPartialTerm()
        {
            return PartialView("ListTermGridLookupPartial", unitOfWork.feeTermService.GetFeeTermList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["SessionID"].ToString())));
        }

        #endregion




    }
}
