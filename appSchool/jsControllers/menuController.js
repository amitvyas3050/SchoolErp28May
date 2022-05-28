var menuController = {

    
  
    keyPressmenufilter: function (s, e) {

        var filtertext = s.GetText();


        if (1 > 2)
        {
            $.ajax({
                url: "/Menu/GetModuleListFiler",
                type: "POST",
                data: { filtertext: filtertext.toString() },
                beforeSend: (function (data) {
                    loadingPanelmenu.Show();
                }),
                success: function (data) {
                    $("#maincontentpage").html(data);
                },
                error: function () {
                }
            }).done(function (data) {
                loadingPanelmenu.Hide();

            });
        }

       
        

    },

    menufilterbyclick: function (s, e) {

        var filtertext = txtfiltermenu.GetText();


        if (filtertext.length > 1) {
            $.ajax({
                url: "/Menu/GetModuleListFiler",
                type: "POST",
                data: { filtertext: filtertext.toString() },
                beforeSend: (function (data) {
                    loadingPanelmenu.Show();
                }),
                success: function (data) {
                    $("#maincontentpage").html(data);
                },
                error: function () {
                }
            }).done(function (data) {
                loadingPanelmenu.Hide();

            });
        }
        else if (filtertext.length == 0) {
            $.ajax({
                url: "/Menu/GetModuleListFilerClear",
                type: "POST",
                data: {},
                beforeSend: (function (data) {
                    loadingPanelmenu.Show();
                }),
                success: function (data) {
                    $("#maincontentpage").html(data);
                },
                error: function () {
                }
            }).done(function (data) {
                loadingPanelmenu.Hide();

            });
        }



    },

   


}