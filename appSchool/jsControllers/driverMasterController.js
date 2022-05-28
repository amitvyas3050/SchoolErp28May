var driverMasterController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'DriverMasterBody':
                GridDriverMaster.SetHeight(e.pane.GetClientHeight());
                break;
                //case 'BusRoutePlanFooter':
            //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
            //    break;
        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'DriverId', driverMasterController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var mID;
        if (values != null) { mID = values;}
        $.ajax({
            url: "/DriverMaster/DriverMasterGridRowChange",
            type: "POST",
            data: { ID: mID },
            success: function (data) {
                $("#DriverMasterSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });
    }
}