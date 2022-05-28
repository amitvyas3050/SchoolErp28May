var homeController = {
    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'HomeLeft':
               // GridHouses.SetHeight(e.pane.GetClientHeight());
                break;
                case 'HomeRight':
                  //  tabClassRecord.SetWidth(e.pane.GetClientWidth());
                    break;
        }
    }

}
