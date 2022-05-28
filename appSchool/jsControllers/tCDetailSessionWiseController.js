var tCDetailSessionWiseController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {

            case 'TCDetailSessionWiseBody':

                break;
            case 'TCDetailSessionFooter':
                GridTCDetailSessionWise.SetHeight(e.pane.GetClientHeight() - 10);
                break;

        }
    },

    GetStudentDetailSessionWise: function (s, e) {
        var sessionid = SessionId.GetValue();
        if (sessionid == null) {
            alert("Please Select Classes");
            return;
        }
        $.ajax({
            url: "/TCDetailSessionWise/GetAllStudentDetailSessionWise",
            type: "POST",
            data: { mSessionId: sessionid.toString() },
            beforeSend: (function (data) {
                loadingPanelTCAllotment.Show();
            }),
            success: function (data) {
                $("#TCDetailSessionWiseSplitter_1_CC").html(data);
                var winHeight = document.getElementById('TCDetailSessionWiseSplitter_1_CC').offsetHeight;
                GridTCDetailSessionWise.SetHeight(winHeight - 10);

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelTCAllotment.Hide();
        });
    },
}