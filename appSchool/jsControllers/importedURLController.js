var importedURLController = {

    splitterResized: function (s, e) {
       
        switch (e.pane.name) {
            
            case 'ImportedURLBody':
                gvEditing1.SetHeight(e.pane.GetClientHeight());
               
                break;
            //case 'ClassesFooter':
            //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
            //    break;
        }
    },

   
    AddNewClicked: function (s, e) {
        $.ajax({
            url: "/ImportedURL/ExternalEditFormEdit",
            type: "POST",
            data: { mURLID: -1 },
            success: function (data) {
                $("#ImportedURLSplitter_1_CC").html(data);
                var winHeight = document.getElementById('ImportedURLSplitter_1_CC').offsetHeight;

                gvEditing1.SetHeight(winHeight);
            },
            error: function () {
            }
        });
    },


    EditClicked: function (s, e) {

        rowIdx = TXTNoticeID.GetValue();
        $.ajax({
            url: "/ImportedURL/ExternalEditFormEdit",
            type: "POST",
            data: { mURLID: rowIdx },
            success: function (data) {
                $("#ImportedURLSplitter_1_CC").html(data);
                var winHeight = document.getElementById('ImportedURLSplitter_1_CC').offsetHeight;

                gvEditing1.SetHeight(winHeight );
            },
            error: function () {
            }
        });
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'URLID', importedURLController.NoticeView);


    },

    FromDateCheck: function (s, e) {

        var mFromDate = FromDate.GetValue();
        var mToDate = ToDate.GetValue();
        if (mFromDate != '' && mToDate != '') {

            if (Date.parse(mFromDate) > Date.parse(mToDate)) {
                FromDate.SetText("");
                alert("From date should not be greater than To date");
            }
        }


    },
    ToDateCheck: function (s, e) {

        var mFromDate = FromDate.GetValue();
        var mToDate = ToDate.GetValue();
        if (mFromDate != '' && mToDate != '') {

            if (Date.parse(mFromDate) > Date.parse(mToDate)) {
                ToDate.SetText("");
                alert("From date should not be greater than To date");
            }
        }


    },

    NoticeView: function (values) {
        
        if (values != null) {
           TXTNoticeID.SetText(values);
        }

    },
   

    Cancle: function (s, e) {
        $.ajax({
            url: "/ImportedURL/Cancle",
            type: "POST",
           
            success: function (data) {
                $("#NoticeSplitter_1_CC").html(data);
                var winHeight = document.getElementById('NoticeSplitter_1_CC').offsetHeight;
                gvEditing1.SetHeight(winHeight );
            },
            error: function () {
            }
        });
    },

    ToDateSelectionByFromdate: function (s, e) {
        var mFromdate = s.GetValue();
        alert(mFromdate);
    }
    
 

    //RefreshTabsView: function (values) {
    //    var mID;
    //    if (values != null) { mID = values;}
    //    $.ajax({
    //        url: "/ClassCategories/ClassCatagoryGridRowChange",
    //        type: "POST",
    //        data: { ID: mID },
    //        success: function (data) {
    //            $("#ClassCategoriesSplitter_1_CC").html(data);
    //        },
    //        error: function () {
    //        }
    //    });
    //}
}