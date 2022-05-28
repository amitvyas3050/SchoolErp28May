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
    public class StudentFeeStructureController : Controller
    {
         private UnitOfWork unitOfWork = new UnitOfWork();



        public ActionResult Index()
        {
            if (Session["UserID"] == null )
            {
                return Redirect("~/");
            }

            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 39, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
        public ActionResult StudentFeeStructureView()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appStudentFeeStructure == 0)
            {
                return Redirect("~/");
            }

            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 39, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
            return PartialView("StudentFeeStructureView");
        }

         public ActionResult PartialGridAllStudentForSFS(int PclassSetupID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             ViewData["ClassIDForSFS"] = PclassSetupID;

             return PartialView("GridStudentListForSFS", new UnitOfWork().studentSessionService.GetAllStudentbyClassIDwithFeeFlagFromViewofStudentSession(PclassSetupID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }

         public ActionResult GetAllStudentListView(string mClassesID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             int newclassID = int.Parse(mClassesID);
             ViewData["ClassIDForSFS"] = newclassID;
             return PartialView("GridStudentListForSFS", new UnitOfWork().studentSessionService.GetAllStudentbyClassIDwithFeeFlagFromViewofStudentSession(newclassID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }


        public ActionResult SaveStudentFeeStructure(string mClassesID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            var text = EditorExtension.GetValue<string>("txtClassForSFS");

            int ClassID = int.Parse(mClassesID);
            byte mSessionID = byte.Parse(Session["SessionID"].ToString());
            ViewData["ClassIDForSFS"] = ClassID;

                try
                {
                    List<int?> objFSDetail = unitOfWork.feesStructureDetailService.GetFSDetailbyClassIDAndSessionIDwithDistinctTermID(ClassID, mSessionID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                    if (objFSDetail.Count ==0)
                    {
                        return PartialView("CallbackPanelPartial");
                    }

                    List<StudentSession> objSS = unitOfWork.studentSessionService.GetAllStudentbyClassIDwithFeeFlag(ClassID, mSessionID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                    foreach (StudentSession objStdSession in objSS)
                    {

                        int DuplicateStudMasterID = unitOfWork.StudentFeesMasterService.GetFeesStructIDForCheckDuplicateByStudentSession(objStdSession);
                        if (DuplicateStudMasterID == 0)
                        {
                          

                            if (objFSDetail.Count > 0)
                            {
                                foreach (int? objFSD in objFSDetail)
                                {

                                    StudentFeesMaster objSFM = new StudentFeesMaster();
                                    objSFM.StudentID = objStdSession.StudentID;
                                    objSFM.CompID = byte.Parse(Session["CompID"].ToString());
                                    objSFM.BranchID = byte.Parse(Session["BranchID"].ToString());
                                    objSFM.SessionID = byte.Parse(Session["SessionID"].ToString());
                                    objSFM.StudentClassId = ClassID;
                                    objSFM.TermID = objFSD;
                                    objSFM.Amount = 0;
                                    //objSFM.Amount = unitOfWork.feesStructureDetailService.GetTotalFeesAmountByClassIDwithTermIDwithSessionID(ClassID, mSessionID, objFSD, objStdSession.IsNewStudent);
                                    objSFM.Type = "Cr";
                                    unitOfWork.StudentFeesMasterService.Insert(objSFM);
                                    unitOfWork.Save();

                                    decimal? TotalFeesAmount =0;

                                    int mStudMasterID = unitOfWork.StudentFeesMasterService.GetFeesStructID(objSFM);
                                    List<FeesStructureDetail> objFSForDetail = unitOfWork.feesStructureDetailService.GetFeeStructureDetailbyClassIDAndSessionIDAndTermID(ClassID, mSessionID, objFSD, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                                    foreach (FeesStructureDetail objForDetailSave in objFSForDetail)
                                    {

                                        string IsOneTimeFee = unitOfWork.feesHeadService.GetByID(objForDetailSave.FeeHeadID).FeesHeadType;
                                        #region
                                        if (objStdSession.IsNewStudent == true)
                                        {
                                           

                                                StudentFeesDetail objStudentFeesDetail = new StudentFeesDetail();
                                                objStudentFeesDetail.StudentID = objStdSession.StudentID;
                                                objStudentFeesDetail.StudentClassId = ClassID;
                                                objStudentFeesDetail.StudMasterId = mStudMasterID;
                                                objStudentFeesDetail.HeadID = objForDetailSave.FeeHeadID;
                                                objStudentFeesDetail.TermID = objForDetailSave.FeeTermID;
                                                objStudentFeesDetail.CompID = byte.Parse(Session["CompID"].ToString());
                                                objStudentFeesDetail.BranchID = byte.Parse(Session["BranchID"].ToString());
                                                objStudentFeesDetail.SessionId = int.Parse(Session["SessionID"].ToString());
                                                objStudentFeesDetail.HeadAmount = objForDetailSave.FeeAmount;
                                                objStudentFeesDetail.Type = "Cr";
                                                unitOfWork.StudentFeesDetailService.Insert(objStudentFeesDetail);
                                                unitOfWork.Save();

                                                TotalFeesAmount = TotalFeesAmount + objForDetailSave.FeeAmount;
                                        }
                                        else
                                        {
                                            if (IsOneTimeFee != "OneTime")
                                            {
                                                StudentFeesDetail objStudentFeesDetail = new StudentFeesDetail();
                                                objStudentFeesDetail.StudentID = objStdSession.StudentID;
                                                objStudentFeesDetail.StudentClassId = ClassID;
                                                objStudentFeesDetail.StudMasterId = mStudMasterID;
                                                objStudentFeesDetail.HeadID = objForDetailSave.FeeHeadID;
                                                objStudentFeesDetail.TermID = objForDetailSave.FeeTermID;
                                                objStudentFeesDetail.CompID = byte.Parse(Session["CompID"].ToString());
                                                objStudentFeesDetail.BranchID = byte.Parse(Session["BranchID"].ToString());
                                                objStudentFeesDetail.SessionId = int.Parse(Session["SessionID"].ToString());
                                                objStudentFeesDetail.HeadAmount = objForDetailSave.FeeAmount;
                                                objStudentFeesDetail.Type = "Cr";
                                                unitOfWork.StudentFeesDetailService.Insert(objStudentFeesDetail);
                                                unitOfWork.Save();

                                                TotalFeesAmount = TotalFeesAmount + objForDetailSave.FeeAmount;
                                            }
                                        }
                                        #endregion

                                    }

                                    unitOfWork.StudentFeesMasterService.UpdateTotalFeesAmount(objSFM.StudentID,objSFM.TermID,objSFM.SessionID,TotalFeesAmount);
                                    unitOfWork.Save();
                                }

                              
                                objStdSession.FeesStructStatus = true;
                                unitOfWork.studentSessionService.UpdateFeesStructStatusinStudentSession(objStdSession);
                                unitOfWork.Save();
                            }
                            else
                            {
                                ViewData["errorFeestructure"] = "Fee Structure is not Design for this Classes";
                            }

                        }
                        else
                        {
                            ViewData["errorFeestructure"]= "Student All Ready Exist";
                        }

                    }
 
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }


           // FeesManager/index
                return PartialView("CallbackPanelPartial");
              //  return RedirectToRoute("StudentFeeStructureView");
        }











         public ActionResult PartialStudentSessionView()
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("ListStudentSessionView", unitOfWork.studentSessionService.GetAllStudentForSession(int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }
        
        [ValidateInput(false)]
        public ActionResult UpdateStudentSessionAll(MVCxGridViewBatchUpdateValues<vStudentSession, int> updateValues)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
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

            return PartialView("ListStudentSessionView", unitOfWork.studentSessionService.GetAllStudentForSession(int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
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


        public ActionResult StudentSessionGridRowChange(int ID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("RouteTabs", ID);
        }

    }


}



    