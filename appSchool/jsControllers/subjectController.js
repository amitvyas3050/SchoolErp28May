var subjectController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'SubjectBody':
                GridSubject.SetHeight(e.pane.GetClientHeight());
                break;
                //case 'SMSTemplateFooter':
                //tabClassRecord.SetWidth(e.pane.GetClientWidth());
                //break;
        }
    },
 RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'SubjectID', subjectController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var mID;
        if (values != null) {
            mID = values;
        }
    $.ajax({
        url: "/Subject/SubjectGridRowChange",
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