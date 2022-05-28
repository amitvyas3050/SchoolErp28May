var tCAllotmentController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'TCAllotmentBody':
           
                break;
            case 'TCAllotmentFooter':
                GridTCAllotment.SetHeight(e.pane.GetClientHeight() - 10);
                break;

        }
    },

    GetStudentListWithTCFalse: function (s,e)
    {
        var classID = ClassID.GetValue();
        if (classID==null) {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/TCAllotment/GetAllStudentListWithTCFalse",
            type: "POST",
            data: { mClassesID: classID.toString() },
            beforeSend: (function (data) {
                loadingPanelTCAllotment.Show();
            }),
            success: function (data) {
                $("#TCAllotmentSplitter_1_CC").html(data);
                var winHeight = document.getElementById('TCAllotmentSplitter_1_CC').offsetHeight;
                GridTCAllotment.SetHeight(winHeight - 10);

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTCAllotment.Hide();
        });



    },

    GetStudentListWithTCTrue: function (s, e) {
        var classID = ClassID.GetValue();
        if (classID==null) {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/TCAllotment/GetAllStudentListWithTCTrue",
            type: "POST",
            data: { mClassesID: classID.toString() },
            beforeSend: (function (data) {
                loadingPanelTCAllotment.Show();
            }),
            success: function (data) {
                $("#TCAllotmentSplitter_1_CC").html(data);
                var winHeight = document.getElementById('TCAllotmentSplitter_1_CC').offsetHeight;
                GridTCAllotment.SetHeight(winHeight - 10);

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTCAllotment.Hide();
        });
    },

    FillClassSetupData: function (value) {
   
        $.ajax({
            url: "/TCAllotment/GetClassSetupList",
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
        s.GetSelectedFieldValues("StudentID", tCAllotmentController.GetSelectedFieldValuesCallback);
    },

    GetSelectedFieldValuesCallback: function (values) {
        StudentIDs.SetText(values);
    },

    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            GridTCAllotment.SelectRows();
        }
        else {
            GridTCAllotment.UnselectRows();
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

    UpdateTCAllotment: function (s, e) {
       
        
        var mClassID = ClassID.GetValue();
        if (mClassID==null) {
            alert("Please Select Classes");
            return;
        }

        var TcFlag = chkTCFlag.GetValue();
        if (TcFlag == null) {
            alert("Please Select Type");
            return;
        }

        var mStudentIDs = StudentIDs.GetText();
        if (mStudentIDs.toString() == "") {
            alert("Please Select Students");
            return;
        }

        
        var mtcdate = TcDate.GetDate().toDateString();
        if (mtcdate.toString() == "") {
            alert("Please Select Tcdate");
            return;
        }

        
        $.ajax({
            url: "/TCAllotment/UpdateTCAllotmentSelectedStudent",
            type: "POST",
            data: { pStudentIDs:mStudentIDs.toString(), pTcFlag:TcFlag, pClassID:mClassID,TCDate:mtcdate},
            beforeSend: (function (data) {
                loadingPanelTCAllotment.Show();
            }),
            success: function (data) {
                $("#TCAllotmentSplitter_1_CC").html(data);
                var winHeight = document.getElementById('TCAllotmentSplitter_1_CC').offsetHeight;
                GridTCAllotment.SetHeight(winHeight - 10);
               // cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTCAllotment.Hide();

        });


    }


}