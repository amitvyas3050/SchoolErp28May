using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.ViewModels;
using appSchool.Repositories;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using Newtonsoft.Json;
using System.Web.SessionState;

namespace appSchool.Controllers
{
    [NoCache]
    public class FeesTransactionController : Controller
    {
        //
        // GET: /FeesStructure/


        UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appFeesTransaction == 0)
            {
                return Redirect("~/");
            }


            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 50, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

            modelStudentFeesReceipt obj = new modelStudentFeesReceipt();
            obj.ReceiptNo = 100;
            //obj.ReceiptNo = 1;

            return View("Index",obj);
        }

        public ActionResult GetAllTermForClasswise(int RegID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            return PartialView("ListTermView", unitOfWork.feesReceiptService.GetTermListForFeesReceiptStudentWise(RegID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public JsonResult GetStudentData(int mStudentID)
        {
            string ErrorMsg = string.Empty;
           
            var studentclass = string.Empty;
            var studentenrollno = string.Empty;
            var studentmobileno = string.Empty;
            var studentfathername = string.Empty;

            StudentRegistration objstud = unitOfWork.studentRegistrationService.GetProfileInfo(mStudentID);
            if(objstud != null)
            {
                 studentclass = objstud.ClassID.ToString();
                 studentenrollno = objstud.EnrollmentNo;
                 studentmobileno = objstud.SMSMobileNo;
                 studentfathername = objstud.FatherName;
            }

            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new
                {
                    studentClass = studentclass,
                    studentEnrollno = objstud.EnrollmentNo,
                    studentMobileno = objstud.SMSMobileNo,
                    studentFathername = objstud.FatherName,
                 }
            };



        }

        public string CheckDuplicateReciept(int mTermID, int mStudentID)
        {
            string error=string.Empty;
            StudentFeesMaster objFeeMaster = unitOfWork.StudentFeesMasterService.GetFeesDetailForStudentFeesReceipt(mStudentID, mTermID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            if (objFeeMaster.PaidFlag == false)
            {
                return error="Not duplicate";
            }
            else
            {
                return error = "Paid"; ;
            }

        }

        public ActionResult GetStudentDataforFeeReceipt(int mTermID,int mStudentID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }

            StudentFeesMaster objFeeMaster = unitOfWork.StudentFeesMasterService.GetFeesDetailForStudentFeesReceipt(mStudentID, mTermID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            if (objFeeMaster.PaidFlag == false)
            {
                StudentRegistration obj = unitOfWork.studentRegistrationService.GetByID(mStudentID);

                modelStudentFeesReceipt objFeeReceipt = new modelStudentFeesReceipt();
                objFeeReceipt.StudentID = obj.StudentID;
                objFeeReceipt.EnrollmentNo = obj.EnrollmentNo;
                objFeeReceipt.StudentName = obj.FirstName + " " + obj.LastName;
                objFeeReceipt.FatherName = obj.FatherName;
                objFeeReceipt.MobileNo = obj.SMSMobileNo;
                objFeeReceipt.ClassID = int.Parse(obj.ClassID.ToString());
                objFeeReceipt.Class = unitOfWork.studentSessionService.GetClassDescriptionByStudentIDandClassIDandSessionID(mStudentID, int.Parse(objFeeMaster.StudentClassId.ToString()), int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                FeeTerm objterm = new FeeTerm();
                objterm = unitOfWork.feeTermService.GetByID(mTermID);

                if(objterm != null)
                {
                    objFeeReceipt.TermName = objterm.FeeTermType;
                    objFeeReceipt.TermId = mTermID;
                    objFeeReceipt.TermTotal = decimal.Parse(objFeeMaster.Amount.ToString());
                    if (objterm.FeeTermToDate.Date < DateTime.Now.Date)
                    {
                        objFeeReceipt.FineAmount = 100;
                    }
                    else
                    {
                        objFeeReceipt.FineAmount = 0;
                    }
                }
                
                objFeeReceipt.ReceiptNo = unitOfWork.feesReceiptService.GetReceiptNoFromSetup(int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                objFeeReceipt.ReceiptDate = DateTime.Now;
                objFeeReceipt.DiscountAmount = 0;
                objFeeReceipt.DiscountPercent = 0;
                objFeeReceipt.OtherAmount = 0;
                objFeeReceipt.SubTotal = 0;
                objFeeReceipt.SubTotal = objFeeReceipt.TermTotal + objFeeReceipt.FineAmount + objFeeReceipt.OtherAmount;
                objFeeReceipt.FinalAmount = objFeeReceipt.SubTotal - objFeeReceipt.DiscountAmount;

                return PartialView("FeesTransactionView", objFeeReceipt);
            }
            else
            {
                modelStudentFeesReceipt objFeeReceipt = new modelStudentFeesReceipt();
                return PartialView("FeesTransactionView", objFeeReceipt);
            }
                      
        }

        [HttpPost]
        public ActionResult SaveFeesReceipt(modelStudentFeesReceipt obj)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            
            if (ModelState.IsValid)
            {
                try
                {

                    bool isDuplicate = false;
                    isDuplicate = unitOfWork.feeCollectionMasterService.CheckDuplicateData(obj.StudentID, obj.TermId, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                    if (isDuplicate == false)
                    {
                        FeesCollectionMaster objFeesCollMaster = new FeesCollectionMaster();

                        objFeesCollMaster.ReceiptNo = obj.ReceiptNo;
                        objFeesCollMaster.ReceiptDate = obj.ReceiptDate;
                        objFeesCollMaster.StudentID = obj.StudentID;
                        objFeesCollMaster.ClassID = obj.ClassID;
                        objFeesCollMaster.TermIds = obj.TermId.ToString();
                        objFeesCollMaster.Mode = obj.Mode;
                        objFeesCollMaster.BankName = obj.BankName;
                        objFeesCollMaster.BranchName = obj.BranchName;
                        objFeesCollMaster.ChequeNo = obj.ChequeNo;
                        objFeesCollMaster.ChequeDate = obj.ChequeDate;
                        objFeesCollMaster.Remark = obj.Remark;
                        objFeesCollMaster.FinalTotal = obj.FinalAmount;
                        objFeesCollMaster.FineAmount = obj.FineAmount;
                        objFeesCollMaster.OtherAmount = obj.OtherAmount;
                        objFeesCollMaster.TermTotal = obj.TermTotal;
                        objFeesCollMaster.PaidAmount = obj.FinalAmount;
                        objFeesCollMaster.DiscountType = obj.DiscountType;
                        objFeesCollMaster.DiscountPercent = obj.DiscountPercent;
                        objFeesCollMaster.DiscountAmount = obj.DiscountAmount;

                        objFeesCollMaster.SessionID = byte.Parse(Session["SessionID"].ToString());
                        objFeesCollMaster.CompID = byte.Parse(Session["CompID"].ToString());
                        objFeesCollMaster.BranchID = byte.Parse(Session["BranchID"].ToString());
                        objFeesCollMaster.UIDAdd = byte.Parse(Session["UserID"].ToString());
                        objFeesCollMaster.AddDate = DateTime.Now;

                        unitOfWork.feeCollectionMasterService.Insert(objFeesCollMaster);
                        unitOfWork.Save();

                        List<StudentFeesDetail> objListStudeFeeDetail = new List<StudentFeesDetail>();
                        objListStudeFeeDetail = unitOfWork.StudentFeesDetailService.GetStudentFeesDetailByTermIDandStudentID(int.Parse(objFeesCollMaster.StudentID.ToString()), int.Parse(objFeesCollMaster.TermID.ToString()), int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                        foreach (StudentFeesDetail objdetail in objListStudeFeeDetail)
                        {
                            FeesCollectionDetail objFeeCollDetail = new FeesCollectionDetail();
                            objFeeCollDetail.ReceiptNo = objFeesCollMaster.ReceiptNo;
                            objFeeCollDetail.SessionID = byte.Parse(Session["SessionID"].ToString());
                            objFeeCollDetail.Amount = objdetail.HeadAmount;
                            objFeeCollDetail.FeesHeadID = objdetail.HeadID;
                            objFeeCollDetail.FeeTermID = objdetail.TermID;
                            objFeeCollDetail.CompID = byte.Parse(Session["CompID"].ToString());
                            objFeeCollDetail.BranchID = byte.Parse(Session["BranchID"].ToString());

                            unitOfWork.feeCollectionDetailService.Insert(objFeeCollDetail);
                            unitOfWork.Save();

                        }

                        long mReceiptNo = objFeesCollMaster.ReceiptNo + 1;

                        unitOfWork.setupService.UpdateReceiptNo(int.Parse(objFeesCollMaster.SessionID.ToString()), mReceiptNo, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                        unitOfWork.Save();
                        decimal PaidAmount = decimal.Parse(obj.FinalAmount.ToString());
                        unitOfWork.StudentFeesMasterService.UpdateFeesPaidFlagINStudentFeesMaster(int.Parse(objFeesCollMaster.StudentID.ToString()), int.Parse(objFeesCollMaster.TermID.ToString()), PaidAmount, int.Parse(objFeesCollMaster.SessionID.ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                        unitOfWork.Save();

                    }
                    else
                    {
                        ViewData["Mesage"] ="Data allready Exist";
                    }

                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            modelStudentFeesReceipt objnext = new modelStudentFeesReceipt();
            return PartialView("FeesTransactionEditView", objnext);
        }









        #region AddFeesTransactions

      
        public string GetClassDescription(string mClassSetupID)
        {

            string[] bits = mClassSetupID.Split(',');
            string mNewClassID = string.Empty;
            string ErrorMsg = string.Empty;
            string dupclass=string.Empty;
            foreach (string bit in bits)
            {
                int mFeesStructID = unitOfWork.feesStructureMasterService.GetClassExist(int.Parse(bit), int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
        public ActionResult GetAllStudentListView(string mClassesID)
        {
            int newclassID = int.Parse(mClassesID);
            ViewData["ClassSetupIDForFee"] = newclassID;
            return PartialView("GridStudentListForFee", new UnitOfWork().studentSessionService.GetStudentForFeeClasswise(newclassID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public ActionResult AllFeesTermTransaction(int RegID)
        {

            modelFeeTransaction obj = new modelFeeTransaction();

            modelFeesStructureDetail objdetail = new modelFeesStructureDetail();
            objdetail.FeeStructSessionID = 1;
            objdetail.FeeStructID = 1;
            objdetail.FeeAmount = 100;
            objdetail.FeeStructDetailID = 1;

            List<modelFeesStructureDetail> objldetail = new List<modelFeesStructureDetail>();
            objldetail.Add(objdetail);

            obj.feesStructDetail=objldetail;


            return PartialView("FeesTransactionView",obj);
        }



        public ActionResult PartialGridAllStudentForFee()
        {


            return PartialView("GridStudentListForFee", new UnitOfWork().studentSessionService.GetAllStudentForSession(int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult ListPartialStudentList()
        {

            return PartialView("ListStudentGridLookupPartial", new UnitOfWork().studentSessionService.GetAllStudentForSession(int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        [HttpPost]
        public ActionResult AddFee(modelFeeTransaction obj)
        {



            //if (!Request.IsAjaxRequest())
            //{ // Theme changing
            //    ModelState.Clear();
            //    return View("Index", obj);
            //}

            // Intentionally pauses server-side processing,
            // to demonstrate the Loading Panel functionality.
            //Thread.Sleep(1000);
            //if (ModelState.IsValid)
            //{

            //    UserLogin objuser = new UserLogin();

            //    objuser = new UnitOfWork().userLoginService.IsValid(obj);

            //    if (objuser != null)
            //    {
            //        Session["UserID"] = objuser.UserID;
            //        Session["SessionID"] = obj.SessionID;
            //        Session["SessionName"] = unitOfWork.academicYearService.GetByID(Session["SessionID"]).SessionName;
            //        Session["FullName"] = objuser.FullName;

            //        return RedirectToAction("Index", "Masters");

            //    }
            //    else
            //    {
            //        return View("Index", obj);
            //    }

            //    //object redirectActionName = "Index";
            //    //return PartialView("Index");
            //}
            //else
            //    return PartialView("Index", obj);


            return PartialView("Index");
        }
        public ActionResult PartialFeeTermView(VFeesCompulsory obj, string mClasses)
        {

            return PartialView("ListAllFeesTerms", new UnitOfWork().feesHeadService.GetTermHeadForFeeStructure(mClasses, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["SessionID"].ToString())));
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
              return PartialView("AddFeesStructure");
            }


            try
            {
                string[] bits = mClasses.Split(',');
                foreach (string bit in bits)
                {
                    FeesStructureMaster objSM = new FeesStructureMaster();
                    objSM.FeeStructSessionID = int.Parse(Session["SessionID"].ToString());
                    objSM.CompID = byte.Parse(Session["CompID"].ToString());
                    objSM.BranchID = byte.Parse(Session["BranchID"].ToString());
                    objSM.UIDAdd = byte.Parse(Session["UserID"].ToString());
                    objSM.AddDate = DateTime.Now;
                    objSM.FeesStructClassID = int.Parse(bit);

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
                    unitOfWork.feesStructureMasterService.Update(objSM);
                    unitOfWork.Save();


                }
            }
            catch (Exception e)
            {

            }
            return PartialView("AddFeesStructure");
        }
        protected void InsertProduct(VFeesCompulsory product, MVCxGridViewBatchUpdateValues<VFeesCompulsory, int> updateValues,int mFeesStructID,int mFeesClassID,int mStructSessionID)
        {
            try
            {

                unitOfWork.feesStructureDetailService.InsertProduct(product,mFeesStructID,mFeesClassID,mStructSessionID,byte.Parse(Session["CompID"].ToString()),byte.Parse(Session["BranchID"].ToString()));
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
