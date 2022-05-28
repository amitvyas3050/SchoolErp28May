var vehicleRTOController = {

    splitterResized: function (s, e) {
        //alert(e.pane.name);
       
        switch (e.pane.name) {
            case 'LeftPane':
                alert(e.pane.GetClientHeight());
                GridBusDetail.SetHeight(e.pane.GetClientHeight() - 1);
                break;
            case 'RightPane':
               // GridAbsentStudent.SetHeight(e.pane.GetClientHeight() - 1);
                break;

            
        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'BusID', vehicleRTOController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {

        var busID = -1;
        if (values != null) {
            busID = values;
        }

        $.ajax({
            url: "/VehicleRTO/GetVehicleRowSelectionView",
            type: "POST",
            data: { mBusID: busID },
            success: function (data) {
                $("#VehicleRTOSplitter_1i0_CC").html(data);
                $("#VehicleRTOSplitter_1i1_CC").html(null);

            },
            error: function () {
            }
        });
    },


    ButtonClick: function (s, e) {
       
        var busID = BusID.GetText();
        if (busID == null) {
            alert("");
        }

        $.ajax({
            url: "/VehicleRTO/GetVehicleTabsView",
            type: "POST",
            data: { mBusID: busID },
            beforeSend: (function (data) {
                loadingPanelRTO.Show();
            }),
            success: function (data) {
                $("#VehicleRTOSplitter_1i1_CC").html(data);

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelRTO.Hide();

        });

    }


   
    //ItemClicked: function (s, e) {
    //    txtclassName.SetText(e.item.GetText());
    //    $.ajax({
            
    //        url: "/AttendanceDatewise/GetTemplateMesssageText",
    //        type: "POST",
    //        data: { mTemplateID: e.item.name },
    //        success: function (data) {
    //            //alert(data);
    //            //ClassSetupID.SetText(data);
    //            $("#AttendanceDatewiseLFSplitter_0_CC").html(data);
    //            var winHeight = document.getElementById('AttendanceDatewiseLFSplitter_0_CC').offsetHeight;
    //            GridAttendance.SetHeight(winHeight - 10);
    //        },
           
    //        error: function () {
    //        }
    //    });
        
    //},

   


   


    }
