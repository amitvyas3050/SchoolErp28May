var employeeshifttimeController = {

   splitterResized: function (s, e) {
        //alert(e.pane.name);
        switch (e.pane.name) {
            case 'EmployeeShiftTimeFooter':
                GridEmployeeList.SetHeight(e.pane.GetClientHeight());
                break;
           

        }
    },

    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            GridStudentAttendance.SelectRows();
        }
        else {
            GridStudentAttendance.UnselectRows();
        }

    },

    UpdateShiftTime: function (s, e) {

        var mInTime = ShiftINTime.GetText();
        if (mInTime == null || mInTime == '') {
            alert("Please Select Time ");
            return;
        }

        var mOutTime = ShiftOutTime.GetText();
        if (mOutTime == null || mOutTime == '') {
            alert("Please Select Time ");
            return;
        }
        //var fulldate = date.toDateString();

        $.ajax({

            url: "/EmployeeShiftTimeMaster/UpdateEmployeeShiftInOutTime",
            type: "POST",
            data: { StartTime: mInTime.toString(), EndTime: mOutTime.toString() },
            beforeSend: (function (data) {
                LoadingPanelEmployeeShifTime.Show();
            }),
            success: function (data) {
                $("#EmployeeShiftTimeSplitter_1_CC").html(data);
                var winHeight = document.getElementById('EmployeeShiftTimeSplitter_1_CC').offsetHeight;
                GridEmployeeList.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelEmployeeShifTime.Hide();

        });
    },

    GetEmployeeList: function (s, e) {
        
        $.ajax({

            url: "/EmployeeShiftTimeMaster/GetEmployeeShiftInOutTimeList",
            type: "POST",
            data: {  },
            beforeSend: (function (data) {
                LoadingPanelEmployeeShifTime.Show();
            }),
            success: function (data) {
                $("#EmployeeShiftTimeSplitter_1_CC").html(data);
                var winHeight = document.getElementById('EmployeeShiftTimeSplitter_1_CC').offsetHeight;
                GridEmployeeList.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelEmployeeShifTime.Hide();

        });
    },
    
   

   }
