var smsReportController = {

    splitterResized: function (s, e) {
       
        switch (e.pane.name) {
            case 'SentSMSReportBody':
              
                break;
            case 'SentSMSReportFooter':
                GridSMSReport.SetHeight(e.pane.GetClientHeight() - 1);
                break;
        }
    },
    
    buttonClick: function (s, e) {
        var a1 = FromDate.GetDate().toDateString();
        var a2 = ToDate.GetDate().toDateString();
        $.ajax({
            url: "/SentSMSReport/ListSMSReportView",
            type: "POST",
            data: { mDt1: a1.toString(), mDt2: a2.toString() },

            beforeSend: (function (data) {
                LoadingPanelSMSReport.Show();
            }),

            success: function (data) {
                //var a = $("#SMSReportSplitter_1_CC").GetClientHeight();
                //alert(a);
              
                $("#SMSReportSplitter_1_CC").html(data);
                var winHeight = document.getElementById('SMSReportSplitter_1_CC').offsetHeight;
                GridSMSReport.SetHeight(winHeight - 10);
                
            },
            error: function () {
            }
        }).done(function (data) {
            LoadingPanelSMSReport.Hide();
           
        });
    }
    ,


}