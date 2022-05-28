var sendParentIDPasswordController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'SendMessegeWishesBody':
                // GridSendMessege.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendMessegeWishesFooter':
                GridSendParentIDPassword.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendMessegeText':
                GridSendParentIDPassword.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendMessegeTemplate':
                GridSMSTemplate.SetHeight(e.pane.GetClientHeight());
                break;
        }
    },
   

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
    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'TemplateID', sendParentIDPasswordController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var nID;
        //var templatetype = TemplateType.GetText();
       
        if (values != null)
        {
            nID = values;
            //alert(nID);
        }
        $.ajax({
            url: "/SendParentIDPassword/GetTemplateMesssageText",
            type: "POST",
            data: { mTemplateID: nID },
            success: function (data) {

                // smsTextEnglish.SetText(data);
              
                $("#SendMessegeSplitterHead_1_CC").html(data);
                //TemplateType.SetText(templatetype.toString());

                var templatetype = TemplateType.GetText();
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
       
        s.GetSelectedFieldValues("FatherMobileNo", sendParentIDPasswordController.GetSelectedFieldValuesCallback);
        s.GetSelectedFieldValues("StudentID", sendParentIDPasswordController.GetSelectedFieldValuesCallbackForStudentID);
      
    },


    GetSelectedFieldValuesCallback: function (values) {
       //alert(values);
        smsMobileNo.SetText(values);
     
    },
    GetSelectedFieldValuesCallbackForStudentID: function (values) {
        //alert(values);
        smsStudentID.SetText(values);

    },
   

    SelectionChangedForClasses: function (s, e) {

        s.GetSelectedFieldValues("ClassSetupID", sendParentIDPasswordController.GetSelectedFieldValuesCallbackForClasses);

    },
    GetSelectedFieldValuesCallbackForClasses: function (values) {
        
         txtclassSetup.SetText(values);
        // $("txtclassSetup").val(values);

    },

    ClickLoadGrid: function (s, e) {
        var classID = txtwishesType.GetText();
      

        //if (classID.toString() == "" ) {
        //    alert("Please Select Type");
        //    return;
        //}

        $.ajax({
            url: "/SendParentIDPassword/GetStudentListForSMS",
            type: "POST",
            data: { mClassesID: classID.toString() },
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
        TemplateType.SetText(mType);


    },

    IncludeNameAndClass: function (s, e) {
        if (s.GetChecked()) {
            var MessageTemp = smsTextEnglish.GetText();
            MessageTemp = MessageTemp + "$Name$" + "$LoginID$" + "$Password$";
            smsTextEnglish.SetText(MessageTemp);

            var MessageTempHindi = smsTextHindi.GetText();
            MessageTempHindi = MessageTempHindi + "$Name$" + "$LoginID$" + "$Password$";
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

    },





    keyPressForWordCountEnglish: function (s, e) {

        var myText = s.GetText();
        //var pretext = PrefixEnglish.GetText();
        myText = myText ;
        // alert(myText);
        //alert(myText.length);
        // $("#lblCharCount_I").val("");

        var mLen = myText.length;
        var mText = "Characters:" + mLen.toString() + " SMS:" + Math.ceil(mLen / 160).toString();
        $("#lblCharCount_I").val(mText);
    },

    keyPressForWordCountHindi: function (s, e) {

        var myText = s.GetText();
        //var pretext = PrefixHindi.GetText();
        myText = myText ;
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


    }
}