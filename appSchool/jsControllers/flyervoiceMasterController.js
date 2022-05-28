var flyervoiceMasterController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'FlyerVoiceMasterBody':
                //GridFlyerVoiceMaster.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'EventListFooter':
                GridFlyerVoiceMaster.SetHeight(e.pane.GetClientHeight() - 1);
                break;
        }
    },

    AddNewClicked: function (s, e) {
        GridFlyerVoiceMaster.AddNewRow();
    },
    EditClicked: function (s, e) {
        rowIdx = GridFlyerVoiceMaster.GetFocusedRowIndex();
        GridFlyerVoiceMaster.StartEditRow(rowIdx);
    },

    DeleteClicked: function (s, e) {
        alert("call delete");
        rowIdx = GridFlyerVoiceMaster.GetFocusedRowIndex();
        GridFlyerVoiceMaster.DeleteRow(rowIdx);
    },

    FilterClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        $.ajax({
            url: "/NewsEventMaster/RefreshNewsEventGrid",
            type: "POST",
            data: { mShowFilter: ShowFilterRow },
            success: function (data) {
                $("#FlyerVoiceMasterSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });

    },

    UpdateVoiceFile: function (s, e) {
        
        FlyerVoice = "instrument"
        $.ajax({
            url: "/FlyerVoiceMaster/UpdateVoiceFile",
            type: "POST",
            datatype: "json",
            data: { PFlyerVoice: FlyerVoice.toString() },
            success: function (data) {
                

                $("#FlyerVoiceMasterSplitter_2_CC").html(data.FileMgrData);
                var winHeight = document.getElementById('FlyerVoiceMasterSplitter_2_CC').offsetHeight;
                GridFlyerVoiceMaster.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        });

    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'FlyerVoiceID', flyervoiceMasterController.RefreshTabsView);


    },
    RefreshTabsView: function (values) {
        var regID;
        if (values != null) {
            regID = values;
        }
        $.ajax({
            url: "/Achievement/EventGridRowChange",
            type: "POST",
            data: { RegID: regID },
            success: function (data) {
                $("#FlyerVoiceMasterSplitter_2_CC").html(data);
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
            url: "/Achievement/EventGridRowChange",
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