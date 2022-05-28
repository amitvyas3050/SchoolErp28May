var studentIDPasswordExportController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'SendMessegeWishesBody':
               //GridStudentIDPasswordExport.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendMessegeText':
               // GridStudentIDPasswordExport.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendMessegeText':
               //GridParentIDPasswordExport.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendMessegeText':
                //GridTeacherIDPasswordExport.SetHeight(e.pane.GetClientHeight());
                break;
        }
    },
   

    ClickLoadGridStud: function (s, e) {
       
        $.ajax({
            url: "/StudentIDPasswordExport/GetStudentListForExport",
            type: "POST",
            // data: { mClassesID: classID.toString() },
            beforeSend: (function (data) {
                loadingPaneLoginID.Show();
            }),
            success: function (data) {
                $("#SendMessegeSplitter_1_CC").html(data);
                var winHeight = document.getElementById('SendMessegeSplitter_1_CC').offsetHeight;
                GridStudentIDPasswordExport.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPaneLoginID.Hide();

        });

    },
  
    ClickLoadGridPar: function (s, e) {

        $.ajax({
            url: "/StudentIDPasswordExport/GetParentListForExport",
            type: "POST",
            // data: { mClassesID: classID.toString() },
            beforeSend: (function (data) {
                loadingPaneLoginID.Show();
            }),
            success: function (data) {
                $("#SendMessegeSplitter_1_CC").html(data);
                var winHeight = document.getElementById('SendMessegeSplitter_1_CC').offsetHeight;
                GridParentIDPasswordExport.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPaneLoginID.Hide();

        });
    },


    ClickLoadGridTeach: function (s, e) {

        $.ajax({
            url: "/StudentIDPasswordExport/GetTeacherListForExport",
            type: "POST",
            // data: { mClassesID: classID.toString() },
            beforeSend: (function (data) {
                loadingPaneLoginID.Show();
            }),
            success: function (data) {
                $("#SendMessegeSplitter_1_CC").html(data);
                var winHeight = document.getElementById('SendMessegeSplitter_1_CC').offsetHeight;
                GridTeacherIDPasswordExport.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPaneLoginID.Hide();

        });
    },

    DisplayReport: function (s, e) {

        $.ajax({
            url: "/StudentIDPasswordExport/GenerateStudentLoginIDReport",
            type: "POST",
            datatype: "json",
           // data: {pClassID:mClassID, pClassSetupID: mClassSetupID },
            beforeSend: (function (data) {
                loadingPanelStudentLoginID.Show();
            }),
            success: function (data) {
                if (data.status == true) {
                    //alert(data.dataResult);
                    $("#StudentLoginSplitter_1_CC").html(data.dataResult);
                }
                else {

                    alert(data.errorMsg);
                    return;
                }

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelStudentLoginID.Hide();

        });


    },
    
}