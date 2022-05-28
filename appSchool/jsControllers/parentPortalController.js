var parentPortalController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'ParentPortalBody':
                GridParentPortal.SetHeight(e.pane.GetClientHeight() - 10);
                break;
            case 'StudentPortalFooter':
               // GridStudentSession.SetHeight(e.pane.GetClientHeight()-10);
                break;

        }
    },
    SelectionChanged: function (s, e) {
        s.GetSelectedFieldValues("StudentID", parentPortalController.GetSelectedFieldValuesCallback);
    },

    GetSelectedFieldValuesCallback: function (values) {
        StudentIDs.SetText(values);
    },

    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            GridParentPortal.SelectRows();
        }
        else {
            GridParentPortal.UnselectRows();
        }

    },
    
   



    UpdateParenttInfo: function (s, e) {

        var mStudentIDs = StudentIDs.GetValue();
        if (mStudentIDs == null) {
            alert("Please Select Students");
            return;
        }

        $.ajax({
            url: "/ParentPortal/CreateParentLoginIDDetails",
            type: "POST",
            dataType: "json",
            data: { pStudentIDs: mStudentIDs.toString() },
            beforeSend: (function (data) {
                loadingPanelParentPortal.Show();
            }),
            success: function (data) {
                alert(data.ErrorMessage);

                $("#ParentPortalSplitter_0_CC").html(data.ListData);
                var winHeight = document.getElementById('ParentPortalSplitter_0_CC').offsetHeight;
                GridParentPortal.SetHeight(winHeight - 10);
                StudentIDs.SetText("");
              
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelParentPortal.Hide();

        });


    },

}