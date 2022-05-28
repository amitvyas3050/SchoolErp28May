var studentPortalController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'StudentPortalBody':
                GridStudentPortal.SetHeight(e.pane.GetClientHeight() - 10);
                break;
            case 'StudentPortalFooter':
               // GridStudentSession.SetHeight(e.pane.GetClientHeight()-10);
                break;

        }
    },
    SelectionChanged: function (s, e) {
     
        s.GetSelectedFieldValues("StudentID", studentPortalController.GetSelectedFieldValuesCallback);
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
            alert("Please Select Students");
            return;
        }

        $.ajax({
            url: "/StudentPortal/CreateStudentLoginDetail",
            type: "POST",
            data: { pStudentIDs: mStudentIDs.toString() },
            beforeSend: (function (data) {
                loadingPanelStudentPortal.Show();
            }),
            success: function (data) {
                $("#StudentPortalSplitter_0_CC").html(data);
                var winHeight = document.getElementById('StudentPortalSplitter_0_CC').offsetHeight;
                GridStudentPortal.SetHeight(winHeight - 10);
                // cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentPortal.Hide();

        });


    },



    UpdateStudentInfo: function (s, e) {

        var mStudentIDs = StudentIDs.GetValue();
        if (mStudentIDs == null) {
            alert("Please Select Students");
            return;
        }

        $.ajax({
            url: "/StudentPortal/CreateStudentLoginIDDetails",
            type: "POST",
            dataType: "json",
            data: { pStudentIDs: mStudentIDs.toString() },
            beforeSend: (function (data) {
                loadingPanelStudentPortal.Show();
            }),
            success: function (data) {
                alert(data.ErrorMessage);

                $("#StudentPortalSplitter_0_CC").html(data.ListData);
                var winHeight = document.getElementById('StudentPortalSplitter_0_CC').offsetHeight;
                GridStudentPortal.SetHeight(winHeight - 10);
                StudentIDs.SetText("");
              
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentPortal.Hide();

        });


    },

}