var photogallerydetailController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'PhotoGalleryDetailBoardBody':
               
                break;
            case 'PhotoGalleryDetailFooter':
                GridPhotoGallery.SetHeight(e.pane.GetClientHeight() - 10);
                break;
        }
    },



    OnSelect: function (s, e) {

        //var StudStatus = rbtnStudStatus.GetValue();

        //if (StudStatus == 1) {
            

        //    ClassID.SetEnabled(false);
        //    //YearID.SetEnabled(false);
        //    //SemID.SetEnabled(false);
        //}

        //if (StudStatus == 2) {


        //    ClassID.SetEnabled(true);
        //    //YearID.SetEnabled(true);
        //    //SemID.SetEnabled(true);
        //}

    },

    GetPhotoDetail: function (s, e) {

        //var mToDate = EventDate.GetDate().toDateString();
        //alert(mToDate)
        var mGalleryID = GalleryID.GetValue();
        if (mGalleryID == null) {
            alert("Please Select GalleryID.");
            return;
        }

      
        $.ajax({
            url: "/PhotoGalleryDetail/GetphotoDetail",
            type: "POST",
            datatype: "json",
            data: { pGalleryID: mGalleryID },
            beforeSend: (function (data) { loadingPanelTopper.Show(); }),
            success: function (data) {

                
            
                    //alert(data.ErrorMsg);
                    Event.SetText(data.EvenetName);
                    EventDate.SetText(data.EventDate);
                    //alert(data.FromDate);
                    FromDate.SetText(data.FromDate);
                    ToDate.SetText(data.ToDate);
                    EventDesc.SetText(data.EveneDesc);
                    //ClassID.SetText("");
                    //YearID.SetText("");
                    //SemID.SetText("");
                    //Rank.SetText("");
                    //Percantage.SetText("");
                

            },
            error: function () {

            }

        }).done(function (data) { loadingPanelTopper.Hide();});
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

    DeleteFolder: function (s, e) {

        var r = confirm("Confirm to delete!");
        if (r == true) {
           
            var mGalleryID = GalleryID.GetValue();
            //alert(mGalleryID)
            if (mGalleryID == null) {
                alert("Please Select Folder.");
                return;
            }

        } else {
            txt = "You pressed Cancel!";
        }


        


        $.ajax({
            url: "/PhotoGalleryDetail/DeleteFolderDetail",
            type: "POST",
            datatype: "json",
            data: { pGalleryID: mGalleryID},
            success: function (data) {
                alert(data.ErrorMsg);

                //$("#PhotoGallerySplitter_0_CC").html(data.FileMgrData);
                //var winHeight = document.getElementById('PhotoGallerySplitter_1_CC').offsetHeight;
                //fileManagerGallery.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        });

    },


    ClickSubmitbutton: function (s, e) {

      var mGalleryID = GalleryID.GetValue();
        //alert(mGalleryID)
            if (mGalleryID == null) {
                alert("Please Select Folder.");
                return;
            }

            var mEvent = Event.GetValue();
            if (mEvent == null) {
                alert("Please Enter Event.");
                return;
            }
           

            var mEventDesc = EventDesc.GetValue();
            if (mEventDesc == null) {
                alert("Please Select EventDesc.");
                return;
            }

            var mFromDate = FromDate.GetDate().toDateString();
            if (mFromDate == null) {
                alert("Please Select FromDate.");
                return;
            }

            var mToDate = ToDate.GetDate().toDateString();
            if (mToDate == null) {
                alert("Please Select ToDate.");
                return;
            }
           
            var mEventDate = EventDate.GetDate().toDateString();
            if (mEventDate == null) {
                alert("Please Select ToDate.");
                return;
            }



            $.ajax({
                url: "/PhotoGalleryDetail/UpdatePhotoDetail",
                type: "POST",
                datatype: "json",
                data: { pGalleryID: mGalleryID, pEvent: mEvent, pEventDesc: mEventDesc, pFromDate: mFromDate, pToDate: mToDate, pEventdate: mEventDate },
                beforeSend: (function (data) { loadingPanelTopper.Show(); }),
                success: function (data) {

                    //if (data.Status == false) {
                    //    alert(data.ErrorMsg);
                    //}
                    
                    alert(data.ErrorMsg);
                        
                        $("#PhotoGalleryDetailSplitter_1_CC").html(data.ListData);
                        var winHeight = document.getElementById('PhotoGalleryDetailSplitter_1_CC').offsetHeight;
                        GridPhotoGallery.SetHeight(winHeight - 10);
                    
                },
                error: function () {

                }

            }).done(function (data) { loadingPanelTopper.Hide(); });
        },
   
    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'StudentID', topperNoticeBoardController.RefreshTabsView);
        s.GetRowValues(s.GetFocusedRowIndex(), 'ClassID', topperNoticeBoardController.RefreshTabsClassIDView);
    },


    RefreshTabsClassIDView: function (values) {
        var regID;
        //alert(values)
       
        ClassIDs.SetText(values);
        
    },

    RefreshTabsView: function (values) {
        var regID;
        //alert(values)

        StudentID.SetText(values);

    },

    //GetYearList: function (s, e) {
    //    var mClassID = s.GetValue();
    //    if (mClassID == null) {
    //        alert("Select Branch");
    //        return;
    //    }
      
    //    $.ajax({
    //       url: "/TopperNoticeBoard/GetYearList",
    //        type: "POST",
    //        datatype: "json",
    //        data: { PClassID:mClassID},
    //        beforeSend: function (data) { loadingPanelTopper.Show(); },
    //        success: function (data) {
               
    //        },
    //        error: function (data) {

    //        }

    //    }).done(function (data) { loadingPanelTopper.Hide(); });


    //},
    SelectYear: function (s, e) {
        var mYearID = s.GetValue();
        if (mYearID == null) {
            alert("Select Year");
            return;
        }

        $.ajax({
            url: "/TopperNoticeBoard/GetSemList",
            datatype: "json",
            type: "POST",
            data:{PYearID:mYearID },
            beforeSend: function (data) { loadingPanelTopper.Show(); },
            success: function (data) {
                if (data.Status) {
                    alert(data.ErrorMsg);
                }
                else {
                    var aa = JSON.parse(data.DataList);
                    SemID.ClearItems();
                    for (var j = 0; j < aa.length; j++) {
                        SemID.AddItem(aa[j].SemName, aa[j].SemID);
                    }
                }
            },
            error: function (data) {

            }

        }).done(function (data) { loadingPanelTopper.Hide(); });


    }


   

}