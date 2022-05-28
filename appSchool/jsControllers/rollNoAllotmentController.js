var rollNoAllotmentController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'RollNoBody':
                break;
            case 'RollNoFooter':
                GridRollNoAllotment.SetHeight(e.pane.GetClientHeight()-20);
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
            url: "/RollNoAllotment/GetAllStudentListView",
            type: "POST",
            data: { mClassesID: classID.toString() },
            beforeSend: (function (data) {
                loadingPanelMenu.Show();
            }),
            success: function (data) {
                $("#divrollnoallotment").html(data);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelMenu.Hide();

        });

    },

    SelectionChanged: function (s, e) {
        s.GetSelectedFieldValues("StudentSessionID", rollNoAllotmentController.GetSelectedFieldValuesCallback);
    },
    GetSelectedFieldValuesCallback: function (values) {
        StudentIDs.SetText(values);
    },

    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            GridRollNoAllotment.SelectRows();
        }
        else {
            GridRollNoAllotment.UnselectRows();
        }

    },

    UpdateStudentRollNo: function (s, e) {
        var mRollNo = $("#txtRollNo_I").val();
        if (mRollNo =="") {
            mRollNo = 0;
            alert("Please enter Initial RollNo!");
            return;
        }
        var mClassID = ClassID.GetValue();
        if (mClassID == null) {
            mClassID = 0;
            alert("Please Select Class.");
            return;
        }

        $.ajax({
            url: "/RollNoAllotment/UpdateStudentRollNoSelectAll", 
            type: "POST",
            data: { argRollNoID: mRollNo.toString(), argClassID: mClassID.toString() },
            beforeSend: (function (data) {
                loadingPanelStudentRegistration.Show();
            }),
            success: function (data) {
                $("#divrollnoallotment").html(data);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentRegistration.Hide();

        });


    },


    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'StudentSessionID', rollNoAllotmentController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var nID;
        if (values != null)
        {
            nID = values;
        }
        $.ajax({
            url: "/RollNoAllotment/RollNoGridRowChange",
            type: "POST",
            data: { ID: nID },
            success: function (data) {
                $("#RollNoAllotmentSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });
    }
}