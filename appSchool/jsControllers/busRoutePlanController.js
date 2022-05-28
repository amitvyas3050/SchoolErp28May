var busRoutePlanController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'BusRoutePlanBody':
                GridBusRoutePlan.SetHeight(e.pane.GetClientHeight());
                break;
                //case 'BusRoutePlanFooter':
            //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
            //    break;
        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'RoutePlanID', busRoutePlanController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var mID;
        if (values != null) { mID = values;}
        $.ajax({
            url: "/BusRoutePlan/BusRoutePlanGridRowChange",
            type: "POST",
            data: { ID: mID },
            success: function (data) {
                $("#BusRoutePlanSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });
    }
}