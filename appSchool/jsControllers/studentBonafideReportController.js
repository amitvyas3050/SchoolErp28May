var studentBonafideReportController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'StudentBonafideReportBody':
           
                break;
            case 'StudentBonafideReportFooter':
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
            url: "/StudentBonafideReport/GetClassSetupList",
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
        s.GetSelectedFieldValues("StudentID", studentBonafideReportController.GetSelectedFieldValuesCallback);
    },

    GetSelectedFieldValuesCallback: function (values) {

        //alert(values)
        StudentIDs.SetText(values);
    },
    


    DisplayBonafideReport: function (s, e) {
     
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

        var mStudentID = StudentIDs.GetValue();

        if (mStudentID == null)
            {
                 alert("Please Select Students.");
                 return;
         }

     

        $.ajax({
            url: "/StudentBonafideReport/GenerateStudentBonafideReport",
            type: "POST",
            datatype: "json",
            data: {pStudentID:mStudentID.toString(), pClassID:mClassID, pClassSetupID: mClassSetupID },
            beforeSend: (function (data) {
                loadingPanelStudentReport.Show();
            }),
            success: function (data) {
                if (data.status == true) {
                    
                    $("#StudentBonafideReportSplitter_1_CC").html(data.dataResult);

                    
                }
                else {

                    alert(data.errorMsg);
                    return;
                }

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentReport.Hide();

        });


    },


    DisplayFeesCertificateReport: function (s, e) {
     
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

        var mStudentID = StudentIDs.GetValue();

        if (mStudentID == null) {
            alert("Please Select Students.");
            return;
        }

     

        $.ajax({
            url: "/StudentBonafideReport/GenerateFeesCertificateReport",
            type: "POST",
            datatype: "json",
            data: { pStudentID: mStudentID.toString(), pClassID: mClassID, pClassSetupID: mClassSetupID },
            beforeSend: (function (data) {
                loadingPanelStudentReport.Show();
            }),
            success: function (data) {
                if (data.status == true) {
                    
                    $("#StudentBonafideReportSplitter_1_CC").html(data.dataResult);

                    
                }
                else {

                    alert(data.errorMsg);
                    return;
                }

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentReport.Hide();

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
            url: "/StudentBonafideReport/GetStudentListByClassID",
            type: "POST",
            datatype: "json",
            data: { pClassID: mClassID.toString(), pClasssetupID: mClassSetupID .toString()},
            beforeSend: (function (data) {
                loadingPanelStudentReport.Show();
            }),
            success: function (data) {
                
                //$("#StudentBonafideReportSplitter_1_CC").html(data.dataResult);
                $("#StudentBonafideReportSplitter_1_CC").html(data.dataResult);
                var winHeight = document.getElementById('StudentBonafideReportSplitter_1_CC').offsetHeight;
                GridStudentList.SetHeight(winHeight - 10);


                

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentReport.Hide();

        });


    },

   
}