var classCategoriesController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'ClassCatagoryBody':
                GridClassCategories.SetHeight(e.pane.GetClientHeight()-2);
                GridClassCategories.SetWidth(e.pane.GetClientWidth()-2);
                break;
            //case 'ClassesFooter':
            //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
            //    break;
        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'ClassCategoryID', classCategoriesController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var mID;
        if (values != null) { mID = values;}
        $.ajax({
            url: "/ClassCategories/ClassCatagoryGridRowChange",
            type: "POST",
            data: { ID: mID },
            success: function (data) {
                $("#ClassCategoriesSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });
    }
}