var sendSMSAbsenteesController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'SendSMSAbsenteesBody':
                // GridSendMessege.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendSMSAbsenteesFooter':
                GridSendMessege.SetHeight(e.pane.GetClientHeight());
                break;
            case 'SendMessegeTemplate':
                GridSMSTemplate.SetHeight(e.pane.GetClientHeight());
                break;
        }
    },
   
    FindAbsenteesClicked: function (s, e) {
       
        mDate = AttendanceDate.getvalues();
       // alert(mDate)
        $.ajax({
            url: "/EmployeeSMSAbsentees/GetAllAbsenteesforGrid",
            type: "POST",
            data: { mAttendanceDate: mDate },
            success: function (data) {
                $("#SendSMSAbsenteesSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });

    },

    


    ClickLoadGrid: function (s, e) {
        //var newDate = s.GetDate().toDateString();

        var newDate = AttendanceDate.GetDate().toDateString();
       
        //alert(newDate)
        if (newDate.toString() == "") {
            alert("Please Select date");
            return;
        }
        ScheduledDate.SetValue(newDate);

        $.ajax({
            url: "/EmployeeSMSAbsentees/GetAllAbsenteesforGrid",
            type: "POST",
            data: { mAttendanceDate: newDate },
            beforeSend: (function (data) {
                loadingPanel.Show();
            }),

            success: function (data) {
                //alert(data);
                $("#divsmsstudent").html(data);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanel.Hide();

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

   
    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'TemplateID', sendSMSAbsenteesController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var nID;
        if (values != null)
        {
            nID = values;
            //alert(nID);
        }
        $.ajax({
            url: "/EmployeeSMSAbsentees/GetTemplateMesssageText",
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
       
        s.GetSelectedFieldValues("MobileNo", sendSMSAbsenteesController.GetSelectedFieldValuesCallback);
        s.GetSelectedFieldValues("TeacherID", sendSMSAbsenteesController.GetSelectedFieldValuesCallbackForStudentID);
    },

   
    GetSelectedFieldValuesCallback: function (values) {
        // alert(values);
        smsMobileNo.SetText(values);

    },
    GetSelectedFieldValuesCallbackForStudentID: function (values) {
        // alert(values);
        smsTeacherID.SetText(values);

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
            MessageTemp = MessageTemp + "$Name$ $EmpCode$ $Date$";
            smsTextEnglish.SetText(MessageTemp);

            var MessageTempHindi = smsTextHindi.GetText();
            MessageTempHindi = MessageTempHindi + "$Name$ $EmpCode$ $Date$";
            smsTextHindi.SetText(MessageTempHindi);



        }
        else {
            var MessageTemp = smsTextEnglish.GetText();
            var res = MessageTemp.replace("$Name$", "");
            res = res.replace("$EmpCode$", "");
            res = res.replace("$Date$", "");
            smsTextEnglish.SetText(res);

            var MessageTempHindi = smsTextHindi.GetText();
            var res = MessageTempHindi.replace("$Name$", "");
            res = res.replace("$EmpCode$", "");
            res = res.replace("$Date$", "");
            smsTextHindi.SetText(res);



        }

    }

  

}