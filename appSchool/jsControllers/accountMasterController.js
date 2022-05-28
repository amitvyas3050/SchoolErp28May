var ShowFilterRow = false;


var accountMasterController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'AccountMasterBody':

                var tt = $("#AccountMasterSplitter_1_CC").width();
                GridAccountMaster.SetHeight(e.pane.GetClientHeight() - 1);
                GridAccountMaster.SetWidth(tt);
                break;
            case 'AccountMasterFooter':
                //tabControl = tabStudentRecord;
                //if (tabControl != 'undefined') {
                //    tabStudentRecord.SetWidth(e.pane.GetClientWidth());
                //};
                break;
        }
    },
    AddNewClicked: function (s, e) {
        GridAccountMaster.AddNewRow();
    },
    EditClicked: function (s, e) {
        rowIdx = GridAccountMaster.GetFocusedRowIndex();
        GridAccountMaster.StartEditRow(rowIdx);
    },

    DeleteClicked: function (s, e) {
        rowIdx = GridAccountMaster.GetFocusedRowIndex();
        GridAccountMaster.StartDeleteRow(rowIdx);
    },
    FilterClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        $.ajax({
            url: "/AccountMaster/RefreshAccountMasterGrid",
            type: "POST",
            data: { mShowFilter: ShowFilterRow },
            success: function (data) {
                $("#AccountMasterSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });

    },
    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'AcCode', accountMasterController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var regID;
        if (values != null) {
            regID = values;
        }
        $.ajax({
            url: "/AccountMaster/AccountMasterGridRowChange",
            type: "POST",
            data: { RegID: regID },
            success: function (data) {
                $("#AccountMasterSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });
    
    }
}