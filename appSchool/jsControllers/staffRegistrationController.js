var ShowFilterRow = false;


var staffRegistrationController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'StaffRegistrationBody':
                var tt = $('#staffRegistrationSplitter_0_CC').width();
                GridStaffs.SetHeight(e.pane.GetClientHeight());
                GridStaffs.SetWidth(tt-10);

                break;
            case 'StaffRegistrationFooter':

                //GridTeacherExprtiseData.SetHeight(e.pane.GetClientHeight() - 70);
                //GridTeacherQualificationData.SetHeight(e.pane.GetClientHeight() - 70);
               // GridSubjectExprtiseData.SetHeight(e.pane.GetClientHeight() - 70);
                //GridTeacherAchivmentData.SetHeight(e.pane.GetClientHeight() - 70);
                //tabControl = tabStudentRecord;
                //if (tabControl != 'undefined') {
                //    tabStudentRecord.SetWidth(e.pane.GetClientWidth());
                //};
                break;
        }
    },
    AddNewClicked: function (s, e) {
        GridStaffs.AddNewRow();
    },
    EditClicked: function (s, e) {
        rowIdx = GridStaffs.GetFocusedRowIndex();
        GridStaffs.StartEditRow(rowIdx);
    },
    FilterClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        $.ajax({
            url: "/StaffRegistration/RefreshStaffRegistrationGrid",
            type: "POST",
            data: { mShowFilter: ShowFilterRow },
            success: function (data) {
                $("#StaffRegistrationSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });
       
    },
    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
      
        s.GetRowValues(s.GetFocusedRowIndex(), 'TeacherID', staffRegistrationController.RefreshTabsView);
        
    },

   
    RefreshTabsView: function (values) {
        var regID;
        if (values != null) {
            regID = values;
        }
     
    $.ajax({
            url: "/StaffRegistration/staffGridRowChange",
            type: "POST",
            data: {RegID:regID},
            success: function (data) {
                $("#StaffRegistrationSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    }
}