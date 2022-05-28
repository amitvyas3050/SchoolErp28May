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


namespace appSchool.Controllers
{
        [NoCache]
    public class AttendanceDatewiseController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /SendSMS/

        public ActionResult Index()
        {
            if (Session["UserID"] == null || (int)SubMenuModules.appAttendanceDatewise == 0)
            {
                return Redirect("~/");
            }

            //UserPermission objuser = new UserPermission();
            //objuser = unitOfWork.userPermissionService.CheckUserPermissionModulewise(int.Parse(Session["UserID"].ToString()), 47, byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString()));
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
        public ActionResult AttendanceTreePartial()
        {
         
            return PartialView("AttendanceTreePartial");
        }

        public ActionResult ListPartialClassSetup()
        {
            return PartialView("ListClassGridLookupPartial", unitOfWork.classSetupService.GetClassSetupList(byte.Parse(Session["CompID"].ToString()), byte.Parse(Session["BranchID"].ToString())));
        }



        public ActionResult GetTemplateMesssageText(int mTemplateID)
        {
            List<vAttendanceClass> obj = new List<vAttendanceClass>();
            obj = new UnitOfWork().attendanceClassService.GetALLAttendance(mTemplateID,byte.Parse(Session["SessionID"].ToString()),byte.Parse(Session["BranchID"].ToString()),byte.Parse(Session["CompID"].ToString()));
            ViewData["ClassSetupID"] = mTemplateID;
            //return unitOfWork.smsTemplateService.GetByID(mTemplateID).TemplateMessage;
           
            return PartialView("ListAllDatewiseAttendance",obj);
        }

        public ActionResult AttendanceDaily( int mTemplateID, DateTime newDate)
        {
            List<vAttendanceClass> obj = new List<vAttendanceClass>();
            obj = new UnitOfWork().attendanceClassService.GetALLAttendanceByDate(mTemplateID, newDate,  byte.Parse(Session["SessionID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["CompID"].ToString()));
            ViewData["ClassSetupID"] = mTemplateID;
             ViewData["AttanndanceDate"] = newDate;
            return PartialView("ListAllDatewiseAttendance", obj);
        }

        public ActionResult AbsentStudentGridRowChange(int RegID)
        {
            List<vStudentAttendanceDateWise> obj = new List<vStudentAttendanceDateWise>();
            obj = new UnitOfWork().attendanceStudentService.GetAbsentStudentClasswise(RegID);

            //return unitOfWork.smsTemplateService.GetByID(mTemplateID).TemplateMessage;
            ViewData["ClassAttendanceID"] = RegID;
            return PartialView("ListAbsentStudentClasswise", obj);
        }



        public ActionResult PartialAllAbsentStudent(int mClassAttendenceID)
        {
            List<vStudentAttendanceDateWise> obj = new List<vStudentAttendanceDateWise>();
            obj = new UnitOfWork().attendanceStudentService.GetAbsentStudentClasswise(mClassAttendenceID);

            //return unitOfWork.smsTemplateService.GetByID(mTemplateID).TemplateMessage;
            ViewData["ClassAttendanceID"] = mClassAttendenceID;
            return PartialView("ListAbsentStudentClasswise", obj);
        }

        public ActionResult PartialAllDatewiseAttendance(int mClassAttendenceID, DateTime AttandanceDate)
        {
            List<vAttendanceClass> obj = new List<vAttendanceClass>();

            ViewData["ClassSetupID"] = mClassAttendenceID;
            ViewData["AttanndanceDate"] = AttandanceDate;
            obj = new UnitOfWork().attendanceClassService.GetALLAttendanceByDate(mClassAttendenceID, AttandanceDate, byte.Parse(Session["SessionID"].ToString()), byte.Parse(Session["BranchID"].ToString()), byte.Parse(Session["CompID"].ToString()));

            return PartialView("ListAllDatewiseAttendance", obj);
        }


        public ActionResult GridStudentsPartial(int mChoice)
        {
            return PartialView("MultiSelectListPartial", new DropDownSelectedItemModel() {ChoiceTypeID= mChoice });
        }
    }
}
