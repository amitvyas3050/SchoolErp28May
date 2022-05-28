var sessionClassAllotmentController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'SessionClassAllotmentBody':
           
                break;
            case 'SessionClassAllotmentFooter':
                GridSessionClass.SetHeight(e.pane.GetClientHeight() - 5);
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
            url: "/SessionClassAllotment/GetAllStudentListView",
            type: "POST",
            data: { mClassesID: classID.toString() },
            success: function (data) {
                $("#SessionClassAllotmentSplitter_1_CC").html(data);
                var winHeight = document.getElementById('SessionClassAllotmentSplitter_1_CC').offsetHeight;
                GridSessionClass.SetHeight(winHeight - 10);
                $("#divError").empty();
              
            },
            error: function () {
            }
        });


        //sessionClassAllotmentController.FillClassSetupData(classID);

    },


    SelectedClassTo: function (s, e) {
        var classIDTo = s.GetValue();
        if (classIDTo.toString() == "") {
            alert("Please Select ClassesTo");
            return;
        }
        $.ajax({
            url: "/SessionClassAllotment/GetClassSetupListByClassID",
            type: "POST",
            data: { mClassID: classIDTo.toString() },
            success: function (data) {
                $("#ClassSetupIDtd").html(data);
                //var winHeight = document.getElementById('ClassSetupIDtd').offsetHeight;
                //GridSessionClass.SetHeight(winHeight - 10);

            },
            error: function () {
            }
        });


        //sessionClassAllotmentController.FillClassSetupData(classID);

    },


    FillClassSetupData: function (value) {
   
        $.ajax({
            url: "/SessionClassAllotment/GetClassSetupList",
            type: "POST",
            data: { mClassID: value },
            success: function (data) {
                $("#ClassSetupIDtd").html(data);
                if (rbtnStudSession.GetValue() == 2) {
                    ClassSetupID.SetVisible(true);
                    lblClassSetupID.SetVisible(true);
                }

                //var winHeight = document.getElementById('StudentSessionSplitter_1_CC').offsetHeight;
                //GridStudentSession.SetHeight(winHeight - 10);




            },
            error: function () {
            }
        });




    },



      RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
          s.GetRowValues(s.GetFocusedRowIndex(), 'StudentSessionID', sessionClassAllotmentController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var regID;
        if (values != null) {
            regID = values;
            StudentIDs.SetText(regID);
        }
    $.ajax({
        url: "/SessionClassAllotment/GetStudentDataForClassChange",
            type: "POST",
            data: { mStudentSessionID: regID },
            success: function (data) {
                $("#StudentDetailID").html(data);
             
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    },




    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            GridStudentSession.SelectRows();
        }
        else {
            GridStudentSession.UnselectRows();
        }

    },

  

    UpdateSessionClassAllotment: function (s, e) {
      
        var mClassID = ClassID.GetValue();
        if (mClassID == null) {
            mClassID = 0;
        }

        var mClassIDTo = ClassIDTo.GetValue();
        if (mClassIDTo == null) {
            mClassIDTo = 0;
            alert("Please Select ToClass.");
            return;
        }

        var mClassSetupID = ClassSetupID.GetValue();
        if (mClassSetupID == null) {
            mClassSetupID = 0;
            alert("Please Select Section.");
            return;
        }

        var mStudentSessionID = StudentIDs.GetValue();
        if (mStudentSessionID == null) {
            mStudentSessionID = 0;
            alert("Please Select Student.");
            return;
        }


        $.ajax({
            url: "/SessionClassAllotment/UpdateSessionClassAllotment",
            type: "POST",
            dataType: "json",
            data: { pClassID: mClassID, pClassIDTo: mClassIDTo, pStudentSessionID: mStudentSessionID,pClassSetupID:mClassSetupID },
            beforeSend: (function (data) {
                loadingPanelSessionClassAllotment.Show();
            }),
            success: function (data) {

                if (data.msg == true) {
                    alert(data.ErrorMessage);
                }
                else {
                   
                    $("#divError").empty();

                    var html = '<h3>' + data.ErrorMessage + '</h3>';
                    $('#divError').append(html);
                  
                  //  lblError.SetText(data.ErrorMessage);

                }
                $("#SessionClassAllotmentSplitter_1_CC").html(data.ListData);
                var winHeight = document.getElementById('SessionClassAllotmentSplitter_1_CC').offsetHeight;
                GridSessionClass.SetHeight(winHeight - 10);
                StudentIDs.SetText("");
            
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelSessionClassAllotment.Hide();

        });


    }


}