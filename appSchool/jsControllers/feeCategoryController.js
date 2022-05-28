var feeCategoryController = {

    splitterResized: function (s, e) {
        
        switch (e.pane.name) {
            case 'FeeCategoryBody':
                
               var tt = $('#FeeCategorySplitter_0_CC').width();
               GridFeeCategory.SetHeight(e.pane.GetClientHeight() - 2);
               GridFeeCategory.SetWidth(tt);
                break;
            //case 'ClassesFooter':
            //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
            //    break;
        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'CategoryID', feeCategoryController.RefreshTabsView);
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