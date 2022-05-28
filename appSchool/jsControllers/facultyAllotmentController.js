var facultyAllotmentController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'FacultyAllotmentBody':
           
                break;

            case 'FacultyAllotmentFooter':
                GridFacultyAllotment.SetHeight(e.pane.GetClientHeight() - 10);
                break;

        }
    },

    SendbuttonClick: function (s, e) {
      
        var mClassID = ClassID.GetValue();
        if (mClassID == null) {
            alert("Please Select Class.");
            return;
        }

        var mClassSetupID = ClassSetupID.GetValue();
        if (mClassSetupID == null) {
            alert("Please Select Section.");
            return;
        }

        var mFacultyID = FacultyID.GetValue();
        if (mFacultyID == null) {
            alert("Please Select Faculty.");
            return;
        }

      
        $.ajax({
            url: "/FacultyAllotment/GetSubjectListView",
            type: "POST",
            data: { pClassID: mClassID.toString(), pClassSetupID: mClassSetupID.toString(), pFacultyID: mFacultyID.toString() },
            beforeSend: (function (data) {
                loadingPanelMenu.Show();
            }),
            success: function (data) {
                $("#divFacultyAllotment").html(data);
                //$("#FacultyAllotmentSplitter_1_CC").html(data);
                //var winHeight = document.getElementById('FacultyAllotmentSplitter_1_CC').offsetHeight;
                //GridFacultyAllotment.SetHeight(winHeight - 10);


            },
            error: function () {

            }
        }).done(function (data) {
            loadingPanelMenu.Hide();

        });
    },


    SelectedClass: function (s, e) {
        var mClassID = s.GetValue();
        if (mClassID == null) {
            alert("Please Select Class.");
            return;
        }

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

    },


    GetFaculty: function (data) {

        var a = data;
        alert(data);
        $.ajax({
            url: "/FacultyAllotment/GetFacultyListByClassID",
            type: "POST",
            data: { pClassID: mClassID.toString() },
            success: function (data) {

                $("#FacultyID").html(data);
               
            },
            error: function () {
            }


        });



    }

 










}