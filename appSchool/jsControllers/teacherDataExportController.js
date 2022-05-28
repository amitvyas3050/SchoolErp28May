var ShowFilterRow = false;


var teacherDataExportController = {

    splitterResized: function (s, e) {
      
        switch (e.pane.name) {
                
            case 'TeacherDataList':
                GridTeacherDataExport.SetHeight(e.pane.GetClientHeight() - 1);
                break;
         
            }
        },



    SelectionChangedForClasses: function (s, e) {

        s.GetSelectedFieldValues("ClassSetupID", teacherDataExportController.GetSelectedFieldValuesCallbackForClasses);

    },
    GetSelectedFieldValuesCallbackForClasses: function (values) {

     
        // $("txtclassSetup").val(values);

    },

  
    ClickTeacherInfo: function (s, e) {

        $.ajax({
            url: "/TeacherDataExport/GetPersonalInfoListClassWise",
            type: "POST",
            data: { },
            success: function (data) {
                $("#TeacherDataExportSplitter_1_CC").html(data);
                var winHeight = document.getElementById('TeacherDataExportSplitter_1_CC').offsetHeight;
                GridTeacherDataExport.SetHeight(winHeight - 10);
                cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        });



    },

  

    SelectAllTeacher: function (s, e) {
        if (s.GetChecked()) {
            GridTeacherDataExport.SelectRows();
        }
        else {
            GridTeacherDataExport.UnselectRows();
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