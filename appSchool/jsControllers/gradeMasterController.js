var gradeMasterController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'GradeMasterBody':
                GridGradeMaster.SetHeight(e.pane.GetClientHeight());
                break;
            //case 'ClassesFooter':
            //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
            //    break;
        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'GradeID', gradeMasterController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var mID;
        if (values != null)
        {
            mID = values;
        }
        $.ajax({
            url: "/GradeMaster/GradeMasterGridRowChange",
            type: "POST",
            data: {ID: mID },
            success: function (data) {
                $("#GradeMasterSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });
    }
}