var shelfMasterController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'ShelfMasterBody':
                GridShelfMaster.SetHeight(e.pane.GetClientHeight() - 2);
                GridShelfMaster.SetWidth(e.pane.GetClientWidth() - 2);
                break;
                //case 'ClassesFooter':
                //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
                //    break;
        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'ShelfId', shelfMasterController.RefreshTabsView);
    },

    //RefreshTabsView: function (values) {
    //    var mID;
    //    if (values != null) { mID = values; }
    //    $.ajax({
    //        url: "/DepartmentMaster/DepartmentMasterGridRowChange",
    //        type: "POST",
    //        data: { ID: mID },
    //        success: function (data) {
    //            $("#DepartmentMasterSplitter_1_CC").html(data);
    //        },
    //        error: function () {
    //        }
    //    });
    //}
}





//var departmentMasterController = {

//    splitterResized: function (s, e) {
//        switch (e.pane.name) {
//            case 'DepartmentMasterBody':
//                GridDepartmentMaster.SetHeight(e.pane.GetClientHeight());
//                break;
//        }
//    },

//    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
//        s.GetRowValues(s.GetFocusedRowIndex(), 'DepartmentID', departmentMasterController.RefreshTabsView);
//    },

//    //RefreshTabsView: function (values) {
//    //    var mID;
//    //    if (values != null) { mID = values;}
//    //    $.ajax({
//    //        url: "/DepartmentMaster/DepartmentGridRowChange",
//    //        type: "POST",
//    //        data: { ID: mID },
//    //        success: function (data) {
//    //            $("#DepartmentMasterSplitter_1_CC").html(data);
//    //        },
//    //        error: function () {
//    //        }
//    //    });
//    //}
//}