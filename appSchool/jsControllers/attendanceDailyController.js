var attendanceDailyController = {

    splitterResized: function (s, e) {
        //alert(e.pane.name);
        switch (e.pane.name) {
            case 'RightPane':
                //tabSelectRecipients.SetHeight(e.pane.GetClientHeight()-10);
                //tabSelectRecipients.SetWidth(e.pane.GetClientWidth() - 10);
              
                break;
            case 'BottomPane':
                GridStudentAttendance.SetHeight(e.pane.GetClientHeight() - 1);
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


    ClickAttendanceButton: function (s, e) {
        var classID = ClassSetupID.GetText();
        var mDate = AttendanceDate.GetDate().toDateString();
      
        if (classID.toString() == "" || mDate=="") {
            alert("Please Select Classes/Date");
            return;
        }
        $.ajax({
            url: "/AttendanceDaily/AttendanceDaily",
            type: "POST",
            data: { mClassesID: classID.toString(), newDate: mDate },
            beforeSend: (function (data) {
                loadingPanelAttendanceDaily.Show();
            }),
            success: function (data) {
                $("#AttendanceDaily1Splitter_1_CC").html(data);
                var winHeight = document.getElementById('AttendanceDaily1Splitter_1_CC').offsetHeight;
                GridStudentAttendance.SetHeight(winHeight - 10);
                //cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelAttendanceDaily.Hide();

        });


    },

    ClickAttendanceUpdate: function (s, e) {
        var StudentIDList = StudentIDs.GetText();
        var mDescription = Description.GetText();
        var AttendanceType = AttendanceTypeID.GetValue();
        var classID = ClassSetupID.GetText();
        var mDate = AttendanceDate.GetDate().toDateString();

        if (StudentIDList.toString() == "") {
            alert("Please Select Students");
            return;
        }
        $.ajax({
            url: "/AttendanceDaily/AttendanceAllDailyUpdate",
            type: "POST",
            data: { mStudentIDList: StudentIDList.toString(), mAttendanceType: AttendanceType.toString(), pDescription: mDescription.toString(),newDate: mDate, mClassesID: classID.toString() },
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
        s.GetSelectedFieldValues("StudentAttendanceID", attendanceDailyController.GetSelectedFieldValuesCallback);
    },

    GetSelectedFieldValuesCallback: function (values) {
        StudentIDs.SetText(values);
    },
   

   }
