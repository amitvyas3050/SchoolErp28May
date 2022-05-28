var newsEventMasterController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'NewsEventMasterBody':
                GridNewsEventMaster.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'EventListFooter':
                
                break;
        }
    },

    AddNewClicked: function (s, e) {
        GridNewsEventMaster.AddNewRow();
    },
    EditClicked: function (s, e) {
        rowIdx = GridNewsEventMaster.GetFocusedRowIndex();
        GridNewsEventMaster.StartEditRow(rowIdx);
    },

    DeleteClicked: function (s, e) {
        alert("call delete");
        rowIdx = GridNewsEventMaster.GetFocusedRowIndex();
        GridNewsEventMaster.DeleteRow(rowIdx);
    },

    FilterClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        $.ajax({
            url: "/NewsEventMaster/RefreshNewsEventGrid",
            type: "POST",
            data: { mShowFilter: ShowFilterRow },
            success: function (data) {
                $("#NewsEventMasterSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });

    },



    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'EventID', newsEventMasterController.RefreshTabsView);


    },
    RefreshTabsView: function (values) {
        var regID;
        if (values != null) {
            regID = values;
        }
        $.ajax({
            url: "/NewsEventMaster/EventGridRowChange",
            type: "POST",
            data: { RegID: regID },
            success: function (data) {
                $("#NewsEventMasterSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    },
    RowSelectionChangeByClick: function(s,e)
    {
        var values = s.name;
        var regID;
        if (values != null) {
            regID = values;
        }
        $.ajax({
            url: "/NewsEventMaster/EventGridRowChange",
            type: "POST",
            data: { RegID: regID },
            success: function (data) {
                $("#RegistrationSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });

    },

    RefreshDocumentList: function (s, e) {
        var regID;
        regID = StudentID.GetText();


        $.ajax({
            url: "/NewsEventMaster/EventGridRowChange",
            type: "POST",
            data: { RegID: regID },
            success: function (data) {
                $("#RegistrationSplitter_2_CC").html(data);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    },

   
}