var examSyllabusController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'ExamSyllabusBody':
           
                break;

            case 'ExamSyllabusFooter':
                GridExamSyllabus.SetHeight(e.pane.GetClientHeight() - 10);
                break;

        }
    },

    SendbuttonClick: function (s, e) {
      
        var mClassID = ClassID.GetValue();
        if (mClassID == null) {
            alert("Please Select Class.");
            return;
        }

        var mSubjectID = SubjectIDL1.GetValue();
        if (mSubjectID == null) {
            alert("Please Select Subject.");
            return;
        }

        var mExamID = ExamID.GetValue();
        if (mExamID == null) {
            alert("Please Select Exam.");
            return;
        }

      
        $.ajax({
            url: "/ExamSyllabus/GetExamSyllabuseDetailView",
            type: "POST",
            data: { pClassID: mClassID.toString(), pSubjectID: mSubjectID.toString(), pExamID: mExamID.toString() },
              beforeSend: (function (data) {
              
                  LoadingPanelExamSyllabus.Show();
            }),
            success: function (data) {
                $("#ExamSyllabusSplitter_1_CC").html(data);
                var winHeight = document.getElementById('ExamSyllabusSplitter_1_CC').offsetHeight;
                GridExamSyllabus.SetHeight(winHeight - 10);
            },
            error: function () {

            }
        }).done( function(data)
        {
            LoadingPanelExamSyllabus.Hide();
        });
    },

    SendUpdatebuttonClick: function (s, e) {

        var mClassID = ClassID.GetValue();
        if (mClassID == null) {
            alert("Please Select Class.");
            return;
        }

        var mSubjectID = SubjectIDL1.GetValue();
        if (mSubjectID == null) {
            alert("Please Select Subject.");
            return;
        }

        var mExamID = ExamID.GetValue();
        if (mExamID == null) {
            alert("Please Select Exam.");
            return;
        }


        $.ajax({
            url: "/ExamSyllabus/GetExamSyllabuseDetailViewUPDATE",
            type: "POST",
            data: { pClassID: mClassID.toString(), pSubjectID: mSubjectID.toString(), pExamID: mExamID.toString() },
            beforeSend: (function (data) {

                LoadingPanelExamSyllabus.Show();
            }),
            success: function (data) {
                $("#ExamSyllabusSplitter_1_CC").html(data);
                var winHeight = document.getElementById('ExamSyllabusSplitter_1_CC').offsetHeight;
                GridExamSyllabus.SetHeight(winHeight - 10);
            },
            error: function () {

            }
        }).done(function (data) {
            LoadingPanelExamSyllabus.Hide();
        });
    },


    SelectedClass: function (s, e) {
        var mClassID = s.GetValue();
      

        $.ajax({
            url: "/ExamSyllabus/GetSubjectList",
            type: "POST",
            data: { pClassID: mClassID.toString() },
            success: function(data)
            {
               
                $("#SubjectIDL1").html(data);
            },
            error: function () {
            }
        });

    }


    //GetFaculty: function (data) {

    //    var a = data;
    //    alert(data);
    //    $.ajax({
    //        url: "/FacultyAllotment/GetFacultyListByClassID",
    //        type: "POST",
    //        data: { pClassID: mClassID.toString() },
    //        success: function (data) {

    //            $("#FacultyID").html(data);
               
    //        },
    //        error: function () {
    //        }


    //    });



    //}

 










}