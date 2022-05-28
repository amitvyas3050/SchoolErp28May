var ShowFilterRow = false;


var teacherRegistrationController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'TeacherRegistrationBody':
                var tt = $('#TeacherRegistrationSplitter_0_CC').width();
                GridTeachers.SetHeight(e.pane.GetClientHeight());
                GridTeachers.SetWidth(tt-10);

                break;
            case 'TeacherRegistrationFooter':

                //GridTeacherExprtiseData.SetHeight(e.pane.GetClientHeight() - 70);
                //GridTeacherQualificationData.SetHeight(e.pane.GetClientHeight() - 70);
               // GridSubjectExprtiseData.SetHeight(e.pane.GetClientHeight() - 70);
                //GridTeacherAchivmentData.SetHeight(e.pane.GetClientHeight() - 70);
                //tabControl = tabStudentRecord;
                //if (tabControl != 'undefined') {
                //    tabStudentRecord.SetWidth(e.pane.GetClientWidth());
                //};
                break;
        }
    },
    AddNewClicked: function (s, e) {
        GridTeachers.AddNewRow();
    },
    EditClicked: function (s, e) {
        rowIdx = GridTeachers.GetFocusedRowIndex();
        GridTeachers.StartEditRow(rowIdx);
    },
    FilterClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        $.ajax({
            url: "/teacherRegistration/RefreshTeacherRegistrationGrid",
            type: "POST",
            data: { mShowFilter: ShowFilterRow },
            success: function (data) {
                $("#TeacherRegistrationSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });
       
    },
    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
      
        s.GetRowValues(s.GetFocusedRowIndex(), 'TeacherID', teacherRegistrationController.GetSelectedFieldValuesCallback);
        
    },

   
    ////RefreshTabsView: function (values) {
    ////    var regID;
    ////    if (values != null) {
    ////        regID = values;
    ////    }
     
    ////$.ajax({
    ////        url: "/TeacherRegistration/teacherGridRowChange",
    ////        type: "POST",
    ////        data: {RegID:regID},
    ////        success: function (data) {
    ////            $("#TeacherRegistrationSplitter_2_CC").html(data);
    ////        },
    ////        error: function () {
    ////        }
    ////    });
    ////    //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
    ////    //alert(values);
    ////},

    ////RefreshSignatureImg: function (s, e) {
    ////    var regID;
    ////    regID = TeacherID.GetText();


    ////    $.ajax({
    ////        url: "/TeacherRegistration/teacherGridRowChange",
    ////        type: "POST",
    ////        data: { RegID: regID },
    ////        success: function (data) {
    ////            $("#TeacherRegistrationSplitter_2_CC").html(data);
    ////        },
    ////        error: function () {
    ////        }
    ////    });
    ////    //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
    ////    //alert(values);
    ////}

    GetSelectedFieldValuesCallback: function (values) {
        TeacherIDs.SetText(values);
        var TeacherID;
        if (values != null) {
            TeacherID = values;
        }

        $.ajax({
            url: "/TeacherRegistration/GetTeacherFullName",
            type: "POST",
            data: { mTeacherID: TeacherID },
            success: function (data) {
                TeacherFullName.SetText(data.TeacherFullName);
                btnPersonal.SetEnabled(true);
                btnPhotoload.SetEnabled(true);
                btnSignature.SetEnabled(true);
                btnExperties.SetEnabled(true);
                btnQualifications.SetEnabled(true);
                btnSubjectExpertise.SetEnabled(true);
                btnTeacherAchievements.SetEnabled(true);
            },
            error: function () {
            }
        });

    },

    ClickPersonalDetail: function (s, e) {

        var mTeacherIDs = TeacherIDs.GetText();
        if (mTeacherIDs.toString() == "") {
            alert("Please Select Teacher");
            return;
        }


        $.ajax({
            url: "/TeacherRegistration/EditPersonaldetailbyClickButton",
            type: "POST",
            data: { mTeacherID: mTeacherIDs },
            datatype:"json",
            beforeSend: (function (data) {
                loadingPanelTeacherRegistration.Show();
            }),
            success: function (data) {
                $("#TeacherRegistrationSplitter_3_CC").html(data.ResultData);
                var winHeight = document.getElementById('TeacherRegistrationSplitter_3_CC').offsetHeight;
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTeacherRegistration.Hide();
            btnPersonal.SetEnabled(false);
            btnPhotoload.SetEnabled(true);
            btnSignature.SetEnabled(true);
            btnExperties.SetEnabled(true);
            btnTeacherDocUpload.SetEnabled(true);
            btnQualifications.SetEnabled(true);
            btnSubjectExpertise.SetEnabled(true);
            btnTeacherAchievements.SetEnabled(true);
        });
    },

    ClickPhotoLoadDetail: function (s, e) {

        var mTeacherIDs = TeacherIDs.GetText();
        if (mTeacherIDs.toString() == "") {
            alert("Please Select Teacher");
            return;
        }


        $.ajax({
            url: "/TeacherRegistration/EditUpLoadPhotobyClickButton",
            type: "POST",
            data: { mTeacherID: mTeacherIDs },
            datatype:"json",
            beforeSend: (function (data) {
                loadingPanelTeacherRegistration.Show();
            }),
            success: function (data) {
                $("#TeacherRegistrationSplitter_3_CC").html(data.ResultData);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTeacherRegistration.Hide();
            btnPersonal.SetEnabled(true);
            btnPhotoload.SetEnabled(false);
            btnSignature.SetEnabled(true);
            btnExperties.SetEnabled(true);
            btnTeacherDocUpload.SetEnabled(true);
            btnQualifications.SetEnabled(true);
            btnSubjectExpertise.SetEnabled(true);
            btnTeacherAchievements.SetEnabled(true);
        });
    },

    ClickTeacherDocumentUploadDetail: function (s, e) {

        var mTeacherIDs = TeacherIDs.GetText();
        if (mTeacherIDs.toString() == "") {
            alert("Please Select Teacher");
            return;
        }


        $.ajax({
            url: "/TeacherRegistration/EditTeacherDocsUploadbyClickButton",
            type: "POST",
            data: { mTeacherID: mTeacherIDs },
            datatype: "json",
            beforeSend: (function (data) {
                loadingPanelTeacherRegistration.Show();
            }),
            success: function (data) {
                $("#TeacherRegistrationSplitter_3_CC").html(data.ResultData);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTeacherRegistration.Hide();
            btnPersonal.SetEnabled(true);
            btnPhotoload.SetEnabled(true);
            btnTeacherDocUpload.SetEnabled(false);
            btnSignature.SetEnabled(true);
            btnExperties.SetEnabled(true);
            btnQualifications.SetEnabled(true);
            btnSubjectExpertise.SetEnabled(true);
            btnTeacherAchievements.SetEnabled(true);
        });
    },

    ClickSignatureDetail: function (s, e) {

        var mTeacherIDs = TeacherIDs.GetText();
        if (mTeacherIDs.toString() == "") {
            alert("Please Select Teacher");
            return;
        }


        $.ajax({
            url: "/TeacherRegistration/EditSignatureDetailbyClickButton",
            type: "POST",
            data: { mTeacherID: mTeacherIDs },
            datatype:"json",
            beforeSend: (function (data) {
                loadingPanelTeacherRegistration.Show();
            }),
            success: function (data) {
                $("#TeacherRegistrationSplitter_3_CC").html(data.ResultData);
                var winHeight = document.getElementById('TeacherRegistrationSplitter_3_CC').offsetHeight;


            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTeacherRegistration.Hide();
            btnPersonal.SetEnabled(true);
            btnPhotoload.SetEnabled(true);
            btnSignature.SetEnabled(false);
            btnExperties.SetEnabled(true);
            btnQualifications.SetEnabled(true);
            btnSubjectExpertise.SetEnabled(true);
            btnTeacherAchievements.SetEnabled(true);
            btnTeacherDocUpload.SetEnabled(true);
        });
    },

    ClickExpertiesDetail: function (s, e) {

        var mTeacherIDs = TeacherIDs.GetText();
        if (mTeacherIDs.toString() == "") {
            alert("Please Select Teacher");
            return;
        }


        $.ajax({
            url: "/TeacherRegistration/EditExpertiesDetailbyClickButton",
            type: "POST",
            data: { mTeacherID: mTeacherIDs },
            datatype:"json",
            beforeSend: (function (data) {
                loadingPanelTeacherRegistration.Show();
            }),
            success: function (data) {
                $("#TeacherRegistrationSplitter_3_CC").html(data.ResultData);
                var winHeight = document.getElementById('TeacherRegistrationSplitter_3_CC').offsetHeight;


            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTeacherRegistration.Hide();
            btnPersonal.SetEnabled(true);
            btnPhotoload.SetEnabled(true);
            btnTeacherDocUpload.SetEnabled(true);
            btnSignature.SetEnabled(true);
            btnExperties.SetEnabled(false);
            btnQualifications.SetEnabled(true);
            btnSubjectExpertise.SetEnabled(true);
            btnTeacherAchievements.SetEnabled(true);

        });
    },

    ClickQualificationsDetail: function (s, e) {

        var mTeacherIDs = TeacherIDs.GetText();
        if (mTeacherIDs.toString() == "") {
            alert("Please Select Teacher");
            return;
        }


        $.ajax({
            url: "/TeacherRegistration/EditQualificationsDetailbyClickButton",
            type: "POST",
            data: { mTeacherID: mTeacherIDs },
            datatype:"json",
            beforeSend: (function (data) {
                loadingPanelTeacherRegistration.Show();
            }),
            success: function (data) {
                $("#TeacherRegistrationSplitter_3_CC").html(data.ResultData);
                var winHeight = document.getElementById('TeacherRegistrationSplitter_3_CC').offsetHeight;
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTeacherRegistration.Hide();
            btnPersonal.SetEnabled(true);
            btnPhotoload.SetEnabled(true);
            btnSignature.SetEnabled(true);
            btnTeacherDocUpload.SetEnabled(true);
            btnExperties.SetEnabled(true);
            btnQualifications.SetEnabled(false);
            btnSubjectExpertise.SetEnabled(true);
            btnTeacherAchievements.SetEnabled(true);
        });
    },


    ClickSubjectExpertisDetail: function (s, e) {

        var mTeacherIDs = TeacherIDs.GetText();
        if (mTeacherIDs.toString() == "") {
            alert("Please Select Teacher");
            return;
        }


        $.ajax({
            url: "/TeacherRegistration/EditSubjectExpertisDetailbyClickButton",
            type: "POST",
            data: { mTeacherID: mTeacherIDs },
            datatype:"json",
            beforeSend: (function (data) {
                loadingPanelTeacherRegistration.Show();
            }),
            success: function (data) {
                $("#TeacherRegistrationSplitter_3_CC").html(data.ResultData);
                var winHeight = document.getElementById('TeacherRegistrationSplitter_3_CC').offsetHeight;


            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTeacherRegistration.Hide();
            btnPersonal.SetEnabled(true);
            btnPhotoload.SetEnabled(true);
            btnSignature.SetEnabled(true);
            btnExperties.SetEnabled(true);
            btnTeacherDocUpload.SetEnabled(true);
            btnQualifications.SetEnabled(true);
            btnSubjectExpertise.SetEnabled(false);
            btnTeacherAchievements.SetEnabled(true);
        });
    },


    ClickTeacherAchievementsDetail: function (s, e) {

        var mTeacherIDs = TeacherIDs.GetText();
        if (mTeacherIDs.toString() == "") {
            alert("Please Select Teacher");
            return;
        }


        $.ajax({
            url: "/TeacherRegistration/EditTeacherAchievementsDetailbyClickButton",
            type: "POST",
            data: { mTeacherID: mTeacherIDs },
            datatype:"json",
            beforeSend: (function (data) {
                loadingPanelTeacherRegistration.Show();
            }),
            success: function (data) {
                $("#TeacherRegistrationSplitter_3_CC").html(data.ResultData);
                var winHeight = document.getElementById('TeacherRegistrationSplitter_3_CC').offsetHeight;


            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTeacherRegistration.Hide();
            btnPersonal.SetEnabled(true);
            btnPhotoload.SetEnabled(true);
            btnSignature.SetEnabled(true);
            btnTeacherDocUpload.SetEnabled(true);
            btnExperties.SetEnabled(true);
            btnQualifications.SetEnabled(true);
            btnSubjectExpertise.SetEnabled(true);
            btnTeacherAchievements.SetEnabled(false);
        });
    },





    RefreshSignatureImg: function (s, e) {
        var regID;
        regID = TeacherID.GetText();


        $.ajax({
            url: "/TeacherRegistration/teacherGridRowChange",
            type: "POST",
            data: { RegID: regID },
            success: function (data) {
                $("#TeacherRegistrationSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    }


}
