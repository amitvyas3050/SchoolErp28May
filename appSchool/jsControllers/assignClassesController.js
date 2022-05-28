var ShowFilterRow = false;


var assignClassesController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'RegistrationBody':
                GridStudents.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'RegistrationFooter':
                //tabControl = tabStudentRecord;
                //if (tabControl != 'undefined') {
                //    tabStudentRecord.SetWidth(e.pane.GetClientWidth());
                //};
                break;
        }
    },
    FillStudentGrid: function (s, e) {
        
        var sessionID = cboSession.GetValue();
        var classSetupID = cboClassSetup.GetValue();
        //alert(sessionID + "::" + classSetupID);
        //alert(selID);
        $.ajax({
            url: "/AssignClasses/FillStudentGrid",
            type: "POST",
            data: { mSessionID: sessionID, mClassSetupID: classSetupID },
            success: function (data) {
                //alert(date);
                //$("#RegistrationSplitter_1_CC").html(data);
            },
            error: function () {
                alert("Error");
            }
        });
    }
   
   
}