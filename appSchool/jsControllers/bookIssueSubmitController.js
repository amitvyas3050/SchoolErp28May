var ShowFilterRow = false;

var bookIssueSubmitController = {

    splitterResized: function (s, e) {
       
        switch (e.pane.name) {
            case 'MiddleBookIssueStudent':
                GridForSubmitBooks.SetHeight(e.pane.GetClientHeight() - 1);
                break;

            case 'BottomBookIssueStudent':
                GridTotalBookIssueSubmit.SetHeight(e.pane.GetClientHeight() - 1);
                break;

            case 'LeftTopPaneBookIssue':
                GridStudentList.SetHeight(e.pane.GetClientHeight() - 1);
                break;

            case 'LeftBottomPaneBookIssue':
              //  GridBookList.SetHeight(e.pane.GetClientHeight() - 1);
                break;

        }
    },

  
        SelectedIndexChanged: function (s, e) {
          
            s.GetRowValues(s.GetFocusedRowIndex(), 'StudentID', bookIssueSubmitController.RefreshTabsView);
            s.GetRowValues(s.GetFocusedRowIndex(), 'FullName', bookIssueSubmitController.SetFullName);

        },

    RefreshTabsView: function (values) {
    
  
        txtStudentID.SetText(values);
    },

    SetFullName: function (values) {
        txtStudentName.SetText(values);
   
    },

    SelectedIndexChangedForBook: function (s, e) {
      //  bookIssueSubmitController.ShowLoading;
    
        s.GetRowValues(s.GetFocusedRowIndex(), 'BookId', bookIssueSubmitController.RefreshTabsViewForBook);
        s.GetRowValues(s.GetFocusedRowIndex(), 'BookTitle', bookIssueSubmitController.SetBookTitle);
    
       // bookIssueSubmitController.HideLoading;
    },

    ShowLoading: function(data)
    {
        LoadingPanelBookIssue.Show();
    },

    HideLoading: function (data) {
        LoadingPanelBookIssue.Hide();
    },


    RefreshTabsViewForBook: function (values) {
        LoadingPanelBookIssue.Show();
        txtBookId.SetText(values);
    },

    SetBookTitle: function (values) {
        txtBookTitle.SetText(values);

        var winHeight = document.getElementById('BookIssueSubmitSplitter_1i1_CC').offsetHeight;
        GridForSubmitBooks.SetHeight(winHeight - 1);
        LoadingPanelBookIssue.Hide();
    },



 
        ClickForStudentDetail: function(s,e)
        {
            var mStudentID = s.name;

            $.ajax({
                url: "/BookIssueSubmit/GetStudentDetailView",
                type: "POST",
                data: { pStudentID: mStudentID },
                beforeSend: (function (data) {
                    LoadingPanelBookIssue.Show();
                }),

                success: function (data) {
                    $("#tblStudentDetail").html(data);
                    bookIssueSubmitController.GetBookListForSubmit(mStudentID);
                    bookIssueSubmitController.GetBookListForIssue(mStudentID);
                },
                error: function () {
                }
            }).done(function (data) {
                LoadingPanelBookIssue.Hide();
            });

    
       
        },

    
    ClickForBookDetail: function (s, e)
        {
            var mBookId = s.name;

            $.ajax({
                url: "/BookIssueSubmit/GetBookDetailView",
                type: "POST",
                data: { pBookId: mBookId },
                beforeSend: (function (data) {
                    LoadingPanelBookIssue.Show();
                }),

                success: function (data) {
                    $("#tblBookDetail").html(data);
                },
                error: function () {
                }
            }).done(function (data) {
                LoadingPanelBookIssue.Hide();
            });

    
       
        },


    GetBookListForSubmit: function(Value)
    {
      
        var mStudentID = Value;

        $.ajax({
            url: "/BookIssueSubmit/GetBookListForSubmitView",
            type: "POST",
            data: { pStudentID: mStudentID.toString() },
            beforeSend: (function (data) {
                LoadingPanelBookIssue.Show();
            }),

            success: function (data) {
                $("#BookIssueSubmitSplitter_1i1_CC").html(data);
                var winHeight = document.getElementById('BookIssueSubmitSplitter_1i1_CC').offsetHeight;
                GridForSubmitBooks.SetHeight(winHeight - 1);

            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelBookIssue.Hide();
        });



    },


    GetBookListForIssue: function (Value) {
        var mStudentID = Value;
      
        $.ajax({
            url: "/BookIssueSubmit/GetBookListForIssueView",
            type: "POST",
            data: { pStudentID: mStudentID.toString() },
            beforeSend: (function (data) {
                LoadingPanelBookIssue.Show();
            }),

            success: function (data) {
                $("#BookIssueSubmitSplitter_1i2_CC").html(data);
                var winHeight = document.getElementById('BookIssueSubmitSplitter_1i2_CC').offsetHeight;
                GridTotalBookIssueSubmit.SetHeight(winHeight - 1);
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelBookIssue.Hide();
        });
    },
 



    buttonClickForIssueBook: function (s, e)
    {
        var mStudentID = StudentID.GetText();
     
        if (mStudentID =="") {
            alert("Please Select Student.");
            return;
        }
        var mBookID = gridLookupBook.GetValue();
        if (mBookID == null) {
            alert("Please Select Book for Issue.");
            return;
        }
        var mIssueDate = IssueDate.GetDate().toDateString();
        alert(mIssueDate);
        if (mIssueDate =="") {
            alert("Please Select IssueDate.");
            return;
        }
       
        $.ajax({
            url: "/BookIssueSubmit/AddBookIssueByStudent",
            type: "POST",
            data: { pStudentID: mStudentID.toString(), pBookID: mBookID.toString(), pIssueDate: mIssueDate},
            beforeSend: (function (data) {
                LoadingPanelBookIssue.Show();
            }),
            success: function (data) {
                $("#BookIssueSubmitSplitter_1i1_CC").html(data);
                var winHeight = document.getElementById('BookIssueSubmitSplitter_1i1_CC').offsetHeight;
                GridForSubmitBooks.SetHeight(winHeight - 1);

                bookIssueSubmitController.GetBooksListinBookLookup();
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelBookIssue.Hide();
        });

    },


    GetBooksListinBookLookup: function()
    {
        alert("call");
        $.ajax({
            url: "/BookIssueSubmit/MultiSelectPartial",
            type: "POST",
            data: { },
        
            success: function (data) {
                alert(data);
                $("#divBookGridLookup").html(data);

                //var winHeight = document.getElementById('BookIssueSubmitSplitter_1i1_CC').offsetHeight;
                //GridForSubmitBooks.SetHeight(winHeight - 1);
            },

        });

    },


    CloseGridLookup: function() {
        gridLookup.ConfirmCurrentSelection();
gridLookup.HideDropDown();
},

    AddNewClicked: function(s, e)
{
    alert("dfsf");
    var a = gridLookupBook.GetValue();
    alert(a);
}


    

    //FilterClicked: function (s, e) {
    //    ShowFilterRow = !ShowFilterRow;
    //    $.ajax({
    //        url: "/BookAccession/RefreshBookAccessionMasterGrid",
    //        type: "POST",
    //        data: { mShowFilter: ShowFilterRow },
    //        success: function (data) {
    //            $("#BookAccessionSplitter_1_CC").html(data);
    //            var winHeight = document.getElementById('BookAccessionSplitter_1_CC').offsetHeight;
    //            GridBookMaster.SetHeight(winHeight - 1);
    //        },
    //        error: function () {
    //        }
    //    });

    //},


    //ClickForAddDetail: function(s,e)
    //{
    //    var mBookAccessionID = s.name;
    //    $.ajax({
    //        url: "/BookAccession/GetBookAccessionDetailGrid",
    //        type: "POST",
    //        data: { pBookAccessionID: mBookAccessionID },
    //        beforeSend: (function (data) {
    //            loadingPanelBookAccession.Show();
    //        }),

    //        success: function (data) {
    //            $("#BookAccessionSplitter_2_CC").html(data);
    //            var winHeight = document.getElementById('BookAccessionSplitter_2_CC').offsetHeight;
    //            GridBookDetail.SetHeight(winHeight - 1);
    //        },
    //        error: function () {
    //        }
    //    }).done(function (data) {
    //        loadingPanelBookAccession.Hide();
    //    });

    //},


    //ClickForAddPhoto: function (s, e) {
    //    alert(s.name);
    //    var pBookId = s.name;
    //    alert(s.name);
    //    $.ajax({
    //        url: "/BookAccession/GetBookDetailForPageCoverUpload",
    //        type: "POST",
    //        data: { mBookId: pBookId },
    //        beforeSend: (function (data) {
    //            loadingPanelBookAccession.Show();
    //        }),

    //        success: function (data) {
    //            $("#BookAccessionSplitter_3_CC").html(data);
              
    //        },
    //        error: function () {
    //        }
    //    }).done(function (data) {
    //        loadingPanelBookAccession.Hide();
    //    });

    //},

    //RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
    //    loadingPanelBookAccession.Show();
    //    s.GetRowValues(s.GetFocusedRowIndex(), 'BookId', bookAccessionController.RefreshTabsView);


    //},

    //RefreshTabsView: function (values) {
      
    //    var regID=0;
    //    if (values != null) {
    //        regID = values;
    //    }
    //    $.ajax({
    //        url: "/BookAccession/GetBookDetailForPageCoverUpload",
    //        type: "POST",
    //        data: { mBookId: regID },
    //        beforeSend: (function (data) {
    //           // loadingPanelBookAccession.Show();
    //        }),

    //        success: function (data) {
    //            $("#BookAccessionSplitter_3_CC").html(data);
    //        },
    //        error: function () {
    //        }
    //    }).done(function (data) {
    //        loadingPanelBookAccession.Hide();
    //    });
    //    //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
    //    //alert(values);
    //},












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
