var letterheadmasterController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'LetterHeadMasterBody':
                //GridLetterHeadMaster.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'EventListFooter':
                
                break;
        }
    },

    AddNewClicked: function (s, e) {
        GridLetterHeadMaster.AddNewRow();
    },
    EditClicked: function (s, e) {
        rowIdx = GridLetterHeadMaster.GetFocusedRowIndex();
        GridLetterHeadMaster.StartEditRow(rowIdx);
    },

    DeleteClicked: function (s, e) {
        alert("call delete");
        rowIdx = GridLetterHeadMaster.GetFocusedRowIndex();
        GridLetterHeadMaster.DeleteRow(rowIdx);
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
        s.GetRowValues(s.GetFocusedRowIndex(), 'EventID', letterheadmasterController.RefreshTabsView);


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

    updateletterhead: function (s, e) {
        
        alert(LetterHeadDesc.GetHtml())
        //var letterheaddecs = LetterHeadDesc.GetHtml();
        var letterheaddecs = "dddddddd";

        var pletterheadid = 1;

        $.ajax({
            url: "/LetterHeadMaster/SaveLetterHeadDescription",
            type: "POST",
            data: { mLetterHeadDesc: letterheaddecs, mLetterheadId: pletterheadid },
           
            beforeSend: (function (data) { loadingPanelTopper.Show(); }),
            success: function (data) {
                $("#LetterHeadMasterSplitter_2_CC").html(data);
                
            },
            error: function () {

            }

        }).done(function (data) { loadingPanelTopper.Hide(); });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    },

   
}