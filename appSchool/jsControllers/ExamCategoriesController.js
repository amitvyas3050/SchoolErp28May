var examCategoriesController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'ExamCatagoryBody':
              
                var tt = $('#ExamCatagorySplitter_0_CC').width();
                GridExamCategory.SetHeight(e.pane.GetClientHeight() - 2);
                GridExamCategory.SetWidth(tt);

                break;
            //case 'ExancategoriesFooter':
            //    tabExamCategoryRecord.SetWidth(e.pane.GetClientWidth());
            //    break;
        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'ExamID', examCategoriesController.RefreshTabsView);
    },

    //RefreshTabsView: function (values) {
    //    var mID;
    //    if (values != null) { mID = values;}
    //    $.ajax({
    //        url: "/ExamCategories/ExamCatagoryGridRowChange",
    //        type: "POST",
    //        data: { ID: mID },
    //        success: function (data) {
    //            $("#ExamCategoriesSplitter_1_CC").html(data);
    //        },
    //        error: function () {
    //        }
    //    });
    //}
}