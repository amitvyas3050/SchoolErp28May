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
    public class SessionTransferController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;

       
         public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appSessionTransfer == 0)
            {
                return Redirect("~/");
            }

            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 94, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

         public ActionResult PartialSessionTransferView(int PclassID,int PSectionID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            ViewData["ClassIDForSessionTransfer"] = PclassID;
            ViewData["SectionIDForSessionTransfer"] = PSectionID;

            return PartialView("ListSessionTransferView", new UnitOfWork().studentSessionService.GetAllStudentForSessionTransfer(PclassID, PSectionID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

         public ActionResult GetAllStudentListView(string mFClassID, string mFSectionID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             int newclassID = int.Parse(mFClassID);
             int newSectionID = int.Parse(mFSectionID);
             ViewData["ClassIDForSessionTransfer"] = newclassID;
             ViewData["SectionIDForSessionTransfer"] = newSectionID;
             return PartialView("ListSessionTransferView", new UnitOfWork().studentSessionService.GetAllStudentForSessionTransfer(newclassID,newSectionID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }

         public ActionResult GetClassSetupList(int mClassID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             List<ClassSetup> obj = new List<ClassSetup>();
             obj=unitOfWork.classSetupService.GetAllClassNameByClassID(mClassID);

             return PartialView("ClassSetupPartial",obj);
         }

         public ActionResult GetToClassList(int mFClassID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             int mDisplayOrder = 0;
             mDisplayOrder = int.Parse(unitOfWork.ClassService.GetByID(mFClassID).DisplayOrder.ToString());

             List<Class> obj = new List<Class>();
             obj = unitOfWork.ClassService.GetToClassListForSessionTransfer(mDisplayOrder, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

             return PartialView("ToClassPartial", obj);
         }

         public ActionResult GetSectionListByFClassIDView(int mFromClassID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             List<ClassSetup> obj = new List<ClassSetup>();
             obj = unitOfWork.classSetupService.GetAllClassNameByClassID(mFromClassID);

             return PartialView("FromSectionPartial",obj);
         }

         public ActionResult GetSectionListByToClassIDView(int mToClassID)
         {
             if (Session["UserID"] == null) { return Redirect("~/"); }
             List<ClassSetup> obj = new List<ClassSetup>();
             obj = unitOfWork.classSetupService.GetAllClassNameByClassID(mToClassID);

             return PartialView("ToSectionPartial", obj);
         }

         public int SaveStudentsInNewSession(String mStudentIds, int mToClassID, int mToSectionID)
         {

             int i = 0;


             SqlCommand cmdMaster = new SqlCommand("Transfer_StudentSession", DB.GetActiveConnection());
             cmdMaster.CommandType = CommandType.StoredProcedure;

             cmdMaster.Parameters.AddWithValue("@StudentIds", mStudentIds);
             cmdMaster.Parameters.AddWithValue("@FromSessionId",byte.Parse(Session["SessionID"].ToString()));
             cmdMaster.Parameters.AddWithValue("@ToClassId",mToClassID);
             cmdMaster.Parameters.AddWithValue("@ToClassSetupId", mToSectionID);
             cmdMaster.Parameters.AddWithValue("@CompID", int.Parse(Session["CompID"].ToString()));
             cmdMaster.Parameters.AddWithValue("@BranchID", int.Parse(Session["BranchID"].ToString()));
             cmdMaster.Parameters.AddWithValue("@UIDAdd", int.Parse(Session["UserID"].ToString()));

                i=cmdMaster.ExecuteNonQuery();
           

             return i;
         }

         public ActionResult UpdateSessionTransfer(string pStudentIDs,int pFromClassID, int pToClassID, int pFromSectionID, int pToSectionID)
         {
             int result = 0;
             if (Session["UserID"] == null) { return Redirect("~/"); }
             try
             {
                 result=SaveStudentsInNewSession(pStudentIDs,pToClassID,pToSectionID);

             }
             catch (Exception e)
             {
                // updateValues.SetErrorText(product, e.Message);
             }
                        
               ViewData["ClassIDForSessionTransfer"] = pFromSectionID;
               ViewData["SectionIDForSessionTransfer"] = pFromSectionID;
             return PartialView("ListSessionTransferView", new UnitOfWork().studentSessionService.GetAllStudentForSessionTransfer(pFromClassID,pFromSectionID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
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
   
        public ActionResult StudentSessionGridRowChange(int ID)
        {
            if (Session["UserID"] == null) { return Redirect("~/"); }
            return PartialView("RouteTabs", ID);
        }

    }


}



    