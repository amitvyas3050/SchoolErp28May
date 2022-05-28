var sendSMSAbsenteesController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'SendSMSAbsenteesBody':
                // GridSendMessege.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendSMSAbsenteesFooter':
                GridSendMessege.SetHeight(e.pane.GetClientHeight());
                break;
        }
    },
   
    FindAbsenteesClicked: function (s, e) {
       
        mDate = AttendanceDate.getvalues();
       // alert(mDate)
        $.ajax({
            url: "/SendSMSAbsentees/GetAllAbsenteesforGrid",
            type: "POST",
            data: { mAttendanceDate: mDate },
            success: function (data) {
                $("#SendSMSAbsenteesSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });

    },

    SelectionChangedForClasses: function (s, e) {

        s.GetSelectedFieldValues("ClassSetupID", sendSMSAbsenteesController.GetSelectedFieldValuesCallbackForClasses);

    },
    GetSelectedFieldValuesCallbackForClasses: function (values) {

        txtclassSetup.SetText(values);
        // $("txtclassSetup").val(values);


    },


    ClickLoadGrid: function (s, e) {
        //var newDate = s.GetDate().toDateString();

        var newDate = AttendanceDate.GetDate().toDateString();
        //alert(newDate)
        if (newDate.toString() == "") {
            alert("Please Select date");
            return;
        }
        ScheduledDate.SetText(newDate);
        
        var classID = txtclassSetup.GetText();
        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }


        $.ajax({
            url: "/SendSMSAbsentees/GetAllAbsenteesforGrid",
            type: "POST",
            data: { mAttendanceDate: newDate, mClassesID: classID.toString() },
            beforeSend: (function (data) {
                loadingPanelSmscomman.Show();
            }),
            success: function (data) {
                //alert(data);
                $("#divsmsstudent").html(data);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelSmscomman.Hide();

        });


    },


    OnDateChanged: function (s, e)
    {
       // var newDate = s.GetDate().toDateString();
       // var classID = txtclassSetup.GetText();
     
       //ScheduledDate.SetText(newDate);
       //if (classID.toString() == "") {
       //    alert("Please Select Classes");
       //    return;
       //}

     
       // $.ajax({      
       //     url: "/SendSMSAbsentees/GetAllAbsenteesforGrid",
       //     type: "POST",
       //     data: { mAttendanceDate: newDate, mClassesID: classID.toString() },
           
       //     success: function (data) {
       //         //alert(data);
       //         $("#RightsmsAbsenteeID").html(data);
       //     },
       //     error: function () {
       //     }
       // });


    }
    ,

    //GetSelectedFieldValuesCallback: function(values)
    //{
    //    SelectedRows.BeginUpdate();
    //    try {
    //        SelectedRows.ClearItems();
    //        for (var i = 0; i < values.length; i++) 
    //        {
    //            SelectedRows.AddItem(values[i]);
    //        }
    //    } 
    //    finally 
    //    {
    //        SelectedRows.EndUpdate();
    //    }
    //},

    CloseGridLookup: function () {
        GridSMSTemplate.ConfirmCurrentSelection();
        GridSMSTemplate.HideDropDown();
        //sendSMSAbsenteesController.ClickLoadGrid();
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'TemplateID', sendSMSAbsenteesController.RefreshTabsView);
    },

    SelectionChangedForClasses: function (s, e) {

        s.GetSelectedFieldValues("ClassSetupID", sendSMSAbsenteesController.GetSelectedFieldValuesCallbackForClasses);

    },
    GetSelectedFieldValuesCallbackForClasses: function (values) {

        txtclassSetup.SetText(values);
        // $("txtclassSetup").val(values);

    },


    SelectedTemplate: function (s, e) {

        var templateid = s.GetValue();
        sendSMSAbsenteesController.RefreshTabsView(templateid);
    },


    RefreshTabsView: function (values) {
        var nID;
        if (values != null)
        {
            nID = values;
            //alert(nID);
        }
        $.ajax({
            url: "/SendSMSAbsentees/GetTemplateMesssageText",
            type: "POST",
            data: { mTemplateID: nID },
            success: function (data) {

               // smsText.SetText(data);
              
                $("#SendSMSAbsenteesSplitterHead_1_CC").html(data);

                var myTextEng = smsTextEnglish.GetText();
                var pretextEng = PrefixEnglish.GetText();
                myTextEng = myTextEng + pretextEng;
                var mLenEng = myTextEng.length;

                var mTextEng = "Characters:" + mLenEng.toString() + " SMS:" + Math.ceil(mLenEng / 160).toString();
                $("#lblCharCount_I").val(mTextEng);


                var myTextHindi = smsTextHindi.GetText();
                var pretextHindi = PrefixHindi.GetText();
                myTextHindi = myTextHindi + pretextHindi;
                var mLenHindi = myTextHindi.length;

                var mTextHindi = "Characters:" + mLenHindi.toString() + " SMS:" + Math.ceil(mLenHindi / 70).toString();
                $("#lblCharCountHindi_I").val(mTextHindi);

            },
            error: function () {
            }
        });
    },

    SelectionChanged: function (s, e) {
       
        s.GetSelectedFieldValues("SMSMobileNo", sendSMSAbsenteesController.GetSelectedFieldValuesCallback);
        s.GetSelectedFieldValues("StudentID", sendSMSAbsenteesController.GetSelectedFieldValuesCallbackForStudentID);
    },
   
    GetSelectedFieldValuesCallback: function (values) {
        // alert(values);
        smsMobileNo.SetText(values);

    },
    GetSelectedFieldValuesCallbackForStudentID: function (values) {
        // alert(values);
        smsStudentID.SetText(values);

    },









    keyPressForWordCountEnglish: function (s, e) {

        var myText = s.GetText();
        var pretext = PrefixEnglish.GetText();
        myText = myText + pretext;
        // alert(myText);
        //alert(myText.length);
        // $("#lblCharCount_I").val("");

        var mLen = myText.length;
        var mText = "Characters:" + mLen.toString() + " SMS:" + Math.ceil(mLen / 160).toString();
        $("#lblCharCount_I").val(mText);
    },

    keyPressForWordCountHindi: function (s, e) {

        var myText = s.GetText();
        var pretext = PrefixHindi.GetText();
        myText = myText + pretext;
        // alert(myText);
        //alert(myText.length);
        // $("#lblCharCount_I").val("");

        var mLen = myText.length;
        var mText = "Characters:" + mLen.toString() + " SMS:" + Math.ceil(mLen / 70).toString();
        $("#lblCharCountHindi_I").val(mText);
    },



    SelectedIdexChangeForLanguage: function (s, e) {

        var GetIndex = SMSLanguage.GetValue();


        if (GetIndex == 1) {
            var div1 = document.getElementById('dxLabelForPrefixHindi');
            div1.style.color = 'Red';
            div1.style.fontWeight = 'bold';

            var div2 = document.getElementById('dxLabelForPrefixEnglish');
            div2.style.color = "Black";
        }
        else {
            var div3 = document.getElementById('dxLabelForPrefixEnglish');
            div3.style.color = "Red";
            div3.style.fontWeight = 'bold';

            var div4 = document.getElementById('dxLabelForPrefixHindi');
            div4.style.color = 'Black';
        }


    },
    IncludeNameAndClass: function (s, e) {
        if (s.GetChecked()) {
            var MessageTemp = smsTextEnglish.GetText();
            MessageTemp = MessageTemp + "$Name$ $Class$ $Date$";
            smsTextEnglish.SetText(MessageTemp);

            var MessageTempHindi = smsTextHindi.GetText();
            MessageTempHindi = MessageTempHindi + "$Name$ $Class$ $Date$";
            smsTextHindi.SetText(MessageTempHindi);



        }
        else {
            var MessageTemp = smsTextEnglish.GetText();
            var res = MessageTemp.replace("$Name$", "");
            res = res.replace("$Class$", "");
            res = res.replace("$Date$", "");
            smsTextEnglish.SetText(res);

            var MessageTempHindi = smsTextHindi.GetText();
            var res = MessageTempHindi.replace("$Name$", "");
            res = res.replace("$Class$", "");
            res = res.replace("$Date$", "");
            smsTextHindi.SetText(res);



        }

    }

  

}