using appSchool.Repositories;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;


namespace appSchool.ViewModels
{
    public enum appModule
    {
        Home = 0,
        appDataManager = 1,
        appAcademicManager = 2,
        appFeesManager = 3,
        appExamsManager = 4,
        appSMSManager = 5,
        appTimetableManager = 6,
        appLfdManager = 7,
        appconfigurationmanager = 8,
        appPermission = 9,
        appReports = 10,
        appCertificate = 11,
        appStaffManager,
        appAttendanceManager,
        appLibraryManager,
        appPayrollManager,
        appAccountsManager,
        appTransportManager,
        appUtilityManager,
        appNothing
    }


    public enum SMSModule
    {
        GeneralSMS,
        ReligionSMS,
        HouseSMS,
        AbsenteesSMS,
        StudentSMS,
        WishesSMS,
        TeacherSMS,
        FeesDefaulterSMS,
        GeneralScheduledSMS,
        SchedulerWishesSMS,
        SchedulerWishesTeacher

          
    }

    public enum CommonThemes
    {
        Aqua,
        BlackGlass,
        DevEx,
        Glass,
        RedWine,
        Office2003Olive,
        Office2003Silver,
        Office2003Blue,
        Office2010Black,
        Office2010Silver,
        Office2010Blue,
        PlasticBlue,
        Moderno,
        Metropolis,
        MetropolisBlue,
        SoftOrange,
        Youthful
    }
    public static class appUIModels
    {
        public static IEnumerable<MVCxNavBarGroup> GetGroupsForModule(appModule currModule)
        {
            MVCxNavBarGroup grpMasters;
            MVCxNavBarGroup grpTransaction;
            MVCxNavBarGroup grpScheduler;
            MVCxNavBarGroup grpReports;
            MVCxNavBarGroup grpUsers;

            MVCxNavBarItem mItem;
            List<MVCxNavBarGroup> lst = new List<MVCxNavBarGroup>();
            switch (currModule)
            {
                case appModule.Home:
                    grpMasters = new MVCxNavBarGroup() { Text = "School Info." };
                    grpMasters.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase32x32;
                   
                    lst.Add(grpMasters);
                   
                    break;
              
                case appModule.appDataManager:
                    #region
                    grpMasters = new MVCxNavBarGroup() { Text = "Master Setup" };
                    grpMasters.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.ArrangeWithtextwrappingBottomright32x32;
                    //grpMasters.Items.Add("Academic Sessions", "Sessions").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                   // if ((int)SubMenuModules.appClassTimeTable == 1)
                       // grpMasters.Items.Add("Class TimeTable", "ClassTimeTable").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if((int)SubMenuModules.appClasses==1)
                        grpMasters.Items.Add("Class", "Classes").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appSections == 1)
                        grpMasters.Items.Add("Section", "Sections").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appClassCategories == 1)
                        grpMasters.Items.Add("Class Category", "ClassCategories").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appClassSetup==1)
                        grpMasters.Items.Add("Class Setup", "ClassSetup").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    //if((int)SubMenuModules.appSubjects==1)
                    //    grpMasters.Items.Add("Subjects", "Subjects").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appDepartment == 1)
                        grpMasters.Items.Add("Department", "Department").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appDesignationMaster == 1)
                        grpMasters.Items.Add("Designation", "DesignationMaster").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                  
                    if ((int)SubMenuModules.appHouses == 1)
                        grpMasters.Items.Add("House", "Houses").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;

                  
                    //if ((int)SubMenuModules.appAdmissionFormSale == 1)
                    //    grpMasters.Items.Add("Admission Form", "AdmissionForm").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                       
                    
                    lst.Add(grpMasters);
                    grpTransaction = new MVCxNavBarGroup() { Text = "Configuration & Settings" };
                    grpTransaction.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.SetupProperties32x32;
                    if ((int)SubMenuModules.appSchoolProfile == 1)
                        grpTransaction.Items.Add("Profile", "SchoolProfile").Image.IconID = DevExpress.Web.ASPxThemes.IconID.EditEdit16x16;
                    if ((int)SubMenuModules.appSettingMaster == 1)
                        grpTransaction.Items.Add("Settings", "SettingMaster").Image.IconID = DevExpress.Web.ASPxThemes.IconID.EditEdit16x16;
                    if((int)SubMenuModules.appThemeSettings==1)
                        grpTransaction.Items.Add("Themes Setting", "ThemeSettings").Image.IconID = DevExpress.Web.ASPxThemes.IconID.FormatPictureshapefillcolor16x16;
                   
                    //  grpTransaction.Items.Add("Icon Setting", "IconSettings").Image.IconID = DevExpress.Web.ASPxThemes.IconID.ContentImage16x16;
                      grpTransaction.Items.Add("Contact List", "ContectList").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailContact16x16;

                    lst.Add(grpTransaction);

                      grpUsers = new MVCxNavBarGroup() { Text = "User Settings" };
                      grpUsers.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.PeopleCustomer32x32;

                      if ((int)SubMenuModules.appUserCreation == 1 )
                          grpUsers.Items.Add("User Creation", "UserCreation").Image.IconID = DevExpress.Web.ASPxThemes.IconID.PeoplePublicfix16x16;
                    if ((int)SubMenuModules.appUserPermission == 1)
                        grpUsers.Items.Add("User Permission", "UserPermission").Image.IconID = DevExpress.Web.ASPxThemes.IconID.PeopleAssignto16x16;

                    lst.Add(grpUsers);
                    #endregion
                    break;

               
                case appModule.appAcademicManager:
                    #region
                    grpMasters = new MVCxNavBarGroup() { Text = "Master Data" };
                    grpMasters.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.ArrangeWithtextwrappingBottomright32x32;
                    if ((int)SubMenuModules.appRegistrations == 1)
                        grpMasters.Items.Add("Registration", "Registrations").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailEditcontact16x16;

                    if ((int)SubMenuModules.appStudentSession == 1)
                        grpMasters.Items.Add("Student Session", "StudentSession").Image.IconID = DevExpress.Web.ASPxThemes.IconID.ChartChartyaxissettings16x16;
                        
                  

                    if ((int)SubMenuModules.appHouseAllotment == 1)
                        grpMasters.Items.Add("House Allotment", "HouseAllotment").Image.IconID = DevExpress.Web.ASPxThemes.IconID.NavigationHome16x16;
                    if ((int)SubMenuModules.appRollNumberAllotment == 1)
                        grpMasters.Items.Add("RollNumber Allotment", "RollNumberAllotment").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;

                    if ((int)SubMenuModules.appRegistrationsWithTC == 1)
                        grpMasters.Items.Add("TC Register", "StudentRegistrationWithTC").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailEditcontact16x16;

                   

                    lst.Add(grpMasters);
                    
                    grpReports = new MVCxNavBarGroup() { Text = "Reports" };
                    grpReports.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsReport32x32;
                    if ((int)SubMenuModules.appStudentDataExport == 1)
                    {
                        mItem = new MVCxNavBarItem() { Text = "Export Student Report", NavigateUrl = "/StudentDataExport/Index" };
                        mItem.Image.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsReport16x16;
                        grpReports.Items.Add(mItem);
                    }
                    if ((int)SubMenuModules.appHouseAllotmentExport == 1)
                    {
                        mItem = new MVCxNavBarItem() { Text = "Export HouseAllotment Report", NavigateUrl = "/HouseAllotmentExport/Index" };
                        mItem.Image.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsReport16x16;
                        grpReports.Items.Add(mItem);
                    }
                    if ((int)SubMenuModules.appStudentDataExport == 1)
                        grpReports.Items.Add("Student Photo Report", "StudentPhotoReport").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailEditcontact16x16;

                    //if ((int)SubMenuModules.appStudentSession == 1)
                    //    grpReports.Items.Add("Student ICard Report", "StudentICardReport").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailEditcontact16x16;

                    //if ((int)SubMenuModules.appStudentSession == 1)
                    //    grpReports.Items.Add("Student Bonafide Certificate Report", "StudentBonafideReport").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailEditcontact16x16;

                    //if ((int)SubMenuModules.appStudentSession == 1)
                    //    grpReports.Items.Add("Student TC Certificate Report", "TCCertificateReport").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailEditcontact16x16;


                    //mItem = new MVCxNavBarItem() { Text = "Export Student Report", NavigateUrl = "/ApplicationImage/Index" };
                    //mItem.Image.IconID= DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    //grpReports.Items.Add(mItem);
                    //grpReports.Items.Add("Report SMS", "ReportSMS");
                    //grpReports.Items.Add("SMS Uses Reports", "SMSUsesReports");
                    lst.Add(grpReports);
                      #endregion
                    break;

                case appModule.appStaffManager:
                    #region
                    grpMasters = new MVCxNavBarGroup() { Text = "Master Data" };
                    grpMasters.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.ArrangeWithtextwrappingBottomright32x32;
                    if ((int)SubMenuModules.appTeacherRegistration == 1)
                        grpMasters.Items.Add("Teachers", "TeacherRegistration").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailEditcontact16x16;
                    lst.Add(grpMasters);
                    // if ((int)SubMenuModules.appStaffRegistration == 1)
                    //    grpMasters.Items.Add("Staffs", "StaffRegistration").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailEditcontact16x16;
                    //lst.Add(grpMasters);
                    if ((int)SubMenuModules.appEmployeeShiftMaster == 1)
                        grpMasters.Items.Add("Employee Shift Master", "EmployeeShiftTimeMaster").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailEditcontact16x16;
                   
                    

                    grpTransaction = new MVCxNavBarGroup() { Text = "Transactions" };
                    grpTransaction.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.SetupProperties32x32;

                     if ((int)SubMenuModules.appEmployeeAttnendEmportData == 1)
                         grpTransaction.Items.Add("Import BioMetric Data", "EmployeeAttendanceDataEmport").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailEditcontact16x16;
                         lst.Add(grpTransaction);

                     if ((int)SubMenuModules.appEmployeeAttnendEmportData == 1)
                         grpTransaction.Items.Add("Empolyee Daily Attendance", "EmployeeAttendanceDaily").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailEditcontact16x16;
                         lst.Add(grpTransaction);

                    grpReports = new MVCxNavBarGroup() { Text = "Reports" };
                    grpReports.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsReport32x32;
                    if ((int)SubMenuModules.appTeacherDataExport == 1)
                    {
                        mItem = new MVCxNavBarItem() { Text = "Teacher Report", NavigateUrl = "/TeacherDataExport/Index" };
                        mItem.Image.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsReport16x16;
                        grpReports.Items.Add(mItem);
                    }

                    if ((int)SubMenuModules.appTeacherDataExportDateWise == 1)
                    {
                        mItem = new MVCxNavBarItem() { Text = "Employee Attendance Report", NavigateUrl = "/TeacherDataExportDateWise/Index" };
                        mItem.Image.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsReport16x16;
                        grpReports.Items.Add(mItem);
                    }
                  
                    //grpReports.Items.Add("Attendance Report Classwise", "AttendanceReport");
                    //grpReports.Items.Add("Attendance Report", "AttendanceReport");
                    lst.Add(grpReports);
                      #endregion
                    break;

                case appModule.appAttendanceManager:
                    #region
                    //grpMasters = new MVCxNavBarGroup() { Text = "Master Data" };
                    //grpMasters.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.MailAttach32x32;
                    //lst.Add(grpMasters);
                    grpTransaction = new MVCxNavBarGroup() { Text = "Transactions" };
                    grpTransaction.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.SchedulingSwitchtimescalesto32x32;

                    if ((int)SubMenuModules.appAttendanceClasswise == 1)
                    {
                        mItem = new MVCxNavBarItem() { Text = "Attendance Classwise", NavigateUrl = "/AttendanceClassWise/Index" };
                        mItem.Image.IconID = DevExpress.Web.ASPxThemes.IconID.SchedulingCalendar16x16;
                        grpTransaction.Items.Add(mItem);
                    }
                    if ((int)SubMenuModules.appAttendanceDaily == 1)
                    {
                        mItem = new MVCxNavBarItem() { Text = "Attendance", NavigateUrl = "/AttendanceDaily/Index" };
                        mItem.Image.IconID = DevExpress.Web.ASPxThemes.IconID.SchedulingToday16x16;
                        grpTransaction.Items.Add(mItem);
                    }
                    if ((int)SubMenuModules.appAttendanceDatewise == 1)
                    {
                        mItem = new MVCxNavBarItem() { Text = "Attendance Register", NavigateUrl = "/AttendanceDatewise/Index" };
                        mItem.Image.IconID = DevExpress.Web.ASPxThemes.IconID.SchedulingCalendar16x16;
                        grpTransaction.Items.Add(mItem);
                    }
                    lst.Add(grpTransaction);
                    
                    grpReports = new MVCxNavBarGroup() { Text = "Reports" };
                    grpReports.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsReport32x32;

                    if ((int)SubMenuModules.appAttendanceDataExport == 1)
                    {
                        mItem = new MVCxNavBarItem() { Text = "Attendance Report", NavigateUrl = "/AttendanceDataExport/Index" };
                        mItem.Image.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsReport16x16;
                        grpReports.Items.Add(mItem);
                    }

                    //grpReports.Items.Add("Attendance Report Classwise", "AttendanceReport");
                    //grpReports.Items.Add("Attendance Report", "AttendanceReport");
                    lst.Add(grpReports);
                      #endregion
                    break;

                case appModule.appSMSManager:
                    #region
                    grpMasters = new MVCxNavBarGroup() { Text = "SMS Setup" };
                    grpMasters.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.MailEmailtemplate32x32;
                    if ((int)SubMenuModules.appDefineSMSType == 1)
                        grpMasters.Items.Add("Define SMS Type", "DefineSMSType").Image.IconID=DevExpress.Web.ASPxThemes.IconID.MailNewcontact16x16;
                    if ((int)SubMenuModules.appMessageMaster == 1)
                        grpMasters.Items.Add("Message Template", "MessageMaster").Image.IconID=DevExpress.Web.ASPxThemes.IconID.MailEmailtemplate16x16;
                    lst.Add(grpMasters);

                       grpTransaction = new MVCxNavBarGroup() { Text = "Send Messages" };
                       grpTransaction.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.MailAttach32x32;
                    //mItem = new MVCxNavBarItem() { Text = "Send SMSs", NavigateUrl = "/SendSMS/Index" };
                    //mItem.Image.IconID = DevExpress.Web.ASPxThemes.IconID.MiscellaneousDesign16x16;
                    //grpTransaction.Items.Add(mItem);
                    if ((int)SubMenuModules.appSendGeneralMessage == 1)
                        grpTransaction.Items.Add("General SMS", "SendGeneralMessage").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailMail16x16;
                    //if ((int)SubMenuModules.appSendGeneralMessage == 1)
                    //    grpTransaction.Items.Add("Personal SMS", "SendPersonalMessege").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailMail16x16;
                    if ((int)SubMenuModules.appSendReligionMessage == 1)
                        grpTransaction.Items.Add("Religion SMS", "SendReligionMessage").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailMail16x16;
                    if ((int)SubMenuModules.appSendHouseMessage == 1)
                        grpTransaction.Items.Add("House SMS", "SendHouseMessage").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailMail16x16;
                    if ((int)SubMenuModules.appSendSMSAbsentees == 1)
                        grpTransaction.Items.Add("Absentees SMS", "SendSMSAbsentees").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailMail16x16;
                    if ((int)SubMenuModules.appEmployeeSendSMSAbsentees == 1)
                        grpTransaction.Items.Add("Employee Absentees SMS", "EmployeeSMSAbsentees").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailMail16x16;
                    if ((int)SubMenuModules.appSendMessage == 1)
                        grpTransaction.Items.Add("Student Message", "SendMessage").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailMail16x16;
                    if ((int)SubMenuModules.appSendMessageWishes == 1)
                        grpTransaction.Items.Add("Parent/Student-Wishes", "SendMessageWishes").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailMail16x16;
                    if ((int)SubMenuModules.appSendSMSAbsentees == 1)
                        grpTransaction.Items.Add("Online Class Absentees SMS", "SendSMSOnlineAbsentees").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailMail16x16;
                    if ((int)SubMenuModules.appSendMessageWishesTeacher == 1)
                        grpTransaction.Items.Add("Teacher Message", "SendMessageWishesTeacher").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailMail16x16;
                    if ((int)SubMenuModules.appFeesDefaulterSMS == 1)
                        grpTransaction.Items.Add("Fees Defaulter", "FeesDefaulterSMS").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailMail16x16;




                    lst.Add(grpTransaction);

                    grpScheduler = new MVCxNavBarGroup() { Text = "SMS Scheduler" };
                    grpScheduler.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.MailSend32x32;
                    if ((int)SubMenuModules.appScheduledSMS == 1)
                        grpScheduler.Items.Add("General SMS", "ScheduledSMS").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailSend16x16;
                    if ((int)SubMenuModules.appSchedulerWishes == 1)
                        grpScheduler.Items.Add("Parent/Student-Wishes", "SchedulerWishes").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailSend16x16;
                    if ((int)SubMenuModules.appSchedulerWishesTeacher == 1)
                        grpScheduler.Items.Add("Teacher Message", "SchedulerWishesTeacher").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailSend16x16;
                     //grpReports.Items.Add("Wishes SMS for Parents", "SchedulerWishesparents");

                    lst.Add(grpScheduler);

                     grpReports = new MVCxNavBarGroup() { Text = "Reports" };
                     grpReports.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsReport32x32;
                     //if ((int)SubMenuModules.appSMSReports == 1) 
                     grpReports.Items.Add("Sent SMS Report", "SMSReports").Image.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsReport16x16;

                    mItem = new MVCxNavBarItem() { Text = "SMS Log Data Export", NavigateUrl = "/SMSLogDataExport/Index" };
                    mItem.Image.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsReport16x16;
                        grpReports.Items.Add(mItem);


                        if ((int)SubMenuModules.appSendMessageWishes == 1)
                            grpTransaction.Items.Add("Send Student ID", "SendStudentIDPassword").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailMail16x16;

                    //if ((int)SubMenuModules.appSendMessageWishes == 1)
                    //    grpTransaction.Items.Add("Send Parent ID", "SendParentIDPassword").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailMail16x16;
                        if ((int)SubMenuModules.appSendMessageWishes == 1)
                            grpTransaction.Items.Add("Send Teacher ID", "SendTeacherIDPassword").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailMail16x16;

                    lst.Add(grpReports);
                    //grpReports.Items.Add("Report SMS", "ReportSMS");
                    //grpReports.Items.Add("SMS Uses Reports", "SMSUsesReports");

                    lst.Add(grpReports);
                      #endregion
                    break;
                case appModule.appTimetableManager:

                    break;
                case appModule.appExamsManager:
                    #region

                    grpMasters=new MVCxNavBarGroup() { Text=" Masters"};
                    grpMasters.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.ArrangeWithtextwrappingBottomright32x32;

                    if ((int)SubMenuModules.appDefineExams == 1)
                        grpMasters.Items.Add("Define Exams", "DefineExams").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appGradeMaster == 1)
                        grpMasters.Items.Add("Grade", "GradeMaster").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                        grpMasters.Items.Add("SubjectLevel1", "SubjectLevel1").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                        grpMasters.Items.Add("SubjectLevel2", "SubjectLevel2").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                        grpMasters.Items.Add("SubjectLevel3", "SubjectLevel3").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appTimeSchedule == 1)
                        grpMasters.Items.Add("Time Schedule", "TimeSchedule").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appExamRemark == 1)
                        grpMasters.Items.Add("Exam Remark", "ExamRemark").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appClassSubjectLevel == 1)
                        grpMasters.Items.Add("Class SubjectLevel", "ClassSubjectLevel").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;


                    lst.Add(grpMasters);
                    grpTransaction = new MVCxNavBarGroup() { Text = "Transactions" };
                    grpTransaction.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.MailAttach32x32;
                  
                    if ((int)SubMenuModules.appClassSyllabus == 1)
                        grpTransaction.Items.Add("Class Syllabus", "ClassSyllabus").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;

                    if ((int)SubMenuModules.appClassTimeTable == 1)
                        grpMasters.Items.Add("Class TimeTable", "ClassTimeTable").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appFacultyAllotment == 1)
                        grpTransaction.Items.Add("Faculty Allotment", "FacultyAllotment").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appSubjectAllotment == 1)
                        grpTransaction.Items.Add("Subject Allotment", "SubjectAllotment").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appExamSyllabus == 1)
                        grpTransaction.Items.Add("Exam Syllabus", "ExamSyllabus").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    //if ((int)SubMenuModules.appExamSetup == 1)
                    //    grpTransaction.Items.Add("Exam Setup", "ExamSetup").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;

                  
                    if ((int)SubMenuModules.appExamMarkEntry == 1)
                        grpTransaction.Items.Add("Exam Marks Entry", "ExamMarkEntry").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;

                    if ((int)SubMenuModules.appSubjectAllotmentDataExport == 1)
                    {
                        mItem = new MVCxNavBarItem() { Text = "Export Subject Allotment", NavigateUrl = "/SubjectAllotmentDataExport/Index" };
                        mItem.Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                        grpTransaction.Items.Add(mItem);
                    }

                    if ((int)SubMenuModules.appExamSetup == 1)
                    {
                        mItem = new MVCxNavBarItem() { Text = "Exam Setup", NavigateUrl = "/ExamSetup/Index" };
                        mItem.Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                        grpTransaction.Items.Add(mItem);
                    }



                    lst.Add(grpTransaction);

                      #endregion
                    break;
                case appModule.appLibraryManager:
                    #region
                    grpMasters = new MVCxNavBarGroup() { Text = "Master Data" };
                    grpMasters.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.ArrangeWithtextwrappingBottomright32x32;

                    if ((int)SubMenuModules.appRack == 1)
                        grpMasters.Items.Add("Rack", "Rack").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appShelf == 1)
                        grpMasters.Items.Add("Shelf", "Shelf").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    lst.Add(grpMasters);
                    //grpMasters.Items.Add("Search", "Search").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataSummary16x16;
                    //grpMasters.Items.Add("Issue/Return", "Issue/Return").Image.IconID = DevExpress.Web.ASPxThemes.IconID.EditEdit16x16;
                    //grpMasters.Items.Add("Book lost & History", "Booklost&History").Image.IconID = DevExpress.Web.ASPxThemes.IconID.ExportExporttodoc16x16;
                    //grpMasters.Items.Add("Daily Report", "Dailyreport").Image.IconID = DevExpress.Web.ASPxThemes.IconID.FindFind16x16;
                    //grpMasters.Items.Add("Book Category", "BookCategory").Image.IconID = DevExpress.Web.ASPxThemes.IconID.FormatSpellcheckasyoutype16x16;

                    grpTransaction = new MVCxNavBarGroup() {Text="Transactions" };
                    grpTransaction.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.DataEditdatasource32x32;

                    if ((int)SubMenuModules.appBookAccession == 1)
                    {
                        mItem = new MVCxNavBarItem() { Text = "Book Accession", NavigateUrl = "/BookAccession/Index" };
                        mItem.Image.IconID = DevExpress.Web.ASPxThemes.IconID.SchedulingCalendar16x16;
                        grpTransaction.Items.Add(mItem);
                    }

                    if ((int)SubMenuModules.appBookIssueSubmit == 1)
                    {
                        mItem = new MVCxNavBarItem() { Text = "Book Issue/Submit", NavigateUrl = "/BookIssueSubmit/Index" };
                        mItem.Image.IconID = DevExpress.Web.ASPxThemes.IconID.SchedulingCalendar16x16;
                        grpTransaction.Items.Add(mItem);
                    }


                    if((int)SubMenuModules.appBookLost==1)
                        grpTransaction.Items.Add("Book Lost", "BookLost").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    lst.Add(grpTransaction);

                    #endregion
                    break;
                case appModule.appFeesManager:
                    #region
                    grpMasters = new MVCxNavBarGroup() { Text = "Master Data" };
                    grpMasters.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.ArrangeWithtextwrappingBottomright32x32;
                    if ((int)SubMenuModules.appFeesCategories == 1)
                    grpMasters.Items.Add("Fees Categories", "FeesCategories").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MiscellaneousCurrency16x16;
                   // grpMasters.Items.Add("Instalment Mode", "InstalmentMaster").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appFeesHead == 1)
                        grpMasters.Items.Add("Fees HeadMaster", "FeesHead").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MiscellaneousCurrency16x16;
                    if ((int)SubMenuModules.appFeesTerm == 1)
                        grpMasters.Items.Add("Fees TermMaster", "FeesTerm").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MiscellaneousCurrency16x16;
                    lst.Add(grpMasters);
                    grpTransaction = new MVCxNavBarGroup() { Text = "Transactions" };
                    grpTransaction.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.DataEditdatasource32x32;
                    //grpTransaction.Items.Add("Fee Receipt", "FeeReceipt").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appAddFeeStructure == 1)
                        grpTransaction.Items.Add("Add Fee Structure", "AddFeeStructure").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MiscellaneousCurrency16x16;
                    if ((int)SubMenuModules.appListFeeStructure == 1)
                        grpTransaction.Items.Add("List Fee Structure", "ListFeeStructure").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MiscellaneousCurrency16x16;
                    if ((int)SubMenuModules.appStudentFeeStructure == 1)
                        grpTransaction.Items.Add("Student Fee Structure", "StudentFeeStructure").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MiscellaneousCurrency16x16;
                    if ((int)SubMenuModules.appStudentFeeStructure == 1)
                        grpTransaction.Items.Add("Student List FeeStructure", "StudentListFeeStructure").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MiscellaneousCurrency16x16;
                    if ((int)SubMenuModules.appFeesTransaction == 1)
                    {
                        mItem = new MVCxNavBarItem() { Text = "Fee Transactions", NavigateUrl = "/FeesTransaction/Index" };
                        mItem.Image.IconID = DevExpress.Web.ASPxThemes.IconID.MiscellaneousCurrency16x16;
                        grpTransaction.Items.Add(mItem);
                    }

                    lst.Add(grpTransaction);


                    //grpMasters = new MVCxNavBarGroup() { Text = "Accounts" };
                   // grpMasters.Items.Add("Account Master", "AccountMaster").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                   // lst.Add(grpMasters);
                    
                    grpReports = new MVCxNavBarGroup() { Text = "Reports" };
                    grpReports.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsReport32x32;
                    if ((int)SubMenuModules.appFeesStructureDataExport == 1)
                    {
                        mItem = new MVCxNavBarItem() { Text = "Fees Structure Report", NavigateUrl = "/FeesStructureDataExport/Index" };
                        mItem.Image.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsReport16x16;
                        grpReports.Items.Add(mItem);
                    }
                    if ((int)SubMenuModules.appStudentFeeStructDataExport == 1)
                    {
                        mItem = new MVCxNavBarItem() { Text = "Student Paid/Dues Report", NavigateUrl = "/StudentFeeStructDataExport/Index" };
                        mItem.Image.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsReport16x16;
                        grpReports.Items.Add(mItem);
                    }
                    lst.Add(grpReports);
                      #endregion
                    break;
                case appModule.appTransportManager:
                    #region
                    grpMasters = new MVCxNavBarGroup() { Text = "Master Data" };
                    grpMasters.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.ArrangeWithtextwrappingBottomright32x32;
                    //if ((int)SubMenuModules.appOwnerMaster == 1)
                      //  grpMasters.Items.Add("Owner Master", "OwnerMaster").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appBusStopMaster== 1)
                        grpMasters.Items.Add("Bus Stop", "BusStopMaster").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appRouteMaster == 1)
                        grpMasters.Items.Add("Bus Route", "RouteMaster").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appBusMaster == 1)
                        grpMasters.Items.Add("Bus Master", "BusMaster").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                  
                    if ((int)SubMenuModules.appDriverMaster == 1)
                        grpMasters.Items.Add("Driver Master", "DriverMaster").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;

                    lst.Add(grpMasters);

                    grpTransaction = new MVCxNavBarGroup() { Text = "Transactions" };
                    grpTransaction.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.SetupProperties32x32;

                    if ((int)SubMenuModules.appBusRoutePlan == 1)
                        grpTransaction.Items.Add("Bus Route Plan", "BusRoutePlan").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;

                    if ((int)SubMenuModules.appVehicleRTO == 1)
                    {
                        mItem = new MVCxNavBarItem() { Text = "Vehicle RTO", NavigateUrl = "/VehicleRTO/Index" };
                        mItem.Image.IconID = DevExpress.Web.ASPxThemes.IconID.SchedulingCalendar16x16;
                        grpTransaction.Items.Add(mItem);
                    }


                    lst.Add(grpTransaction);

                    grpReports = new MVCxNavBarGroup() { Text = "Reports" };
                    grpReports.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsReport32x32;
                  //  grpReports.Items.Add("Route Plan", "RoutePlan").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                   // grpReports.Items.Add("Vehicle Report", "VehicleReport").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    lst.Add(grpReports);

                      #endregion
                    break;
                case appModule.appUtilityManager:
                    #region 
                       grpMasters = new MVCxNavBarGroup() { Text = "Master Data" };
                    grpMasters.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.ArrangeWithtextwrappingBottomright32x32;
                  
                    //--
                    //if ((int)SubMenuModules.appBusStopMaster== 1)
                    //    grpMasters.Items.Add("Bus Stop", "BusStopMaster").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    
                    //  if ((int)SubMenuModules.appPersonalContectList == 1)
                    //    grpMasters.Items.Add("Personal Contact List", "PersonalContectList").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailContact16x16;
                    
                   
                    //if ((int)SubMenuModules.appImportedURL == 1)
                    //    grpMasters.Items.Add("URL Detail", "ImportedURL").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailMail16x16;
                    //lst.Add(grpMasters);
                    //if ((int)SubMenuModules.appNoticeBoard == 1)
                    //    grpMasters.Items.Add("NoticeBoard", "NoticeBoard").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    //--

                    lst.Add(grpMasters);

                    grpTransaction = new MVCxNavBarGroup() { Text = "Transactions" };
                    grpTransaction.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.SetupProperties32x32;

                    if ((int)SubMenuModules.appSessionTransfer == 1)
                        grpTransaction.Items.Add("Student Session Transfer", "SessionTransfer").Image.IconID = DevExpress.Web.ASPxThemes.IconID.ChartChartyaxissettings16x16;
                  
                  if ((int)SubMenuModules.appTCAllotment == 1)
                         grpTransaction.Items.Add("TC Allotment", "TCAllotment").Image.IconID = DevExpress.Web.ASPxThemes.IconID.ChartChartyaxissettings16x16;

                    if ((int)SubMenuModules.appTCSessionWise == 1)
                        grpTransaction.Items.Add("TC Detail Session wise", "TCDetailSessionWise").Image.IconID = DevExpress.Web.ASPxThemes.IconID.ChartChartyaxissettings16x16;


                    if ((int)SubMenuModules.appSessionClassAllotment == 1)
                         grpTransaction.Items.Add("Class Promote/Demote", "SessionClassAllotment").Image.IconID = DevExpress.Web.ASPxThemes.IconID.ChartChartyaxissettings16x16;

                    //--
                //     if ((int)SubMenuModules.appNewsEventMaster == 1)
                //         grpTransaction.Items.Add("Exam SetUp Transfer", "ExamSetupTransfer").Image.IconID = DevExpress.Web.ASPxThemes.IconID.ChartChartyaxissettings16x16;
                //     if ((int)SubMenuModules.appMessageBroadcast == 1)
                //         grpMasters.Items.Add("Message Broadcast", "MessageBroadcast").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;

                
                //if ((int)SubMenuModules.appSchoolImageGallery == 1)
                //        grpTransaction.Items.Add("Image Gallery", "SchoolImageGallery").Image.IconID = DevExpress.Web.ASPxThemes.IconID.ContentImage16x16;
                    
                //     if ((int)SubMenuModules.appStudentLoginCreate == 1)
                //         grpTransaction.Items.Add("Create Student LoginID", "StudentPortal").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;

                //    if ((int)SubMenuModules.appParentLoginCreate == 1)
                //        grpTransaction.Items.Add("Create Parent LoginID", "ParentPortal").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;

                //    if ((int)SubMenuModules.appTeacherLoginCreate == 1)
                //        grpTransaction.Items.Add("Create Teacher LoginID", "TeacherPortal").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    
                   
                //     if ((int)SubMenuModules.appSendMessageWishes == 1)
                //         grpTransaction.Items.Add("ID-Password Register", "StudentIDPasswordExport").Image.IconID = DevExpress.Web.ASPxThemes.IconID.ChartChartyaxissettings16x16;
                //     if ((int)SubMenuModules.appStudentExamResult == 1)
                //         grpTransaction.Items.Add("Student Exam Result", "StudentExamResult").Image.IconID = DevExpress.Web.ASPxThemes.IconID.ChartChartyaxissettings16x16;

                    //--
                     

                    lst.Add(grpTransaction);
                    //--
                  //  grpReports = new MVCxNavBarGroup() { Text = "Reports" };
                  //  grpReports.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.ReportsReport32x32;
                  ////  grpReports.Items.Add("Route Plan", "RoutePlan").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                  // // grpReports.Items.Add("Vehicle Report", "VehicleReport").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                  //  lst.Add(grpReports);
                    //--
                    #endregion
                    break;
                case appModule.appLfdManager:
                    #region
                    grpMasters = new MVCxNavBarGroup() { Text = "Master Data" };
                    grpMasters.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.ArrangeWithtextwrappingBottomright32x32;

                    
                    if ((int)SubMenuModules.appImageFlyer == 1)
                        grpMasters.Items.Add("Image Flyer Master", "ImageFlyer").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;

                    if ((int)SubMenuModules.appThoughtMaster == 1)
                        grpMasters.Items.Add("Thoughts", "ThoughtMaster").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;

                    if ((int)SubMenuModules.appFlyerVoice == 1)
                        grpMasters.Items.Add("Flyer Voice", "FlyerVoiceMaster").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;


                   

                    lst.Add(grpMasters);

                    grpTransaction = new MVCxNavBarGroup() { Text = "Transactions" };
                    grpTransaction.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.SetupProperties32x32;

                    if ((int)SubMenuModules.appFlyer == 1)
                        grpTransaction.Items.Add("Flyer Master", "FlyerMaster").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appLetterHeadMaster == 1)
                        grpTransaction.Items.Add("LetterHead", "LetterHeadMaster").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;


                     if ((int)SubMenuModules.appTopperNoticeBoard == 1)
                         grpTransaction.Items.Add("Topper Notice Board", "TopperNoticeBoard").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;

                    if ((int)SubMenuModules.appTopperNoticeBoard == 1)
                        grpTransaction.Items.Add("Topper's Photo", "TopperOldStudent").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;

                    //if ((int)SubMenuModules.appTopperNoticeBoard == 1)
                    //    grpTransaction.Items.Add("Topper List Register", "TopperList").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appAchievement == 1)
                        grpTransaction.Items.Add("Achievement", "Achievement").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;

                    if ((int)SubMenuModules.appNewsEventMaster == 1)
                        grpTransaction.Items.Add("News Event", "NewsEventMaster").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appNoticeBoard == 1)
                        grpTransaction.Items.Add("NoticeBoard", "NoticeBoard").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
                    if ((int)SubMenuModules.appPhotoGallery == 1)
                        grpTransaction.Items.Add("Photo Gallery Master", "PhotoGallery").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailNewcontact16x16;

                    if ((int)SubMenuModules.appPhotoGallery == 1)
                        grpTransaction.Items.Add("Photo Gallery Detail", "PhotoGalleryDetail").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailNewcontact16x16;

                    
                    lst.Add(grpTransaction);

                    #endregion
                    break;
                //case appModule.appOnlinePortal:
                //    #region
                //    grpMasters = new MVCxNavBarGroup() { Text = "Master Data" };
                //    grpMasters.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.ArrangeWithtextwrappingBottomright32x32;


                //    if ((int)SubMenuModules.appAndroidloginDetail == 1)
                //        grpMasters.Items.Add("Android Install Detail", "AndroidloginDetail").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;

                //    if ((int)SubMenuModules.appOnlineClassDetail == 1)
                //        grpMasters.Items.Add("OnlineClass Detail", "OnlineClassDetail").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;

                //    if ((int)SubMenuModules.appOnlineClassAttendDetail == 1)
                //        grpMasters.Items.Add("OnlineClass Attend Detail", "OnlineClassAttendDetail").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;

                //    if ((int)SubMenuModules.appOnlinePortalLoginDetail == 1)
                //        grpMasters.Items.Add("Online Portal Login Detail", "OnlinePortalLoginDetail").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;


                //    lst.Add(grpMasters);

                //    grpTransaction = new MVCxNavBarGroup() { Text = "Transactions" };
                //    grpTransaction.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.SetupProperties32x32;



                //    #endregion
                //   break;
            }
            return lst;


        }



        public static IEnumerable<MVCxNavBarGroup> GetGroupsForModuleDynamic(appModule currModule)
        {
            MVCxNavBarGroup grpMasters;
            MVCxNavBarGroup grpTransaction;
            
            MVCxNavBarGroup grpUsers;

            
            List<MVCxNavBarGroup> lst = new List<MVCxNavBarGroup>();
            #region
            grpMasters = new MVCxNavBarGroup() { Text = "Master Setup" };
            grpMasters.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.ArrangeWithtextwrappingBottomright32x32;
            //grpMasters.Items.Add("Academic Sessions", "Sessions").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
            // if ((int)SubMenuModules.appClassTimeTable == 1)
            // grpMasters.Items.Add("Class TimeTable", "ClassTimeTable").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
            if ((int)SubMenuModules.appClasses == 1)
                grpMasters.Items.Add("Class", "Classes").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
            if ((int)SubMenuModules.appSections == 1)
                grpMasters.Items.Add("Section", "Sections").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
            if ((int)SubMenuModules.appClassCategories == 1)
                grpMasters.Items.Add("Class Category", "ClassCategories").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
            if ((int)SubMenuModules.appClassSetup == 1)
                grpMasters.Items.Add("Class Setup", "ClassSetup").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
            //if((int)SubMenuModules.appSubjects==1)
            //    grpMasters.Items.Add("Subjects", "Subjects").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
            if ((int)SubMenuModules.appDepartment == 1)
                grpMasters.Items.Add("Department", "Department").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;
            if ((int)SubMenuModules.appDesignationMaster == 1)
                grpMasters.Items.Add("Designation", "DesignationMaster").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;

            if ((int)SubMenuModules.appHouses == 1)
                grpMasters.Items.Add("House", "Houses").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;


            //if ((int)SubMenuModules.appAdmissionFormSale == 1)
            //    grpMasters.Items.Add("Admission Form", "AdmissionForm").Image.IconID = DevExpress.Web.ASPxThemes.IconID.DataDatabase16x16;


            lst.Add(grpMasters);
            grpTransaction = new MVCxNavBarGroup() { Text = "Configuration & Settings" };
            grpTransaction.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.SetupProperties32x32;
            if ((int)SubMenuModules.appSchoolProfile == 1)
                grpTransaction.Items.Add("Profile", "SchoolProfile").Image.IconID = DevExpress.Web.ASPxThemes.IconID.EditEdit16x16;
            if ((int)SubMenuModules.appSettingMaster == 1)
                grpTransaction.Items.Add("Settings", "SettingMaster").Image.IconID = DevExpress.Web.ASPxThemes.IconID.EditEdit16x16;
            if ((int)SubMenuModules.appThemeSettings == 1)
                grpTransaction.Items.Add("Themes Setting", "ThemeSettings").Image.IconID = DevExpress.Web.ASPxThemes.IconID.FormatPictureshapefillcolor16x16;

            //  grpTransaction.Items.Add("Icon Setting", "IconSettings").Image.IconID = DevExpress.Web.ASPxThemes.IconID.ContentImage16x16;
            grpTransaction.Items.Add("Contact List", "ContectList").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailContact16x16;

            grpTransaction.Items.Add("Teacher", "Teacher").Image.IconID = DevExpress.Web.ASPxThemes.IconID.MailContact16x16;


            lst.Add(grpTransaction);

            grpUsers = new MVCxNavBarGroup() { Text = "User Settings" };
            grpUsers.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.PeopleCustomer32x32;

            if ((int)SubMenuModules.appUserCreation == 1)
                grpUsers.Items.Add("User Creation", "UserCreation").Image.IconID = DevExpress.Web.ASPxThemes.IconID.PeoplePublicfix16x16;
            if ((int)SubMenuModules.appUserPermission == 1)
                grpUsers.Items.Add("User Permission", "UserPermission").Image.IconID = DevExpress.Web.ASPxThemes.IconID.PeopleAssignto16x16;

            lst.Add(grpUsers);
            #endregion
            return lst;


        }


        public static IEnumerable<MVCxNavBarGroup> GetSMSNavigationBar()
        {
            MVCxNavBarGroup grpMasters;
            List<MVCxNavBarGroup> lst = new List<MVCxNavBarGroup>();

            foreach (listItem typeItem in ((new SMSTypeRepository()).GetSMSTypesItems()))
            {
                grpMasters = new MVCxNavBarGroup() { Text = typeItem.Description };
                grpMasters.HeaderImage.IconID = DevExpress.Web.ASPxThemes.IconID.MailAttach16x16;
                foreach (listItem termplateItem in ((new SMSTemplateRepository()).GetSMSTemplateItemsforSMSType(typeItem.Value)))
                {
                    grpMasters.Items.Add(termplateItem.Description, termplateItem.Value.ToString()).Image.IconID = DevExpress.Web.ASPxThemes.IconID.NavigationHome16x16;
                }
                lst.Add(grpMasters);
            }
            return lst;
        }

       



        public static IEnumerable<listItem> GetStudentSelctionOptions()
        {
            List<listItem> lst = new List<listItem>();
            lst.Add( new listItem(){ Value=1, Description="Search Classwise"});
            lst.Add(new listItem() { Value = 2, Description = "Search Housewise" });
            lst.Add(new listItem() { Value = 3, Description = "Search Buswise" });
            lst.Add(new listItem() { Value = 4, Description = "Search Hostelwise" });
            lst.Add(new listItem() { Value = 5, Description = "Search Subjectwise" });
            return lst;
        }
        public static IEnumerable<listItem> StudentSelectionListSource(int listType)
        {
            IEnumerable<listItem> sourceList = null;
            switch (listType)
            {
                case 1: sourceList = (new ClassSetupRepository()).GetClassSetupforSelect(); break;
                case 2: sourceList = (new HouseRepository()).GetHousesforSelect(); break;

            }
            return sourceList;
        }
    }

        public static class Utils
        {
            const string
                CurrentThemeCookieKey = "theme",
                DefaultTheme = "Aqua";


            static HttpContext Context
            {
                get
                {

                    return HttpContext.Current;
                }
            }

            static HttpRequest Request
            {
                get { return Context.Request; }
            }

            public static string CurrentTheme
            {
                get
                {
                    if (Request.Cookies[CurrentThemeCookieKey] != null)
                        return HttpUtility.UrlDecode(Request.Cookies[CurrentThemeCookieKey].Value);
                   

                    return DefaultTheme;
                }
            }
            public static SessionModel CurrentSession
            {
                get
                {
                    if (HttpContext.Current.Session["oSession"] != null)
                        return (SessionModel)HttpContext.Current.Session["oSession"];
                    else
                    {
                        SessionModel session = new SessionModel();
                        HttpContext.Current.Session["oSession"] = session;
                        return session;
                    }
                }
            }


            static bool? _isSiteMode;
            public static bool IsSiteMode
            {
                get
                {
                    if (!_isSiteMode.HasValue)
                    {
                        _isSiteMode = ConfigurationManager.AppSettings["SiteMode"].Equals("true", StringComparison.InvariantCultureIgnoreCase);
                    }
                    return _isSiteMode.Value;
                }
            }



        }
        public class ThemesModel
        {
            public String Name { get; set; }
            public CommonThemes Theme { get; set; }
        }
        [Serializable]
        public class SessionModel
        {
            public int AcademicSessionID { get; set; }

            public SessionModel()
            {
                this.AcademicSessionID = (new AcademicyearRepository()).GetCurrentSession().SessionId;
            }

        }

        public class myModel
        {
            public ThemesModel currTheme { get; set; }
            public appModule currModule { get; set; }
        }

        public class MyEditorsBinder : DevExpressEditorsBinder
        {
            protected override object GetPropertyValue(ControllerContext controllerContext,
                ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, IModelBinder propertyBinder)
            {
                if (propertyDescriptor.PropertyType == typeof(CommonThemes))
                {
                    TypeConverter tc = propertyDescriptor.Converter;
                    return tc.ConvertFrom(EditorExtension.GetValue<String>(propertyDescriptor.Name));
                }

                return base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, propertyBinder);
            }
        }

        public class DropDownSelectedItemModel
        {
            public int ChoiceTypeID { get; set; }
            public string ContactIDs { get; set; }

        }

        


   
}






