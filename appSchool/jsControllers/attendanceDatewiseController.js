var attendanceDatewiseController = {

    splitterResized: function (s, e) {
        //alert(e.pane.name);
       
        switch (e.pane.name) {
            case 'LeftPane1':
                GridAttendance.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'RightPane1':
                GridAbsentStudent.SetHeight(e.pane.GetClientHeight() - 1);
                break
        }
    },

   
    ItemClicked: function (s, e) {
        txtclassName.SetText(e.item.GetText());

        txtclassID.SetText(e.item.name);

        


        //$.ajax({
            
        //    url: "/AttendanceDatewise/GetTemplateMesssageText",
        //    type: "POST",
        //    data: { mTemplateID: e.item.name },
        //    beforeSend: (function (data) {
        //        loadingPanelAttendanceDateWise.Show();
        //    }),
        //    success: function (data) {
        //        //alert(data);
        //        //ClassSetupID.SetText(data);
        //        $("#AttendanceDatewiseLFSplitter_0_CC").html(data);
        //        var winHeight = document.getElementById('AttendanceDatewiseLFSplitter_0_CC').offsetHeight;
        //        GridAttendance.SetHeight(winHeight - 10);
        //    },
           
        //    error: function () {
        //    }
        //}).done(function (data) {
        //    loadingPanelAttendanceDateWise.Hide();

        //});
        
    },


    ClickAttendanceButton: function (s, e) {
        //txtclassName.SetText(e.item.GetText());
        var classID = txtclassID.GetText();

        

        var mDate = AttendanceDate.GetDate().toDateString();
        if (mDate == "") {
            alert("Please Select Classes/Date");
            return;
        }

        $.ajax({

            url: "/AttendanceDatewise/AttendanceDaily",
            type: "POST",
            data: { mTemplateID: classID, newDate: mDate },
            beforeSend: (function (data) {
                loadingPanelAttendanceDateWise.Show();
            }),
            success: function (data) {
                //alert(data);
                //ClassSetupID.SetText(data);
                $("#AttendanceDatewiseLFSplitter_0_CC").html(data);
                var winHeight = document.getElementById('AttendanceDatewiseLFSplitter_0_CC').offsetHeight;
                GridAttendance.SetHeight(winHeight - 10);
            },

            error: function () {
            }
        }).done(function (data) {
            loadingPanelAttendanceDateWise.Hide();

        });

    },


    RowSelectionChangebyAttandanceID: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'ClassAttendanceID', attendanceDatewiseController.RefreshTabsView);
      

    },

    RefreshTabsView: function (values) {
        
        var regID=-1;
        if (values != null) {
            regID = values;
        }
        
      
        $.ajax({
            url: "/AttendanceDatewise/AbsentStudentGridRowChange",
            type: "POST",
            data: { RegID: regID },
            success: function (data) {
                $("#AttendanceDatewiseLFSplitter_1_CC").html(data);
                var winHeight = document.getElementById('AttendanceDatewiseLFSplitter_1_CC').offsetHeight;
                GridAbsentStudent.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    }



    //StudentSelectionOptionChanged: function (s, e) {
    //    var choice = rbtnListStudentSelectionOption.GetItem(rbtnListStudentSelectionOption.GetSelectedIndex()).value;
    //    //alert(choice);
    //    $.ajax({
    //        url: "/SendSMS/GetStudentDDLPartialView",
    //        type: "POST",
    //        data: { mChoice: choice },
    //        success: function (data) {
    //            $("#divStudentDropDown").html(data);
    //            //txtMessage.SetText(data);
    //        },
    //        error: function () {
    //        }
    //    });
    //    }



    }
