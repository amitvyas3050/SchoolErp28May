using appSchool.Repositories;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;


namespace appSchool.ViewModels
{


    public enum TopMenuModules
    {
        Home = 0,
        appDataManager = 1,
        appAcademicManager = 2,
        appStudentManager = 1,
        appStaffManager = 1,
        appAttendanceManager = 1,
        appSMSManager = 1 ,
        appTimetableManager =1,
        appExamsManager =1,
        appLibraryManager =0,
        appFeesManager = 1,
        appPayrollManager = 0,
        appAccountsManager = 0,
        appTransportManager =0,
        appNothing = 0,
        appLfdManager =1,
        appUtilityManager=1
        //appOnlinePortalManager = 1,
        
    }

    public enum SubMenuModules
    {
            appAcademicyear = 1,
            appSchoolProfile=1,
            appSchoolAssets=1,
            appSchoolAwards=1,
            appSchoolTestimonials=1,
            appSchoolHistory=1,
            appSchoolImageGallery=1,
            appSchoolKeys=1,
            appThemeSettings=1,
            appSelectIcon=1,
            appClasses=1,
            appSubjectAllotment=1,
            appSections=1,
            appClassCategories=1,
            appClassSetup=1,
            appSubjects=1,
            appDepartment=1,
            appNewsEventMaster=1,
            appLetterHeadMaster=1,
            appThoughtMaster = 1,
            appAchievement = 1,
            appImageFlyer=1,
            appFlyerVoice = 1,
            appFlyer = 1,
            appDesignationMaster=1,
            appRegistrations=1,
            appStudentSession=1,
            appHouseAllotment=1,
            appTeacherRegistration=1,
            appDefineSMSType=1,
            appMessageMaster=1,
            appSendGeneralMessage=1,
            appSendReligionMessage=1,
            appSendHouseMessage=1,
            appSendSMSAbsentees=1,
            appSendSMSOnlineAbsentees = 1,
            appEmployeeSendSMSAbsentees = 1,
            appSendMessage=1,
            appSendMessageWishes=1,
            appSendMessageWishesTeacher=1,
            appScheduledSMS=1,
            appSchedulerWishes=1,
            appSchedulerWishesTeacher=1,
            appSMSReports=1,
            appDefineExams=1,
            appGradeMaster=1,
            appFeesCategories=1,
            appFeesHead=1,
            appFeesTerm=1,
            AddFeeStructure=1,
            appListFeeStructure=1,
            appStudentFeeStructure=1,
            appOwnerMaster=1,
            appBusStopMaster=1,
            appRouteMaster=1,
            appBusMaster=1,
            appStudentDataExport=1,
            appHouseAllotmentExport=1,
            appTeacherDataExport=1,
            appTeacherDataExportDateWise = 1,
            appAttendanceDaily=1,
            appAttendanceClasswise=1,
            appAttendanceDatewise=1,
            appAttendanceDataExport=1,
            appAddFeeStructure=1,
            appFeesTransaction=1,
            appFeesStructureDataExport=1,
            appStudentFeeStructDataExport=1,
            appHouses=1,
            appNoticeBoard=1,
            appLibrary=1,
            appStudentRegistration=1,
            appUserPermission=1,
            appUserCreation=1,
            appFeesDefaulterSMS=1,
            appRack = 1,
            appShelf = 1,
            appClassSyllabus = 1,
            appTimeSchedule = 1,
            appFacultyAllotment=1,
            appClassSubjectLevel = 1,
            appExamRemark = 1,
            appSubjectLevelOne = 1,
            appSubjectLevelTwo = 1,
            appSubjectLevelThree = 1,
            appExamSyllabus=1,
            appExamSetup=1,
            appBusRoutePlan=1,
            appDriverMaster=1,
            appAdmissionFormSale=0,
            appBookLost=1,
            appVehicleRTO=1,
            appBookAccession=1,
            appBookIssueSubmit=1,
            appSessionTransfer = 1,
            appPersonalContectList=1,
            appTopperNoticeBoard=1,
            appPhotoGalleryDetail=1,
            appImportedURL=1,
            appStaffRegistration=1,
            appTCAllotment=1,
            appTCSessionWise=1,
            appSessionClassAllotment =0,
            appTransportManager=1,
            appStudentExamResult=1,
            appRegistrationsWithTC=1,
            appRollNumberAllotment=1,
            appSettingMaster=1,
            appStudentLoginCreate = 1,
            appParentLoginCreate=1,
            appTeacherLoginCreate = 1,
            appExamMarkEntry=1,
            appSubjectAllotmentDataExport=1,
            appClassTimeTable=1,
            appPhotoGallery = 1,
            appMessageBroadcast=0,
            appEmployeeAttnendEmportData=1,
            appEmployeeattendancereport = 1,
            appEmployeeShiftMaster=1,
    }


    public static class MenuModels
    {


    }


   
}






