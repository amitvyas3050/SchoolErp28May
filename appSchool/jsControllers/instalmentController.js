var instalmentController = {

    splitterResized: function (s, e) {
        
        switch (e.pane.name) {
            case 'InstalmentMasterBody':
                var tt = $('#InstalmentMasterSplitter_0_CC').width();
                GridinstalmentMaster.SetHeight(e.pane.GetClientHeight());
                GridinstalmentMaster.SetWidth(tt);

                break;
            //case 'ClassesFooter':
            //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
            //    break;
        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'InstalmentID', instalmentController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var regID;
        if (values != null) {
            regID = values;
        }
    //$.ajax({
    //    url: "/FeesManager/FeesGridRowChange",
    //        type: "POST",
    //        data: {RegID:regID},
    //        success: function (data) {
    //            $("#ClassesSplitter_1_CC").html(data);
    //        },
    //        error: function () {
    //        }
    //    });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    }
}