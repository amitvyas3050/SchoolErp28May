var studentFeeStructureController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'StudentFeeStructureBody':
                GridStudentforSFS.SetHeight(e.pane.GetClientHeight());
                break;
                //case 'ClassesFooter':
                //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
                //    break;
        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'StudentID', studentFeeStructureController.RefreshTabsView);
    },

    SelectedClass: function (s, e) {
        // var classID = ClassSetupID.GetSelectedValues();
        var classID = s.GetValue();
        $("#txtClassForSFS_I").val(classID);
        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/StudentFeeStructure/GetAllStudentListView",
            type: "POST",
            data: { mClassesID: classID.toString() },
            success: function (data) {
               
                $("#StudentFeeStructureSplitter_1_CC").html(data);
                var winHeight = document.getElementById('StudentFeeStructureSplitter_1_CC').offsetHeight;
                GridStudentforSFS.SetHeight(winHeight - 10);

            },
            error: function () {
            }
        });

    },

    SaveFeeStructure: function (s, e) {
        // var classID = ClassSetupID.GetSelectedValues();
        var classID = txtClassForSFS.GetValue();
       //alert(classID)
        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }

        DataView["SFeeStructclassID"] = classID;
        //alert(DataView["SFeeStructclassID"]);
        e.customArgs["mClassesID"] = classID.toString();
        CallbackPanel.PerformCallback();
        //$.ajax({
        //    url: "/StudentFeeStructure/SaveStudentFeeStructure",
        //    type: "POST",
        //    data: { mClassesID: classID.toString() },
        //    success: function (data) {

        //        $("#StudentFeeStructureSplitter").html(data);
        //    },
        //    error: function () {
        //    }
        //});

    },


        RefreshTabsView: function (values) {
            var nID;
            if (values != null)
            {
                nID = values;
            }
            $.ajax({
                url: "/StudentFeeStructure/StudentFeeStructureGridRowChange",
                type: "POST",
                data: { ID: nID },
                success: function (data) {
                    $("#StudentSessionSplitter_1_CC").html(data);
                },
                error: function () {
                }
            });
        }
    }
