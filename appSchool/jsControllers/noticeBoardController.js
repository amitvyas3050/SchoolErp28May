var noticeBoardController = {

    splitterResized: function (s, e) {
       
        switch (e.pane.name) {
            
            case 'NoticeBody':
                gvEditing1.SetHeight(e.pane.GetClientHeight());
               
                break;
            //case 'ClassesFooter':
            //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
            //    break;
        }
    },

   
    AddNewClicked: function (s, e) {
        $.ajax({
            url: "/NoticeBoard/ExternalEditFormEdit",
            type: "POST",
            data: { mNoticeID: -1 },
            success: function (data) {
                $("#NoticeSplitter_1_CC").html(data);
                var winHeight = document.getElementById('NoticeSplitter_1_CC').offsetHeight;

                gvEditing1.SetHeight(winHeight);
            },
            error: function () {
            }
        });
    },


    EditClicked: function (s, e) {

        rowIdx = TXTNoticeID.GetValue();
        $.ajax({
            url: "/NoticeBoard/ExternalEditFormEdit",
            type: "POST",
            data: { mNoticeID: rowIdx },
            success: function (data) {
                $("#NoticeSplitter_1_CC").html(data);
                var winHeight = document.getElementById('NoticeSplitter_1_CC').offsetHeight;

                gvEditing1.SetHeight(winHeight );
            },
            error: function () {
            }
        });
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'NoticeID', noticeBoardController.NoticeView);


    },

    NoticeView: function (values) {
        
        if (values != null) {
           TXTNoticeID.SetText(values);
        }

    },
   

    Cancle: function (s, e) {
        $.ajax({
            url: "/NoticeBoard/Cancle",
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