var topperListController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'TopperListBody':
                GridTopperList.SetHeight(e.pane.GetClientHeight() - 10);
                break;
            case 'StudentListRemarkFooter':
                GridTopperList.SetHeight(e.pane.GetClientHeight() - 10);
                break;
        }
    },

    

   
   
    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'StudentID', topperNoticeBoardController.RefreshTabsView);
    },


  

    
    SelectYear: function (s, e) {
        var mYearID = s.GetValue();
        if (mYearID == null) {
            alert("Select Year");
            return;
        }

        $.ajax({
            url: "/TopperNoticeBoard/GetSemList",
            datatype: "json",
            type: "POST",
            data:{PYearID:mYearID },
            beforeSend: function (data) { loadingPanelTopper.Show(); },
            success: function (data) {
                if (data.Status) {
                    alert(data.ErrorMsg);
                }
                else {
                    var aa = JSON.parse(data.DataList);
                    SemID.ClearItems();
                    for (var j = 0; j < aa.length; j++) {
                        SemID.AddItem(aa[j].SemName, aa[j].SemID);
                    }
                }
            },
            error: function (data) {

            }

        }).done(function (data) { loadingPanelTopper.Hide(); });


    }


   

}