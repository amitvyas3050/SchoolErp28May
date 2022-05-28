var employeeattendanceReportController = {

   splitterResized: function (s, e) {
        //alert(e.pane.name);
        switch (e.pane.name) {
            case 'EmployeeAttenDanceFooter':
                GridEmployeeAttendanceList.SetHeight(e.pane.GetClientHeight());
                break;
        }
    },

    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            GridEmployeeAttendanceList.SelectRows();
        }
        else {
            GridEmployeeAttendanceList.UnselectRows();
        }

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
        
    },

    ClickAllAttendance: function (s, e) {

        var txtdat = AttendanceDateAll.GetText();
        if (txtdat == null) {
            alert("please select date")
            return;
        }

        var mDate = AttendanceDateAll.GetDate().toDateString();
      
        $.ajax({
            url: "/EmployeeAttendanceDaily/GetEmployeeAtendanceList",
            type: "POST",
            data: { newDate: mDate },
            beforeSend: (function (data) {
                loadingPanelAttendanceDaily.Show();
            }),
            success: function (data) {
                $("#EmployeeAttendanceDailySplitter_1_CC").html(data);
                var winHeight = document.getElementById('EmployeeAttendanceDailySplitter_1_CC').offsetHeight;
                GridEmployeeAttendanceList.SetHeight(winHeight - 10);
               
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelAttendanceDaily.Hide();

        });


    },

    

    ClickAttendanceUpdate: function (s, e) {
        var EmployeeAttendancIDList = EmployeeAttendanceLogID.GetText();
        var mDescription = Description.GetText();
        var AttendanceType = AttendanceTypeID.GetText();
        var mDate = AttendanceDateAll.GetDate().toDateString();

        if (EmployeeAttendancIDList.toString() == "") {
            alert("Please Select Employee");
            return;
        }
        $.ajax({
            url: "/EmployeeAttendanceDaily/AttendanceAllDailyUpdate",
            type: "POST",
            data: { mEmployeeAttendancIDList: EmployeeAttendancIDList.toString(), mAttendanceType: AttendanceType.toString(), pDescription: mDescription.toString(), newDate: mDate },
            beforeSend: (function (data) {
                loadingPanelAttendanceDaily.Show();
            }),
            success: function (data) {
                $("#AttendanceDaily1Splitter_1_CC").html(data);
                var winHeight = document.getElementById('AttendanceDaily1Splitter_1_CC').offsetHeight;
                GridStudentAttendance.SetHeight(winHeight - 10);
                StudentIDs.SetText("");
                Description.SetText("");


                //cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelAttendanceDaily.Hide();

        });


    },

    SelectionChanged: function (s, e) {
        s.GetSelectedFieldValues("AttendanceLogId", employeeattendanceDailyController.GetSelectedFieldValuesCallback);
    },

    GetSelectedFieldValuesCallback: function (values) {
        EmployeeAttendanceLogID.SetText(values);
    },


   }
