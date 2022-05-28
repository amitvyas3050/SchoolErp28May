var ShowFilterRow = false;


var studentDataExportController = {

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
                GridStudentData.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'StudentDataClass':
                GridClassSetup.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            }
        },




    SelectionChangedForClasses: function (s, e) {

        s.GetSelectedFieldValues("ClassSetupID", studentDataExportController.GetSelectedFieldValuesCallbackForClasses);

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
            url: "/StudentDataExport/GetStudentListClassWise",
            type: "POST",
            data: { mClassesID: classID.toString() },
            success: function (data) {
                $("#StudentDataBottomSplitter_1_CC").html(data);
                var winHeight = document.getElementById('StudentDataBottomSplitter_1_CC').offsetHeight;
            
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
            url: "/StudentDataExport/GetPersonalInfoListClassWise",
            type: "POST",
            data: { mClassesID: classID.toString() },
            beforeSend: (function (data) {
                LoadingPanelStudentDataExport.Show();
            }),
            success: function (data) {
                $("#StudentDataBottomSplitter_1_CC").html(data);
                var winHeight = document.getElementById('StudentDataBottomSplitter_1_CC').offsetHeight;

                GridStudentData.SetHeight(winHeight - 10);
                cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelStudentDataExport.Hide();

        });



    },

    ClickParentsInfo: function (s, e) {
        var classID = txtclassSetup.GetText();
       
        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/StudentDataExport/GetParentsInfoListClassWise",
            type: "POST",
            data: { mClassesID: classID.toString() },
            beforeSend: (function (data) {
                LoadingPanelStudentDataExport.Show();
            }),
            success: function (data) {
                $("#StudentDataBottomSplitter_1_CC").html(data);
                var winHeight = document.getElementById('StudentDataBottomSplitter_1_CC').offsetHeight;

                GridStudentData.SetHeight(winHeight-10);
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelStudentDataExport.Hide();

        });



    },

    ClickGardianInfo: function (s, e) {
        var classID = txtclassSetup.GetText();


        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/StudentDataExport/GetGardianInfoListClassWise",
            type: "POST",
            data: { mClassesID: classID.toString() },
            beforeSend: (function (data) {
                LoadingPanelStudentDataExport.Show();
            }),
            success: function (data) {
                $("#StudentDataBottomSplitter_1_CC").html(data);
                var winHeight = document.getElementById('StudentDataBottomSplitter_1_CC').offsetHeight;

                GridStudentData.SetHeight(winHeight-10);
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelStudentDataExport.Hide();

        });



    },

    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            GridStudentData.SelectRows();
         
        }
        else {
            GridStudentData.UnselectRows();
        }

    },


    //---------------------------Student Photo--------------------------------------------
    SelectedClass: function (s, e) {
        var classID = s.GetValue();
        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/StudentReportExport/GetAllStudentPhotoView",
            type: "POST",
            dataType: "json",
            data: { mClassesID: classID.toString() },
            success: function (data) {

                if (data.status = false) {

                    alert(data.errormsg);


                    $("#StudentPhotoSplitter_1_CC").html(data.Listdata);
                    var winHeight = document.getElementById('StudentPhotoSplitter_1_CC').offsetHeight;
                    GridStudentPhoto.SetHeight(winHeight - 10);

                    return;
                }
                else {
                  
                    $("#StudentPhotoSplitter_1_CC").html(data.Listdata);
                    var winHeight = document.getElementById('StudentPhotoSplitter_1_CC').offsetHeight;
                    GridStudentPhoto.SetHeight(winHeight - 10);

                }




           


            },
            error: function () {
            }
        });

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