var userPermissionController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'UserPermissionBody':
              //  GridStudentSession.SetHeight(e.pane.GetClientHeight());
                break;
            case 'UserPermissionFooter':
                GridUserPermission.SetHeight(e.pane.GetClientHeight() - 20);
                break;

            //case 'ClassesFooter':
            //    tabClassRecord.SetWidth(e.pane.GetClientWidth());
            //    break;
        }
    },

    SelectedUser: function (s, e) {
        // var classID = ClassSetupID.GetSelectedValues();
        var classID = s.GetValue();
        $("#txtUserName_I").val(classID);
        if (classID.toString() == "") {
            alert("Please Select User");
            return;
        }
       
    },

    SelectAllStudent: function (s, e) {

        if (s.GetChecked()) {
            alert(s.GetChecked());
            GridUserPermission.row[12].cells[5].checked = true;
          
        







           
        }
        else {
        
        }

    },



    CreateUserPermission: function (s, e) {
       
        // var classID = ClassSetupID.GetSelectedValues();
        var classID = txtUserName.GetValue();
        if (classID.toString() == "") {
            alert("Please Select User");
            return;
        }
        $.ajax({
            url: "/UserPermission/CreateUserPermission",
            type: "POST",
            data: { mUserID: classID.toString() },
            beforeSend: function () {
                LoadingPanelUserPermission.Show();
            },
            complete: function(){
                LoadingPanelUserPermission.Hide();
            },
          
            success: function (data) {
                $("#UserPermissionSplitter_1_CC").html(data);
                var winHeight = document.getElementById('UserPermissionSplitter_1_CC').offsetHeight;
              
                GridUserPermission.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        });

    },





    SetAllUserPermission: function (s, e) {
        // var classID = ClassSetupID.GetSelectedValues();
        var option = chkBoxLstAll.GetSelectedValues();
      
        var UserID = txtUserName.GetValue();
        if (UserID.toString() == "") {
            alert("Please Select User");
            return;
        }
        $.ajax({
            url: "/UserPermission/SetAllUserPermission",
            type: "POST",
            data: { mOption: option.toString(), mUserID:UserID.toString() },
            beforeSend: function () {
                LoadingPanelUserPermission.Show();
            },
            complete: function () {
                LoadingPanelUserPermission.Hide();
            },
            success: function (data) {
                //CallbackPanelUP.PerformCallback();

                $("#UserPermissionSplitter_1_CC").html(data);
                var winHeight = document.getElementById('UserPermissionSplitter_1_CC').offsetHeight;

                GridUserPermission.SetHeight(winHeight - 10);
            },
            error: function () {
            }
        });

    },














    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'PermitId', userPermissionController.RefreshTabsView);
    },

    RefreshTabsView: function (values) {
        var nID;
        if (values != null)
        {
            nID = values;
        }
        $.ajax({
            url: "/UserPermission/UserPermissionGridRowChange",
            type: "POST",
            data: { ID: nID },
            success: function (data) {
                $("#UserPermissionSplitter_1_CC").html(data);
            },
            error: function () {
            }
        });
    }
}