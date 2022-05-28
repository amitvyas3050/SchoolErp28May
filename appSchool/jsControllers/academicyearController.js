var academicyearController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'AcademicyearBody':
                GridAcademicyear.SetHeight(e.pane.GetClientHeight() - 2);
                GridAcademicyear.SetWidth(e.pane.GetClientWidth() - 2);
                break;
         
        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {

        s.GetRowValues(s.GetFocusedRowIndex(), 'SessionId', academicyearController.RefreshTabsView);
    },
  
}