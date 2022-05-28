//// var sessionController = {

////    CurrentSessionchanged: function (s, e) {
////        var selID = cboSessionMain.GetValue();
////        //alert(selID);
////        $.ajax({
////            url: "/App/Sessionchanged",
////            type: "POST",
////            data: { newSessionID: selID }
////        });

////    }
////}

var lftNavMenuController = {

    ItemClicked: function (s,e) {
        var selItem = e.item.name;
        // alert(selItem);
        switch (selItem)
        {
            


            case "SchoolProfile":
                $.ajax({ url: "/SchoolMaster/SchoolMasterView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide()});
                break;
                
            case "SettingMaster":
                $.ajax({ url: "/SettingMaster/SettingMasterView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "SchoolImageGallery":
                $.ajax({ url: "/Home/SchoolImageGallery", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "ThemeSettings":
                $.ajax({ url: "/Home/ChangeTheme", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "IconSettings":
                $.ajax({ url: "/Home/IconSettings", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "UserPermission":
                $.ajax({ url: "/UserPermission/UserPermissionView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "Session":
                $.ajax({ url: "/Academicyear/AcademicyearView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            
            case "Classes":
                $.ajax({ url: "/Classes/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "Sections":
                $.ajax({ url: "/Sections/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "Subjects":
                $.ajax({ url: "/Subject/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "Houses":
                $.ajax({ url: "/Houses/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "ClassCategories":
                $.ajax({
                    url: "/ClassCategories/Index",
                    beforeSend: (function (data)
                    {
                        //$("#maincontentpage").html(null);
                        loadingPanelMenu.Show();
                    }),
                    success: function (data)
                    {
                        $("#maincontentpage").html(data);
                    }
                }).done(function (data)
                {
                    loadingPanelMenu.Hide()
                });
                break;
            case "ClassSetup":
                $.ajax({
                    url: "/ClassSetup/Index", beforeSend: (function (data)
                    {
                        //$("#maincontentpage").html(null);
                        loadingPanelMenu.Show();
                    }),
                    success: function (data) { $("#maincontentpage").html(data); }
                }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "StudentRegistration":
                $.ajax({
                    url: "/StudentRegistration/Index", beforeSend: (function (data) { loadingPanelMenu.Show() }),
                    success: function (data) { $("#maincontentpage").html(data); }
                }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "StudentSession":
                $.ajax({
                    url: "/StudentSession/Index",
                    beforeSend: (function (data)
                    {
                        loadingPanelMenu.Show();
                    }),
                    success: function (data)
                    {
                        $("#maincontentpage").html(data);
                    }
                }).done(function (data)
                { loadingPanelMenu.Hide() });
                break;
                
            case "StudentPhotoReport":
                $.ajax({ url: "/StudentReportExport/StudentPhotoView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "SessionTransfer":
                $.ajax({ url: "/SessionTransfer/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "StudentRegistrationWithTC":
                $.ajax({ url: "/StudentRegistrationWithTC/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
                
            case "NewsEventMaster":
                // alert("call");

                $.ajax({ url: "/NewsEventMaster/NewsEventMasterView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                //$.ajax({ url: "/StudentPortal/StudentIDCreateView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "TCAllotment":
                $.ajax({ url: "/TCAllotment/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "DefineSMSType":
                $.ajax({ url: "/SMSManager/SMSTypesView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "FeesCategory":
                $.ajax({ url: "/FeesCategory/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide(); });
                break;

            case "DefineExams":

                $.ajax({
                    url: "/ExamsManager/Index",
                    beforeSend: (function (data) { loadingPanelMenu.Show(); }),
                    success: function (data) {
                        $("#maincontentpage").html(data);
                    }
                }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "MessageMaster":
                $.ajax({ url: "/SMSTemplate/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "AccountMaster":
                $.ajax({ url: "/AccountMaster/AccountMasterView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
          
            case "DepartmentMaster":
                $.ajax({ url: "/DepartmentMaster/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "DesignationMaster":
                $.ajax({ url: "/DesignationMaster/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "BusStopMaster":
                $.ajax({ url: "/BusStopMaster/BusStopView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "RouteMaster":
                $.ajax({ url: "/RouteMaster/RouteView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "GradeMaster":
                $.ajax({ url: "/GradeMaster/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "TeacherRegistration":
                $.ajax({ url: "/TeacherRegistration/TeacherRegistrationsView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
   
            case "BusMaster":
                $.ajax({ url: "/BusMaster/BusMasterView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "InstalmentMaster":
                $.ajax({ url: "/FeesManager/InstalmentMaster", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "OwnerMaster":
                $.ajax({ url: "/OwnerMaster/OwnerMasterView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "StudentwiseReport":
                $.ajax({ url: "/StudentRegistration/StudentwiseReport", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "SendMessage":
                $.ajax({ url: "/SendMessege/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
                
            case "SendMessageWishes":
                $.ajax({ url: "/SendMessegeWishes/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
                
            case "SendMessageWishesTeacher":
                $.ajax({ url: "/SendMessegeWishesTeacher/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "SendSMSAbsentees":
                $.ajax({ url: "/SendSMSAbsentees/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "FeesHead":
                $.ajax({ url: "/FeesHead/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "FeesTerm":
                $.ajax({ url: "/FeesTermMaster/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "AddFeeStructure":
                $.ajax({ url: "/FeesStructure/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
                
            case "ListFeesStructure":

                $.ajax({ url: "/ListFeesStructure/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
                
            case "FeesTransaction":
                    $.ajax({ url: "/FeesTransaction/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }),  success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "SMSReports":
                $.ajax({ url: "/SentSMSReport/SentSMSReportView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
                
            case "StudentFeeStructure":
               // alert(" st");
                $.ajax({ url: "/StudentFeeStructure/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "Reportexport":
                $.ajax({ url: "/StudentReportExport/Export", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "SendGeneralMessage":
                $.ajax({ url: "/SendGeneralMessege/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "SendReligionMessage":
            
                $.ajax({ url: "/SendReligionMessege/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "ScheduledSMS":
                $.ajax({ url: "/ScheduledSMS/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "SchedulerWishes":
                $.ajax({ url: "/SchedulerWishes/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "SchedulerWishesTeacher":
                $.ajax({ url: "/SchedulerWishesTeacher/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "UserCreation":
                $.ajax({ url: "/UserCreation/UserRegistrations", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "NoticeBoard":
                $.ajax({ url: "/NoticeBoard/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "HouseAllotment":
                $.ajax({ url: "/HouseAllotment/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "SendHouseMessage":
                $.ajax({ url: "/SendHouseMessege/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "FeesDefaulterSMS":
                $.ajax({ url: "/FeesDefaulter/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "ContectList":
                $.ajax({ url: "/ContectList/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "Teacher":
                $.ajax({ url: "/Teacher/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "RollNoAllotment":
                $.ajax({ url: "/RollNoAllotment/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "StudentListFeesStructure":
                $.ajax({ url: "/StudentListFeesStructure/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "Rack":
                $.ajax({ url: "/RackMaster/RackMasterView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "Shelf":
                $.ajax({ url: "/ShelfMaster/ShelfMasterView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
                
            case "ClassSyllabus":
                $.ajax({ url: "/ClassSyllabus/ClassSyllabusView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
                
            case "SubjectLevel1":
                $.ajax({ url: "/SubjectLevel1Master/index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "SubjectLevel2":
                $.ajax({ url: "/SubjectLevel2Master/index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "SubjectLevel3":
                $.ajax({ url: "/SubjectLevel3Master/index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "TimeSchedule":
                $.ajax({ url: "/TimeScheduleMaster/index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
                
            case "FacultyAllotment":
                $.ajax({ url: "/FacultyAllotment/index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "ClassSubjectLevel":
                $.ajax({ url: "/ClassSubjectLevel/index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "ExamRemark":
                $.ajax({ url: "/ExamRemarkMaster/index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
                
            case "ExamSyllabus":
                $.ajax({ url: "/ExamSyllabus/index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "ExamSetup":
                $.ajax({ url: "/ExamSetup/index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
                
            case "BusRoutePlan":
                $.ajax({ url: "/BusRoutePlan/index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

                
            case "DriverMaster":
                $.ajax({ url: "/DriverMaster/index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "AdmissionForm":
                $.ajax({ url: "/AdmissionFormSale/index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
                
            case "BookLost":
               
                $.ajax({ url: "/BookLost/BookLostView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "StaffRegistration":
                $.ajax({ url: "/StaffRegistration/StaffRegistrationView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
                
            case "SessionClassAllotment":
                $.ajax({ url: "/SessionClassAllotment/SessionClassAllotmentView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "StudentExamResult":
                $.ajax({ url: "/StudentExamResult/StudentExamResultView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "TCRegister":
                $.ajax({ url: "/StudentRegistrationWithTC/Index", beforeSend: (function (data) {  loadingPanelMenu.Show()  }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "StudentPortal":
                //alert("call");
                $.ajax({ url: "/StudentPortal/StudentIDCreateView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "ParentPortal":
                //alert("call");
                $.ajax({ url: "/ParentPortal/ParentIDCreateView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "TeacherPortal":
                alert("call");
                $.ajax({ url: "/TeacherPortal/TeacherIDCreateView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "SubjectAllotment":
                //alert("call");
                $.ajax({ url: "/SubjectAllotment/index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "StudentIDPasswordExport":
                $.ajax({ url: "/StudentIDPasswordExport/StudentIDPasswordExportView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;


            case "SendStudentIDPassword":
                //alert("call");
                $.ajax({ url: "/SendStudentIDPassword/SendStudentIDPasswordView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "SendParentIDPassword":
                //alert("call");
                $.ajax({ url: "/SendParentIDPassword/SendParentIDPasswordView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "SendTeacherIDPassword":
                //alert("call");
                $.ajax({ url: "/SendTeacherIDPassword/SendTeacherIDPasswordView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "ExamMarkEntry":
                $.ajax({ url: "/ExamMarkEntry/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
                
            case "ClassTimeTable":
                $.ajax({ url: "/ClassTimeTable/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "SubjectAllotment":
                $.ajax({ url: "/SubjectAllotment/SubjectAllotmentView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "StudentICardReport":
                //alert("call");
                $.ajax({ url: "/StudentManagerReport/StudentICardView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "StudentBonafideReport":
                //alert("call");
                $.ajax({ url: "/StudentBonafideReport/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "TCCertificateReport":
                //alert("call");
                $.ajax({ url: "/TCCertificateReport/TCCertificateReportView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "TopperList":
                // alert("call");
                $.ajax({ url: "/TopperList/TopperListView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "TopperNoticeBoard":

                $.ajax({ url: "/TopperNoticeBoard/TopperNoticeBoardView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "TopperOldStudent":
                $.ajax({ url: "/TopperOldStudent/TopperOldStudentView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "ImportedURL":
                //alert("call");
                $.ajax({ url: "/ImportedURL/ImportedURLView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "PhotoGallery":
                // alert("call");
                $.ajax({ url: "/PhotoGallery/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "SendPersonalMessege":
                // alert("call");
                $.ajax({ url: "/SendPersonalMessege/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

            case "ExamSetupTransfer":
                $.ajax({ url: "/ExamSetupTransfer/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "Achievement":
                // alert("call");

                $.ajax({ url: "/Achievement/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                //$.ajax({ url: "/StudentPortal/StudentIDCreateView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "ImageFlyer":
                // alert("call");

                $.ajax({ url: "/ImageFlyer/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                //$.ajax({ url: "/StudentPortal/StudentIDCreateView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "PhotoGalleryDetail":
                // alert("call");

                $.ajax({ url: "/PhotoGalleryDetail/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                //$.ajax({ url: "/StudentPortal/StudentIDCreateView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "ThoughtMaster":
                // alert("call");

                $.ajax({ url: "/ThoughtMaster/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                //$.ajax({ url: "/StudentPortal/StudentIDCreateView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "FlyerMaster":
                // alert("call");

                $.ajax({ url: "/FlyerMaster/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                //$.ajax({ url: "/StudentPortal/StudentIDCreateView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "LetterHeadMaster":
                // alert("call");

                $.ajax({ url: "/LetterHeadMaster/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                //$.ajax({ url: "/StudentPortal/StudentIDCreateView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "FlyerVoiceMaster":
                // alert("call");

                $.ajax({ url: "/FlyerVoiceMaster/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                //$.ajax({ url: "/StudentPortal/StudentIDCreateView", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "TCDetailSessionWise":
                // alert("call");
                $.ajax({ url: "/TCDetailSessionWise/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "EmployeeAttendanceDataEmport":
                // alert("call");
                $.ajax({ url: "/EmployeeAttendanceDataEmport/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });

                break;
            case "EmployeeShiftTimeMaster":
                // alert("call");
                $.ajax({ url: "/EmployeeShiftTimeMaster/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });

                break;
            case "EmployeeAttendanceDaily":
                // alert("call");
                $.ajax({ url: "/EmployeeAttendanceDaily/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });

                break;
            case "EmployeeSMSAbsentees":
                // alert("call");
                $.ajax({ url: "/EmployeeSMSAbsentees/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });

                break;
            case "EmployeeAttendanceReport":
                // alert("call");
                $.ajax({ url: "/EmployeeAttendanceReport/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;
            case "SendSMSOnlineAbsentees":
                $.ajax({ url: "/SendSMSOnlineAbsentees/Index", beforeSend: (function (data) { loadingPanelMenu.Show(); }), success: function (data) { $("#maincontentpage").html(data); } }).done(function (data) { loadingPanelMenu.Hide() });
                break;

        }
    }


}
