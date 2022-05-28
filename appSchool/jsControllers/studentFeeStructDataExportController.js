var ShowFilterRow = false;


var studentFeeStructDataExportController = {

    splitterResized: function (s, e) {
      
        switch (e.pane.name) {
                case 'StudentDataExportHeader':

                break;
                case 'RegistrationBody':
                   // GridStudents.SetHeight(e.pane.GetClientHeight() - 1);
                    break;
                case 'RegistrationFooter':
                    //tabControl = tabStudentRecord;
                    //if (tabControl != 'undefined') {
                    //    tabStudentRecord.SetWidth(e.pane.GetClientWidth());
                    //};
                    break;
            case 'StudentDataList':
                GridStudentFeeStructData.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'StudentDataClass':
                GridClassSetup.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            }
        },



    SelectionChangedForClasses: function (s, e) {

        s.GetSelectedFieldValues("ClassID", studentFeeStructDataExportController.GetSelectedFieldValuesCallbackForClasses);

    },
    GetSelectedFieldValuesCallbackForClasses: function (values) {

        txtclassSetup.SetText(values);
        // $("txtclassSetup").val(values);

    },

    ClickLoadGrid: function (s, e) {
        var classID = txtclassSetup.GetText();


        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/StudentFeeStructDataExport/GetStudentListClassWise",
            type: "POST",
            data: { mClassesID: classID.toString() },
            success: function (data) {
                $("#StudentDataBottomSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });



    },

    ClickFeesStructureInfo: function (s, e) {
        var classID = txtclassSetup.GetText();
        var paidflag = rbtnPaidFlag.GetValue();
       

        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/StudentFeeStructDataExport/GetStudentListClassWise",
            type: "POST",
            data: { mClassesID: classID.toString(), mPaidFlag: paidflag.toString() },
            beforeSend: (function (data) {
                LoadingPanelStudentFeeStructDataExport.Show();
            }),
            success: function (data) {
                $("#StudentDataBottomSplitter_1_CC").html(data);
                var winHeight = document.getElementById('StudentDataBottomSplitter_1_CC').offsetHeight;

                GridStudentFeeStructData.SetHeight(winHeight - 10);

                cbSelectAll.SetChecked(false);

            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelStudentFeeStructDataExport.Hide();

        });



    },

    //ClickParentsInfo: function (s, e) {
    //    var classID = txtclassSetup.GetText();


    //    if (classID.toString() == "") {
    //        alert("Please Select Classes");
    //        return;
    //    }
    //    $.ajax({
    //        url: "/StudentFeeStructDataExport/GetParentsInfoListClassWise",
    //        type: "POST",
    //        data: { mClassesID: classID.toString() },
    //        beforeSend: (function (data) {
    //            LoadingPanelStudentDataExport.Show();
    //        }),
    //        success: function (data) {
    //            $("#StudentDataBottomSplitter_1_CC").html(data);
    //        },
    //        error: function () {
    //        }
    //    }).done(function (data) {
    //        LoadingPanelStudentDataExport.Hide();

    //    });



    //},

    //ClickGardianInfo: function (s, e) {
    //    var classID = txtclassSetup.GetText();


    //    if (classID.toString() == "") {
    //        alert("Please Select Classes");
    //        return;
    //    }
    //    $.ajax({
    //        url: "/StudentFeeStructDataExport/GetGardianInfoListClassWise",
    //        type: "POST",
    //        data: { mClassesID: classID.toString() },
    //        beforeSend: (function (data) {
    //            LoadingPanelStudentDataExport.Show();
    //        }),
    //        success: function (data) {
    //            $("#StudentDataBottomSplitter_1_CC").html(data);
    //        },
    //        error: function () {
    //        }
    //    }).done(function (data) {
    //        LoadingPanelStudentDataExport.Hide();

    //    });



    //},

    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            GridStudentFeeStructData.SelectRows();
        }
        else {
            GridStudentFeeStructData.UnselectRows();
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