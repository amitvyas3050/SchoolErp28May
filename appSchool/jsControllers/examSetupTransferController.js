var examSetupTransferController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'NewsEventMasterBody':
                GridNewsEventMaster.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'EventListFooter':
                
                break;
        }
    },

    
    

   

    



    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'ExamSetupID', examSetupTransferController.ExamSetUpView);
        s.GetRowValues(s.GetFocusedRowIndex(), 'ClassID', examSetupTransferController.ExamSetUpViewForClassID);
        s.GetRowValues(s.GetFocusedRowIndex(), 'ExamID', examSetupTransferController.ExamSetUpViewForExamID);
    },
    TransferExamSetUp: function (s, e) {
        var mExamSetupID = EXamSetUpID.GetValue();
        if (mExamSetupID == null) {
            alert("Please Select Exam");
            return;
        }
        var classID = ClassID.GetValue();
        if (classID == null) {
            alert("Please Select Class");
            return;
        }
        var examID = ExamID.GetValue();
        if (examID == null) {
            alert("Please Select Class");
            return;
        }

        $.ajax({
            url: "/ExamSetupTransfer/ExamSetUpTransferbyClassID",
            type: "POST",
            datatype: "json",
            data: { pExamSetUpID: mExamSetupID, pClassID: classID, pExamID:examID },
            success: function (data) {

                
                alert(data.ErrorMessage);
                $("#NewsEventMasterSplitter_1_CC").html(data.ListData);
                var winHeight = document.getElementById('NewsEventMasterSplitter_1_CC').offsetHeight;
                GridNewsEventMaster.SetHeight(winHeight - 10);
                ClassID.SetText("");
                EXamSetUpID.SetText("");

            },
            error: function () {

            }

        });
    },



    ExamSetUpView: function (values) {

        if (values != null) {
            EXamSetUpID.SetText(values);
        }

    },

    ExamSetUpViewForClassID: function (values) {

        if (values != null) {
            ClassID.SetText(values);
        }

    },

    ExamSetUpViewForExamID: function (values) {

        if (values != null) {
            ExamID.SetText(values);
        }

    },

   
   
}