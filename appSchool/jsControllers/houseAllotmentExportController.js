var ShowFilterRow = false;


var houseAllotmentExportController = {

    splitterResized: function (s, e) {
      
        switch (e.pane.name) {
                case 'HouseAllotmentExportHeader':

                break;
            case 'HouseAllotmentExportBody':
                   // GridStudents.SetHeight(e.pane.GetClientHeight() - 1);
                    break;
            case 'HouseAllotmentExportFooter':
                    //tabControl = tabStudentRecord;
                    //if (tabControl != 'undefined') {
                    //    tabStudentRecord.SetWidth(e.pane.GetClientWidth());
                    //};
                    break;
            case 'StudentDataList':
                GridStudentData.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'StudentDataClass':
                GridClassSetup.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            }
        },



    SelectionChangedForClasses: function (s, e) {

        s.GetSelectedFieldValues("ClassSetupID", houseAllotmentExportController.GetSelectedFieldValuesCallbackForClasses);

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
            url: "/HouseAllotmentExport/GetStudentListClassWise",
            type: "POST",
            data: { mClassesID: classID.toString() },
            success: function (data) {
                $("#HouseBottomSplitter_1_CC").html(data);
                var winHeight = document.getElementById('HouseBottomSplitter_1_CC').offsetHeight;
            
                GridStudentData.SetHeight(winHeight);

            },
            error: function () {
            }
        });



    },

    ClickPersonalInfo: function (s, e) {
        var classID = txtclassSetup.GetText();


        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/HouseAllotmentExport/GetPersonalInfoListClassWise",
            type: "POST",
            data: { mClassesID: classID.toString() },
            beforeSend: (function (data) {
                LoadingPanelStudentDataExport.Show();
            }),
            success: function (data) {
                $("#HouseBottomSplitter_1_CC").html(data);
                var winHeight = document.getElementById('HouseBottomSplitter_1_CC').offsetHeight;

                GridStudentData.SetHeight(winHeight - 10);
                cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelStudentDataExport.Hide();

        });



    },

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
    //        beforeSend: (function (data) {
    //            LoadingPanelStudentDataExport.Show();
    //        }),
    //        success: function (data) {
    //            $("#StudentDataBottomSplitter_1_CC").html(data);
    //            var winHeight = document.getElementById('StudentDataBottomSplitter_1_CC').offsetHeight;

    //            GridStudentData.SetHeight(winHeight-10);
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
    //        url: "/StudentDataExport/GetGardianInfoListClassWise",
    //        type: "POST",
    //        data: { mClassesID: classID.toString() },
    //        beforeSend: (function (data) {
    //            LoadingPanelStudentDataExport.Show();
    //        }),
    //        success: function (data) {
    //            $("#StudentDataBottomSplitter_1_CC").html(data);
    //            var winHeight = document.getElementById('StudentDataBottomSplitter_1_CC').offsetHeight;

    //            GridStudentData.SetHeight(winHeight-10);
    //        },
    //        error: function () {
    //        }
    //    }).done(function (data) {
    //        LoadingPanelStudentDataExport.Hide();

    //    });



    //},

    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            GridStudentData.SelectRows();
         
        }
        else {
            GridStudentData.UnselectRows();
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