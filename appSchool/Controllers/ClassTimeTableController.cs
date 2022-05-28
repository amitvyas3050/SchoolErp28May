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
    public class ClassTimeTableController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;


        public ActionResult Index()
        {


            return View();
        }


        public ActionResult PartialGridClassTimeTable(int pClassID)
        {
            ViewData["ClassID"] = pClassID;
            return PartialView("GridViewPartial", unitOfWork.vClassTimeTableService.GetClassTimeTableList(pClassID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public JsonResult GetClassTimeTable(int ClassID)
        {
            int NewID = 0 ;
            string Errormsg = string.Empty; 





            NewID = AddClassTimeTableAllotment(ClassID);

            if (NewID > 0)
            {
                Errormsg = "Succesfully Allotted Class Time Table";
            }
            else
            {
                Errormsg = "Already Time Table Allotted For this Class";
            }


            ViewData["ClassID"] = ClassID;
             List<vClassTimeTable> lst = unitOfWork.vClassTimeTableService.GetClassTimeTableList( ClassID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()) );

             return Json(new { Displaymsg = Errormsg, ListData = cCommon.RenderRazorViewToString("GridViewPartial", lst, ControllerContext, ViewData, TempData) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckSubjectAllottedForFaculty(int mClassID, string pScheduleName, string pFromTime, int pMonFID, int pTueFID, int pWedFID, int pThuFID, int pFriFID, int pSatFID, int pMonSID, int pTueSID, int pWedSID, int pThuSID, int pFriSID, int pSatSID)
        {
            
            string Errormsg = string.Empty;
          
           


          Errormsg = "Already Alloted Faculty For This Class";
         return Json(new { Displaymsg = Errormsg }, JsonRequestBehavior.AllowGet);
        }


        public int AddClassTimeTableAllotment(int mClassID)
        {
            int Res = 0;

            SqlCommand cmd = new SqlCommand("Add_ClassTimeTable", DB.GetActiveConnection());
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@ClassID", mClassID);

            cmd.Parameters.AddWithValue("@UIDAdd", byte.Parse(Session["UserID"].ToString()));
            cmd.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmd.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
            cmd.Parameters.AddWithValue("SessionID", byte.Parse(Session["SessionID"].ToString()));

            Res = cmd.ExecuteNonQuery();
            return Res;
        }



        public ActionResult UpdateTimetableAllotment(MVCxGridViewBatchUpdateValues<vClassTimeTable, int> updateValues, int PClassID)
        {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             ViewData["ClassID"] = PClassID;
            
            foreach (var product in updateValues.Update)
            {
                if (updateValues.IsValid(product))
                    UpdateProduct(product, updateValues);
            }


            return PartialView("GridViewPartial", unitOfWork.vClassTimeTableService.GetClassTimeTableList(PClassID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
           


        }


        protected void UpdateProduct(vClassTimeTable product, MVCxGridViewBatchUpdateValues<vClassTimeTable, int> updateValues)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {

                unitOfWork.vClassTimeTableService.UpdateClassTimeTable(product);
                unitOfWork.Save();


                //int mClassID = product.ClassId;
                //int mSchudeleID = int.Parse(product.ScheduleId.ToString());

                //TimeTable objtimeTbl = new TimeTable();

            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
      


    }
}
