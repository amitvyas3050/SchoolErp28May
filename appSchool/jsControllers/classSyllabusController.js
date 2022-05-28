var classSyllabusController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'ClassSyllabusBody':
           
                break;
            case 'ClassSyllabusFooter':
                GridClassSyllabus.SetHeight(e.pane.GetClientHeight() - 10);
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



        var mPreferredBook = txtboxPrefBook.GetText();
      
        $.ajax({
            url: "/ClassSyllabus/GetClassSyllabusDataView",
            type: "POST",
            data: { pClassID: mClassID.toString(), pSubjectID: mSubjectID.toString(), pPreferredBook: mPreferredBook.toString() },

            beforeSend: (function (data) {
                loadingPanelMenu.Show();
            }),

            success: function (data) {
                $("#divclasssyllabus").html(data);
                //$("#ClassSyllabusSplitter_1_CC").html(data);
                //var winHeight = document.getElementById('ClassSyllabusSplitter_1_CC').offsetHeight;
                //GridClassSyllabus.SetHeight(winHeight - 10);

             },
            error: function () {

            }
        }).done(function (data) {
            loadingPanelMenu.Hide();
        });
    },


    SelectedClass: function (s, e) {
        var mClassID = s.GetValue();

        $.ajax({
            url: "/ClassSyllabus/GetSubjectListByClassID",
            type: "POST",
            data: { pClassID: mClassID.toString() },
            success: function(data)
            {
              
                $("#SubjectIDL1").html(data);


            },
            error: function () {
            }


        });



    },

    SelectedSubject: function (s, e) {

        var mClassID = ClassID.GetValue();
        var mSubjectID = s.GetValue();


        $.ajax({
            url: "/ClassSyllabus/GetPreferredBookByClass",
            type: "POST",
            dataType: "json",
            data: { pClassID:mClassID.toString(), pSubjectID:mSubjectID.toString() },
            success: function (data) {
                    txtboxPrefBook.SetText(data.result);
                    if (data.Status == true) {
                      
                        $('#divmsg').empty();
                        var html = "<h3>" + data.errorMsg + "</h3>"
                        $('#divmsg').append(html);
                    }
                    else {
                        $('#divmsg').empty();
                    }

            },
            error: function () {
            }


        });


    }


}