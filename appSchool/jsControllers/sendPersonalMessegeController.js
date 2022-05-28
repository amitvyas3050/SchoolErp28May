var sendPersonalMessegeController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'SendMessegeBody':
                // GridSendMessege.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendMessegeFooter':
                GridSendMessege.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendMessegeText':
                GridSendMessege.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendMessegeClass':
                GridClassSetup.SetHeight(e.pane.GetClientHeight()-30);
                break;
            case 'SendMessegeTemplate':
                GridSMSTemplate.SetHeight(e.pane.GetClientHeight());
                break;
        }
    },
   

  
    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'TemplateID', sendPersonalMessegeController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var nID;
        if (values != null)
        {
            nID = values;
            //alert(nID);
        }
        $.ajax({
            url: "/SendPersonalMessege/GetTemplateMesssageText",
            type: "POST",
            data: { mTemplateID: nID },
            success: function (data) {

                // smsTextEnglish.SetText(data);
              
                $("#SendMessegeSplitterHead_1_CC").html(data);
                $("#SendMessegeSplitterHead_1_CC").html(data);
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
     

        s.GetSelectedFieldValues("MobileNO", sendPersonalMessegeController.GetSelectedFieldValuesCallback);


        s.GetSelectedFieldValues("PersonalPersonID", sendPersonalMessegeController.GetSelectedFieldValuesCallbackForStudentID);
       
     
    },
    GetSelectedFieldValuesCallback: function (values) {
       // alert(values);
        MobileNO.SetText(values);
     
    },
    GetSelectedFieldValuesCallbackForStudentID: function (values) {
        // alert(values);
        smsStudentID.SetText(values);

    },



    SelectionChangedForClasses: function (s, e) {

        s.GetSelectedFieldValues("CourseID", sendDroOutMessegeController.GetSelectedFieldValuesCallbackForClasses);

    },
    GetSelectedFieldValuesCallbackForClasses: function (values) {
        
         txtclassSetup.SetText(values);
        // $("txtclassSetup").val(values);

    },

    ClickLoadGrid: function (s, e) {
        var classID = txtclassSetup.GetText();
      

        if (classID.toString() == "" ) {
            alert("Please Select Course");
            return;
        }
        $.ajax({
            url: "/SendMessege/GetStudentListForSMS",
            type: "POST",
            data: { mClassesID: classID.toString() },
            success: function (data) {
                $("#SendMessegeSplitterBottom_1_CC").html(data);
            },
            error: function () {
            }
        });



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
            MessageTemp = MessageTemp + "$Name$ $Class$ ";
            smsTextEnglish.SetText(MessageTemp);

            var MessageTempHindi = smsTextHindi.GetText();
            MessageTempHindi = MessageTempHindi + "$Name$ $Class$ ";
            smsTextHindi.SetText(MessageTempHindi);



        }
        else {
            var MessageTemp = smsTextEnglish.GetText();
            var res = MessageTemp.replace("$Name$", "");
            res = res.replace("$Class$", "");
            smsTextEnglish.SetText(res);

            var MessageTempHindi = smsTextHindi.GetText();
            var res = MessageTempHindi.replace("$Name$", "");
            res = res.replace("$Class$", "");
            smsTextHindi.SetText(res);



        }

    }

}