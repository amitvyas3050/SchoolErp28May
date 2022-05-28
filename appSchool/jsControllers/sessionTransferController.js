var sessionTransferController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'SessionTransferBody':
           
                break;
            case 'SessionTransferFooter':
                GridSessionTransfer.SetHeight(e.pane.GetClientHeight() - 10);
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
            url: "/SessionTransfer/GetSectionListByFClassIDView",
            type: "POST",
            data: { mFromClassID: FromClassID },
            success: function (data) {
                $("#FromSectionIdTD").html(data);

            },
            error: function () {
            }
        });

        sessionTransferController.FillToClassData(FromClassID);
    },

    FillToClassData: function (value) {

        $.ajax({
            url: "/SessionTransfer/GetToClassList",
            type: "POST",
            data: { mFClassID: value },
            success: function (data) {
                $("#ToClassIDtd").html(data);
              
            },
            error: function () {
            }
        });




    },

    SelectedToClass: function (s, e) {
        var ToClassID = s.GetValue();
        if (ToClassID.toString() == "") {
            alert("Please Select ToClass");
            return;
        }
        $.ajax({
            url: "/SessionTransfer/GetSectionListByToClassIDView",
            type: "POST",
            data: { mToClassID: ToClassID },
            success: function (data) {
                $("#ToSectionIDtd").html(data);

            },
            error: function () {
            }
        });
       
    },
  


    SelectStudentListSessionTransfer: function(s,e)
    {
        var FClassID = FromClassID.GetValue();
        if (FClassID==null) {
            alert("Please Select Class");
            return;
        }
        var FSectionID = FromSectionID.GetValue();
        if (FSectionID==null) {
            alert("Please Select Section");
            return;
        }

        $.ajax({
            url: "/SessionTransfer/GetAllStudentListView",
            type: "POST",
            data: { mFClassID: FClassID.toString(), mFSectionID: FSectionID.toString() },
            beforeSend: (function (data) {
                loadingPanelSessionTransfer.Show();
            }),
            success: function (data) {
                $("#SessionTransferSplitter_1_CC").html(data);
                var winHeight = document.getElementById('SessionTransferSplitter_1_CC').offsetHeight;
                GridSessionTransfer.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelSessionTransfer.Hide();

        });





    },




    SelectedClass: function (s, e) {
        var classID = s.GetValue();
        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/SessionTransfer/GetAllStudentListView",
            type: "POST",
            data: { mClassesID: classID.toString() },
            success: function (data) {
                $("#SessionTransferSplitter_1_CC").html(data);
                var winHeight = document.getElementById('SessionTransferSplitter_1_CC').offsetHeight;
                GridSessionTransfer.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        });


        sessionTransferController.FillClassSetupData(classID);

    },


    FillClassSetupData: function (value) {
   
        $.ajax({
            url: "/SessionTransfer/GetClassSetupList",
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
        s.GetSelectedFieldValues("StudentID", sessionTransferController.GetSelectedFieldValuesCallback);
    },
    GetSelectedFieldValuesCallback: function (values) {
        StudentIDs.SetText(values);
    },

    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            GridSessionTransfer.SelectRows();
        }
        else {
            GridSessionTransfer.UnselectRows();
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
            url: "/SessionTransfer/UpdateSessionTransfer",
            type: "POST",
            data: { pStudentIDs:mStudentIDs.toString(), pFromClassID: mFromClassID,pToClassID: mToClassID,pFromSectionID:mFromSectionID, pToSectionID:mToSectionID },
            beforeSend: (function (data) {
                loadingPanelSessionTransfer.Show();
            }),
            success: function (data) {
                $("#SessionTransferSplitter_1_CC").html(data);
                var winHeight = document.getElementById('SessionTransferSplitter_1_CC').offsetHeight;
                GridSessionTransfer.SetHeight(winHeight - 10);
                StudentIDs.SetText("");
               // cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelSessionTransfer.Hide();

        });


    }


}