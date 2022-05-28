var housesController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'HousesBody':
                GridHouses.SetHeight(e.pane.GetClientHeight());
                break;
        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'HouseID', housesController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var mID;
        if (values != null) { mID = values;}
        $.ajax({
            url: "/Houses/HouseGridRowChange",
            type: "POST",
            data: { ID: mID },
            success: function (data) {
                $("#HousesSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });
    }
}