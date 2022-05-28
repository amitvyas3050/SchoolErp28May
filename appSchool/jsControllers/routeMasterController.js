var routeMasterController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'RouteMasterTop':
                GridRouteMaster.SetHeight(e.pane.GetClientHeight());
                break;
            case 'RouteMasterBottom':
                GridRouteDetail.SetWidth(e.pane.GetClientWidth());
                break;
        }
    },

    SubmitClick: function (s, e) {
        var mRouteID = RouteID.GetText();
        if (mRouteID == null) {
            alert("Please Select Route.");
            return;
        }

        $.ajax({
            url: "/RouteMaster/GetRouteDetailView",
            type: "POST",
            data: {pRouteID: mRouteID.toString() },
            beforeSend: (function (data) {

                LoadingPanelRouteMaster.Show();
            }),
            success: function (data) {
                $("#RouteMasterSplitter_2_CC").html(data);
                var winHeight = document.getElementById('RouteMasterSplitter_2_CC').offsetHeight;
                GridRouteDetail.SetHeight(winHeight-10);
               
            },
            error: function () {

            }
        }).done(function (data) {
            LoadingPanelRouteMaster.Hide();
        });
    },




    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'RouteID', routeMasterController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var nID;
        if (values != null)
        {
            nID = values;
        }
        $.ajax({
            url: "/RouteMaster/RouteMasterGridRowChange",
            type: "POST",
            data: { ID: nID },
            success: function (data) {
                $("#RouteDisplayID").html(data);

                data = null;
                $("#RouteMasterSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });
    }
}