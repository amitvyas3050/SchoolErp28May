var ShowFilterRow = false;


var applicationImageController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'ApplicationImageBody':
               // GridApplicationImage.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'ApplicationImageFooter':
                //tabControl = tabStudentRecord;
                //if (tabControl != 'undefined') {
                //    tabStudentRecord.SetWidth(e.pane.GetClientWidth());
                //};
                break;
        }
    },
    //AddNewClicked: function (s, e) {
    //    GridStudents.AddNewRow();
    //},
    //EditClicked: function (s, e) {
    //    rowIdx = GridStudents.GetFocusedRowIndex();
    //    GridStudents.StartEditRow(rowIdx);
    //},
    FilterClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        alert("refresh");
        $.ajax({
            url: "/ApplicationImage/RefreshRegistrationGrid",
            type: "POST",
            data: { mShowFilter: ShowFilterRow },
            success: function (data) {
                $("#ApplicationImageSplitter_0_CC").html(data);
            },
            error: function () {
            }
        });
       
    },
    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        alert("select");
        s.GetRowValues(s.GetFocusedRowIndex(), 'StudentID', applicationImageController.RefreshTabsView);
    },
   
    RefreshTabsView: function (values) {
        var regID;
        if (values != null) {
            regID = values;
        }
        alert(regID);
        $.ajax({
          
            url: "/ApplicationImage/ApplicationImageGridRowChange",
            type: "POST",
            data: {RegID:regID},
            success: function (data) {
                $("#ApplicationImageSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    }
}