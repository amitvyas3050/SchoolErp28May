var classSetupController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'ClassSetupBody':
                GridClassSetup.SetHeight(e.pane.GetClientHeight());
                break;
            //case 'ClassesFooter':
            //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
            //    break;
        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'ClassID', classSetupController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var regID;
        if (values != null) {
            regID = values;
        }
    $.ajax({
            url: "/Classes/ClassGridRowChange",
            type: "POST",
            data: {RegID:regID},
            success: function (data) {
                $("#ClassesSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    }
}