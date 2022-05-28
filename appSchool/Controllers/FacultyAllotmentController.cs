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
    public class FacultyAllotmentController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;


        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appFacultyAllotment == 0)
            {
                return Redirect("~/");
            }

            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 79, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

            ViewData["FacultyAllotmentID"] = 0;
            ViewData["ClassID_ForFacultyAllotment"] = 0;
            ViewData["ClassSetupID_ForFacultyAllotment"] = 0;
            ViewData["FacultyID_ForFacultyAllotment"] = 0;

            return PartialView("Index");
        }

        public ActionResult PartialFacultyAllotmentView(int pFacultyAllotmentID, int pClassID, int pClassSetupID, int pFacultyID)
        {
            ViewData["FacultyAllotmentID"] = pFacultyAllotmentID;
            ViewData["ClassID_ForFacultyAllotment"] = pClassID;
            ViewData["ClassSetupID_ForFacultyAllotment"] = pClassSetupID;
            ViewData["FacultyID_ForFacultyAllotment"] = pFacultyID;

            return PartialView("ListFacultyAllotment",unitOfWork.FacultyAllotmentDetailService.GetFacultyAllotmentDetailListByFacultyAllotmentID(pFacultyAllotmentID,byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }
       

         public ActionResult GetClassSetupList(string pClassID)
         {
             List<ClassSetup> obj = new List<ClassSetup>();

             obj=unitOfWork.classSetupService.GetAllClassNameByClassID(int.Parse(pClassID));

             return PartialView("ClassSetupPartialView", obj);
         }

         public ActionResult GetFacultyListByClassID(string mClassID)
         {

              


             return PartialView("FacultyPartialView");
         }


         public string GetPreferredBookByClass(string pClassID, string pSubjectID)
         {
             int mClassID = int.Parse(pClassID);
             int mSubjectID = int.Parse(pSubjectID);
             string mPreferredBook = string.Empty;
             //string mPreferredBook=unitOfWork.ClassSyllabusMasterService.

             ClassSyllabusMaster objmaster = new ClassSyllabusMaster();
             objmaster = unitOfWork.ClassSyllabusMasterService.GetClassSyllabusDataByClassIDANDSubjectID(mClassID, mSubjectID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
             if (objmaster != null)
             {
                 mPreferredBook = objmaster.PreferredBooks;
             }


             return  mPreferredBook;
         }



         public ActionResult GetSubjectListView(string pClassID, string pClassSetupID, string pFacultyID)
         {
             int FacultyAllotmentID = 0;
             int mClassID = int.Parse(pClassID);
             int mClassSetupID = int.Parse(pClassSetupID);

             FacultyAllotmentMaster objFill = new FacultyAllotmentMaster();
             objFill.ClassID = int.Parse(pClassID);
             objFill.ClassSetupID = int.Parse(pClassSetupID);
             objFill.FacultyID = int.Parse(pFacultyID);
             objFill.CompID = byte.Parse(Session["CompID"].ToString());
             objFill.BranchID = byte.Parse(Session["BranchID"].ToString());
             objFill.SessionID = byte.Parse(Session["SessionID"].ToString());
             objFill.UIDAdd = byte.Parse(Session["UserID"].ToString());
             objFill.AddDate = DateTime.Now;


             FacultyAllotmentMaster objCheck = new FacultyAllotmentMaster();
             objCheck = unitOfWork.FacultyAllotmentMasterService.GetFacultyAllotmentMasterData(objFill);
             if (objCheck==null)
             {

                 unitOfWork.FacultyAllotmentMasterService.InsertFacultyAllotmentMaster(objFill);
                 unitOfWork.Save();
                 FacultyAllotmentID = objFill.FacultyAllotmentID;
             }
             else
             {
                 objCheck.UIDMod = byte.Parse(Session["UserID"].ToString());
                 objCheck.ModDate = DateTime.Now;

                 unitOfWork.FacultyAllotmentMasterService.UpdateFacultyAllotmentMaster(objCheck);
                 unitOfWork.Save();

                 FacultyAllotmentID = objCheck.FacultyAllotmentID;
             }

             List<FacultyAllotmentDetail> obj = new List<FacultyAllotmentDetail>();
             if (FacultyAllotmentID > 0)
             {
                 obj = unitOfWork.FacultyAllotmentDetailService.GetFacultyAllotmentDetailListByFacultyAllotmentID(FacultyAllotmentID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
             }

             ViewData["FacultyAllotmentID"] = FacultyAllotmentID;
             ViewData["ClassID_ForFacultyAllotment"] = pClassID;
             ViewData["ClassSetupID_ForFacultyAllotment"] = pClassSetupID;
             ViewData["FacultyID_ForFacultyAllotment"] = pFacultyID;

             return PartialView("ListFacultyAllotment", obj);
         }





   

        [ValidateInput(false)]
         public ActionResult updateFacultyAllotmentAll(MVCxGridViewBatchUpdateValues<FacultyAllotmentDetail, int> updateValues, int pFacultyAllotmentID, int pClassID, int pClassSetupID, int pFacultyID)
        {
            if (pFacultyAllotmentID == 0 || pClassID == 0 || pClassSetupID == 0 || pFacultyID == 0)
            {
                return PartialView("ListFacultyAllotment", new UnitOfWork().FacultyAllotmentDetailService.GetFacultyAllotmentDetailListByFacultyAllotmentID(pFacultyAllotmentID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
            }


            ViewData["FacultyAllotmentID"] = pFacultyAllotmentID;
            ViewData["ClassID_ForFacultyAllotment"] = pClassID;
            ViewData["ClassSetupID_ForFacultyAllotment"] = pClassSetupID;
            ViewData["FacultyID_ForFacultyAllotment"] = pFacultyID;


            foreach (var product in updateValues.Insert)
            {
                if (updateValues.IsValid(product))
                    InsertProduct(product, updateValues, pFacultyAllotmentID);
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


            return PartialView("ListFacultyAllotment", new UnitOfWork().FacultyAllotmentDetailService.GetFacultyAllotmentDetailListByFacultyAllotmentID(pFacultyAllotmentID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        protected void UpdateProduct(FacultyAllotmentDetail product, MVCxGridViewBatchUpdateValues<FacultyAllotmentDetail, int> updateValues)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {
                product.BranchID = byte.Parse(Session["BranchID"].ToString());
                product.CompID = byte.Parse(Session["CompID"].ToString());
                product.SessionID = byte.Parse(Session["SessionID"].ToString());
                product.SubjectIDL2 = -1;
                product.SubjectIDL3 = -1;
                unitOfWork.FacultyAllotmentDetailService.UpdateFacultyAllotmentDetail(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
        protected void DeleteProduct(int product, MVCxGridViewBatchUpdateValues<FacultyAllotmentDetail, int> updateValues)
        {
            try
            {
                unitOfWork.FacultyAllotmentDetailService.Delete(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
        protected void InsertProduct(FacultyAllotmentDetail product, MVCxGridViewBatchUpdateValues<FacultyAllotmentDetail, int> updateValues, int mFacultyAllotmentID)
        {
            try
            {


                product.BranchID = byte.Parse(Session["BranchID"].ToString());
                product.CompID = byte.Parse(Session["CompID"].ToString());
                product.SessionID = byte.Parse(Session["SessionID"].ToString());
                product.FacultyAllotmentID = mFacultyAllotmentID;

                unitOfWork.FacultyAllotmentDetailService.InsertFacultyAllotmentDetail(product);
                unitOfWork.Save();

            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
     
    

        public ActionResult StudentSessionGridRowChange(int ID)
        {
            return PartialView("RouteTabs", ID);
        }

    }


}



    