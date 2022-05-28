var classTimeTableController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'ClassTimeTableBody':
           
                break;
            case 'ClassTimeTableFooter':
                GridTimeTable.SetHeight(e.pane.GetClientHeight()-10);
                break;

        }
    },

    btnClickAddTimeTableList: function (s, e) {
        var mClassID = ClassID.GetValue();
        if (mClassID == null) {
            alert("Please Select Class");
            return;
        }
        $.ajax({
            url: "/ClassTimeTable/GetClassTimeTable",
            type: "POST",
            datatype:"json",
            data: { ClassID: mClassID },

            beforeSend: (function (data) {
                loadingPanelClassTimeTable.Show();
            }),


            success: function (data) {
              
                alert(data.Displaymsg);

                //$("#tdlbl").empty();

                //var html = '<h4>' + data.ErrorMessage + '</h4>';
                //$('#tdlbl').append(html);
               
                 $("#ClassTimeTableSplitter_1_CC").html(data.ListData);
                 var winHeight = document.getElementById('ClassTimeTableSplitter_1_CC').offsetHeight;
                 GridTimeTable.SetHeight(winHeight - 1);
               


            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelClassTimeTable.Hide();

        });

      

    },

   



   

  


}