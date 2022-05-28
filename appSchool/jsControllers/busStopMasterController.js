var busStopMasterController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'BusStopMasterBody':
                GridBusStopMaster.SetHeight(e.pane.GetClientHeight());
                break;
            //case 'ClassesFooter':
            //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
            //    break;
        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'StopID', busStopMasterController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var mID;
        if (values != null) { mID = values;}
        $.ajax({
            url: "/BusStopMaster/BusStopMasterGridRowChange",
            type: "POST",
            data: { ID: mID },
            success: function (data) {
                $("#BusStopMasterSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });
    }
}