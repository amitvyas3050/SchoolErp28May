var studentExamResultController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'StudentExamResultBody':
           
                break;
            case 'StudentExamResultFooter':
                GridStudentResult.SetHeight(e.pane.GetClientHeight() - 5);
                break;

        }
    },

    SelectedClass: function (s, e) {
      
        var classID = s.GetValue();
        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/StudentExamResult/GetExamListView",
            type: "POST",
            dataType: "json",
            data: { mClassID: classID.toString() },
            success: function (data) {

                if (data.msg == true) {
                    alert(data.ErrorMessage);
                }
                else {

                    $("#divError").empty();

                    var html = '<h3>' + data.ErrorMessage + '</h3>';
                    $('#divError').append(html);

                }
                $("#divExamList").html(data.ListData);
                $("#divError").empty();
              
            },
            error: function () {
            }
        });


    },


    CreateStudentExamResult: function (s, e) {
      
        var mClassID = ClassID.GetValue();
        if (mClassID == null) {
            mClassID = 0;
        }

        var mExamID = ExamID.GetValue();
        if (mExamID == null) {
            mExamID = 0;
            alert("Please Select Exam.");
            return;
        }

        $.ajax({
            url: "/StudentExamResult/CreateStudentExamResult",
            type: "POST",
            dataType: "json",
            data: { pClassID: mClassID, pExamID: mExamID },
            beforeSend: (function (data) {
                loadingPanelStudentExamResult.Show();
            }),
            success: function (data) {

                if (data.msg == true) {
                    alert(data.ErrorMessage);
                }
                else {
                   
                    $("#divError").empty();
                    var html = '<h3>' + data.ErrorMessage + '</h3>';
                    $('#divError').append(html);

                }
                $("#StudentExamResultSplitter_1_CC").html(data.ListData);
                var winHeight = document.getElementById('StudentExamResultSplitter_1_CC').offsetHeight;
                GridStudentResult.SetHeight(winHeight - 10);
            
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentExamResult.Hide();

        });


    }


}