var smsManagerController = {

    splitterResized: function (s, e) {
       
        switch (e.pane.name) {
            case 'SMSTypeBody':
                GridSMSType.SetHeight(e.pane.GetClientHeight());
                break;
        }
    }

   
}