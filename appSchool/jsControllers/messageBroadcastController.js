var messageBroadcastController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'SessionTransferBody':
           
                break;
            case 'SessionTransferFooter':
                GridMessageBroadcast.SetHeight(e.pane.GetClientHeight() - 10);
                break;

        }
    },

   

    SelectedFromClass: function (s, e) {
        var FromClassID = s.GetValue();
        if (FromClassID.toString() == "") {
            alert("Please Select Class");
            return;
        }
        $.ajax({
            url: "/MessageBroadcast/GetSectionListByFClassIDView",
            type: "POST",
            data: { mFromClassID: FromClassID },
            success: function (data) {
                $("#FromSectionIdTD").html(data);

            },
            error: function () {
            }
        });

        
    },

  

    StudentListForMessageSend: function (s, e) {
        var mClassIID = FromClassID.GetValue();
        if (mClassIID == null) {
            alert("Please Select Branch.");
            return;
        }
        var ClasssetupID = FromSectionID.GetValue();
        if (ClasssetupID == null) {
            alert("Please Select Subject Combination.");
            return;
        }
        
        $.ajax({
            url: "/MessageBroadcast/GetAllStudentSessionDetail",
            type: "POST",
            datatype: "json",
            data: { pClassID: mClassIID, pClasssetupID: ClasssetupID },
            beforeSend: (function (data) {
                loadingPanelMessageBroadcast.Show();
            }),
            success: function (data) {


                if (data.returnStatus == false) {
                    alert(data.RetErrorMsg);
                }
                //alert(data.ListData);
                $("#SessionTransferSplitter_1_CC").html(data.ListData);
                var winHeight = document.getElementById('SessionTransferSplitter_1_CC').offsetHeight;
                GridMessageBroadcast.SetHeight(winHeight - 10);

            },
            error: function () {

            }

        }).done(function (data) {
            loadingPanelMessageBroadcast.Hide();

        });
    },

    SelectionChanged: function (s, e) {
        s.GetSelectedFieldValues("StudentID", messageBroadcastController.GetSelectedFieldValuesCallback);
    },

    UpdateStudentSessionTransfer: function (s, e) {

        var mStudentIDs = StudentIDs.GetText();
        if (mStudentIDs.toString() == "") {
            alert("Please Select Students");
            return;
        }
        var teacherID = FacultyID.GetValue();
        if (teacherID.toString() == "") {
            alert("Please Select Teacher");
            return;
        }

        var pMessage = memoMessage.GetText();
        if (pMessage.toString() == "") {
            alert("Please Enter Message");
            return;
        }


        var mEToDate = EToDate.GetDate().toDateString();
        if (mEToDate == null) {
            mEToDate = 0;
            alert("Please Select ToDate.");
            return;
        }



        var mFromDate = FromDate.GetDate().toDateString();
        if (mFromDate == null) {
            mFromDate = 0;
            alert("Please Select FromDate.");
            return;
        }


        var pSubject = Subject.GetText();
        if (pSubject.toString() == "") {
            alert("Please Enter Subject");
            return;
        }

        var mClassID = FromClassID.GetValue();
        if (mClassID == null) {
            alert("Please Select ClassID");
            return;
        }
        var mClasssetupID = FromSectionID.GetValue();
        if (mClasssetupID == null) {
            alert("Please Select ClasssetupID");
            return;
        }



        $.ajax({
            url: "/MessageBroadcast/UpdateStudentSessionTransfer",
            type: "POST",
            data: { pStudentIDs: mStudentIDs.toString(), FromDate: mFromDate, ToDate: mEToDate, Message: pMessage.toString(), Subject: pSubject.toString(), PClassID: mClassID, PClasssetupID: mClasssetupID, PTeacherID: teacherID },
            beforeSend: (function (data) {
                loadingPanelMessageBroadcast.Show();
            }),
            success: function (data) {
                alert(data.ResultMessage)

                $("#SessionTransferSplitter_1_CC").html(data.ListData);
                var winHeight = document.getElementById('SessionTransferSplitter_1_CC').offsetHeight;
                GridMessageBroadcast.SetHeight(winHeight - 10);
                StudentIDs.SetText("");


                // cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelMessageBroadcast.Hide();

        });


    },

    GetSelectedFieldValuesCallback: function (values) {
        
        StudentIDs.SetText(values);
    },

    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            GridMessageBroadcast.SelectRows();
        }
        else {
            GridMessageBroadcast.UnselectRows();
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

    

    UpdateSessionTransfer: function (s, e) {
      
        var mToClassID = ToClassID.GetValue();
        if (mToClassID == null) {
            mToClassID = 0;
            alert("Please Select ToClass.");
            return;
        }
        var mFromClassID = FromClassID.GetValue();
        if (mFromClassID == null) {
            mToClassID = 0;
            alert("Please Select FromClass.");
            return;
        }
        var mFromSectionID = FromSectionID.GetValue();
        if (mFromSectionID == null) {
            mFromSectionID = 0;
            alert("Please Select FromSection.");
            return;
        }
        var mToSectionID = ToSectionID.GetValue();
        if (mToSectionID == null) {
            mToSectionID = 0;
            alert("Please Select ToSection.");
            return;
        }

        var mStudentIDs = StudentIDs.GetText();
        if (mStudentIDs.toString()=="") {
            alert("Please Select Students");
            return;
        }

        $.ajax({
            url: "/MessageBroadcast/UpdateSessionTransfer",
            type: "POST",
            data: { pStudentIDs:mStudentIDs.toString(), pFromClassID: mFromClassID,pToClassID: mToClassID,pFromSectionID:mFromSectionID, pToSectionID:mToSectionID },
            beforeSend: (function (data) {
                loadingPanelMessageBroadcast.Show();
            }),
            success: function (data) {
                $("#SessionTransferSplitter_1_CC").html(data);
                var winHeight = document.getElementById('SessionTransferSplitter_1_CC').offsetHeight;
                GridMessageBroadcast.SetHeight(winHeight - 10);
                StudentIDs.SetText("");
               // cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
          //  loadingPanelMessageBroadcast.Hide();

        });


    }


}