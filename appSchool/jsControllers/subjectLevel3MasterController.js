var subjectLevel3MasterController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'subjectLevel3Body':
                GridsubjectLevel3Master.SetHeight(e.pane.GetClientHeight() - 2);
                GridsubjectLevel3Master.SetWidth(e.pane.GetClientWidth() - 2);
                break;
                //case 'ClassesFooter':
                //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
                //    break;
        }
    },

    SelectedSubjectOne: function(s,e){
        var mpIdL1 = s.GetValue();
     

        $.ajax({
            url: "/SubjectLevel3Master/GetSubjectLevelTwoListByClassID",
            type: "POST",
            data: { pIdL1: mpIdL1.toString() },
            success: function (data) {
             
                $("#subjectLevel3MasterSplitter_1_CC").html(data);
            },
            error: function () {

            }
        });
      },




    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'IdL3', subjectLevel3MasterController.RefreshTabsView);
    },



    //SelectedID: function (s, e) {
    //  var mIDL1 = s.GetValue();
    //  if (mIDL1.toString() == "") {
    //      alert("Please Select Classes");
    //      return;
    //  }


    FillIDL2SubjectData: function (value) {

        $.ajax({
            url: "/SubjectLevel3Master/GetIDL2SubjectList",
            type: "POST",
            data: { mIdL1: value },
            success: function (data) {
                $("#IdL2").html(data);
                //if (rbtnStudSession.GetValue() == 2) {
                //    ClassSetupID.SetVisible(true);
                //    lblClassSetupID.SetVisible(true);
                //}

                //var winHeight = document.getElementById('StudentSessionSplitter_1_CC').offsetHeight;
                //GridStudentSession.SetHeight(winHeight - 10);




            },
            error: function () {
            }


            //RefreshTabsView: function (values) {
            //    var mID;
            //    if (values != null) { mID = values; }
            //    $.ajax({
            //        url: "/DepartmentMaster/DepartmentMasterGridRowChange",
            //        type: "POST",
            //        data: { ID: mID },
            //        success: function (data) {
            //            $("#DepartmentMasterSplitter_1_CC").html(data);
            //        },
            //        error: function () {
            //        }
            //    });
            //}
            //}





            //var departmentMasterController = {

            //    splitterResized: function (s, e) {
            //        switch (e.pane.name) {
            //            case 'DepartmentMasterBody':
            //                GridDepartmentMaster.SetHeight(e.pane.GetClientHeight());
            //                break;
            //        }
            //    },

            //    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
            //        s.GetRowValues(s.GetFocusedRowIndex(), 'DepartmentID', departmentMasterController.RefreshTabsView);
            //    },

            //    //RefreshTabsView: function (values) {
            //    //    var mID;
            //    //    if (values != null) { mID = values;}
            //    //    $.ajax({
            //    //        url: "/DepartmentMaster/DepartmentGridRowChange",
            //    //        type: "POST",
            //    //        data: { ID: mID },
            //    //        success: function (data) {
            //    //            $("#DepartmentMasterSplitter_1_CC").html(data);
            //    //        },
            //    //        error: function () {
            //    //        }
            //    //    });
            //    //}
        })
    }
}