var houseAllotmentController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'HouseBody':
                break;
            case 'HouseFooter':
                GridHouseAllotment.SetHeight(e.pane.GetClientHeight()-20);
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
            url: "/HouseAllotment/GetAllStudentListView",
            type: "POST",
            data: { mClassesID: classID.toString() },
            beforeSend: (function (data) {
                loadingPanelMenu.Show();
            }),
            success: function (data) {
                $("#divhouseallotment").html(data);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelMenu.Hide();

        });

    },

    SelectionChanged: function (s, e) {
        s.GetSelectedFieldValues("StudentSessionID", houseAllotmentController.GetSelectedFieldValuesCallback);
    },
    GetSelectedFieldValuesCallback: function (values) {
        StudentIDs.SetText(values);
    },

    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            GridHouseAllotment.SelectRows();
        }
        else {
            GridHouseAllotment.UnselectRows();
        }

    },

    UpdateStudentHouse: function (s, e) {
        var mHouseID = HouseID.GetValue();
        if (mHouseID == null) {
            mHouseID = 0;
        }
        var mStudentIDs = StudentIDs.GetText();
        if (mStudentIDs == null) {
            alert("Please Select Students");
            return;
        }

        var mClassID = ClassID.GetValue();
        if (mClassID == null) {
            mClassID = 0;
        }

        $.ajax({
            url: "/HouseAllotment/UpdateStudentHouseSelectAll",
            type: "POST",
            data: { pStudentIDs:mStudentIDs.toString(), pHouseID: mHouseID, pClassID:mClassID },
            beforeSend: (function (data) {
                loadingPanelMenu.Show();
            }),
            success: function (data) {
                $("#divhouseallotment").html(data);
              },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelMenu.Hide();

        });


    },


    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'StudentSessionID', houseAllotmentController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var nID;
        if (values != null)
        {
            nID = values;
        }
        $.ajax({
            url: "/HouseAllotment/HouseGridRowChange",
            type: "POST",
            data: { ID: nID },
            success: function (data) {
                $("#divhouseallotment").html(data);
            },
            error: function () {
            }
        });
    }
}