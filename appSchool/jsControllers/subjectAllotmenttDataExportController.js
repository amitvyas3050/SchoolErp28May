var ShowFilterRow = false;


var subjectAllotmenttDataExportController = {

    splitterResized: function (s, e) {

        switch (e.pane.name) {
           
            case 'SubjectAllotmentDataExportHeader':
              
                break;
           
               
                case 'RightPanel':
                    GridSubjectAllotmentData.SetHeight(e.pane.GetClientHeight() - 1);
                    break;
            case 'LeftPanel':
                GridClass.SetHeight(e.pane.GetClientHeight() - 1);
                break;
        }
    },




    SelectionChangedForClasses: function (s, e) {
        s.GetSelectedFieldValues("ClassID", subjectAllotmenttDataExportController.GetSelectedFieldValuesCallbackForClasses);
       
    },
    GetSelectedFieldValuesCallbackForClasses: function (values) {
        txtclassSetup.SetText(values);
        // $("txtclassSetup").val(values);

    },

    ClickLoadGrid: function (s, e) {
        var classID = txtclassSetup.GetText();


        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/SubjectAllotmentDataExport/GetClassList",
            type: "POST",
            data: { mClassesID: classID.toString() },
            success: function (data) {
                $("#SubjectDataBottomSplitter_1_CC").html(data);
                var winHeight = document.getElementById('SubjectAllotmentDataBottomSplitter_1_CC').offsetHeight;

                GridClass.SetHeight(winHeight);

            },
            error: function () {
            }
        });



    },

    ClickSubjectInfo: function (s, e) {
        var classID = txtclassSetup.GetText();
        //alert(classID);

        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/SubjectAllotmentDataExport/GetSubjectInfoListClassWise",
            type: "POST",
            data: { mClassesID: classID.toString() },
            beforeSend: (function (data) {
                LoadingPanelSubjectAllotmentDataExport.Show();
            }),
            success: function (data) {
                $("#SubjectAllotmentDataExportSplitter_1i1_CC").html(data);
                var winHeight = document.getElementById('SubjectAllotmentDataExportSplitter_1i1_CC').offsetHeight;

                GridSubjectAllotmentData.SetHeight(winHeight - 10);
                cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelSubjectAllotmentDataExport.Hide();

        });



    },

    //ClickParentsInfo: function (s, e) {
    //    var classID = txtclassSetup.GetText();

    //    if (classID.toString() == "") {
    //        alert("Please Select Classes");
    //        return;
    //    }
    //    $.ajax({
    //        url: "/StudentDataExport/GetParentsInfoListClassWise",
    //        type: "POST",
    //        data: { mClassesID: classID.toString() },
    //        beforeSend: (function (data) {
    //            LoadingPanelStudentDataExport.Show();
    //        }),
    //        success: function (data) {
    //            $("#StudentDataBottomSplitter_1_CC").html(data);
    //            var winHeight = document.getElementById('StudentDataBottomSplitter_1_CC').offsetHeight;

    //            GridStudentData.SetHeight(winHeight - 10);
    //        },
    //        error: function () {
    //        }
    //    }).done(function (data) {
    //        LoadingPanelStudentDataExport.Hide();

    //    });



    //},

    //ClickGardianInfo: function (s, e) {
    //    var classID = txtclassSetup.GetText();


    //    if (classID.toString() == "") {
    //        alert("Please Select Classes");
    //        return;
    //    }
    //    $.ajax({
    //        url: "/StudentDataExport/GetGardianInfoListClassWise",
    //        type: "POST",
    //        data: { mClassesID: classID.toString() },
    //        beforeSend: (function (data) {
    //            LoadingPanelStudentDataExport.Show();
    //        }),
    //        success: function (data) {
    //            $("#StudentDataBottomSplitter_1_CC").html(data);
    //            var winHeight = document.getElementById('StudentDataBottomSplitter_1_CC').offsetHeight;

    //            GridStudentData.SetHeight(winHeight - 10);
    //        },
    //        error: function () {
    //        }
    //    }).done(function (data) {
    //        LoadingPanelStudentDataExport.Hide();

    //    });



    //},

    SelectAllSubject: function (s, e) {

        if (s.GetChecked()) {
            GridSubjectAllotmentData.SelectRows();

        }
        else {
            GridSubjectAllotmentData.UnselectRows();
        }

    },

        //---------------------------Student Photo--------------------------------------------
        SelectedClass: function (s, e) {
            var classID = s.GetValue();
            if (classID.toString() == "") {
                alert("Please Select Classes");
                return;
            }
            $.ajax({
                url: "/SubjectAllotmentReportExport/GetAllStudentPhotoView",
                type: "POST",
                dataType: "json",
                data: { mClassesID: classID.toString() },
                success: function (data) {

                    if (data.status = false) {

                        alert(data.errormsg);


                        $("#SbjectPhotoSplitter_1_CC").html(data.Listdata);
                        var winHeight = document.getElementById('SubjectPhotoSplitter_1_CC').offsetHeight;
                        GridStudentPhoto.SetHeight(winHeight - 10);

                        return;
                    }
                    else {
                  
                        $("#StudentPhotoSplitter_1_CC").html(data.Listdata);
                        var winHeight = document.getElementById('StudentPhotoSplitter_1_CC').offsetHeight;
                        GridStudentPhoto.SetHeight(winHeight - 10);

                    }




           


                },
                error: function () {
                }
            });

        },
        }
