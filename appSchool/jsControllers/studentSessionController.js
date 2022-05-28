var studentSessionController = {

    //splitterResized: function (s, e) {
    //    switch (e.pane.name) {
            
    //        case 'StudentSessionBody':
           
    //            break;
    //        case 'StudentSessionFooter':
    //            GridStudentSession.SetHeight(e.pane.GetClientHeight()-10);
    //            break;

    //    }
    //},

    //SelectedClass: function (s, e) {
    //    var classID = s.GetValue();
    //    alert(classID);
    //    if (classID.toString() == "") {
    //        alert("Please Select Classes");
    //        return;
    //    }
        


    //    $.ajax({
    //        url: "/StudentSession/GetAllStudentListView",
    //        type: "POST",
    //        data: { mClassesID: classID.toString() },
    //        beforeSend: (function (data) {
    //            loadingPanelMenu.Show();
    //        }),
    //        success: function (data) {
    //            //$("#StudentSessionSplitter_1_CC").html(data);
    //            //var winHeight = document.getElementById('StudentSessionSplitter_1_CC').offsetHeight;
    //            //GridStudentSession.SetHeight(winHeight - 10);


    //            $("#divstudsession").html(data);
    //            //var winHeight = document.getElementById('StudentSessionSplitter_1_CC').offsetHeight;
    //            //GridStudentSession.SetHeight(winHeight - 10);
    //            loadingPanelMenu.Hide();
              
    //        },
    //        error: function () {
    //        }
    //    });


    //    //studentSessionController.FillClassSetupData(classID);

    //},

    SelectedClass: function (s, e) {
        var classID = s.GetValue();
        //alert(classID);
        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }

        $.ajax({
            url: "/StudentSession/GetAllStudentListView",
            type: "POST",
            data: { mClassesID: classID.toString() },
            beforeSend: (function (data) {
                loadingPanelMenu.Show();
            }),
            success: function (data) {
                $("#divstudsession").html(data);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelMenu.Hide();

        });


    },




    SelectedHomeSession: function (s, e) {
        var sessionID = s.GetValue();
        var sessionName = cmbSessionID.GetText();
       
        

        $.ajax({
            url: "/StudentSession/SetSessionAndRedirectToHome",
            type: "POST",
            data: { msessionID: sessionID.toString(), msessionName: sessionName.toString() },
            success: function (data) {
                
                window.location.href = "/Menu";

               
            },
            error: function () {
            }
        });

        //window.location = "/Menu/SessionView/"+sessionID;

        
    },


    FillClassSetupData: function (value) {
   
        $.ajax({
            url: "/StudentSession/GetClassSetupList",
            type: "POST",
            data: { mClassID: value },
            success: function (data) {
                $("#ClassSetupIDtd").html(data);
                if (rbtnStudSession.GetValue() == 2) {
                    ClassSetupID.SetVisible(true);
                    lblClassSetupID.SetVisible(true);
                }

                //var winHeight = document.getElementById('StudentSessionSplitter_1_CC').offsetHeight;
                //GridStudentSession.SetHeight(winHeight - 10);




            },
            error: function () {
            }
        });




    },





    SelectionChanged: function (s, e) {
        s.GetSelectedFieldValues("StudentSessionID", studentSessionController.GetSelectedFieldValuesCallback);
    },
    GetSelectedFieldValuesCallback: function (values) {
        StudentIDs.SetText(values);
    },

    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            GridStudentSession.SelectRows();
        }
        else {
            GridStudentSession.UnselectRows();
        }

    },

    SelectedIndexChangedForRbtnUpdate: function (s, e) {
        var v = s.GetValue();

        if (s.GetValue()==1) {

            HouseID.SetVisible(true);
            lblHouseID.SetVisible(true);

            ClassSetupID.SetVisible(false);
            lblClassSetupID.SetVisible(false);

            chkSMSInHindi.SetVisible(false);
        }
        else if(s.GetValue()==2)
        {
            ClassSetupID.SetVisible(true);
            lblClassSetupID.SetVisible(true);

            HouseID.SetVisible(false);
            lblHouseID.SetVisible(false);

            chkSMSInHindi.SetVisible(false);
        }
        else if (s.GetValue() == 3) {

            chkSMSInHindi.SetVisible(true);

            ClassSetupID.SetVisible(false);
            lblClassSetupID.SetVisible(false);

            HouseID.SetVisible(false);
            lblHouseID.SetVisible(false);
        }
    },

    UpdateStudentSession: function (s, e) {
        var chkType = rbtnStudSession.GetValue();
        if (chkType == null) {
            alert("Please Select Type");
            return;
        }
        

        var mHouseID = HouseID.GetValue();
        if (mHouseID == null) {
            mHouseID = 0;
        }
        var mClassSetupID = ClassSetupID.GetValue();
        if (mClassSetupID == null) {
            mClassSetupID = 0;
        }
        var mClassID = ClassID.GetValue();
        if (mClassID == null) {
            mClassID = 0;
        }

       

        var IsSMS = chkSMSInHindi.GetValue();
        if (chkType == 3 && IsSMS == null) {
            alert("Please Select SMS Type");
        }

        var mStudentIDs = StudentIDs.GetText();
        if (mStudentIDs == null) {
            alert("Please Select Students");
            return;
        }

        $.ajax({
            url: "/StudentSession/UpdateStudentSessionHouseWise",
            type: "POST",
            data: { pStudentIDs:mStudentIDs.toString(), pchkType: chkType,pHouseID: mHouseID,pClassSetupID: mClassSetupID,pIsSMS:IsSMS, pClassID:mClassID },
            beforeSend: (function (data) {
                loadingPanelMenu.Show();
            }),
            success: function (data) {
                $("#divstudsession").html(data);
                //var winHeight = document.getElementById('StudentSessionSplitter_1_CC').offsetHeight;
                //GridStudentSession.SetHeight(winHeight - 10);
               // cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelMenu.Hide();

        });


    }


}