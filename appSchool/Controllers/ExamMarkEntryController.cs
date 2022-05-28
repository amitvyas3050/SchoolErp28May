using appSchool.Repositories;
using appSchool.ViewModels;
using DevExpress.Web.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace appSchool.Controllers
{
    [NoCache]
    public class ExamMarkEntryController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;


        public ActionResult Index()
        {


            return View();
        }

        public JsonResult GetExamListView(int mClassID)
        {
            string ErrorMsg = string.Empty;
            bool msgFlag = false;
            var DataExamList =string.Empty;
            var DataClassSetupList = string.Empty;
            var DataSub1List = string.Empty;
            if (mClassID > 0)
            {
                List<ExamMaster> objlst = new List<ExamMaster>();
                objlst = unitOfWork.examSetupMasterService.GetExamListFromExamSetupMasterByClasswise(mClassID, byte.Parse(Session["SessionID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["CompID"].ToString()));
                if (objlst.Count == 0)
                {
                    ErrorMsg += " Exams Not Founds.";
                    msgFlag = true;
                }

                List<ClassSetup> objClassSetup = new List<ClassSetup>();
                objClassSetup = unitOfWork.classSetupService.GetAllClassNameByClassID(mClassID);
                if (objClassSetup.Count == 0)
                {
                    ErrorMsg += " Section Not Found. ";
                    msgFlag = true;
                }

               
             
                
                    List<SubjectLevelOne> objSub1list = unitOfWork.examSetupMasterService.GetSubjectLevelOneListFromExamSetup(byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["SessionID"].ToString()), mClassID);
                    if (objSub1list.Count==0)
                    {
                        ErrorMsg += " Subject Not Found. ";
                        msgFlag = true;
                    }
                    DataExamList = JsonConvert.SerializeObject(objlst);
                    DataClassSetupList = JsonConvert.SerializeObject(objClassSetup);
                    DataSub1List = JsonConvert.SerializeObject(objSub1list);
            }
            else
            {
                ErrorMsg += " Please Select Class. ";
                msgFlag = true;
            }


            return new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new  { 
                    Status=msgFlag,
                    DisplayMsg=ErrorMsg,
                    ExamNameList = DataExamList,
                    ClassSetupList = DataClassSetupList  ,
                    SubjectList = DataSub1List
                  }
            };

           

        }

        public JsonResult GetStudentListForMarkEntry(int mCLassID, int mClassSetupID,int mExamID, int mSubjectID)
        {
            string ErrorMsg = string.Empty;
            bool msgStatus = false;
            int ExamOrder = 0;
            int MaxMark = 0;
            int MinMark = 0;
            string MarkType = string.Empty;

            ViewData["ClassID"] = mCLassID;
            ViewData["ClassSetupID"] = mClassSetupID;
            ViewData["ExamID"] = mExamID;
            ViewData["SubjectID"] = mSubjectID;

            string sql = " SELECT DISTINCT  dbo.ExamSetupMaster.ExamID, dbo.ExamSetupMaster.ClassID, dbo.ExamSetupMaster.ExamOrder, dbo.ExamSetupDetail.SubjectIDL1, "+
                         " dbo.ExamSetupDetail.MinMark, dbo.ExamSetupDetail.MaxMark, dbo.ExamSetupDetail.MarksType "+
                         " FROM   dbo.ExamSetupMaster INNER JOIN "+
                         " dbo.ExamSetupDetail ON dbo.ExamSetupMaster.SessionID = dbo.ExamSetupDetail.SessionId AND "+
                         " dbo.ExamSetupMaster.ExamSetupID = dbo.ExamSetupDetail.ExamSetupID AND dbo.ExamSetupMaster.CompID = dbo.ExamSetupDetail.CompID AND "+
                         " dbo.ExamSetupMaster.BranchID = dbo.ExamSetupDetail.BranchID "+
                         " Where dbo.ExamSetupMaster.CLassID=" + mCLassID + " AND dbo.ExamSetupMaster.CompID="+byte.Parse(Session["CompID"].ToString())+" AND " +
                         " dbo.ExamSetupMaster.ExamID=" + mExamID + " AND dbo.ExamSetupDetail.SubjectIDL1="+mSubjectID;

            DataRow dr = DB.ExecuteSingleRow(sql);
            if (dr != null)
            {
                ExamOrder = int.Parse(dr["ExamOrder"].ToString());
                MaxMark = int.Parse(dr["MaxMark"].ToString());
                MinMark = int.Parse(dr["MinMark"].ToString());
                MarkType = dr["MarksType"].ToString();

                ViewData["ExamOrder"] = ExamOrder;

                List<vExamMarkEntry> objLst = new List<vExamMarkEntry>();
                objLst = unitOfWork.vExamMarkEntryService.GetStudentListForMarkEntry(mCLassID, mClassSetupID, mExamID, int.Parse(ExamOrder.ToString()), MaxMark, MinMark, mSubjectID, byte.Parse(Session["UserID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["SessionID"].ToString()));

                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        DisplayMsg = ErrorMsg,
                        Status = msgStatus,
                        DataList = cCommon.RenderRazorViewToString("GridStudentMarkList", objLst, ControllerContext, ViewData, TempData)
                    }
                };
            }



            else
            {
                 List<vExamMarkEntry> objList = new List<vExamMarkEntry>();
                  objList = null;
                  ErrorMsg = "Data Not Found";

                  return new JsonResult()
                 {
                      JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                     Data = new
                     {
                        DisplayMsg = ErrorMsg,
                        Status = msgStatus,
                        DataList = cCommon.RenderRazorViewToString("GridStudentMarkList", objList, ControllerContext, ViewData, TempData)
                     }
                 };


            }
        }


        public ActionResult PartialStudentMarkList(int PClassID,int PClassSetupID, int PExamID, int PSubjectID, int PExamOrder)
        {
           
            int MaxMark = 0;
            int MinMark = 0;
            ViewData["ClassID"] = PClassID;
            ViewData["ClassSetupID"] = PClassSetupID;
            ViewData["ExamID"] = PExamID;
            ViewData["SubjectID"] = PSubjectID;
            ViewData["ExamOrder"] = PExamOrder;


            List<vExamMarkEntry> objLst = new List<vExamMarkEntry>();
            objLst = unitOfWork.vExamMarkEntryService.GetStudentListForMarkEntry(PClassID, PClassSetupID, PExamID, PExamOrder, MaxMark, MinMark, PSubjectID, byte.Parse(Session["UserID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["SessionID"].ToString()));

            return PartialView("GridStudentMarkList",objLst);
        }



        [ValidateInput(false)]
        public ActionResult updateStudentMarkAll(MVCxGridViewBatchUpdateValues<vExamMarkEntry, int> updateValues, int PClassID, int PClassSetupID, int PExamID, int PSubjectID,int PExamOrder)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            int MaxMark = 0;
            int MinMark = 0;
            string MarkType = string.Empty;
            ViewData["ClassID"] = PClassID;
            ViewData["ClassSetupID"] = PClassSetupID;
            ViewData["ExamID"] = PExamID;
            ViewData["SubjectID"] = PSubjectID;
            ViewData["ExamOrder"] = PExamOrder;

          




            foreach (var product in updateValues.Update)
            {
                if (updateValues.IsValid(product))
                    UpdateProduct(product, updateValues,PClassID,PClassSetupID,PExamID,PSubjectID,PExamOrder);
            }

            List<vExamMarkEntry> objLst = new List<vExamMarkEntry>();
            objLst = unitOfWork.vExamMarkEntryService.GetStudentListForMarkEntry(PClassID, PClassSetupID, PExamID, PExamOrder, MaxMark, MinMark, PSubjectID, byte.Parse(Session["UserID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["SessionID"].ToString()));


            return PartialView("GridStudentMarkList",objLst);
        }

        protected void UpdateProduct(vExamMarkEntry product, MVCxGridViewBatchUpdateValues<vExamMarkEntry, int> updateValues,int ClassID,int ClassSetupID,int ExamID,int SubjectID,int ExamOrder)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {
               
                SqlCommand cmdMaster = new SqlCommand("Update_ExamResult", _mConn);
                cmdMaster.CommandType = CommandType.StoredProcedure;
                cmdMaster.Transaction = _mTran;

                cmdMaster.Parameters.AddWithValue("@StudentID", product.StudentID);
                cmdMaster.Parameters.AddWithValue("@ClassID", ClassID);
                cmdMaster.Parameters.AddWithValue("@ExamID",ExamID);
                cmdMaster.Parameters.AddWithValue("@SubjectID", SubjectID);
                cmdMaster.Parameters.AddWithValue("@ObtainMark", product.ObtainMark);
                cmdMaster.Parameters.AddWithValue("@IsAbsent", product.IsAbsent);
                cmdMaster.Parameters.AddWithValue("@ExamOrder", ExamOrder);
                cmdMaster.Parameters.AddWithValue("@UIDMod", byte.Parse(Session["UserID"].ToString()));
                cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
                cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
                cmdMaster.Parameters.AddWithValue("@SessionID", byte.Parse(Session["SessionID"].ToString()));
                
                int i= cmdMaster.ExecuteNonQuery();
                if (i > 0)
                    _mTran.Commit();
                else
                    _mTran.Rollback();
               
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
      


    }
}
