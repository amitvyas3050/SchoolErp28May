var ShowFilterRow = false;


var smsLogDataExportController = {

    splitterResized: function (s, e) {
      
        switch (e.pane.name) {
            case 'SMSLogDataExportHeader':

                break;
                //case 'AttendanceDataList':
                //      //GridAttendanceDataExport.SetHeight(e.pane.GetClientHeight() - 1);
                //break;
                //case 'AttendanceDataLeftBottom':
                //     // GridMonthList.SetHeight(e.pane.GetClientHeight() - 1);
                //break;
                //case 'AttendanceDataLeftTop':
                //     // GridClassSetup.SetHeight(e.pane.GetClientHeight() - 1);
                //      break;
            case 'SentSMSReportFooter':
                GridSMSLogDataExport.SetHeight(e.pane.GetClientHeight() - 1);
                break;
                      
            }
        },



    //SelectionChangedForClasses: function (s, e) {

    //    s.GetSelectedFieldValues("ClassSetupID", smsLogDataExportController.GetSelectedFieldValuesCallbackForClasses);

    //},
    //GetSelectedFieldValuesCallbackForClasses: function (values) {

    //    txtclassSetup.SetText(values);
    //    // $("txtclassSetup").val(values);

    //},

    //SelectionChangedForMonth: function (s, e) {
    
    //    s.GetSelectedFieldValues("MonthID", smsLogDataExportController.GetSelectedFieldValuesCallbackForMonth);

    //},
    //GetSelectedFieldValuesCallbackForMonth: function (values) {
      
    //    txtMonth.SetText(values);
    //    // $("txtclassSetup").val(values);

    //},





 

    ClickPersonalInfo: function (s, e) {
        //var classID = txtclassSetup.GetText();
        var mfromDate = FromDate.GetDate().toDateString();
        var mToDate = ToDate.GetDate().toDateString();
       

        //if (classID.toString() == "") {
        //    alert("Please Select Classes");
        //    return;
        //}
        $.ajax({
            url: "/SMSLogDataExport/GetSMSLogList",
            type: "POST",
            data: {newFromDate: mfromDate, newToDate: mToDate },
            beforeSend: (function (data) {
                LoadingPanelAttDataExport.Show();
            }),
            success: function (data) {
                //alert(data);
                $("#ExportButtonSplitter_1_CC").html(data);
                var winHeight = document.getElementById('ExportButtonSplitter_1_CC').offsetHeight;
                GridSMSLogDataExport.SetHeight(winHeight - 10);
                cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelAttDataExport.Hide();

        });


    },

    //ClickMonthlyInfo: function (s, e) {
    //    var classID = txtclassSetup.GetText();
    //    var MonthID = txtMonth.GetText();
      
       

    //    if (classID.toString() == "") {
    //        alert("Please Select Classes");
    //        return;
    //    }
    //    if (MonthID.toString() == "") {
    //        alert("Please Select Months");
    //        return;
    //    }


    //    $.ajax({
    //        url: "/SMSLogDataExport/GetAttendanceInfoListMonthwise",
    //        type: "POST",
    //        data: { mClassesID: classID.toString(), mMonthsID: MonthID.toString() },
    //        beforeSend: (function (data) {
    //            LoadingPanelAttDataExport.Show();
    //        }),
    //        success: function (data) {
    //            $("#ExportButtonSplitter_1_CC").html(data);
    //            var winHeight = document.getElementById('ExportButtonSplitter_1_CC').offsetHeight;
    //            GridAttendanceDataExport.SetHeight(winHeight - 10);
    //            cbSelectAll.SetChecked(false);
    //        },
    //        error: function () {
    //        }
    //    }).done(function (data) {
    //        LoadingPanelAttDataExport.Hide();

    //    });
    //},


    //ClickParentsInfo: function (s, e) {
    //    var classID = txtclassSetup.GetText();


    //    if (classID.toString() == "") {
    //        alert("Please Select Classes");
    //        return;
    //    }
    //    $.ajax({
    //        url: "/StudentDataExport/GetParentsInfoListClassWise",
    //        type: "POST",
    //        data: { mClassesID: classID.toString() },
    //        success: function (data) {
    //            $("#StudentDataBottomSplitter_1_CC").html(data);
    //        },
    //        error: function () {
    //        }
    //    });



    //},

    //ClickGardianInfo: function (s, e) {
    //    var classID = txtclassSetup.GetText();


    //    if (classID.toString() == "") {
    //        alert("Please Select Classes");
    //        return;
    //    }
    //    $.ajax({
    //        url: "/StudentDataExport/GetGardianInfoListClassWise",
    //        type: "POST",
    //        data: { mClassesID: classID.toString() },
    //        success: function (data) {
    //            $("#StudentDataBottomSplitter_1_CC").html(data);
    //        },
    //        error: function () {
    //        }
    //    });



    //},

    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            GridSMSLogDataExport.SelectRows();
            
        }
        else {
            GridSMSLogDataExport.UnselectRows();
           
        }

    },



    //splitterResized: function (s, e) {
    //    switch (e.pane.name) {
    //        case 'RegistrationBody':
    //            GridStudents.SetHeight(e.pane.GetClientHeight() - 1);
    //            break;
    //        case 'RegistrationFooter':
    //            //tabControl = tabStudentRecord;
    //            //if (tabControl != 'undefined') {
    //            //    tabStudentRecord.SetWidth(e.pane.GetClientWidth());
    //            //};
    //            break;
    //    }
    //},
    //AddNewClicked: function (s, e) {
    //    GridStudents.AddNewRow();
    //},
    //EditClicked: function (s, e) {
    //    rowIdx = GridStudents.GetFocusedRowIndex();
    //    GridStudents.StartEditRow(rowIdx);
    //},

    //DeleteClicked: function (s, e) {
    //    GridStudents.DeleteRow();
    //},


    //FilterClicked: function (s, e) {
    //    ShowFilterRow = !ShowFilterRow;
    //    $.ajax({
    //        url: "/StudentRegistration/RefreshRegistrationGrid",
    //        type: "POST",
    //        data: { mShowFilter: ShowFilterRow },
    //        success: function (data) {
    //            $("#RegistrationSplitter_1_CC").html(data);
    //        },
    //        error: function () {
    //        }
    //    });
       
    //},
   
    
    //RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
    //    s.GetRowValues(s.GetFocusedRowIndex(), 'StudentID', studentRegistrationController.RefreshTabsView);

       
    //},

    //RefreshTabsView: function (values) {
    //    var regID;
    //    if (values != null) {
    //        regID = values;
    //    }
    //$.ajax({
    //        url: "/StudentRegistration/StudentGridRowChange",
    //        type: "POST",
    //        data: {RegID:regID},
    //        success: function (data) {
    //            $("#RegistrationSplitter_2_CC").html(data);
    //        },
    //        error: function () {
    //        }
    //    });
    //    //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
    //    //alert(values);
    //}
}