var attendanceClasswiseController = {

    splitterResized: function (s, e) {
        //alert(e.pane.name);
        switch (e.pane.name) {
            case 'LeftPane':
                GridClassSetup.SetHeight(e.pane.GetClientHeight());
                break;
            case 'BottomPane':
                GridStudentAttendance.SetHeight(e.pane.GetClientHeight());
                break;

            
        }
    },
    SelectionChangedForClasses: function (s, e) {

        s.GetSelectedFieldValues("ClassSetupID", attendanceClasswiseController.GetSelectedFieldValuesCallbackForClasses);

    },
    GetSelectedFieldValuesCallbackForClasses: function (values) {

        txtclassSetup.SetText(values);
        // $("txtclassSetup").val(values);


    },

    ClickAllAttendance: function (s, e) {
    
        var date = AttendanceDateAll.GetValue();
        if (date == null || date == '') {
            alert("Please Select Date ");
            return;
        }
        var fulldate = date.toDateString();

        var classes = txtclassSetup.GetText();
        if (classes == null || classes == '') {
            alert("Please Select Class ");
            return;
        }
      
        $.ajax({
            
            url: "/AttendanceClassWise/GetAllAttendanceDatewise",
            type: "POST",
            data: { mDate: fulldate, mClasses: classes.toString() },
            beforeSend: (function (data) {
                LoadingPanelAttendanceClass.Show();
            }),
            success: function (data) {
                //  ClassSetupID.SetText(data);
            
                $("#AttendanceClasswiseBodySplitter_1_CC").html(data);
              
                var winHeight = document.getElementById('AttendanceClasswiseBodySplitter_1_CC').offsetHeight;

                GridStudentAttendance.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelAttendanceClass.Hide();

        });
    } ,

    ClickAllAttendanceList: function (s, e) {

        var date = AttendanceDateAll.GetValue();
        if (date == null || date == '') {
            alert("Please Select Date ");
            return;
        }
        var fulldate = date.toDateString();

        var classes = txtclassSetup.GetText();
        if (classes == null || classes == '') {
            alert("Please Select Class ");
            return;
        }

        $.ajax({

            url: "/AttendanceClassWise/GetAllAttendanceDatewiseList",
            type: "POST",
            data: { mDate: fulldate, mClasses: classes.toString() },
            beforeSend: (function (data) {
                LoadingPanelAttendanceClass.Show();
            }),
            success: function (data) {
                //  ClassSetupID.SetText(data);
         
                $("#AttendanceClasswiseBodySplitter_1_CC").html(data);
                var winHeight = document.getElementById('AttendanceClasswiseBodySplitter_1_CC').offsetHeight;

                GridStudentAttendance.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelAttendanceClass.Hide();

        });
    },
    ClickAllAttendanceChart: function (s, e) {

        var date = AttendanceDateAll.GetValue();
        if (date == null || date == '') {
            alert("Please Select Date ");
            return;
        }
        var fulldate = date.toDateString();

        var classes = txtclassSetup.GetText();
        if (classes == null || classes == '') {
            alert("Please Select Class ");
            return;
        }

        $.ajax({

            url: "/AttendanceClassWise/GetAllAttendanceDatewiseChart",
            type: "POST",
            data: { mDate: fulldate, mClasses: classes.toString() },
            beforeSend: (function (data) {
                LoadingPanelAttendanceClass.Show();
            }),
            success: function (data) {
                //  ClassSetupID.SetText(data);
              
                $("#AttendanceClasswiseBodySplitter_1_CC").html(data);
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelAttendanceClass.Hide();

        });
    },



   
    ItemClicked: function (s, e) {
    
        ClassSetupName.SetText(e.item.GetText());
        ClassSetupID.SetText(e.item.name);
            
        $.ajax({
            
            url: "/AttendanceDaily/GetTemplateMesssageText",
            type: "POST",
            data: { mTemplateID: e.item.name },
            success: function (data) {
                //  ClassSetupID.SetText(data);
                $("#attendanceBottomID").html(data);
            },
            error: function () {
            }
        });
        
    }
  



 
}