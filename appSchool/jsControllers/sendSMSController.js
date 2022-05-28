var sendSMSController = {

    splitterResized: function (s, e) {
        //alert(e.pane.name);
        switch (e.pane.name) {
            case 'RightPane':
                tabSelectRecipients.SetHeight(e.pane.GetClientHeight()-10);
                tabSelectRecipients.SetWidth(e.pane.GetClientWidth()-10);
                break;
            
        }
    },
    ItemClicked: function (s, e) {
        txtTemplateName.SetText(e.item.GetText());
        txtStudentPanelTitle.SetText("Select Students recipients for "+e.item.GetText());
        $.ajax({
            url: "/SMSTemplate/GetTemplateMesssageText",
            type: "POST",
            data: { mTemplateID: e.item.name },
            success: function (data) {
              
                txtMessage.SetText(data);
            },
            error: function () {
            }
        });
        
    },
    StudentSelectionOptionChanged: function (s, e) {
        var choice = rbtnListStudentSelectionOption.GetItem(rbtnListStudentSelectionOption.GetSelectedIndex()).value;
        //alert(choice);
        $.ajax({
            url: "/SendSMS/GetStudentDDLPartialView",
            type: "POST",
            data: { mChoice: choice },
            success: function (data) {
                $("#divStudentDropDown").html(data);
                //txtMessage.SetText(data);
            },
            error: function () {
            }
        });
        }
    }
