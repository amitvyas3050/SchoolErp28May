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
    public class ExamSetupController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;


        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appExamSetup == 0)
            {
                return Redirect("~/");
            }

            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 86, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

            ViewData["ExamSetupID"] = 0;
            ViewData["StartDate_ForExamSetup"] = DateTime.Now;
            ViewData["EndDate_ForExamSetup"] = DateTime.Now;
            ViewData["ExamID_ForExamSetup"] = 0;
            ViewData["Order_ForExamSetup"] = 0;
            ViewData["ClassID_ForExamSetup"] = 0;

            return View("Index");
        }

        public ActionResult PartialExamSetupView(int pExamSetupID, DateTime pStartDate, DateTime pEndDate, int pExamID, int pClassID, int pOrderID)
        {
            ViewData["ExamSetupID"] = pExamSetupID;
            ViewData["StartDate_ForExamSetup"] = pStartDate;
            ViewData["EndDate_ForExamSetup"] = pEndDate;
            ViewData["ExamID_ForExamSetup"] = pExamID;
            ViewData["Order_ForExamSetup"] = pOrderID;
            ViewData["ClassID_ForExamSetup"] = pClassID;

            return PartialView("ListExamSetup",unitOfWork.examSetupDetailService.GetExamSetupDetailListByExamSetupID(pExamSetupID,byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString())));
        }
       

         //public ActionResult GetClassSetupList(string pClassID)
         //{
         //    List<ClassSetup> obj = new List<ClassSetup>();

         //    obj=unitOfWork.classSetupService.GetAllClassNameByClassID(int.Parse(pClassID));

         //    return PartialView("ClassSetupPartialView", obj);
         //}

         //public string GetPreferredBookByClass(string pClassID, string pSubjectID)
         //{
         //    int mClassID = int.Parse(pClassID);
         //    int mSubjectID = int.Parse(pSubjectID);
         //    string mPreferredBook = string.Empty;
              
         //    ClassSyllabusMaster objmaster = new ClassSyllabusMaster();
         //    objmaster = unitOfWork.ClassSyllabusMasterService.GetClassSyllabusDataByClassIDANDSubjectID(mClassID, mSubjectID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
         //    if (objmaster != null)
         //    {
         //        mPreferredBook = objmaster.PreferredBooks;
         //    }

         //    return  mPreferredBook;
         //}


         public ActionResult CheckDuplicateMasterAction(string pExamID, string pClassID)
         {
             int ExamSetupID = 0;
             int mClassID = int.Parse(pClassID);
             int mExamID = int.Parse(pExamID);

             ExamSetupMaster objFill = new ExamSetupMaster();
             objFill.ClassID = int.Parse(pClassID);
             objFill.ExamID = int.Parse(pExamID);
             objFill.CompID = byte.Parse(Session["CompID"].ToString());
             objFill.BranchID = byte.Parse(Session["BranchID"].ToString());
             objFill.SessionID = byte.Parse(Session["SessionID"].ToString());
           
             ExamSetupMaster objCheck = new ExamSetupMaster();
             objCheck = unitOfWork.examSetupMasterService.GetExamSetupMasterData(objFill);

             return PartialView("DateOrderPartialView", objCheck);
         }

         public ActionResult GetExamSetupDetailView(string pClassID, string pExamID, DateTime pStartDate, DateTime pEndDate, string pOrderID)
         {
             int ExamSetupID = 0;
             int mClassID = int.Parse(pClassID);
             int mExamID = int.Parse(pExamID);
             int mOrderID = int.Parse(pOrderID);


             ExamSetupMaster objFill = new ExamSetupMaster();
             objFill.ClassID = int.Parse(pClassID);
             objFill.ExamID = int.Parse(pExamID);
             objFill.ExamOrder = int.Parse(pOrderID);
             objFill.StartDate = pStartDate;
             objFill.EndDate = pEndDate;
             objFill.CompID = byte.Parse(Session["CompID"].ToString());
             objFill.BranchID = byte.Parse(Session["BranchID"].ToString());
             objFill.SessionID = byte.Parse(Session["SessionID"].ToString());
             objFill.UIDAdd = byte.Parse(Session["UserID"].ToString());
             objFill.AddDate = DateTime.Now;

             ExamSetupMaster objCheck = new ExamSetupMaster();
             objCheck = unitOfWork.examSetupMasterService.GetExamSetupMasterData(objFill);
             if (objCheck==null)
             {
                 unitOfWork.examSetupMasterService.InsertExamSetupMaster(objFill);
                 unitOfWork.Save();
                 ExamSetupID = objFill.ExamSetupID;
             }
             else
             {
                 objCheck.UIDMod = byte.Parse(Session["UserID"].ToString());
                 objCheck.ModDate = DateTime.Now;

                 unitOfWork.examSetupMasterService.UpdateExamSetupMaster(objCheck);
                 unitOfWork.Save();

                 ExamSetupID = objCheck.ExamSetupID;
             }

             List<ExamSetupDetail> obj = new List<ExamSetupDetail>();
             if (ExamSetupID > 0)
             {
                 obj = unitOfWork.examSetupDetailService.GetExamSetupDetailListByExamSetupID(ExamSetupID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                 if (obj.Count == 0)
                 {
                     List<SubjectAllotment> objSubAllot = new List<SubjectAllotment>();
                     objSubAllot = unitOfWork.examSetupDetailService.GetSubjectAllotmentListByClassID(mClassID,byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString()));
                     if (objSubAllot != null)
                     {
                         foreach(SubjectAllotment objSubAllotDetail in objSubAllot)
                         {
                             ExamSetupDetail objDetail = new ExamSetupDetail();
                             objDetail.BranchID = byte.Parse(Session["BranchID"].ToString());
                             objDetail.CompID = byte.Parse(Session["CompID"].ToString());
                             objDetail.ExamSetupID = ExamSetupID;
                             objDetail.SessionId = byte.Parse(Session["SessionID"].ToString());
                             objDetail.SubjectIDL1 = objSubAllotDetail.IDL1;
                             objDetail.SubjectIDL2 = objSubAllotDetail.IDL2;
                             objDetail.SubjectIDL3 = objSubAllotDetail.IDL3;

                             unitOfWork.examSetupDetailService.InsertExamSetupDetail(objDetail);
                             unitOfWork.Save();
                         }
                     }
                     obj = unitOfWork.examSetupDetailService.GetExamSetupDetailListByExamSetupID(ExamSetupID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                 }
             }

             ViewData["ExamSetupID"] = ExamSetupID;
             ViewData["StartDate_ForExamSetup"] = pStartDate;
             ViewData["EndDate_ForExamSetup"] = pEndDate;
             ViewData["ExamID_ForExamSetup"] = mExamID;
             ViewData["Order_ForExamSetup"] = mOrderID;
             ViewData["ClassID_ForExamSetup"] = mClassID;

             return PartialView("ListExamSetup", obj);
         }

         public ActionResult GetExamSetupListForupdateData(string pClassID, string pExamID, DateTime pStartDate, DateTime pEndDate, string pOrderID)
         {
             int ExamSetupID = 0;
             int mClassID = int.Parse(pClassID);
             int mExamID = int.Parse(pExamID);
             int mOrderID = int.Parse(pOrderID);


             ExamSetupMaster objFill = new ExamSetupMaster();
             objFill.ClassID = int.Parse(pClassID);
             objFill.ExamID = int.Parse(pExamID);
             objFill.ExamOrder = int.Parse(pOrderID);
             objFill.StartDate = pStartDate;
             objFill.EndDate = pEndDate;
             objFill.CompID = byte.Parse(Session["CompID"].ToString());
             objFill.BranchID = byte.Parse(Session["BranchID"].ToString());
             objFill.SessionID = byte.Parse(Session["SessionID"].ToString());
             objFill.UIDAdd = byte.Parse(Session["UserID"].ToString());
             objFill.AddDate = DateTime.Now;

             ExamSetupMaster objCheck = new ExamSetupMaster();
             objCheck = unitOfWork.examSetupMasterService.GetExamSetupMasterData(objFill);
             if (objCheck == null)
             {
                 unitOfWork.examSetupMasterService.InsertExamSetupMaster(objFill);
                 unitOfWork.Save();
                 ExamSetupID = objFill.ExamSetupID;
             }
             else
             {
                 objCheck.UIDMod = byte.Parse(Session["UserID"].ToString());
                 objCheck.ModDate = DateTime.Now;

                 unitOfWork.examSetupMasterService.UpdateExamSetupMaster(objCheck);
                 unitOfWork.Save();

                 ExamSetupID = objCheck.ExamSetupID;
             }

             List<ExamSetupDetail> obj = new List<ExamSetupDetail>();
             if (ExamSetupID > 0)
             {
                 obj = unitOfWork.examSetupDetailService.GetExamSetupDetailListByExamSetupID(ExamSetupID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                 if (obj.Count == 0)
                 {
                     List<SubjectAllotment> objSubAllot = new List<SubjectAllotment>();
                     objSubAllot = unitOfWork.examSetupDetailService.GetSubjectAllotmentListByClassID(mClassID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                     if (objSubAllot != null)
                     {
                         foreach (SubjectAllotment objSubAllotDetail in objSubAllot)
                         {
                             ExamSetupDetail objDetail = new ExamSetupDetail();
                             objDetail.BranchID = byte.Parse(Session["BranchID"].ToString());
                             objDetail.CompID = byte.Parse(Session["CompID"].ToString());
                             objDetail.ExamSetupID = ExamSetupID;
                             objDetail.SessionId = byte.Parse(Session["SessionID"].ToString());
                             objDetail.SubjectIDL1 = objSubAllotDetail.IDL1;
                             objDetail.SubjectIDL2 = objSubAllotDetail.IDL2;
                             objDetail.SubjectIDL3 = objSubAllotDetail.IDL3;

                             unitOfWork.examSetupDetailService.InsertExamSetupDetail(objDetail);
                             unitOfWork.Save();
                         }
                     }
                     obj = unitOfWork.examSetupDetailService.GetExamSetupDetailListByExamSetupID(ExamSetupID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                 }
                 else
                 {

                     foreach (ExamSetupDetail objdelete in obj)
                     {
                         unitOfWork.examSetupDetailService.Delete(objdelete.ExamSetupDetailID);
                         unitOfWork.Save();
                     }

                     obj = null;

                     List<SubjectAllotment> objSubAllot = new List<SubjectAllotment>();
                     objSubAllot = unitOfWork.examSetupDetailService.GetSubjectAllotmentListByClassID(mClassID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                     if (objSubAllot != null)
                     {
                         foreach (SubjectAllotment objSubAllotDetail in objSubAllot)
                         {
                             ExamSetupDetail objDetail = new ExamSetupDetail();
                             objDetail.BranchID = byte.Parse(Session["BranchID"].ToString());
                             objDetail.CompID = byte.Parse(Session["CompID"].ToString());
                             objDetail.ExamSetupID = ExamSetupID;
                             objDetail.SessionId = byte.Parse(Session["SessionID"].ToString());
                             objDetail.SubjectIDL1 = objSubAllotDetail.IDL1;
                             objDetail.SubjectIDL2 = objSubAllotDetail.IDL2;
                             objDetail.SubjectIDL3 = objSubAllotDetail.IDL3;

                             unitOfWork.examSetupDetailService.InsertExamSetupDetail(objDetail);
                             unitOfWork.Save();
                         }
                     }
                     obj = unitOfWork.examSetupDetailService.GetExamSetupDetailListByExamSetupID(ExamSetupID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));


                 }

             }

             ViewData["ExamSetupID"] = ExamSetupID;
             ViewData["StartDate_ForExamSetup"] = pStartDate;
             ViewData["EndDate_ForExamSetup"] = pEndDate;
             ViewData["ExamID_ForExamSetup"] = mExamID;
             ViewData["Order_ForExamSetup"] = mOrderID;
             ViewData["ClassID_ForExamSetup"] = mClassID;



             return PartialView("ListExamSetup",obj);
         }

        [ValidateInput(false)]
         public ActionResult updateExamSetupAll(MVCxGridViewBatchUpdateValues<ExamSetupDetail, int> updateValues, int pExamSetupID, DateTime pStartDate, DateTime pEndDate , int pExamID, int pClassID, int pOrderID)
        {
            ViewData["ExamSetupID"] = pExamSetupID;
            ViewData["StartDate_ForExamSetup"] = pStartDate;
            ViewData["EndDate_ForExamSetup"] = pEndDate;
            ViewData["ExamID_ForExamSetup"] = pExamID;
            ViewData["Order_ForExamSetup"] = pOrderID;
            ViewData["ClassID_ForExamSetup"] = pClassID;

            if (pExamSetupID == 0)
            {
                return PartialView("ListExamSetup", new UnitOfWork().examSetupDetailService.GetExamSetupDetailListByExamSetupID(pExamSetupID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
            }

          
            //foreach (var product in updateValues.Insert)
            //{
            //    if (updateValues.IsValid(product))
            //        InsertProduct(product, updateValues, pExamSetupID);
            //}
            foreach (var product in updateValues.Update)
            {
                if (updateValues.IsValid(product))
                    UpdateProduct(product, updateValues);
            }
            foreach (var productID in updateValues.DeleteKeys)
            {
                DeleteProduct(productID, updateValues);
            }
        
            return PartialView("ListExamSetup", new UnitOfWork().examSetupDetailService.GetExamSetupDetailListByExamSetupID(pExamSetupID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        protected void UpdateProduct(ExamSetupDetail product, MVCxGridViewBatchUpdateValues<ExamSetupDetail, int> updateValues)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {
                product.BranchID = byte.Parse(Session["BranchID"].ToString());
                product.CompID = byte.Parse(Session["CompID"].ToString());

                unitOfWork.examSetupDetailService.UpdateExamSetupDetail(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
        protected void DeleteProduct(int product, MVCxGridViewBatchUpdateValues<ExamSetupDetail, int> updateValues)
        {
            try
            {
                unitOfWork.examSetupDetailService.Delete(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
        protected void InsertProduct(ExamSetupDetail product, MVCxGridViewBatchUpdateValues<ExamSetupDetail, int> updateValues, int mExamSetupID)
        {
            try
            {

                product.BranchID = byte.Parse(Session["BranchID"].ToString());
                product.CompID = byte.Parse(Session["CompID"].ToString());
                product.ExamSetupID = mExamSetupID;
                unitOfWork.examSetupDetailService.InsertExamSetupDetail(product);
                unitOfWork.Save();

            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }

    }


}



    