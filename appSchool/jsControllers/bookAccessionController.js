var ShowFilterRow = false;

var bookAccessionController = {

    splitterResized: function (s, e) {
       
        switch (e.pane.name) {
            case 'BookAccessionMasterPane':
               
                GridBookMaster.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'BookAccessionMasterPane':
            
                GridBookDetail.SetHeight(e.pane.GetClientHeight() - 1);
                break;

        }
    },

    AddNewClicked: function (s, e) {
        GridBookMaster.AddNewRow();
    },

    EditClicked: function (s, e) {
        rowIdx = GridBookMaster.GetFocusedRowIndex();
        GridBookMaster.StartEditRow(rowIdx);
    },

    DeleteClicked: function (s, e) {
        rowIdx = GridBookMaster.GetFocusedRowIndex();
        GridBookMaster.DeleteRow(rowIdx);
    },


    FilterClicked: function (s, e) {
        ShowFilterRow = !ShowFilterRow;
        $.ajax({
            url: "/BookAccession/RefreshBookAccessionMasterGrid",
            type: "POST",
            data: { mShowFilter: ShowFilterRow },
            success: function (data) {
                $("#BookAccessionSplitter_1_CC").html(data);
                var winHeight = document.getElementById('BookAccessionSplitter_1_CC').offsetHeight;
                GridBookMaster.SetHeight(winHeight - 1);
            },
            error: function () {
            }
        });

    },


    ClickForAddDetail: function(s,e)
    {
        var mBookAccessionID = s.name;
        $.ajax({
            url: "/BookAccession/GetBookAccessionDetailGrid",
            type: "POST",
            data: { pBookAccessionID: mBookAccessionID },
            beforeSend: (function (data) {
                loadingPanelBookAccession.Show();
            }),

            success: function (data) {
                $("#BookAccessionSplitter_2_CC").html(data);
                var winHeight = document.getElementById('BookAccessionSplitter_2_CC').offsetHeight;
                GridBookDetail.SetHeight(winHeight - 1);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelBookAccession.Hide();
        });

    },


    ClickForAddPhoto: function (s, e) {
        alert(s.name);
        var pBookId = s.name;
        alert(s.name);
        $.ajax({
            url: "/BookAccession/GetBookDetailForPageCoverUpload",
            type: "POST",
            data: { mBookId: pBookId },
            beforeSend: (function (data) {
                loadingPanelBookAccession.Show();
            }),

            success: function (data) {
                $("#BookAccessionSplitter_3_CC").html(data);
              
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelBookAccession.Hide();
        });

    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        loadingPanelBookAccession.Show();
        s.GetRowValues(s.GetFocusedRowIndex(), 'BookId', bookAccessionController.RefreshTabsView);


    },

    RefreshTabsView: function (values) {
      
        var regID=0;
        if (values != null) {
            regID = values;
        }
        $.ajax({
            url: "/BookAccession/GetBookDetailForPageCoverUpload",
            type: "POST",
            data: { mBookId: regID },
            beforeSend: (function (data) {
               // loadingPanelBookAccession.Show();
            }),

            success: function (data) {
                $("#BookAccessionSplitter_3_CC").html(data);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelBookAccession.Hide();
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    },












    //RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
    //    s.GetRowValues(s.GetFocusedRowIndex(), 'BusID', vehicleRTOController.RefreshTabsView);
    //},

    //RefreshTabsView: function (values) {

    //    var busID = -1;
    //    if (values != null) {
    //        busID = values;
    //    }

    //    $.ajax({
    //        url: "/VehicleRTO/GetVehicleRowSelectionView",
    //        type: "POST",
    //        data: { mBusID: busID },
    //        success: function (data) {
    //            $("#VehicleRTOSplitter_1i0_CC").html(data);
    //            $("#VehicleRTOSplitter_1i1_CC").html(null);

    //        },
    //        error: function () {
    //        }
    //    });
    //},


    //ButtonClick: function (s, e) {
       
    //    var busID = BusID.GetText();
    //    if (busID == null) {
    //        alert("");
    //    }

    //    $.ajax({
    //        url: "/VehicleRTO/GetVehicleTabsView",
    //        type: "POST",
    //        data: { mBusID: busID },
    //        beforeSend: (function (data) {
    //            loadingPanelRTO.Show();
    //        }),
    //        success: function (data) {
    //            $("#VehicleRTOSplitter_1i1_CC").html(data);

    //        },
    //        error: function () {
    //        }
    //    }).done(function (data) {
    //        loadingPanelRTO.Hide();

    //    });

    //}


   
   

   


    }
