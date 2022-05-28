var ShowFilterRow = false;


var studentRegistrationWithTCController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'RegistrationBody':
                GridStudents.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'RegistrationFooter':
               
                //$("#pcClientSideAPI_CC").SetHeight(e.pane.GetClientHeight() - 100);
               // GridDocuments.SetHeight(e.pane.GetClientHeight() - 70);
             
                //tabControl = tabStudentRecord;
                //if (tabControl != 'undefined') {
                //    tabStudentRecord.SetWidth(e.pane.GetClientWidth());
                //};
                break;
        }
    },
    AddNewClicked: function (s, e) {
        GridStudents.AddNewRow();
    },
    EditClicked: function (s, e) {
        rowIdx = GridStudents.GetFocusedRowIndex();
        GridStudents.StartEditRow(rowIdx);
    },

    DeleteClicked: function (s, e) {
        alert("call delete");
        rowIdx = GridStudents.GetFocusedRowIndex();
        GridStudents.DeleteRow(rowIdx);
    },


    FilterClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        $.ajax({
            url: "/studentRegistrationWithTC/RefreshRegistrationGrid",
            type: "POST",
            data: { mShowFilter: ShowFilterRow },
            success: function (data) {
                $("#RegistrationSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });
       
    },
   
    
    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'StudentID', studentRegistrationWithTCController.RefreshTabsView);

       
    },

    RefreshTabsView: function (values) {
        var regID;
        if (values != null) {
            regID = values;
        }
    $.ajax({
            url: "/StudentRegistrationWithTC/StudentGridRowChange",
            type: "POST",
            data: {RegID:regID},
            success: function (data) {
                $("#RegistrationSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    },


    GetTCRegiterList: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        var SessionID = CmbSessionID.GetValue();
        if (SessionID.toString() == "") {
            alert("Please Select Session");
            return;
        }

        $.ajax({
            url: "/StudentRegistrationWithTC/GetStudentTCListSessionWise",
            type: "POST",
            data: { mShowFilter: ShowFilterRow, mSessionID: SessionID },

            beforeSend: (function (data) {
                loadingPanelMenu.Show();
            }),
            success: function (data) {
                $("#divstudregistrationwithtc").html(data);
                
                //$("#RegistrationSplitter_1_CC").html(data);
                //var winHeight = document.getElementById('RegistrationSplitter_1_CC').offsetHeight;
                //GridStudents.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelMenu.Hide();
        });
    },

    RefreshDocumentList: function (s,e) {
        var regID;
        regID=StudentID.GetText();
      

        $.ajax({
            url: "/StudentRegistrationWithTC/StudentGridRowChange",
            type: "POST",
            data: {RegID:regID},
            success: function (data) {
                $("#divstudregistrationwithtc").html(data);

                //$("#RegistrationSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    }

}


