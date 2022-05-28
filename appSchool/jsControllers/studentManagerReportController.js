var studentManagerReportController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'StudentManagerReportBody':
           
                break;
            case 'StudentManagerReportFooter':
                //GridStudentSession.SetHeight(e.pane.GetClientHeight()-10);
                break;

        }
    },

    SelectedClass: function (s, e) {
        var classID = s.GetValue();
        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/StudentManagerReport/GetClassSetupList",
            datatype: "json",
            type: "POST",
            data: { mClassID: classID },
            success: function (data) {

                $("#ClassSetupIDtd").html(data.dataResult);

            },
            error: function () {
            }
        });

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

    DisplayReport: function (s, e) {
     
        var mClassID = ClassID.GetValue();
        if (mClassID == null) {
            mClassID = 1;
            //alert("Please Select Class.");
            //return;
        }

        var mClassSetupID = ClassSetupID.GetValue();
        if (mClassSetupID == null) {
            mClassSetupID = 1;
        }
     

        $.ajax({
            url: "/StudentManagerReport/GenerateStudentICardReport",
            type: "POST",
            datatype: "json",
            data: {pClassID:mClassID, pClassSetupID: mClassSetupID },
            beforeSend: (function (data) {
                loadingPanelStudentICard.Show();
            }),
            success: function (data) {
                if (data.status == true) {
                    $("#StudentManagerReportSplitter_1_CC").html(data.dataResult);
                }
                else {

                    alert(data.errorMsg);
                    return;
                }

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentICard.Hide();

        });


    }


}