var teacherPortalController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'TeacherPortalBody':
                GridTeacherPortal.SetHeight(e.pane.GetClientHeight() - 10);
                break;
            case 'StudentPortalFooter':
               // GridStudentSession.SetHeight(e.pane.GetClientHeight()-10);
                break;

        }
    },
    SelectionChanged: function (s, e) {
        s.GetSelectedFieldValues("TeacherID", teacherPortalController.GetSelectedFieldValuesCallback);
    },

    GetSelectedFieldValuesCallback: function (values) {
        StudentIDs.SetText(values);
    },

    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            GridStudentPortal.SelectRows();
        }
        else {
            GridStudentPortal.UnselectRows();
        }

    },
    
    UpdateStudentSession: function (s, e) {
              
        var mStudentIDs = StudentIDs.GetValue();
        if (mStudentIDs == null) {
            alert("Please Select Teacher");
            return;
        }

        $.ajax({
            url: "/TeacherPortal/CreateTeacherLoginDetail",
            type: "POST",
            data: { pStudentIDs: mStudentIDs.toString() },
            beforeSend: (function (data) {
                loadingPanelTeacherPortal.Show();
            }),
            success: function (data) {
                $("#TeacherPortalSplitter_0_CC").html(data);
                var winHeight = document.getElementById('TeacherPortalSplitter_0_CC').offsetHeight;
                GridTeacherPortal.SetHeight(winHeight - 10);
                // cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTeacherPortal.Hide();

        });


    },

    UpdateTeacherSession: function (s, e) {

        var mStudentIDs = StudentIDs.GetValue();
        if (mStudentIDs == null) {
            alert("Please Select Teacher");
            return;
        }

        $.ajax({
            url: "/TeacherPortal/CreateTeacherLoginIDDetails",
            type: "POST",
            dataType: "json",
            data: { pStudentIDs: mStudentIDs.toString() },
            beforeSend: (function (data) {
                loadingPanelTeacherPortal.Show();
            }),
            success: function (data) {

                alert(data.ErrorMessage);

                $("#TeacherPortalSplitter_0_CC").html(data.ListData);
                var winHeight = document.getElementById('TeacherPortalSplitter_0_CC').offsetHeight;
                GridTeacherPortal.SetHeight(winHeight - 10);
                StudentIDs.SetText("");


                //$("#TeacherPortalSplitter_0_CC").html(data);
                //var winHeight = document.getElementById('TeacherPortalSplitter_0_CC').offsetHeight;
                //GridTeacherPortal.SetHeight(winHeight - 10);
                // cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTeacherPortal.Hide();

        });


    },
}