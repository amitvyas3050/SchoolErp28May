var feeManagerController = {
    splitterResized: function (s, e) {
        switch (e.pane.name) {
            case 'FeeCategoryBody':
                GridFeeCategory.SetHeight(e.pane.GetClientHeight());
                break;
        }
    }

}
