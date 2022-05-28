var feesDefaulterController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'SendSMSAbsenteesBody':
                // GridSendMessege.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendSMSAbsenteesFooter':
                GridFeeDefaulter.SetHeight(e.pane.GetClientHeight());
                break;
        }
    },
   
    FindAbsenteesClicked: function (s, e) {
        //alert("hi")
        //mDate = SessionID.getvalues();
       
        $.ajax({
            url: "/FeesDefaulter/GetAllAbsenteesforGrid",
            type: "POST",
            //data: { mSessionID: mDate },
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


    //OnDateChanged: function (s, e)
    //{
    //   var newDate = s.GetDate().toDateString();
     
    //    $.ajax({      
    //        url: "/FeesReminder/GetAllAbsenteesforGrid",
    //        type: "POST",
    //        data: { mSessionID: newDate },
           
    //        success: function (data) {
    //            //alert(data);
    //            //$("#RightsmsAbsenteeID").html(data);
    //        },
    //        error: function () {
    //        }
    //    });


    //}
    //,

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

        s.GetRowValues(s.GetFocusedRowIndex(), 'TemplateID', feesDefaulterController.RefreshTabsView);
       

    },

    RefreshTabsView: function (values) {
        var nID = 0;
   
        if (values != null)
        {
            nID = values;
            //alert(nID);
        }
        $.ajax({
            url: "/FeesDefaulter/GetTemplateMesssageText",
            type: "POST",
            data: { mTemplateID: nID },
            beforeSend: (function (data) {
                loadingPanelSmscomman.Show();
            }),
            success: function (data) {

               // smsText.SetText(data);
                //divsmsstudent SendSMSAbsenteesSplitterHead_1_CC
              
                $("#divsmsstudent").html(data);

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelSmscomman.Hide();

        });
    },

    SelectionChanged: function (s, e) {
       
        s.GetSelectedFieldValues("StudentID", feesDefaulterController.GetSelectedFieldValuesCallback);
    },

    GetSelectedFieldValuesCallback: function (values) {
        smsMobileNo.SetText(values);
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

    IncludeNameAndClass: function (s, e) {
        if (s.GetChecked()) {
            var MessageTemp = smsTextEnglish.GetText();
            MessageTemp = MessageTemp + "$Name$ $TotalFee$ $Fine$ $Class$ $Term$";
            smsTextEnglish.SetText(MessageTemp);

            var MessageTempHindi = smsTextHindi.GetText();
            MessageTempHindi = MessageTempHindi + "$Name$ $TotalFee$ $Fine$ $Class$ $Term$";
            smsTextHindi.SetText(MessageTempHindi);



        }
        else {
            var MessageTemp = smsTextEnglish.GetText();
            var res = MessageTemp.replace("$Name$", "");
            res = res.replace("$TotalFee$", "");
            res = res.replace("$Fine$", "");
            res = res.replace("$Class$", "");
            res = res.replace("$Term$", "");

            smsTextEnglish.SetText(res);

            var MessageTempHindi = smsTextHindi.GetText();
            var res = MessageTempHindi.replace("$Name$", "");
            res = res.replace("$TotalFee$", "");
            res = res.replace("$Fine$", "");
            res = res.replace("$Class$", "");
            res = res.replace("$Term$", "");


            smsTextHindi.SetText(res);



        }

    }
  

}