var flyermasterController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'FlyerMasterBody':
                GridFlyerMaster.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'EventListFooter':
                
                break;
        }
    },

    AddNewClicked: function (s, e) {
        GridFlyerMaster.AddNewRow();
    },
    EditClicked: function (s, e) {
        rowIdx = GridFlyerMaster.GetFocusedRowIndex();
        GridFlyerMaster.StartEditRow(rowIdx);
    },

    DeleteClicked: function (s, e) {
        alert("call delete");
        rowIdx = GridFlyerMaster.GetFocusedRowIndex();
        GridFlyerMaster.DeleteRow(rowIdx);
    },

    FilterClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        $.ajax({
            url: "/NewsEventMaster/RefreshNewsEventGrid",
            type: "POST",
            data: { mShowFilter: ShowFilterRow },
            success: function (data) {
                $("#AchievementSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });

    },



    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'FlyerID', flyermasterController.RefreshTabsView);


    },
    RefreshTabsView: function (values) {
        var regID;
        if (values != null) {
            regID = values;
        }
        $.ajax({
            url: "/FlyerMaster/EventGridRowChange",
            type: "POST",
            data: { RegID: regID },
            success: function (data) {
                $("#FlyerMasterSplitter_2_CC").html(data);
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
            url: "/FlyerMaster/EventGridRowChange",
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
            url: "/FlyerMaster/EventGridRowChange",
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