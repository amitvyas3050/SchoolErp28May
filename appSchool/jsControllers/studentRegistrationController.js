var ShowFilterRow = false;


var studentRegistrationController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'RegistrationBody':
                GridStudents.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'RegistrationFooter':
               
                //$("#pcClientSideAPI_CC").SetHeight(e.pane.GetClientHeight() - 100);
               // GridDocuments.SetHeight(e.pane.GetClientHeight() - 70);
             
                //tabControl = tabStudentRecord;
                //if (tabControl != 'undefined') {
                //    tabStudentRecord.SetWidth(e.pane.GetClientWidth());
                //};
                break;
        }
    },
    AddNewClicked: function (s, e) {
        GridStudents.AddNewRow();
    },
    EditClicked: function (s, e) {
        rowIdx = GridStudents.GetFocusedRowIndex();
        GridStudents.StartEditRow(rowIdx);
    },

    DeleteClicked: function (s, e) {
        alert("call delete");
        rowIdx = GridStudents.GetFocusedRowIndex();
        GridStudents.DeleteRow(rowIdx);
    },


    FilterClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        $.ajax({
            url: "/StudentRegistration/RefreshRegistrationGrid",
            type: "POST",
            data: { mShowFilter: ShowFilterRow },
            beforeSend: (function (data) {
                loadingPanelStudentRegistration.Show();
            }),
            success: function (data) {
                $("#divstudregistration").html(data);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentRegistration.Hide();
        });
       
    },
   

    RefreshClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        $.ajax({
            url: "/StudentRegistration/RefreshRegistrationGrid",
            type: "POST",
            data: { mShowFilter: ShowFilterRow },

            beforeSend: (function (data) {
                loadingPanelStudentRegistration.Show();
            }),
            success: function (data) {
                $("#divstudregistration").html(data);
                //var winHeight = document.getElementById('divstudregistration').offsetHeight;
                //  GridStudents.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentRegistration.Hide();
        });
    },


    


    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'StudentID', studentRegistrationController.GetSelectedFieldValuesCallback);

       
    },

    GetSelectedFieldValuesCallback: function (values) {
        StudentIDs.SetText(values);

        var regID;
        if (values != null) {
            regID = values;
        }
        $.ajax({
            url: "/StudentRegistration/GetStudentFullName",
            type: "POST",
            data: { StudentID: regID },
            success: function (data) {
                /*StudentFullName.SetText(data);*/

                document.getElementById("StudentFullName").innerHTML = data;
                //$("#RegistrationSplitter_3_CC").html("");
                //btnPersonal.SetEnabled(true);
                //btnParnt.SetEnabled(true);
                //btnDocLoad.SetEnabled(true);
                //btnPhotoload.SetEnabled(true);
                //btnPreviousDetail.SetEnabled(true);
                //btnPreviousSessiondetail.SetEnabled(true);
            },
            error: function () {
            }
        });




    },


    //ClickPersonalDetail: function (s, e) {
      ClickPersonalDetail: function () {

        var mStudentIDs = StudentIDs.GetText();
        if (mStudentIDs.toString() == "") {
            alert("Please Select Students");
            return;
        }

        $.ajax({
            url: "/StudentRegistration/EditPersonaldetailbyClick",
            type: "POST",
            data: { mStudentID: mStudentIDs },
            datatype: "json",
            beforeSend: (function (data) {
                loadingPanelStudentRegistration.Show();
            }),
            success: function (data) {
                $("#divstudregistration").html(data.ResultData);
                //var winHeight = document.getElementById('divstudregistration').offsetHeight;



            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentRegistration.Hide();

            //btnPersonal.SetEnabled(false);
            //btnParnt.SetEnabled(true);
            //btnDocLoad.SetEnabled(true);
            //btnPhotoload.SetEnabled(true);
            //btnPreviousDetail.SetEnabled(true);
            //btnPreviousSessiondetail.SetEnabled(true);
        });
    },

    ClickParntDetail: function (s, e) {

        var mStudentIDs = StudentIDs.GetText();
        if (mStudentIDs.toString() == "") {
            alert("Please Select Students");
            return;
        }


        $.ajax({
            url: "/StudentRegistration/EditParentdetailbyClick",
            type: "POST",
            data: { mStudentID: mStudentIDs },
            beforeSend: (function (data) {
                loadingPanelStudentRegistration.Show();
            }),

            success: function (data) {
                $("#divstudregistration").html(data.ResultData);
                //var winHeight = document.getElementById('divstudregistration').offsetHeight;


            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentRegistration.Hide();
            //btnPersonal.SetEnabled(true);
            //btnParnt.SetEnabled(false);
            //btnDocLoad.SetEnabled(true);
            //btnPhotoload.SetEnabled(true);
            //btnPreviousDetail.SetEnabled(true);
            //btnPreviousSessiondetail.SetEnabled(true);
        });
    },


    ClickDocsLoadDetail: function (s, e) {

        var mStudentIDs = StudentIDs.GetText();
        if (mStudentIDs.toString() == "") {
            alert("Please Select Students");
            return;
        }


        $.ajax({
            url: "/StudentRegistration/EditUpLoadDocsbyClick",
            type: "POST",
            data: { mStudentID: mStudentIDs },
            beforeSend: (function (data) {
                loadingPanelStudentRegistration.Show();
            }),

            success: function (data) {
                $("#divstudregistration").html(data);
                //var winHeight = document.getElementById('divstudregistration').offsetHeight;


            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentRegistration.Hide();
            //btnPersonal.SetEnabled(true);
            //btnParnt.SetEnabled(true);
            //btnDocLoad.SetEnabled(false);
            //btnPhotoload.SetEnabled(true);
            //btnPreviousDetail.SetEnabled(true);
            //btnPreviousSessiondetail.SetEnabled(true);
        });
    },

    ClickPhotoLoadDetail: function (s, e) {

        var mStudentIDs = StudentIDs.GetText();
        if (mStudentIDs.toString() == "") {
            alert("Please Select Students");
            return;
        }


        $.ajax({
            url: "/StudentRegistration/EditUpLoadPhotobyClick",
            type: "POST",
            data: { mStudentID: mStudentIDs },
            beforeSend: (function (data) {
                loadingPanelStudentRegistration.Show();
            }),
            success: function (data) {
                $("#divstudregistration").html(data);
                //var winHeight = document.getElementById('divstudregistration').offsetHeight;


            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentRegistration.Hide();
            //btnPersonal.SetEnabled(true);
            //btnParnt.SetEnabled(true);
            //btnDocLoad.SetEnabled(true);
            //btnPhotoload.SetEnabled(false);
            //btnPreviousDetail.SetEnabled(true);
            //btnPreviousSessiondetail.SetEnabled(true);
        });
    },

    ClickStudentPreviousDetail: function (s, e) {
        var mStudentIDs = StudentIDs.GetText();
        if (mStudentIDs.toString() == "") {
            alert("Please Select Student");
            return;
        }

        $.ajax({
            url: "/StudentRegistration/EditPreviousDetail",
            type: "Post",
            datatype: "json",
            data: { mStudentID: mStudentIDs },
            beforeSend: (function (data) {
                loadingPanelStudentRegistration.Show();
            }),
            success: function (data) {
                $("#divstudregistration").html(data.ResultData);
               // var winHeight = document.getElementById('divstudregistration').offsetHeight;

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentRegistration.Hide();
            //btnPersonal.SetEnabled(true);
            //btnParnt.SetEnabled(true);
            //btnDocLoad.SetEnabled(true);
            //btnPhotoload.SetEnabled(true);
            //btnPreviousDetail.SetEnabled(false);
            //btnPreviousSessiondetail.SetEnabled(true);
        });

    },

    ClickStudentBackLockSessionDetail: function (s, e) {

        var mStudentIDs = StudentIDs.GetText();
        if (mStudentIDs.toString() == "") {
            alert("Please Select Students");
            return;
        }

        $.ajax({
            url: "/StudentRegistration/EditStudentBackLockSessionDetailbyClick",
            type: "POST",
            data: { mStudentID: mStudentIDs },
            datatype: "json",
            beforeSend: (function (data) {
                loadingPanelStudentRegistration.Show();
            }),
            success: function (data) {
                $("#divstudregistration").html(data);
                //var winHeight = document.getElementById('divstudregistration').offsetHeight;
                //GridAllSession.SetHeight(winHeight - 10);


            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentRegistration.Hide();

            //btnPersonal.SetEnabled(true);
            //btnParnt.SetEnabled(true);
            //btnDocLoad.SetEnabled(true);
            //btnPhotoload.SetEnabled(true);
            //btnPreviousDetail.SetEnabled(true);
            //btnPreviousSessiondetail.SetEnabled(false);
            //btnPreviousSessiondetail.SetEnabled(false);
        });
    },




    RefreshTabsView: function (values) {
        var regID;
        if (values != null) {
            regID = values;
        }
    $.ajax({
            url: "/StudentRegistration/StudentGridRowChange",
            type: "POST",
            data: {RegID:regID},
            success: function (data) {
                $("#RegistrationSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    },


    RowSelectionChangeByClick: function(s,e)
    {
        var values = s.name;
        var regID;
        if (values != null) {
            regID = values;
        }
        $.ajax({
            url: "/StudentRegistration/StudentGridRowChange",
            type: "POST",
            data: { RegID: regID },
            success: function (data) {
                $("#RegistrationSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });


    },


    RefreshDocumentList: function (s,e) {
        var regID;
        regID=StudentID.GetText();
      

        $.ajax({
            url: "/StudentRegistration/StudentGridRowChange",
            type: "POST",
            data: {RegID:regID},
            success: function (data) {
                $("#RegistrationSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    },


    StudentDetailItemClicked: function (s, e)
    {
        var selItem = e.item.name;
        
        if (selItem == "updatepersonal") {
            studentRegistrationController.ClickPersonalDetail();
        }
        else if (selItem == "updateparant") {
            studentRegistrationController.ClickParntDetail();
        }
        else if (selItem == "uploaddoc") {
            studentRegistrationController.ClickDocsLoadDetail();
        }
        else if (selItem == "uploadphotp") {
            studentRegistrationController.ClickPhotoLoadDetail();
        }
        else if (selItem == "previoussessiondetail") {
            studentRegistrationController.ClickStudentBackLockSessionDetail();
        }
        else if (selItem == "previousDetail") {
            studentRegistrationController.ClickStudentPreviousDetail();
        }
        else if (selItem == "refresh") {
            studentRegistrationController.FilterClicked();
        }
        
    },


    

}


