var ShowFilterRow = false;


var studentManagerController = {

    splitterResized: function (s, e) {
      
        switch (e.pane.name) {
            
            case 'StudentManagerBody':
               // GridStudentBirthday.SetHeight(e.pane.GetClientHeight());
              
                break;
            case 'StudentManagerFooter':
                GridStudentBirthday.SetHeight(e.pane.GetClientHeight());
                GridParentBirthday.SetHeight(e.pane.GetClientHeight());

                break;

                      
            }
        },


  


  




}