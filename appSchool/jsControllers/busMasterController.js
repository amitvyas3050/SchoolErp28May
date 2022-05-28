var ShowFilterRow = false;


var busMasterController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'BusMasterBody':
                GridBusMaster.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'BusMasterFooter':
                //tabControl = tabStudentRecord;
                //if (tabControl != 'undefined') {
                //    tabStudentRecord.SetWidth(e.pane.GetClientWidth());
                //};
                break;
        }
    },
    AddNewClicked: function (s, e) {
        GridBusMaster.AddNewRow();
    },
    EditClicked: function (s, e) {
        rowIdx = GridBusMaster.GetFocusedRowIndex();
        GridBusMaster.StartEditRow(rowIdx);
    },

    DeleteClicked: function (s, e) {
        rowIdx = GridBusMaster.GetFocusedRowIndex();
        GridBusMaster.StartDeleteRow(rowIdx);
    },
    FilterClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        $.ajax({
            url: "/BusMaster/RefreshBusMasterGrid",
            type: "POST",
            data: { mShowFilter: ShowFilterRow },
            success: function (data) {
                $("#BusMasterSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });
       
    },
    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'BusID', busMasterController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var regID;
        if (values != null) {
            regID = values;
        }
    $.ajax({
            url: "/BusMaster/BusMasterGridRowChange",
            type: "POST",
            data: {RegID:regID},
            success: function (data) {
                $("#BusMasterSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    }
}