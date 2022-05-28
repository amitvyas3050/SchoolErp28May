var ShowFilterRow = false;


var ownerMasterController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'OwnerMasterBody':
                GridOwnerMaster.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'OwnerMasterFooter':
                //tabControl = tabStudentRecord;
                //if (tabControl != 'undefined') {
                //    tabStudentRecord.SetWidth(e.pane.GetClientWidth());
                //};
                break;
        }
    },
    AddNewClicked: function (s, e) {
        GridOwnerMaster.AddNewRow();
    },
    EditClicked: function (s, e) {

        rowIdx = GridOwnerMaster.GetFocusedRowIndex();
       
        GridOwnerMaster.StartEditRow(rowIdx);
    },

    DeleteClicked: function (s, e) {
        rowIdx = GridOwnerMaster.GetFocusedRowIndex();
     
        GridOwnerMaster.StartDeleteRow(rowIdx);
    },
    FilterClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        $.ajax({
            url: "/OwnerMaster/RefreshOwnerMasterGrid",
            type: "POST",
            data: { mShowFilter: ShowFilterRow },
            success: function (data) {
                $("#OwnerMasterSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });
       
    },
    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'OwnerID', ownerMasterController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var regID;
        if (values != null) {
            regID = values;
        }
    $.ajax({
            url: "/OwnerMaster/OwnerMasterGridRowChange",
            type: "POST",
            data: {RegID:regID},
            success: function (data) {
                $("#OwnerMasterSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    }
}