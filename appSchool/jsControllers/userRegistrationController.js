var ShowFilterRow = false;


var userRegistrationController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'UserRegistrationBody':
                GridUser.SetHeight(e.pane.GetClientHeight() - 10);
                break;
            case 'UserRegistrationFooter':
               // tabControl = tabStudentRecord;
                //if (tabControl != 'undefined') {
                   // tabStudentRecord.SetWidth(e.pane.GetClientWidth());
               // };
                break;
        }
    },
    AddNewClicked: function (s, e) {
        GridUser.AddNewRow();
        var winHeight = document.getElementById('UserRegistrationSplitter_1_CC').offsetHeight;
        GridUser.SetHeight(winHeight - 10);
    },
    EditClicked: function (s, e) {
        rowIdx = GridUser.GetFocusedRowIndex();
        GridUser.StartEditRow(rowIdx);
        var winHeight = document.getElementById('UserRegistrationSplitter_1_CC').offsetHeight;
        GridUser.SetHeight(winHeight - 10);
    },

    //DeleteClicked: function (s, e) {
    //    GridStudents.DeleteRow();
    //},


    FilterClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        $.ajax({
            url: "/UserCreation/RefreshRegistrationGrid",
            type: "POST",
            data: { mShowFilter: ShowFilterRow },
            success: function (data) {
                $("#UserRegistrationSplitter_1_CC").html(data);
                var winHeight = document.getElementById('UserRegistrationSplitter_1_CC').offsetHeight;
                GridUser.SetHeight(winHeight - 10);


            },
            error: function () {
            }
        });
       
    },
   
    
    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'UserID', userRegistrationController.RefreshTabsView);

       
    },

    RefreshTabsView: function (values) {
        var regID;
        if (values != null) {
            regID = values;
        }
    $.ajax({
        url: "/UserCreation/StudentGridRowChange",
            type: "POST",
            data: {RegID:regID},
            success: function (data) {
                $("#UserRegistrationSplitter_2_CC").html(data);
               
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    }
}