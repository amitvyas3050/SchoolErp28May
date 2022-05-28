var employeeshiftmasterController = {

    splitterResized: function (s, e) {
        //alert(e.pane.name);
        switch (e.pane.name) {
            case 'EmployeeAttenDanceFooter':
                GridEmployeeBiometric.SetHeight(e.pane.GetClientHeight());
                break;
            case 'EmployeeAttenDanceBottom':
                GridEmployeeAttendance.SetHeight(e.pane.GetClientHeight());
                break;

        }
    },


    UpdateShiftTime: function (s, e) {

        var mTime = ShiftTime.GetText();
        if (mTime == null || mTime == '') {
            alert("Please Select Time ");
            return;
        }
        //var fulldate = date.toDateString();

        $.ajax({

            url: "/EmployeeShiftMaster/UpdateEmployeeShiftTime",
            type: "POST",
            data: { mShiftTime: mTime.toString() },
            beforeSend: (function (data) {
                LoadingPanelEmployeeAttendance.Show();
            }),
            success: function (data) {
                //  ClassSetupID.SetText(data);

              alert("Update Successfully")
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelEmployeeAttendance.Hide();

        });
    },



   
   
  



 
}