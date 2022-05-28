var schedulerWishesController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'SendMessegeWishesBody':
                // GridSendMessege.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendMessegeWishesFooter':
                GridSendMessegeWishes.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendMessegeText':
                GridSendMessegeWishes.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendMessegeTemplate':
                GridSMSTemplate.SetHeight(e.pane.GetClientHeight());
                break;
            
            
        }
    },
    CheckDate: function (s, e) {
        var newDate = s.GetDate();
        var oDateOne = new Date();

        if (newDate - oDateOne <= 0) {
            alert("Schedule Date can't be less than current Datetime");
        }


    },

   
    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'TemplateID', schedulerWishesController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var nID;
        var templatetype = TemplateType.GetText();

        if (values != null) {
            nID = values;
         
        }
        $.ajax({
            url: "/SchedulerWishes/GetTemplateMesssageText",
            type: "POST",
            data: { mTemplateID: nID },
            success: function (data) {

                // smsTextEnglish.SetText(data);
              
                $("#SendMessegeSplitterHead_1_CC").html(data);
                TemplateType.SetText(templatetype.toString());

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
       
        s.GetSelectedFieldValues("SMSMobileNo", schedulerWishesController.GetSelectedFieldValuesCallback);

        s.GetSelectedFieldValues("StudentID", schedulerWishesController.GetSelectedFieldValuesCallbackForStudentID);
     
    },
    GetSelectedFieldValuesCallback: function (values) {
       // alert(values);
        smsMobileNo.SetText(values);
     
    },
    GetSelectedFieldValuesCallbackForStudentID: function (values) {
        smsStudentID.SetText(values);

    },

    SelectionChangedForClasses: function (s, e) {

        s.GetSelectedFieldValues("ClassSetupID", schedulerWishesController.GetSelectedFieldValuesCallbackForClasses);

    },
    GetSelectedFieldValuesCallbackForClasses: function (values) {
        
         txtclassSetup.SetText(values);
        // $("txtclassSetup").val(values);

    },

    ClickLoadGrid: function (s, e) {
        var classID = txtwishesType.GetText();
        if (classID.toString() == "") {
            alert("Please Select WishesType.");
            return;
        }
        var mFromDate = FromDate.GetDate().toDateString();
        if (mFromDate.toString() == "") {
            alert("Please Select FromDate.");
            return;
        }

        var mToDate = ToDate.GetDate().toDateString();
        if (mToDate.toString() == "") {
            alert("Please Select ToDate.");
            return;
        }
        //if (mFromDate.toDateString() > mToDate.toDateString()) {
        //    alert("Selected Date is invalid.");
        //    return;
        //}

     
        $.ajax({
            url: "/SchedulerWishes/GetStudentListForSMS",
            type: "POST",
            data: { mClassesID: classID.toString(), pFromDate: mFromDate, pToDate: mToDate},
            success: function (data) {
                $("#SendMessegeSplitterBottom_1_CC").html(data);
            },
            error: function () {
            }
        });



    },
  

    SelectedWishType: function (s, e) {
    
        var mType = s.GetValue();
      
       // $("txtwishesType_I").val(Type);
        txtwishesType.SetText(mType);


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
    SelectedWishType: function (s, e) {

        var mType = s.GetValue();

        // $("txtwishesType_I").val(Type);
        txtwishesType.SetText(mType);
        TemplateType.SetText(mType);


    },
    IncludeName: function (s, e) {
        if (s.GetChecked()) {
            var MessageTemp = smsTextEnglish.GetText();
            MessageTemp = MessageTemp + "$Name$";
            smsTextEnglish.SetText(MessageTemp);

            var MessageTempHindi = smsTextHindi.GetText();
            MessageTempHindi = MessageTempHindi + "$Name$ ";
            smsTextHindi.SetText(MessageTempHindi);



        }
        else {
            var MessageTemp = smsTextEnglish.GetText();
            var res = MessageTemp.replace("$Name$", "");
         
            smsTextEnglish.SetText(res);

            var MessageTempHindi = smsTextHindi.GetText();
            var res = MessageTempHindi.replace("$Name$", "");
     
            smsTextHindi.SetText(res);



        }

    }
}