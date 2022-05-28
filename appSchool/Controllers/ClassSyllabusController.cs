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
    public class ClassSyllabusController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private SqlConnection _mConn;
        private SqlTransaction _mTran;

       
         public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appClassSyllabus == 0)
            {
                return Redirect("~/");
            }

            UserPermission objuser = new UserPermission();
            objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 77, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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

            ViewData["ClassSyllabusID"] = 0;
            ViewData["ClassID_ForClassSyllabus"] = 0;
            ViewData["SubjectID_ForClassSyllabus"] = 0;
            ViewData["PreferredBook_ForClassSyllabus"] =string.Empty;

            return PartialView("Index");
        }

         public ActionResult PartialClassSyllabusView(int pClassSyllabusID, int pClassID, int pSubjectID, string pPreferredBook)
        {
            //ViewData["ClassIDForStudentSession"] = PclassSetupID;

            ViewData["ClassSyllabusID"] = pClassSyllabusID;
            ViewData["ClassID_ForClassSyllabus"] = pClassID;
            ViewData["SubjectID_ForClassSyllabus"] = pSubjectID;
            ViewData["PreferredBook_ForClassSyllabus"] = pPreferredBook;


            return PartialView("ListClassSyllabus", unitOfWork.ClassSyllabusDetailService.GetClassSyllabusDetailList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

         public ActionResult GetAllStudentListView(string mClassesID)
         {
             int newclassID = int.Parse(mClassesID);
             ViewData["ClassIDForStudentSession"] = newclassID;
             return PartialView("ListStudentSessionView", new UnitOfWork().studentSessionService.GetAllStudentbyClassID(newclassID, int.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
         }

         public ActionResult GetClassSetupList(int mClassID)
         {
             List<ClassSetup> obj = new List<ClassSetup>();
             obj=unitOfWork.classSetupService.GetAllClassNameByClassID(mClassID);

             return PartialView("ClassSetupPartial",obj);
         }


         public ActionResult GetSubjectListByClassID(string pClassID)
         {

             List<vSubjectAllotmentWithIDLOne> obj = new List<vSubjectAllotmentWithIDLOne>();
             obj = unitOfWork.ClassSyllabusMasterService.GetSubjectList(int.Parse(pClassID));
             ViewData["ClassSyllabusID"] = 0;
             ViewData["ClassID_ForClassSyllabus"] = 0;
             ViewData["SubjectID_ForClassSyllabus"] = 0;
             ViewData["PreferredBook_ForClassSyllabus"] = string.Empty;

             return PartialView("SubjectPartialView", obj);
         }

         public ActionResult GetPreferredBookByClass(string pClassID, string pSubjectID)
         {
             int mClassID = int.Parse(pClassID);
             int mSubjectID = int.Parse(pSubjectID);
             string mPreferredBook = string.Empty;
             string DisplayMsg=string.Empty;
             bool mStatus = false;
             //string mPreferredBook=unitOfWork.ClassSyllabusMasterService.

             ClassSyllabusMaster objmaster = new ClassSyllabusMaster();
             objmaster = unitOfWork.ClassSyllabusMasterService.GetClassSyllabusDataByClassIDANDSubjectID(mClassID, mSubjectID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
             if (objmaster != null)
             {
                 mStatus = true;
                 mPreferredBook = objmaster.PreferredBooks;
                 DisplayMsg = "Syllabus Allready Created for this Subject.";
             }
             else
             {
                 mStatus = false;
                 DisplayMsg = "";
             }

             return Json(new {Status=mStatus ,errorMsg=DisplayMsg, result=mPreferredBook  },JsonRequestBehavior.AllowGet);

            
         }



         public ActionResult GetClassSyllabusDataView(string pClassID, string pSubjectID, string pPreferredBook)
         {
             int ClassSyllabusID = 0;
             int mClassID = int.Parse(pClassID);
             int mSubjectIDL1 = int.Parse(pSubjectID);


             ClassSyllabusMaster objCheck = new ClassSyllabusMaster();

             objCheck = unitOfWork.ClassSyllabusMasterService.GetClassSyllabusDataByClassIDANDSubjectID(mClassID, mSubjectIDL1, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
             if (objCheck==null)
             {
                 ClassSyllabusMaster objMaster = new ClassSyllabusMaster();
                 objMaster.AddDate = DateTime.Now;
                 objMaster.BranchID = byte.Parse(Session["BranchID"].ToString());
                 objMaster.ClassID = mClassID;
                 objMaster.CompID = byte.Parse(Session["CompID"].ToString());
                 objMaster.PreferredBooks = pPreferredBook;
                 objMaster.SessionID = byte.Parse(Session["SessionID"].ToString());
                 objMaster.SubjectIDL1 = mSubjectIDL1;
                 objMaster.UIDAdd = byte.Parse(Session["UserID"].ToString());

                 unitOfWork.ClassSyllabusMasterService.InsertClassSyllabusMaster(objMaster);
                 unitOfWork.Save();
                 ClassSyllabusID = objMaster.ClassSyllabusID;
             }
             else
             {
                 objCheck.PreferredBooks = pPreferredBook;
                 objCheck.UIDMod = byte.Parse(Session["UserID"].ToString());
                 objCheck.ModDate = DateTime.Now;

                 unitOfWork.ClassSyllabusMasterService.UpdateSyllabusMaster(objCheck);
                 unitOfWork.Save();
                 ClassSyllabusID = objCheck.ClassSyllabusID;
             }

             List<ClassSyllabusDetail> obj = new List<ClassSyllabusDetail>();
             if (ClassSyllabusID > 0)
             {
                 obj = unitOfWork.ClassSyllabusDetailService.GetClassSyllabusDetailListByClassSyllabusID(ClassSyllabusID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
             }
            
             ViewData["ClassSyllabusID"] = ClassSyllabusID;
             ViewData["ClassID_ForClassSyllabus"] = mClassID;
             ViewData["SubjectID_ForClassSyllabus"] = mSubjectIDL1;
             ViewData["PreferredBook_ForClassSyllabus"] = pPreferredBook;
             return PartialView("ListClassSyllabus",obj);
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
         public ActionResult updateClassSyllabusAll(MVCxGridViewBatchUpdateValues<ClassSyllabusDetail, int> updateValues, int pClassSyllabusID, int pClassID, int pSubjectID, string pPreferredBook)
        {
            if (pClassSyllabusID == 0 || pClassID == 0 || pSubjectID == 0)
            {
                return PartialView("ListClassSyllabus", new UnitOfWork().ClassSyllabusDetailService.GetClassSyllabusDetailListByClassSyllabusID(pClassSyllabusID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
            }


            ViewData["ClassSyllabusID"] = pClassSyllabusID;
            ViewData["ClassID_ForClassSyllabus"] = pClassID;
            ViewData["SubjectID_ForClassSyllabus"] = pSubjectID;
            ViewData["PreferredBook_ForClassSyllabus"] = pPreferredBook;

            foreach (var product in updateValues.Insert)
            {
                if (updateValues.IsValid(product))
                    InsertProduct(product, updateValues, pClassSyllabusID);
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


            return PartialView("ListClassSyllabus", new UnitOfWork().ClassSyllabusDetailService.GetClassSyllabusDetailListByClassSyllabusID(pClassSyllabusID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        protected void UpdateProduct(ClassSyllabusDetail product, MVCxGridViewBatchUpdateValues<ClassSyllabusDetail, int> updateValues)
        {
            _mConn = DB.GetActiveConnection();
            _mTran = _mConn.BeginTransaction(IsolationLevel.Snapshot);
            try
            {
                product.BranchID = byte.Parse(Session["BranchID"].ToString());
                product.CompID = byte.Parse(Session["CompID"].ToString());
                product.SessionID = byte.Parse(Session["SessionID"].ToString());

                unitOfWork.ClassSyllabusDetailService.UpdateClassSyllabusDetail(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
        protected void DeleteProduct(int product, MVCxGridViewBatchUpdateValues<ClassSyllabusDetail, int> updateValues)
        {
            try
            {
                unitOfWork.ClassSyllabusDetailService.Delete(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
        protected void InsertProduct(ClassSyllabusDetail product, MVCxGridViewBatchUpdateValues<ClassSyllabusDetail, int> updateValues, int mClassSyllabusID)
        {
            try
            {


                product.BranchID = byte.Parse(Session["BranchID"].ToString());
                product.CompID = byte.Parse(Session["CompID"].ToString());
                product.SessionID = byte.Parse(Session["SessionID"].ToString());
                product.ClassSyllabusID = mClassSyllabusID;

                unitOfWork.ClassSyllabusDetailService.InsertClassSyllabusDetail(product);
                unitOfWork.Save();

            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }
        }
     
    }


}



    