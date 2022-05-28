var employeeattendancedataemportController = {

    splitterResized: function (s, e) {
        //alert(e.pane.name);
        switch (e.pane.name) {
            case 'EmployeeAttenDanceFooter':
                GridEmployeeBiometric.SetHeight(e.pane.GetClientHeight());
                break;
            case 'EmployeeAttenDanceBottom':
                GridEmployeeAttendance.SetHeight(e.pane.GetClientHeight());
                break;
            
        }
    },
   
    ClickAllAttendance: function (s, e) {

        var date = AttendanceDateAll.GetValue();
        if (date == null || date == '') {
            alert("Please Select Date ");
            return;
        }
        var fulldate = date.toDateString();

        $.ajax({

            url: "/EmployeeAttendanceDataEmport/GetAllAttendanceDatewise",
            type: "POST",
            data: { mDate: fulldate },
            beforeSend: (function (data) {
                LoadingPanelEmployeeAttendance.Show();
            }),
            success: function (data) {
                //  ClassSetupID.SetText(data);

                $("#EmployeeAttendanceDataEmportSplitter_1_CC").html(data);
                var winHeight = document.getElementById('EmployeeAttendanceDataEmportSplitter_1_CC').offsetHeight;
                GridEmployeeBiometric.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelEmployeeAttendance.Hide();

        });
    },


  

    UpdateAttendanceDateWise: function (s, e) {

        var date = AttendanceDateAll.GetValue();
        if (date == null || date == '') {
            alert("Please Select Date ");
            return;
        }
        var fulldate = date.toDateString();

        $.ajax({

            url: "/EmployeeAttendanceDataEmport/UpdateAttendanceDatewise",
            type: "POST",
            data: { mDate: fulldate },
            beforeSend: (function (data) {
                LoadingPanelEmployeeAttendance.Show();
            }),
            success: function (data) {
                //  ClassSetupID.SetText(data);

                $("#EmployeeAttendanceDataEmportSplitter_2_CC").html(data);
                var winHeight = document.getElementById('EmployeeAttendanceDataEmportSplitter_2_CC').offsetHeight;
                GridEmployeeAttendance.SetHeight(winHeight - 10);
                alert("Sucessfully Import employee data");
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelEmployeeAttendance.Hide();

        });
    },



   
   
  



 
}