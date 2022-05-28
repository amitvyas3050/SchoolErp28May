var designationMasterController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'DesignationMasterBody':
                GridDesignationMaster.SetHeight(e.pane.GetClientHeight());
                break;
            //case 'ClassesFooter':
            //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
            //    break;
        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'DesignationID', designationMasterController.RefreshTabsView);
    },

    //RefreshTabsView: function (values) {
    //    var mID;
    //    if (values != null) { mID = values;}
    //    $.ajax({
    //        url: "/ClassCategories/ClassCatagoryGridRowChange",
    //        type: "POST",
    //        data: { ID: mID },
    //        success: function (data) {
    //            $("#ClassCategoriesSplitter_1_CC").html(data);
    //        },
    //        error: function () {
    //        }
    //    });
    //}
}