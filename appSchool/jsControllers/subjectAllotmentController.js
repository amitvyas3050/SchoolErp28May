var subjectAllotmentController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {

            case 'SubjectAllotmentMiddel':
                //GridSubjectlevelOne.SetHeight(e.pane.GetClientHeight() - 10);
                break;
            case 'SubjectAllotmentBottom':
                //GridSubjectsAllotment.SetHeight(e.pane.GetClientHeight() - 10);
                break;

        }
    },

    SelectionChanged: function (s, e) {
        s.GetSelectedFieldValues("IdL1", subjectAllotmentController.GetSelectedFieldValuesCallbackForLevelOne);
  },

    SelectionChangedForSubjectLevelTwo: function (s, e) {
        s.GetSelectedFieldValues("IdL2", subjectAllotmentController.GetSelectedFieldValuesCallbackForLevelTwo);
    },

    SelectionChangedForSubjectLevelThree: function (s, e) {
        s.GetSelectedFieldValues("IdL3", subjectAllotmentController.GetSelectedFieldValuesCallbackForLevelThree);
    },


    GetSelectedFieldValuesCallbackForLevelOne: function (values) {
        //alert(values);
        SubjectLevelD.SetText(values);
    },

    GetSelectedFieldValuesCallbackForLevelTwo: function (values) {
        //alert(values);
       SubjectLevelD.SetText(values);
    },

    GetSelectedFieldValuesCallbackForLevelThree: function (values) {
        //alert("call");
        SubjectLevelD.SetText(values);
    },
  

   SelectedClass: function (s, e) {

        var mClassID = s.GetValue();
        if (mClassID == null) {
            alert("Please Select Class");
            return;
        }

        $.ajax({
            url: "/SubjectAllotment/GetSubjectlevelbyClassID",
            type: "Post",
            datatype: "json",
            data: { MClassID: mClassID.toString() },
            success: function (data) {
               
                txtSubjectlevel.SetText(data.ResultSubLevel);
                txtSubjectlevelName.SetText(data.ResultSubLevelName);
            },
            error: function () {

            }

        });

    },
   
   ShowSubjectForClass: function (s, e) {

       var mClassID = ClassID.GetValue();
       if (mClassID == null) {
           alert("Please Select Class");
           return;
       }
       var mSubjectLevel = txtSubjectlevel.GetText();
      

       $.ajax({

           url: "/SubjectAllotment/ShowSubjectForClass",
           type: "Post",
           datatype: "json",
           data: { PClassID: mClassID, PSubjectlevel: mSubjectLevel },

           beforeSend: (function (data) { loadingPanelSubjectAllotment.Show(); }),

           success: function (data) {
               if (data.DisplaySubjectLevel == 1) {

                   $("#SubjectAllotmentSplitter_1_CC").html(data.ListData);
                   var winHeight = document.getElementById('SubjectAllotmentSplitter_1_CC').offsetHeight;
                   GridSubjectlevelOne.SetHeight(winHeight - 1);

                   $("#SubjectAllotmentSplitter_3_CC").html(data.ListDataSuballot);
                   var winHeight = document.getElementById('SubjectAllotmentSplitter_3_CC').offsetHeight;
                   GridSubjectAllotment.SetHeight(winHeight - 1);


               }
             else {
                   if (data.DisplaySubjectLevel == 2) {
                       $("#SubjectAllotmentSplitter_1_CC").html(data.ListData);
                       var winHeight = document.getElementById('SubjectAllotmentSplitter_1_CC').offsetHeight;
                       GridSubjectlevelTwo.SetHeight(winHeight - 1);

                       $("#SubjectAllotmentSplitter_3_CC").html(data.ListDataSuballot);
                       var winHeight = document.getElementById('SubjectAllotmentSplitter_3_CC').offsetHeight;
                       GridSubjectAllotment.SetHeight(winHeight - 1);
                   }

                   else
                   {
                       $("#SubjectAllotmentSplitter_1_CC").html(data.ListData);
                       var winHeight = document.getElementById('SubjectAllotmentSplitter_1_CC').offsetHeight;
                       GridSubjectlevelThree.SetHeight(winHeight - 1);

                       $("#SubjectAllotmentSplitter_3_CC").html(data.ListDataSuballot);
                       var winHeight = document.getElementById('SubjectAllotmentSplitter_3_CC').offsetHeight;
                       GridSubjectAllotment.SetHeight(winHeight - 1);
                   }
                }
               SubjectLevelD.SetText("");
           },
           error: function () {

           }

       }).done(function (data) { loadingPanelSubjectAllotment.Hide(); });


   },

  
   AllotSubjectForClass: function (s, e) {

       var mClassID = ClassID.GetValue();
       if (mClassID == null) {
           alert("Please Select Class");
           return;
       }

       var mSubjectLevel = txtSubjectlevel.GetText();
       if (mSubjectLevel== null) {
           alert("Please Select Subject Level");
           return;
       }

       var mSubjectLevelDs = SubjectLevelD.GetValue();
       if (mSubjectLevelDs == null) {
           alert("Please Select Subject");
           return;
       }
      
       $.ajax({

           url: "/SubjectAllotment/AllotmentSubjectForClass",
           type: "Post",
           datatype: "json",
           data: { ClassID: mClassID, pSubjectlevel: mSubjectLevel,  PSubjectlevelID: mSubjectLevelDs.toString() },

           beforeSend: (function (data) { loadingPanelSubjectAllotment.Show(); }),

           success: function (data) {
               alert(data.Displaymsg);

               $("#SubjectAllotmentSplitter_3_CC").html(data.ListData);
               var winHeight = document.getElementById('SubjectAllotmentSplitter_3_CC').offsetHeight;
               GridSubjectAllotment.SetHeight(winHeight - 1);

               SubjectLevelD.SetText("");

           },
           error: function () {

           }

       }).done(function (data) { loadingPanelSubjectAllotment.Hide(); });


   },




}