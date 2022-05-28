var topperNoticeBoardController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'TopperNoticeBoardBody':
               
                break;
            case 'StudentListRemarkFooter':
                GridTopperNoticeBoard.SetHeight(e.pane.GetClientHeight() - 10);
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

    OnSelecttopperType: function (s, e) {

        var TopperType = ToppersType.GetValue();

        if (TopperType == "Toppers") {
            Rank.SetText("0");
        }

},

    ClickGetStudent: function (s, e) {


        //var StudStatus = rbtnStudStatus.GetValue();
        //if (StudStatus == null) {
        //    alert("Please Select  StudentStatus.");
        //    return;
        //}

        //if (StudStatus == 1) {
        //    ClassID.SetText("0");
        //    //YearID.SetText("0");
        //    //SemID.SetText("0");

        //    ClassID.SetEnabled(false);
        //    //YearID.SetEnabled(false);
        //    //SemID.SetEnabled(false);
        //}


        //var mSessionID = SessionID.GetValue();
        //if (mSessionID == null) {
        //    alert("Please Select Session.");
        //    return;
        //}

        var mclassID = ClassID.GetValue();
        if (mclassID == null) {
            alert("Please Select Class.");
            return;
        }

        //var mYearID = YearID.GetValue();
        //if (mYearID == null) {
        //    alert("Please Select Year.");
        //    return;
        //}

        //var mSemID = SemID.GetValue();
        //if (mSemID == null) {
        //    alert("Please Select Semester.");
        //    return;
        //}

        $.ajax({
            url: "/TopperNoticeBoard/GetStudentDetailForRemark",
            type: "POST",
            datatype: "json",
            data: { pClassID: mclassID},
            beforeSend: (function (data) { loadingPanelTopper.Show(); }),
            success: function (data) {

                if (data.returnStatus == false) {
                    //alert(data.RetErrorMsg);
                }
                $("#TopperNoticeBoardSplitter_1_CC").html(data.ListData);
                var winHeight = document.getElementById('TopperNoticeBoardSplitter_1_CC').offsetHeight;
                GridTopperNoticeBoard.SetHeight(winHeight - 10);

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




    ClickSubmitbutton: function (s, e) {

       //var StudStatus = rbtnStudStatus.GetValue();
        //alert(StudStatus)

        //if (StudStatus == 1) {
        //    //StudentID.SetText("0");
         
            
        //}

        var mPercantage = Percantage.GetValue();
        if (mPercantage == null) {
            alert("Please Enter Percantage.");
                return;
            }
            var mRank = Rank.GetValue();
            if (mRank == null) {
                alert("Please Enter Rank.");
                return;
            }

            var mSession = Session.GetValue();
            if (mRank == null) {
                alert("Please Enter Session.");
                return;
            }

            var mStudentID = StudentID.GetValue();
            if (mStudentID == null) {
                alert("Please Select Student.");
                return;
            }

            var mClassID = ClassID.GetValue();
            //alert(mClassID)
            if (mClassID == null) {
                alert("Please Select Student.");
                return;
            }

            var mToppersType = ToppersType.GetValue();
            if (mToppersType == null) {
                alert("Please Enter Session.");
                return;
            }
            //var mFromDate = FromDate.GetDate().toDateString();
            //if (mFromDate == null) {
            //    alert("Please Select FromDate.");
            //    return;
            //}

            //var mToDate = ToDate.GetDate().toDateString();
            //if (mToDate == null) {
            //    alert("Please Select ToDate.");
            //    return;
            //}



            $.ajax({
                url: "/TopperNoticeBoard/AddTopperStudent",
                type: "POST",
                datatype: "json",
                data: { pStudentID: mStudentID, pClassID: mClassID, pPercantage: mPercantage,pToppersType:mToppersType,pRank: mRank, pSession: mSession },
                beforeSend: (function (data) { loadingPanelTopper.Show(); }),
                success: function (data) {

                    if (data.Status == false) {
                        alert(data.ErrorMsg);
                    }
                    else {
                        alert(data.ErrorMsg);
                        
                        StudentID.SetText("");
                        ClassID.SetText("");
                        //YearID.SetText("");
                        //SemID.SetText("");
                        Rank.SetText("");
                        Percantage.SetText("");
                    }
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