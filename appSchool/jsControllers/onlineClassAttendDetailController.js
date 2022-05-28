var onlineClassAttendDetailController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'OnlineClassAttendDetailBody':
              //  GridStudentSession.SetHeight(e.pane.GetClientHeight());
                break;
            case 'OnlineClassAttendDetailFooter':
                GridOnlineClassAttendDetailView.SetHeight(e.pane.GetClientHeight() - 20);
                break;

            //case 'ClassesFooter':
            //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
            //    break;
        }
    },

    

    SelectionChanged: function (s, e) {
        s.GetSelectedFieldValues("ID", onlineClassAttendDetailController.GetSelectedFieldValuesCallback);
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

    GetOnlineClassAttendDetail: function (s, e) {

        var mClassSetupID = ClassSetupID.GetValue();
        var mAttendDate = AttendDate.GetDate().toDateString();
        var mIsAllClass = chkisallclass.GetChecked();
        
        $.ajax({
            url: "/OnlineClassAttendDetail/GetAOnlineClassAttendListView",
            type: "GET",
            data: { ClassSetupID: mClassSetupID, AttendDate: mAttendDate, IsAllClass: mIsAllClass },
            beforeSend: (function (data) {
                loadingPanelOnlineClassAttendDetail.Show();
            }),
            success: function (data) {
                $("#OnlineClassAttendDetailSplitter_1_CC").html(data);
                var winHeight = document.getElementById('OnlineClassAttendDetailSplitter_1_CC').offsetHeight;
                GridOnlineClassAttendDetailView.SetHeight(winHeight - 10);
                // cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelOnlineClassAttendDetail.Hide();

        });


    },


    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'StudentSessionID', rollNoAllotmentController.RefreshTabsView);
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