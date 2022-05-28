var tcCertificateReportController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'TCCertificateReportBody':
           
                break;
            case 'TCCertificateReportBody':
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
            url: "/TCCertificateReport/GetClassSetupList",
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
        s.GetSelectedFieldValues("StudentID", tcCertificateReportController.GetSelectedFieldValuesCallback);
    },
   
    GetSelectedFieldValuesCallback: function (values) {

        //alert(values)
        StudentIDs.SetText(values);
    },

    


    DisplayReport: function (s, e) {
     
        var mClassID = ClassID.GetValue();
        if (mClassID == null) {
            //mClassID = 1;
            alert("Please Select Class.");
            return;
        }

        var mClassSetupID = ClassSetupID.GetValue();
        if (mClassSetupID == null) {
            alert("Please Select Section.");
            return;
        }


        var TcFlag = chkPromotFlag.GetValue();
        if (TcFlag == null) {
            alert("Please Select Type");
            return;
        }

        if (TcFlag == 1) {
            TcFlag = "Promote"
        }

        if (TcFlag == 2) {
            TcFlag = "Demote"
        }

        var mStudentIDs = StudentIDs.GetText();
        if (mStudentIDs.toString() == "") {
            alert("Please Select Students");
            return;
        }


     

        $.ajax({
            url: "/TCCertificateReport/GenerateTcCertificateReport",
            type: "POST",
            datatype: "json",
            data: { pStudentIDs: mStudentIDs.toString(), pTcFlag: TcFlag.toString(), pClassID: mClassID, pClassSetupID: mClassSetupID },
            beforeSend: (function (data) {
                loadingPanelTCReport.Show();
            }),
            success: function (data) {
                if (data.status == true) {
                    
                    $("#TCCertificateReportSplitter_1_CC").html(data.dataResult);

                    
                }
                else {

                    alert(data.errorMsg);
                    return;
                }

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTCReport.Hide();

        });


    },

    ViewStudentList: function (s, e) {

        var mClassID = ClassID.GetValue();
        if (mClassID == null) {
            //mClassID = 1;
            alert("Please Select Class.");
            return;
        }

        var mClassSetupID = ClassSetupID.GetValue();


        if (mClassSetupID == null) {
            alert("Please Select Section.");
            return;
        }




        $.ajax({
            url: "/TCCertificateReport/GetStudentListByClassID",
            type: "POST",
            datatype: "json",
            data: { pClassID: mClassID.toString(), pClasssetupID: mClassSetupID .toString()},
            beforeSend: (function (data) {
                loadingPanelTCReport.Show();
            }),
            success: function (data) {
                
                    //$("#StudentBonafideReportSplitter_1_CC").html(data.dataResult);
                    $("#TCCertificateReportSplitter_1_CC").html(data.dataResult);
                    var winHeight = document.getElementById('TCCertificateReportSplitter_1_CC').offsetHeight;
                    GridStudentList.SetHeight(winHeight - 10);


                

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTCReport.Hide();

        });


    },

   
}