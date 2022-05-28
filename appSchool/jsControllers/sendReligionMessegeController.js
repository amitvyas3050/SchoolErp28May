var sendReligionMessegeController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'SendReligionMessegeBody':
                // GridSendMessege.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendReligionMessegeFooter':
                GridSendGeneralMessege.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendMessegeText':
                GridSendReligionMessege.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendMessegeClass':
                GridClassSetup.SetHeight(e.pane.GetClientHeight()-30);
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
        sendReligionMessegeController.ClickLoadStudentGrid();
    },
    CloseReligionGridLookup: function () {
        gridLookupreligion.ConfirmCurrentSelection();
        gridLookupreligion.HideDropDown();
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'TemplateID', sendReligionMessegeController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var nID;
        if (values != null)
        {
            nID = values;
            //alert(nID);
        }
        $.ajax({
            url: "/SendReligionMessege/GetTemplateMesssageText",
            type: "POST",
            data: { mTemplateID: nID },
            success: function (data) {

                // smsTextEnglish.SetText(data);
              
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
       
        s.GetSelectedFieldValues("SMSMobileNo", sendReligionMessegeController.GetSelectedFieldValuesCallback);
        s.GetSelectedFieldValues("StudentID", sendReligionMessegeController.GetSelectedFieldValuesCallbackForStudentID);

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

        s.GetSelectedFieldValues("ClassSetupID", sendReligionMessegeController.GetSelectedFieldValuesCallbackForClasses);

    },
    GetSelectedFieldValuesCallbackForClasses: function (values) {
        
         txtclassSetup.SetText(values);
        // $("txtclassSetup").val(values);

    },

    SelectionChangedGridlookupForReligion: function (s, e) {
        s.GetSelectedFieldValues("ReligionName", sendReligionMessegeController.GetSelectedFieldNamesCallbackForReligion);
    },
    GetSelectedFieldNamesCallbackForReligion: function (values) {

        txtReligion.SetText(values);
        // $("txtclassSetup").val(values);

    },

    ClickLoadGrid: function (s, e) {
        var classID = txtclassSetup.GetText();
        var religion = txtReligion.GetText();

        if (classID.toString() == "" ) {
            alert("Please Select Classes");
            return;
        }
        if (religion.toString() == "") {
            alert("Please Select Religion");
            return;
        }
        alert(religion)

        $.ajax({
            url: "/SendReligionMessege/GetStudentListForSMS",
            type: "POST",
            data: { mClassesID: classID.toString(), mReligion: religion.toString() },
            success: function (data) {
                $("#SendMessegeSplitterBottom_1_CC").html(data);
            },
            error: function () {
            }
        });



    },


    ClickLoadStudentGrid: function () {
        var classID = txtclassSetup.GetText();
        var religion = txtReligion.GetText();

        if (classID.toString() == "") {
            alert("Please Select Classes");
            return;
        }
        if (religion.toString() == "") {
            alert("Please Select Religion");
            return;
        }

        $.ajax({
            url: "/SendReligionMessege/GetStudentListForSMS",
            type: "POST",
            data: { mClassesID: classID.toString(), mReligion: religion.toString() },
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
    SelectionChangedForReligion: function (s, e) {
        var aa = s.GetSelectedValues();
        $("#txtReligion_I").val(aa);
       
    },
   
    SelectedIdexChangeForLanguage: function (s, e) {

        var GetIndex = SMSLanguage.GetValue();
       // alert(GetIndex);

        if (GetIndex == 1) {
            var div1 = document.getElementById('dxLabelForPrefixHindi');
            div1.style.color = 'Red';

            var div2 = document.getElementById('dxLabelForPrefixEnglish');
            div2.style.color = "Black";
        }
        else
        {
            var div3 = document.getElementById('dxLabelForPrefixEnglish');
            div3.style.color = "Red";

            var div4 = document.getElementById('dxLabelForPrefixHindi');
            div4.style.color = 'Black';
        }


    }
}