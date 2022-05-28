var ShowFilterRow = false;


var teacherDataExportDateWiseController = {

    splitterResized: function (s, e) {
      
        switch (e.pane.name) {
                
            case 'TeacherDataListDateWise':
                GridTeacherDataExportDateWise.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            }
        },



    SelectionChangedForClasses: function (s, e) {

        s.GetSelectedFieldValues("ClassSetupID", teacherDataExportDateWiseController.GetSelectedFieldValuesCallbackForClasses);

    },
    GetSelectedFieldValuesCallbackForClasses: function (values){

    },

  
    ClickTeacherInfo: function (s, e) {

        $.ajax({
            url: "/TeacherDataExportDateWise/GetPersonalInfoListClassWise",
            type: "POST",
            data: { },
            success: function (data) {
                $("#TeacherDataExportSplitter_1_CC").html(data);
                var winHeight = document.getElementById('TeacherDataExportSplitter_1_CC').offsetHeight;
                GridTeacherDataExportDateWise.SetHeight(winHeight - 10);
                cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        });



    },

    ClickAllAttendance: function (s, e) {

        var a1 = FromDate.GetDate().toDateString();
        var a2 = ToDate.GetDate().toDateString();
        $.ajax({
            url: "/TeacherDataExportDateWise/GetEmployeeAtendanceList",
            type: "POST",
            data: { newFromDate: a1.toString(), newToDate: a2.toString() },
            success: function (data) {
                $("#TeacherDataExportSplitter_1_CC").html(data);
                var winHeight = document.getElementById('TeacherDataExportSplitter_1_CC').offsetHeight;
                GridTeacherDataExportDateWise.SetHeight(winHeight - 10);

            },
            error: function () {
            }
            });
    },

    SelectAllTeacher: function (s, e) {
       
        if (s.GetChecked()) {
            
            GridTeacherDataExportDateWise.SelectRows();
           
        }
        else {
            GridTeacherDataExportDateWise.UnselectRows();
        }

    },

}