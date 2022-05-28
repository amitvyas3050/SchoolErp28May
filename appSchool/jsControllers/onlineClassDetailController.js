var onlineClassDetailController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'OnlineClassDetailBody':
              //  GridStudentSession.SetHeight(e.pane.GetClientHeight());
                break;
            case 'OnlineClassDetailFooter':
                GridOnlineClassDetailView.SetHeight(e.pane.GetClientHeight() - 20);
                break;

            //case 'ClassesFooter':
            //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
            //    break;
        }
    },

    

    SelectionChanged: function (s, e) {
        s.GetSelectedFieldValues("ID", onlineClassDetailController.GetSelectedFieldValuesCallback);
    },
    GetSelectedFieldValuesCallback: function (values) {
        //StudentIDs.SetText(values);
    },

    SelectAllStudent: function (s, e) {

        //if (s.GetChecked()) {
        //    GridRollNoAllotment.SelectRows();
        //}
        //else {
        //    GridRollNoAllotment.UnselectRows();
        //}

    },

    GetOnlineClassDetail: function (s, e) {

        var mClassSetupID = ClassSetupID.GetValue();
        var mAttendDate = AttendDate.GetDate().toDateString();
        var mIsAllClass = chkisallclass.GetChecked();
        
        $.ajax({
            url: "/OnlineClassDetail/GetOnlineClassListView",
            type: "GET",
            data: { ClassSetupID: mClassSetupID, AttendDate: mAttendDate, IsAllClass : mIsAllClass},
            beforeSend: (function (data) {
                loadingPanelOnlineClassDetail.Show();
            }),
            success: function (data) {
                $("#OnlineClassDetailSplitter_1_CC").html(data);
                var winHeight = document.getElementById('OnlineClassDetailSplitter_1_CC').offsetHeight;
                GridOnlineClassDetailView.SetHeight(winHeight - 10);
                // cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelOnlineClassDetail.Hide();

        });


    },


    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'StudentSessionID', onlineClassDetailController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var nID;
        if (values != null)
        {
            nID = values;
        }
        $.ajax({
            url: "/RollNoAllotment/RollNoGridRowChange",
            type: "POST",
            data: { ID: nID },
            success: function (data) {
                $("#RollNoAllotmentSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });
    }
}