var studentListfeesStructureController = {

    splitterResized: function (s, e) {
       
        switch (e.pane.name) {
            case 'ListFeesStructureFooter':
                GridFeetermList.SetHeight(e.pane.GetClientHeight() - 60);
                break;

            case 'SplitterStudentListFeesStructureBody':
                GridStudentList.SetHeight(e.pane.GetClientHeight() - 60);
                break;

            case 'ListFeesStructureFooter1':
                GridFeeStructureEdit.SetHeight(e.pane.GetClientHeight() - 10);
                break;


        }
    },

     
    RowSelectionChangeforGridListFeesStructure: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'StudentID', studentListfeesStructureController.RefreshTabsViewForGridStudentList);
    },
    RefreshTabsViewForGridStudentList: function (values) {
        var mStudentID;

        if (values != null) {
            mStudentID = values;
        }
        $.ajax({
            url: "/StudentListFeesStructure/GetFeesTermForListEdit",
                type: "POST",
                data: { newStudentID: mStudentID },
                success: function (data) {
                    $("#SplitterStudentListFeesStructure_1_CC").html(data);
                },
                error: function () {
                }
            });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    },

    
    
    RowSelectionChangeforHeadList: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'StudmasterID', studentListfeesStructureController.RefreshTabsViewForHeadList);
    },

    RefreshTabsViewForHeadList: function (values) {
    var mStudMasterID;
  
    if (values != null) {
        mStudMasterID = values;
          
    }
    $.ajax({
        url: "/StudentListFeesStructure/GetFeesHeadForListEdit",
        type: "POST",
        data: { newStudMasterID: mStudMasterID },
        success: function (data) {
            $("#StudentListFeesStructureSplitter_1_CC").html(data);
        },
        error: function () {
        }
    });
  
}



}