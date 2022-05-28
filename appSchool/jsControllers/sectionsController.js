var sectionsController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'SectionsBody':
                GridSections.SetHeight(e.pane.GetClientHeight());
                break;
            //case 'ClassesFooter':
            //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
            //    break;
        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'SectionID', sectionsController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var mID;
        if (values != null) {
            mID = values;
        }
    $.ajax({
        url: "/Sections/SectionGridRowChange",
            type: "POST",
            data: { ID: mID },
            success: function (data) {
                $("#SectionsSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });
    }
}