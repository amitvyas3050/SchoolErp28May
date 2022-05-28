using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appSchool.ViewModels;
using appSchool.Repositories;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using System.Web.SessionState;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace appSchool.Controllers
{
    [NoCache]
    public class AttendanceDailyController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
     
        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appAttendanceDaily==0)
            {

                return Redirect("~/");
            }

            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 46, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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


            List<AttendanceStudent> objA=new List<AttendanceStudent>();
            ViewData["AttendanceStudent"] =objA ;

            return View();
        }
        public ActionResult AttendanceTreePartial()
        {
            if (Session["UserID"] == null)
            {
                return Redirect("~/");
            }
         
            return PartialView("AttendanceTreePartial");
        }

        public ActionResult GetTemplateMesssageText(int mTemplateID)
        {
            
            List<AttendanceStudent> obj = new List<AttendanceStudent>(); 

            return PartialView("ListStudentAttendance",obj);
        }


        public ActionResult PartialStudentAttendanceView(int mClassAttendenceID)
        {
            return PartialView("ListStudentAttendance", unitOfWork.attendanceStudentService.GetAttendanceStudentForGrid(mClassAttendenceID,   byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        public ActionResult ListPartialClassSetup()
        {
            return PartialView("ListClassGridLookupPartial", unitOfWork.classSetupService.GetClassSetupList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }


        public int UpdateAttendanceAll(int mSessionID, int mClassSetupID, DateTime mAttendanceDate, int mUIDAdd, string mClassSetupName)
        {
            int i = 0;

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DataConnectionString"].ToString());
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Update_AttendeceAll";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@SessionId", mSessionID);
            cmd.Parameters.AddWithValue("@CompID", byte.Parse(Session["CompID"].ToString()));
            cmd.Parameters.AddWithValue("@BranchID", byte.Parse(Session["BranchID"].ToString()));
            cmd.Parameters.AddWithValue("@AttendanceDate", mAttendanceDate);
            cmd.Parameters.AddWithValue("@ClassSetupId", mClassSetupID);
            cmd.Parameters.AddWithValue("@UIDAdd", mUIDAdd);
            cmd.Parameters.AddWithValue("@ClassSetupName", mClassSetupName);

            SqlParameter outPutParameter = new SqlParameter();
            outPutParameter.ParameterName = "@ClassAttendanceID";
            outPutParameter.SqlDbType = SqlDbType.Int;
            outPutParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outPutParameter);

            cmd.ExecuteNonQuery();
            string AttendanceClassID = outPutParameter.Value.ToString();

            if (AttendanceClassID != string.Empty && AttendanceClassID != "" &&  AttendanceClassID!=null)
            {
                i = int.Parse(AttendanceClassID);
            }
            con.Close();
            cmd.Dispose();

            return i;

        }
        public ActionResult AttendanceDaily(DateTime newDate, string mClassesID)
        {
            string NewClassIDs = string.Empty;

                string ClassSetupName = unitOfWork.classSetupService.GetByID(int.Parse(mClassesID)).ClassDescription;
                int newID = UpdateAttendanceAll(int.Parse(Session["SessionID"].ToString()), int.Parse(mClassesID), newDate, int.Parse(Session["UserID"].ToString()), ClassSetupName);
                if (NewClassIDs != string.Empty)
                {
                    NewClassIDs =  newID.ToString();
                }
                else
                {
                    NewClassIDs = newID.ToString();
                }

                int mClassAttendanceID = 0;
                mClassAttendanceID = int.Parse(NewClassIDs);
                if (mClassAttendanceID == 0)
                {
                    mClassAttendanceID = unitOfWork.attendanceClassService.GetAttendanceInfoByClassSetupIDAndDate(int.Parse(mClassesID), newDate, byte.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
                }  

            ViewData["AttendanceStudent"] = null;
            ViewData["AttendanceStudent"] = unitOfWork.attendanceStudentService.GetAttendanceStudentForGrid(mClassAttendanceID,  byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            ViewData["ClassAttendanceID"] = mClassAttendanceID;


            return PartialView("ListStudentAttendance", ViewData["AttendanceStudent"]);
        }
        public ActionResult AttendanceAllDailyUpdate(string mStudentIDList, string mAttendanceType, string pDescription, DateTime newDate, string mClassesID)
        {
            try
            {
                string NewClassIDs = string.Empty;
                string[] StudentIDList = mStudentIDList.Split(',');
                foreach (string StudentID in StudentIDList)
                {
                    AttendanceStudent objStudent = new AttendanceStudent();
                    objStudent.StudentAttendanceID = int.Parse(StudentID);
                    objStudent.Attendance = mAttendanceType;
                    objStudent.Description = pDescription;

                    unitOfWork.attendanceStudentService.UpdateStudentAttendance(objStudent);
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
                mClassAttendanceID = unitOfWork.attendanceClassService.GetAttendanceInfoByClassSetupIDAndDate(int.Parse(mClassesID), newDate, byte.Parse(Session["SessionID"].ToString()), byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            }
            ViewData["AttendanceStudent"] = null;
            ViewData["AttendanceStudent"] = unitOfWork.attendanceStudentService.GetAttendanceStudentForGrid(mClassAttendanceID,  byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            ViewData["ClassAttendanceID"] = mClassAttendanceID;


            return PartialView("ListStudentAttendance", ViewData["AttendanceStudent"]);
        }
   

        [HttpPost]
        public ActionResult AddAttendanceMaster(AttendanceClass objSMSTemplate)
        {
          
            int ClassAttendanceIDFinal = 0;

            if (ModelState.IsValid)
            {
                try
                {

                    int mClassAttendanceID = unitOfWork.attendanceClassService.GetAttendanceInfo(objSMSTemplate);
                  
                    if (mClassAttendanceID > 0)
                    {
                        ClassAttendanceIDFinal = mClassAttendanceID;

                    }
                    else
                    {

                        objSMSTemplate.AttendanceDate = objSMSTemplate.AttendanceDate.Date;
                        objSMSTemplate.UIDAdd = byte.Parse(Session["UserID"].ToString());
                        objSMSTemplate.AddDate = DateTime.Now;
                        objSMSTemplate.CompID = byte.Parse(Session["CompID"].ToString());
                        objSMSTemplate.BranchID = byte.Parse(Session["BranchID"].ToString());

                        unitOfWork.attendanceClassService.Insert(objSMSTemplate);
                        unitOfWork.Save();

                        int mClassAttendanceID1 = unitOfWork.attendanceClassService.GetAttendanceInfo(objSMSTemplate);
                        ClassAttendanceIDFinal = mClassAttendanceID1;
                        List<StudentSession> objSS = new UnitOfWork().studentSessionService.GetStudentForAttendance(objSMSTemplate.ClassSetupID, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));

                        foreach (StudentSession objStdSession in objSS)
                        {
                            AttendanceStudent objAS = new AttendanceStudent();
                            objAS.ClassAttendanceID = mClassAttendanceID1;
                            objAS.StudentID = objStdSession.StudentID;
                            objAS.Attendance = "P";
                            objAS.CompID = byte.Parse(Session["CompID"].ToString());
                            objAS.BranchID = byte.Parse(Session["BranchID"].ToString());

                            unitOfWork.attendanceStudentService.Insert(objAS);
                            unitOfWork.Save();

                        }
                    }
                    
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["EditableClass"] = objSMSTemplate;
            ViewData["AttendanceStudent"] = null;
            ViewData["AttendanceStudent"] = unitOfWork.attendanceStudentService.GetAttendanceStudentForGrid(ClassAttendanceIDFinal,  byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
            ViewData["ClassAttendanceID"] = ClassAttendanceIDFinal;
         
            return PartialView("AttendanceTemplatePartial",objSMSTemplate);
        }
        [ValidateInput(false)]
        public ActionResult UpdateStudentAttendanceAll(MVCxGridViewBatchUpdateValues<AttendanceStudent, int> updateValues, int mClassAttendenceID1)
        {
            int mClassAtttendanceID = 0;
            mClassAtttendanceID = mClassAttendenceID1;
            foreach (var product in updateValues.Update)
            {
                if (updateValues.IsValid(product))
                    UpdateProduct(product, updateValues);
            }
            return PartialView("ListStudentAttendance", unitOfWork.attendanceStudentService.GetAttendanceStudentForGrid(mClassAtttendanceID,  byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }

        protected void UpdateProduct(AttendanceStudent product, MVCxGridViewBatchUpdateValues<AttendanceStudent, int> updateValues)
        {
            //if (product.Attendance == "A" || product.Attendance == "P" || product.Attendance == "L")
            //{ 
               
               

            //}

            try
            {
                unitOfWork.attendanceStudentService.UpdateStudentAttendance(product);
                unitOfWork.Save();
            }
            catch (Exception e)
            {
                updateValues.SetErrorText(product, e.Message);
            }

            
        }

        public string SubmitMultiselectStudents(DropDownSelectedItemModel test)
        {
            //var values = DropDownEditExtension.GetValue<string>("contractsDDE");
            return test.ContactIDs;


        }

        public ActionResult GetStudentDDLPartialView(int mChoice)
        {
            return PartialView("MultiSelectListPartial", new DropDownSelectedItemModel() {ChoiceTypeID= mChoice });
        }

        public ActionResult GridStudentsPartial(int mChoice)
        {
            return PartialView("MultiSelectListPartial", new DropDownSelectedItemModel() {ChoiceTypeID= mChoice });
        }
    }
}
