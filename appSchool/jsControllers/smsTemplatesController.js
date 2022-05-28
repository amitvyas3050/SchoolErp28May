

var smsTemplatesController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'SMSTemplateBody':

                //var tt = $('#SMSTemplateSplitter_0_CC').width();
                //GridSMSTemplate.SetHeight(e.pane.GetClientHeight() - 2);
                //GridSMSTemplate.SetWidth(tt);

                break;
            case 'SMSTemplateFooter':
                    var tt = $('#SMSTemplateSplitter_0_CC').width();
                    GridSMSTemplate.SetHeight(e.pane.GetClientHeight() - 2);
                    GridSMSTemplate.SetWidth(tt);
                    break;
        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'TemplateID', smsTemplatesController.RefreshTabsView);
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








//var ShowFilterRow = false;
//var smsTemplatesController = {

//    splitterResized: function (s, e) {
//        switch (e.pane.name) {
//            case 'pnlTemplateBody':
//                GridSMSTemplate.SetHeight(e.pane.GetClientHeight() - 1);
//                break;
//        }
//    },
//    AddNewClicked: function (s, e) {
//        GridSMSTemplate.OpenAddEditFormView(-1);
//    },
//    EditClicked: function (s, e) {
//        rowIdx = GridStudents.GetFocusedRowIndex();
//        GridStudents.StartEditRow(rowIdx);
//    },
//    FilterClicked: function (s, e) {
//        ShowFilterRow = !ShowFilterRow;
//        $.ajax({
//            url: "/SMSTemplate/RefreshRegistrationGrid",
//            type: "POST",
//            data: { mShowFilter: ShowFilterRow },
//            success: function (data) {
//                $("#RegistrationSplitter_1_CC").html(data);
//            },
//            error: function () {
//            }
//        });
       
//    },
//    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
//        s.GetRowValues(s.GetFocusedRowIndex(), 'StudentID', GridSMSTemplate.OpenAddEditFormView);
//    },

//    OpenAddEditFormView: function (values) {
//        var regID;
//        if (values != null) {
//            regID = values;
//        }
//        $.ajax({
//            url: "/SMSTemplate/SMSTemplateGridRowChange",
//            type: "POST",
//            data: {RegID:regID},
//            success: function (data) {
//                $("#TemplateSplitter_2_CC").html(data);
//            },
//            error: function () {
//            }
//        });
       
//    }
//}