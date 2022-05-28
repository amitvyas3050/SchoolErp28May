var onlinePortalLoginDetailController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'OnlinePortalLoginDetailBody':
              //  GridStudentSession.SetHeight(e.pane.GetClientHeight());
                break;
            case 'OnlinePortalLoginDetailFooter':
                GridOnlinePortalLoginDetail.SetHeight(e.pane.GetClientHeight() - 20);
                break;

            //case 'ClassesFooter':
            //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
            //    break;
        }
    },

    

    SelectionChanged: function (s, e) {
        s.GetSelectedFieldValues("ID", onlinePortalLoginDetailController.GetSelectedFieldValuesCallback);
    },
    GetSelectedFieldValuesCallback: function (values) {
        //StudentIDs.SetText(values);
    },
    
    GetOnlinePortalLoginDetail: function (s, e) {

        var mLoginTypeID = AndroidLoginType.GetValue();
        var mLoginDate =  LoginDate.GetDate().toDateString();
        //var mIsAllDate = chkisalldate.GetChecked();
        
        $.ajax({
            url: "/OnlinePortalLoginDetail/GetOnlinePortalLoginDetailListView",
            type: "GET",
            data: { LoginTypeID: mLoginTypeID, LoginDate: mLoginDate },
            beforeSend: (function (data) {
                loadingPanelOnlinePortalLoginDetail.Show();
            }),
            success: function (data) {
                $("#OnlinePortalLoginDetailSplitter_1_CC").html(data);
                var winHeight = document.getElementById('OnlinePortalLoginDetailSplitter_1_CC').offsetHeight;
                GridOnlinePortalLoginDetail.SetHeight(winHeight - 10);
                // cbSelectAll.SetChecked(false);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelOnlinePortalLoginDetail.Hide();

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