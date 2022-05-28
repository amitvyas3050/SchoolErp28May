var sendGeneralMessegeController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'SendGeneralMessegeBody':
                // GridSendMessege.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendGeneralMessegeFooter':
                GridSendGeneralMessege.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendMessegeText':
                GridSendGeneralMessege.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendMessegeClass':
                GridClassSetup.SetHeight(e.pane.GetClientHeight()-30);
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
    CloseGridLookup: function () {
        gridLookup.ConfirmCurrentSelection();
        gridLookup.HideDropDown();

        sendGeneralMessegeController.ClickLoadStudentGrid();
    },


    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'TemplateID', sendGeneralMessegeController.RefreshTabsView);
    },


    
    SelectedTemplate: function(s, e) {

        var templateid = s.GetValue();
        sendGeneralMessegeController.RefreshTabsView(templateid);
    },
    

    RefreshTabsView: function (values) {
        var nID;
        if (values != null)
        {
            nID = values;
            //alert(nID);
        }
        $.ajax({
            url: "/SendGeneralMessege/GetTemplateMesssageText",
            type: "POST",
            data: { mTemplateID: nID },
            success: function (data) {

                // smsTextEnglish.SetText(data);
              
                $("#SendMessegeSplitterHead_1_CC").html(data);


                var myTextEng = smsTextEnglish.GetText();
                var pretextEng = PrefixEnglish.GetText();
                myTextEng = myTextEng + pretextEng;
                // alert(myText);
                //alert(myText.length);
                // $("#lblCharCount_I").val("");

                var mLenEng = myTextEng.length;
               
                var mTextEng = "Characters:" + mLenEng.toString() + " SMS:" + Math.ceil(mLenEng / 160).toString();
                $("#lblCharCount_I").val(mTextEng);


                var myTextHindi = smsTextHindi.GetText();
                var pretextHindi = PrefixHindi.GetText();
                myTextHindi = myTextHindi + pretextHindi;
                // alert(myText);
                //alert(myText.length);
                // $("#lblCharCount_I").val("");

                var mLenHindi = myTextHindi.length;
              
                var mTextHindi = "Characters:" + mLenHindi.toString() + " SMS:" + Math.ceil(mLenHindi / 70).toString();
                $("#lblCharCountHindi_I").val(mTextHindi);


            },
            error: function () {
            }
        });
    },

    SelectionChanged: function (s, e) {
       
        s.GetSelectedFieldValues("SMSMobileNo", sendGeneralMessegeController.GetSelectedFieldValuesCallback);

        s.GetSelectedFieldValues("StudentID", sendGeneralMessegeController.GetSelectedFieldValuesCallbackForStudentID);
     
    },
    GetSelectedFieldValuesCallback: function (values) {
       // alert(values);
        smsMobileNo.SetText(values);
     
    },

    GetSelectedFieldValuesCallbackForStudentID: function (values) {
        // alert(values);
        smsStudentID.SetText(values);

    },

    SelectionChangedForClasses: function (s, e) {

        s.GetSelectedFieldValues("ClassSetupID", sendGeneralMessegeController.GetSelectedFieldValuesCallbackForClasses);

    },
    GetSelectedFieldValuesCallbackForClasses: function (values) {
        
         txtclassSetup.SetText(values);
        // $("txtclassSetup").val(values);


    },
    filtermenubyname: function () {

        alert()

    },








    //ClickLoadGrid: function (s, e) {
        ClickLoadStudentGrid: function () {
        var classID = txtclassSetup.GetText();
      

        if (classID.toString() == "" ) {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/SendGeneralMessege/GetStudentListForSMS",
            type: "POST",
            data: { mClassesID: classID.toString() },
            beforeSend: (function (data) {
                loadingPanelSmscomman.Show();
            }),
            success: function (data) {
                $("#divsmsstudent").html(data);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelSmscomman.Hide();

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
     

        if (GetIndex == 1) {  //english 


            ///****new code block text box ****/////
            var divhindi = document.getElementById('divhindismstext');
            divhindi.style.display = "none";
            var diveng = document.getElementById('divengsmstext');
            diveng.style.display = "block";
             ///****new code block text box ****/////


            var div3 = document.getElementById('lblengheader');
            div3.style.color = "Red";
            div3.style.fontWeight = 'bold';

            var div4 = document.getElementById('lblhindiheader');
            div4.style.color = 'Black';


           

        }
        else  // 2 for //hindi 
        {
            ///****new code block text box ****/////
            var divhindi = document.getElementById('divhindismstext');
            divhindi.style.display = "block";
            var diveng = document.getElementById('divengsmstext');
            diveng.style.display = "none";
            ///****new code block text box ****/////

            var div1 = document.getElementById('lblhindiheader');
            div1.style.color = 'Red';
            div1.style.fontWeight = 'bold';

            var div2 = document.getElementById('lblengheader');
            div2.style.color = "Black";

        }


    }

  


}