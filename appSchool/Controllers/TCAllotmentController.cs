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
    public class TCAllotmentController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;


        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appTCAllotment == 0)
            {
                return Redirect("~/");
            }

            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 67, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
            ViewData["TCGivenFlag"] = false;

            return View();
        }

        public ActionResult PartialTCAllotmentView(int PclassSetupID, bool TcGivenFlag)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["ClassIDForTCAllotment"] = PclassSetupID;
            ViewData["TCGivenFlag"] = TcGivenFlag;
            if (bool.Parse(ViewData["TCGivenFlag"].ToString()) == true)
            {
                return PartialView("ListTCAllotmentView", new UnitOfWork().studentSessionService.GetAllStudentWithTCTruebyClassID(PclassSetupID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
            }
            else
            {
                return PartialView("ListTCAllotmentView", new UnitOfWork().studentSessionService.GetAllStudentWithTCFalsebyClassID(PclassSetupID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
            }
            
        }

        public ActionResult GetAllStudentListWithTCFalse(string mClassesID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             int newclassID = int.Parse(mClassesID);
             ViewData["ClassIDForTCAllotment"] = newclassID;
             ViewData["TCGivenFlag"] = false;
             return PartialView("ListTCAllotmentView", new UnitOfWork().studentSessionService.GetAllStudentWithTCFalsebyClassID(newclassID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }

        public ActionResult GetAllStudentListWithTCTrue(string mClassesID)
        {
            
            if (Session["UserID"] == null) { return Redirect("~/"); }
            int newclassID = int.Parse(mClassesID);
            ViewData["ClassIDForTCAllotment"] = newclassID;
            ViewData["TCGivenFlag"] = true;
            return PartialView("ListTCAllotmentView", new UnitOfWork().studentSessionService.GetAllStudentWithTCTruebyClassID(newclassID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


         public ActionResult GetClassSetupList(int mClassID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             List<ClassSetup> obj = new List<ClassSetup>();
             obj=unitOfWork.classSetupService.GetAllClassNameByClassID(mClassID);

             return PartialView("ClassSetupPartial",obj);
         }

         public ActionResult UpdateTCAllotmentSelectedStudent(string pStudentIDs, int pTcFlag, int pClassID, DateTime TCDate)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             try
             {

                 string[] StudentIDList = pStudentIDs.Split(',');
                 foreach (string StudentID in StudentIDList)
                 {
                     StudentRegistration objStudent = new StudentRegistration();
                     objStudent.StudentID = int.Parse(StudentID);
                     if (pTcFlag == 1)
                     {
                         objStudent.TCGiven = true;
                         objStudent.TCDate = TCDate;
                         objStudent.TCSessionID = byte.Parse(Session["SessionID"].ToString());
                     }
                     else
                     {
                         objStudent.TCGiven = false;
                     }

                     unitOfWork.studentRegistrationService.UpdateTcAllotmentStudentWise(objStudent, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["SessionID"].ToString()));
                     unitOfWork.Save();

                 }


             }
             catch (Exception e)
             {
                 // updateValues.SetErrorText(product, e.Message);
             }

             int mClassAttendanceID = 0;

             if (mClassAttendanceID == 0)
             {
                 //  mClassAttendanceID = unitOfWork.attendanceClassService.GetAttendanceInfoByClassSetupIDAndDate(int.Parse(mClassesID), newDate);
             }
             //ViewData["AttendanceStudent"] = unitOfWork.attendanceStudentService.GetAttendanceStudentForGrid(mClassAttendanceID);
             //ViewData["ClassAttendanceID"] = mClassAttendanceID;
             ViewData["ClassIDForStudentSession"] = pClassID;
             ViewData["TCGivenFlag"] = false;
            
             return PartialView("ListTCAllotmentView", new UnitOfWork().studentSessionService.GetAllStudentWithTCFalsebyClassID(pClassID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }
         public void SaveUserLogForUpdate(vStudentSession product)
         {
             SqlCommand cmdMaster = new SqlCommand("UPDATE_StudentSessionHistory", _mConn);
             cmdMaster.CommandType = CommandType.StoredProcedure;
             cmdMaster.Transaction = _mTran;

             cmdMaster.Parameters.AddWithValue("@StudentSessionID", product.StudentSessionID);
             cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
             cmdMaster.Parameters.AddWithValue("@IsDeleted", false);
             cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
             cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));

             cmdMaster.ExecuteNonQuery();

         }
         public void SaveUserLogForDelete(vStudentSession product)
         {
             SqlCommand cmdMaster = new SqlCommand("UPDATE_StudentSessionHistory", _mConn);
             cmdMaster.CommandType = CommandType.StoredProcedure;
             cmdMaster.Transaction = _mTran;

             cmdMaster.Parameters.AddWithValue("@StudentSessionID", product.StudentSessionID);
             cmdMaster.Parameters.AddWithValue("@ChangeBy", int.Parse(Session["UserID"].ToString()));
             cmdMaster.Parameters.AddWithValue("@IsDeleted", true);
             cmdMaster.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
             cmdMaster.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
             cmdMaster.ExecuteNonQuery();

         }

         [ValidateInput(false)]
         public ActionResult UpdateTCAllotmentAll(MVCxGridViewBatchUpdateValues<vStudentSession, int> updateValues, int PclassSetupID, bool TcGivenFlag)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["ClassIDForTCAllotment"] = PclassSetupID;
            ViewData["TCGivenFlag"] = TcGivenFlag;
           
            foreach (var product in updateValues.Update)
            {
                //if (updateValues.IsValid(product))
                //    //UpdateProduct(product, updateValues);
            }
          
            if (bool.Parse(ViewData["TCGivenFlag"].ToString()) == true)
            {
                return PartialView("ListTCAllotmentView", new UnitOfWork().studentSessionService.GetAllStudentWithTCTruebyClassID(PclassSetupID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
            }
            else
            {
                return PartialView("ListTCAllotmentView", new UnitOfWork().studentSessionService.GetAllStudentWithTCFalsebyClassID(PclassSetupID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
            }

         
        }

        protected void UpdateProduct(vStudentSession product, MVCxGridViewBatchUpdateValues<vStudentSession, int> updateValues)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {
                SaveUserLogForUpdate(product);
                _mTran.Commit();

                unitOfWork.studentRegistrationService.UpdateTCAllotment(product, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["SessionID"].ToString()));
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
      
        public ActionResult TCAllotmentGridRowChange(int ID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("RouteTabs", ID);
        }



    }


}



    