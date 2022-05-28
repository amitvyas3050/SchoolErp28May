var examSetupController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'ExamSetupBody':
           
                break;

            case 'ExamSetupFooter':
                GridExamSetup.SetHeight(e.pane.GetClientHeight() - 10);
                break;

        }
    },

    SendbuttonClick: function (s, e) {
      
        var mClassID = ClassID.GetValue();
        if (mClassID == null) {
            alert("Please Select Class.");
            return;
        }

        var mExamID = ExamID.GetValue();
        if (mExamID == null) {
            alert("Please Select Exam.");
            return;
        }

        var mOrderID = ExamOrder.GetValue();
        if (mOrderID == null) {
            alert("Please Select Order.");
            return;
        }

        var mFromDate = StartDate.GetValue();
        if (mFromDate == null) {
            alert("Please Select FromDate.");
            return;
        }

        var mToDate = EndDate.GetValue();
        if (mToDate == null) {
            alert("Please Select ToDate.");
            return;
        }

      
        $.ajax({
            url: "/ExamSetup/GetExamSetupDetailView",
            type: "POST",
            data: { pClassID: mClassID.toString(), pExamID: mExamID.toString(), pStartDate: mFromDate.toDateString(), pEndDate: mToDate.toDateString(), pOrderID: mOrderID.toString() },
            beforeSend: (function (data) {
                LoadingPanelExamSetup.Show();
            }),
            success: function (data) {
                $("#ExamSetupSplitter_1_CC").html(data);
                var winHeight = document.getElementById('ExamSetupSplitter_1_CC').offsetHeight;
                GridExamSetup.SetHeight(winHeight - 10);
            },
            error: function () {

            }
        }).done(function (data) {
            LoadingPanelExamSetup.Hide();

        });
    },


    UpdateButtonClick: function(s,e)
    {
        var mClassID = ClassID.GetValue();
        if (mClassID == null) {
            alert("Please Select Class.");
            return;
        }

        var mExamID = ExamID.GetValue();
        if (mExamID == null) {
            alert("Please Select Exam.");
            return;
        }

        var mOrderID = ExamOrder.GetValue();
        if (mOrderID == null) {
            alert("Please Select Order.");
            return;
        }

        var mFromDate = StartDate.GetValue();
        if (mFromDate == null) {
            alert("Please Select FromDate.");
            return;
        }

        var mToDate = EndDate.GetValue();
        if (mToDate == null) {
            alert("Please Select ToDate.");
            return;
        }

      


        $.ajax({ 
            url:"/ExamSetup/GetExamSetupListForupdateData",
            type:"POST",
            data: { pClassID: mClassID.toString(), pExamID: mExamID.toString(), pStartDate: mFromDate.toDateString(), pEndDate: mToDate.toDateString(), pOrderID: mOrderID.toString() },
            beforeSend: (function (data) {
              
                LoadingPanelExamSetup.Show();
            }),
            success: function(data)
            {
              
                $("#ExamSetupSplitter_1_CC").html(data);
                var winHeight = document.getElementById('ExamSetupSplitter_1_CC').offsetHeight;
                GridExamSetup.SetHeight(winHeight - 10);
            },
            error: function()
            {

            }
        } ).done( function(data)
        {
            LoadingPanelExamSetup.Hide();
        }) ;
    },




     SelectedExam: function (s, e) {
        var mExamID = s.GetValue();
        var mClassID = ClassID.GetValue();
      

        $.ajax({
            url: "/ExamSetup/CheckDuplicateMasterAction",
            type: "POST",
            data: { pExamID: mExamID.toString(), pClassID: mClassID.toString() },
        
        
            success: function(data)
            {
                $("#DateOrderPartial").html(data);
            },
            error: function () {
            }
        });

    },

    SelectedClass: function (s, e) {
        var mClassID = s.GetValue();
        alert(mClassID);

        $.ajax({
            url: "/FacultyAllotment/GetClassSetupList",
            type: "POST",
            data: { pClassID: mClassID.toString() },
            success: function(data)
            {
                $("#ClassSetupID").html(data);
            },
            error: function () {
            }
        });

    }










}