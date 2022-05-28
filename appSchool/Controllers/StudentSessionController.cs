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
    //[NoCache]
    public class StudentSessionController : Controller
    {


        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;


        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appStudentSession == 0)
            {
                return Redirect("~/");
            }

            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 19, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

            return PartialView("Index");
        }

        public ActionResult StudentSessionView()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appStudentSession == 0)
            {
                return Redirect("~/");
            }

            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 19, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

            return PartialView("StudentSessionView");
        }

         public ActionResult PartialStudentSessionView(int PclassSetupID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["ClassIDForStudentSession"] = PclassSetupID;
            return PartialView("ListStudentSessionView", new UnitOfWork().studentSessionService.GetAllStudentWithTCFalsebyClassID(PclassSetupID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

         public ActionResult GetAllStudentListView(string mClassesID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             int newclassID = int.Parse(mClassesID);
             ViewData["ClassIDForStudentSession"] = newclassID;
             return PartialView("ListStudentSessionView", new UnitOfWork().studentSessionService.GetAllStudentWithTCFalsebyClassID(newclassID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }

         public ActionResult GetClassSetupList(int mClassID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             List<ClassSetup> obj = new List<ClassSetup>();
             obj=unitOfWork.classSetupService.GetAllClassNameByClassID(mClassID);

             return PartialView("ClassSetupPartial",obj);
         }

         public ActionResult UpdateStudentSessionHouseWise(string pStudentIDs, int pchkType, int pHouseID, int pClassSetupID, int pIsSMS, int pClassID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             try
             {
           
                 string[] StudentIDList = pStudentIDs.Split(',');
                 foreach (string StudentID in StudentIDList)
                 {
                     StudentSession objStudent = new StudentSession();
                     objStudent.StudentSessionID = int.Parse(StudentID);
                     objStudent.HouseID = pHouseID;
                     objStudent.ClassSetupID = pClassSetupID;
                     if (pIsSMS == 1)
                     {
                         objStudent.SMSInHindi = true;
                     }


                     unitOfWork.studentSessionService.UpdateStudentSessionHousewise(objStudent,pchkType);
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
             return PartialView("ListStudentSessionView", new UnitOfWork().studentSessionService.GetAllStudentWithTCFalsebyClassID(pClassID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
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
         public ActionResult UpdateStudentSessionAll(MVCxGridViewBatchUpdateValues<vStudentSession, int> updateValues, int PclassSetupID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["ClassIDForStudentSession"] = PclassSetupID;
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

            return PartialView("ListStudentSessionView", new UnitOfWork().studentSessionService.GetAllStudentWithTCFalsebyClassID(PclassSetupID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        protected void UpdateProduct(vStudentSession product, MVCxGridViewBatchUpdateValues<vStudentSession, int> updateValues)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {
                if (SettingMasterStaticClass._ManageHistory == true)
                {
                    SaveUserLogForUpdate(product);
                    _mTran.Commit();
                }
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
     
   
        public ActionResult StudentSessionGridRowChange(int ID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("RouteTabs", ID);
        }

        public string SetSessionAndRedirectToHome(string msessionID, string msessionName)
        {
            string response = "Ok";
            Session["SessionID"] = msessionID;
            Session["SessionName"] = msessionName;

            return response;
        }


    }


}



    