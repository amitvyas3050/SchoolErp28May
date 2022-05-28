var feesStructureController = {

    splitterResized: function (s, e) {
        
        switch (e.pane.name) {
            case 'FeesStructureFooter':
                GridFeeTerm.SetHeight(e.pane.GetClientHeight() - 60);
              
                break;
            case 'ListFeesStructureFooter':
                GridFeesStructure.SetHeight(e.pane.GetClientHeight() - 60);
                break;

        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'FeesCategoryID', feesStructureController.RefreshTabsView);
    },
    RefreshTabsView: function (values) {
        var regID;
        if (values != null) {
            regID = values;
        }
    //$.ajax({
    //    url: "/FeesManager/FeesGridRowChange",
    //        type: "POST",
    //        data: {RegID:regID},
    //        success: function (data) {
    //            $("#ClassesSplitter_1_CC").html(data);
    //        },
    //        error: function () {
    //        }
    //    });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    },
    ClickLoadGrid: function (s, e) {
        var classID = txtclasses.GetText();
        var TermID = txtTerm.GetText();
      /*  GridFeeTerm.Visible = false;*/
      
        if (classID.toString() == "" || TermID=="") {
            alert("Please Select Classes/Term");
            return;
        }
        $.ajax({
            url: "/FeesStructure/GetFeesTermHeadGridData",
            type: "POST",
            data: { mClassesID: classID.toString(), mTermID: TermID.toString() },
            beforeSend: (function (data) {
                loadingPanelFeestructure.Show();
            }),
            success: function (data) {
                $("#divfeesstructure").html(data);
            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelFeestructure.Hide();

        });

       
      
    },

    SelectionChangedForTerm: function (s, e) {
       
        s.GetSelectedFieldValues("FeeTermID", feesStructureController.GetSelectedFieldValuesCallbackTerm);
     
    },
    GetSelectedFieldValuesCallbackTerm: function (values) {
        // alert(values);
       
        $("#txtTerm_I").val(values);
    },


    RefreshClasses: function (s, e) {
        //var classID = txtclasses.GetText();
        $.ajax({
            url: "/FeesStructure/GetFeesTermHeadGridDataRefresh",
            type: "POST",
            data: { mClassesID: classID.toString() },
            success: function (data) {
                $("#feestructurebottom").html(data);
          
              
            },
            error: function () {
            }
        });

    },

    SelectionChanged: function (s, e) {
        var aa = s.GetSelectedValues();
        $("#txtclasses_I").val(aa);
        $("#lblclasserror_I").val("");
        $.ajax({
            url: "/FeesStructure/GetClassDescription",
            type: "POST",
            data: { mClassSetupID: aa.toString() },
            success: function (data) {
                if (data == " " || data == null ||data=="[object XMLDocument]") {
                    $("#lblclasserror_I").val("");
                }
                else {
                   
                    $("#lblclasserror_I").val(data);
                }
            },
            error: function () {
            }
        });
        feesStructureController.SelectionChangedGetClassName(aa.toString());
    },

    SelectionChangedGetClassName: function (values) {
        
        $("#lblclassName_I").val("");
        $.ajax({
            url: "/FeesStructure/GetClassName",
            type: "POST",
            data: { mClassSetupID: values.toString() },
            success: function (data) {
                if (data == " " || data == null || data == "[object XMLDocument]") {
                    $("#lblclassName_I").val("");
                }
                else {
                    $("#lblclassName_I").val(data);
                }
            },
            error: function () {
            }
        });
    },
 



    GetSelectedFieldValuesCallback: function (values) {
     //alert(values);
     txtclasses.SetText(values);
     
},
  
    RowSelectionChangeforGridListFeesStructure: function OnGridFocusedRowChanged(s, e) {
        s.GetRowValues(s.GetFocusedRowIndex(), 'FeeStructID', feesStructureController.RefreshTabsViewForGridListFeeStructure);
        s.GetRowValues(s.GetFocusedRowIndex(), 'ClassName', feesStructureController.RefreshTabsViewForClassName);
      
    },
    RefreshTabsViewForGridListFeeStructure: function (values) {
        var mFeesStructID;

        if (values != null) {
            mFeesStructID = values;
          
        }
        $.ajax({
            url: "/FeesStructure/GetFeesStructureForEdit",
                type: "POST",
                data: { newFeesStructID: mFeesStructID },
                success: function (data) {
                    $("#Listfeestructurebottom").html(data);
                },
                error: function () {
                }
            });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    },

    RefreshTabsViewForClassName: function (values) {
      
     
        lblclassNameEdit.SetText("Class Name:" + values);
       
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    },

    CloseclassGridLookup: function () {
        gridLookup.ConfirmCurrentSelection();
        gridLookup.HideDropDown();
        
    },
    ClosetermGridLookup: function () {
        gridLookupterm.ConfirmCurrentSelection();
        gridLookupterm.HideDropDown();

    },


    SelectionChangedForClasses: function (s, e) {

        s.GetSelectedFieldValues("ClassID", feesStructureController.GetSelectedFieldValuesCallbackForClasses);

    },
    GetSelectedFieldValuesCallbackForClasses: function (values) {

        txtclasses.SetText(values);
        // $("txtclassSetup").val(values);


    },

    SelectionChangedForTerms: function (s, e) {

        s.GetSelectedFieldValues("FeeTermID", feesStructureController.GetSelectedFieldValuesCallbackForTerms);

    },
    GetSelectedFieldValuesCallbackForTerms: function (values) {

        txtTerm.SetText(values);
        // $("txtclassSetup").val(values);


    },

}