var ShowFilterRow = false;


var attendanceController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'AttendanceBody':
                GridStudents.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'AttendanceFooter':
                //tabControl = tabStudentRecord;
                //if (tabControl != 'undefined') {
                //    tabStudentRecord.SetWidth(e.pane.GetClientWidth());
                //};
                break;
        }
    },
    AddNewClicked: function (s, e) {
        GridAttendance.AddNewRow();
    },
    EditClicked: function (s, e) {
        rowIdx = GridAttendance.GetFocusedRowIndex();
        GridAttendance.StartEditRow(rowIdx);
    },
    FilterClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        $.ajax({
            url: "/AttendanceManager/RefreshAttendanceGrid",
            type: "POST",
            data: { mShowFilter: ShowFilterRow },
            success: function (data) {
                $("#AttendanceSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });
       
    },
  

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        //s.GetRowValues(s.GetFocusedRowIndex(), 'StudentID', attendanceController.RefreshTabsView);
        s.GetRowValues(s.GetFocusedRowIndex(), 'ClassSetupID', attendanceController.RefreshTabsView);

    },

    RefreshTabsView: function (values) {
        var regID;
        if (values != null) {
            regID = values;
            //alert(regID);

        }
    $.ajax({
            url: "/AttendanceManager/AttendanceGridRowChange",
            type: "POST",
            data: {RegID:regID},
            success: function (data) {
                $("#AttendanceSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    }
}