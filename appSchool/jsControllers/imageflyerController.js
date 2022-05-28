var imageflyerController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'ImageFlyerBody':
                GridImageFlyer.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'ImageFlyerListFooter':
                
                break;
        }
    },

    AddNewClicked: function (s, e) {
        GridImageFlyer.AddNewRow();
    },
    EditClicked: function (s, e) {
        rowIdx = GridImageFlyer.GetFocusedRowIndex();
        GridImageFlyer.StartEditRow(rowIdx);
    },

    DeleteClicked: function (s, e) {
        alert("call delete");
        rowIdx = GridImageFlyer.GetFocusedRowIndex();
        GridImageFlyer.DeleteRow(rowIdx);
    },

    FilterClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        $.ajax({
            url: "/ImageFlyer/RefreshNewsEventGrid",
            type: "POST",
            data: { mShowFilter: ShowFilterRow },
            success: function (data) {
                $("#ImageFlyerSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });

    },



    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'FlyerID', imageflyerController.RefreshTabsView);


    },
    RefreshTabsView: function (values) {
        var regID;
        if (values != null) {
            regID = values;
        }
        $.ajax({
            url: "/ImageFlyer/EventGridRowChange",
            type: "POST",
            data: { RegID: regID },
            success: function (data) {
                $("#ImageFlyerSplitter_2_CC").html(data);
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
            url: "/ImageFlyer/EventGridRowChange",
            type: "POST",
            data: { RegID: regID },
            success: function (data) {
                $("#ImageFlyerSplitter_2_CC").html(data);
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