var feesTransactionController = {

    splitterResized: function (s, e) {
        
        switch (e.pane.name) {
            //case 'FeesStructureFooter':
            //  //  GridFeeTerm.SetHeight(e.pane.GetClientHeight() - 40);
            //    break;
            //case 'ListFeesStructureFooter':
            // //   GridFeesStructure.SetHeight(e.pane.GetClientHeight() - 40);
            //    break;

        }
    },

    RowSelectionChange: function OnGridFocusedRowChanged(s, e) {
    
        s.GetRowValues(s.GetFocusedRowIndex(),'StudentID', feesTransactionController.RefreshTabsView);
       
    },
    RefreshTabsView: function(values) {
        var regID;
       // $("#txtStudentID").val(values);
        $("#txtStudentID").val(values);
        $("#txtstudentname").val(values);
     
        if (values != null) {
            regID = values;
          
        }
    $.ajax({
        url: "/FeesTransaction/GetAllTermForClasswise",
            type: "POST",
            data: {RegID:regID},
            success: function (data) {
                //alert(data);
                //$("#GridTermListForFeeID").html(data);
            },
            error: function () {
            }
        });
        //$("#BodySplitter_1_CC").html("<h1>"+values+"</h1>");
        //alert(values);
    },
  

    SelectedFeeTerm: function (s, e) {
        // var classID = ClassSetupID.GetSelectedValues();
      
        var TermID = s.GetValue();
        var StudentID = txtStudentID.GetValue();
        if (StudentID.toString() == "") {
            alert("Please Select Student for Fee");
        }
        if (TermID.toString() == "") {
            alert("Please Select FeeTerm");
            return;
        }
        $.ajax({
            url: "/FeesTransaction/GetStudentDataforFeeReceipt",
            type: "POST",
            data: { mTermID: TermID, mStudentID:StudentID },
            success: function (data) {
                alert(data);
                $("#FeesTransactionBodySplitter_1_CC").html(data);
            },
            error: function () {
            }
        });

    },

    ClickCheckFeePaidnew: function (value) {

        //alert(value)

        var TermID = value;
        var paidflag = false;
        var StudentID = txtStudentID.GetValue();
        if (StudentID.toString() == "") {
            alert("Please Select Student for Fee");
        }
        if (TermID.toString() == "") {
            alert("Please Select FeeTerm");
            return;
        }
        $.ajax({
            url: "/FeesTransaction/CheckDuplicateReciept",
            type: "POST",
            data: { mTermID: TermID, mStudentID: StudentID },
            beforeSend: (function (data) {
                loadingPanelFeestructure.Show();
            }),
            success: function (data) {

                if (data == "Paid") {

                    paidflag = true;
                    alert("This Term Fee Already paid");
                }
                else {
                    paidflag = false;
                }

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelFeestructure.Hide();

        });

        if (paidflag == false) {

            $.ajax({
                url: "/FeesTransaction/GetStudentDataforFeeReceipt",
                type: "POST",
                data: { mTermID: TermID, mStudentID: StudentID },
                success: function (data) {
                    $("#GridHeadListForFeeID").html(data);

                },
                error: function () {
                }
            }).done(function (data) {
                loadingPanelFeestructure.Hide();

            });
        }


    },


    ClickCheckFeePaid: function (s, e) {

       
        var TermID = s.name;
        var paidflag = false;
        var StudentID = txtStudentID.GetValue();
        if (StudentID.toString() == "") {
            alert("Please Select Student for Fee");
        }
        if (TermID.toString() == "") {
            alert("Please Select FeeTerm");
            return;
        }
        $.ajax({
            url: "/FeesTransaction/CheckDuplicateReciept",
            type: "POST",
            data: { mTermID: TermID, mStudentID: StudentID },
            success: function (data) {
              
                if (data == "Paid") {

                    paidflag = true;
                    alert("This Term Fee Already paid");
                }
                else {
                    paidflag = false;
                }
               
            },
            error: function () {
            }
        });

     
        if (paidflag == false) {
        
            $.ajax({
                url: "/FeesTransaction/GetStudentDataforFeeReceipt",
                type: "POST",
                data: { mTermID: TermID, mStudentID: StudentID },
                success: function (data) {
                        $("#FeesTransactionBodySplitter_1_CC").html(data);
                   
                },
                error: function () {
                }
            });
        }

    },
     
   
    Gettotal: function (s, e) {

        var total = parseInt(TermTotal.GetValue()) + parseInt(FineAmount.GetValue()) + parseInt(OtherAmount.GetValue());
        $("#SubTotal_I").val(total);
        $("#FinalAmount_I").val(total);

        var mode = DiscountType.GetValue();
        if (mode == "P") {
            if (isNaN(SubTotal.GetValue())) {
                $("#SubTotal_I").val(0);
            }
            if (isNaN(DiscountPercent.GetValue())) {
                $("#DiscountPercent_I").val(0);
            }

            var disAmount = (parseInt(SubTotal.GetValue()) * parseInt(DiscountPercent.GetValue())) / 100;
            $("#DiscountAmount_I").val(disAmount);
            var finalamt = (parseInt(SubTotal.GetValue()) - parseInt(DiscountAmount.GetValue()));
            $("#FinalAmount_I").val(finalamt);
        }
        else {

            if (isNaN(SubTotal.GetValue())) {
                $("#SubTotal_I").val(0);
            }
            if (isNaN(DiscountPercent.GetValue())) {
                $("#DiscountPercent_I").val(0);
            }

            var disAmount = parseInt(DiscountPercent.GetValue());
            $("#DiscountAmount_I").val(disAmount);
            var finalamt = (parseInt(SubTotal.GetValue()) - parseInt(DiscountAmount.GetValue()));
            $("#FinalAmount_I").val(finalamt);

        }

    },
    
    SelectionChangePaymode: function (s, e) {
        var mode = s.GetValue();
        if (mode == "Cash") {
          //  ChequeNo.GetInputElement().setAttribute('style', 'background:#CCCCCC;');
            ChequeNo.GetInputElement().readOnly = true;
           // ChequeDate.GetInputElement().readOnly = true;
            BankName.GetInputElement().readOnly = true;
            BranchName.GetInputElement().readOnly = true;
          
            $("ChequeNo_I").val("");
          //  $("ChequeDate_I").val(" ");
            $("BankName_I").val("");
            $("BranchName_I").val("");
         
        }
        else {
          //  ChequeNo.GetInputElement().setAttribute('style', 'background:#CCCCCC;');
            ChequeNo.GetInputElement().readOnly = false;
           // ChequeDate.GetInputElement().readOnly = false;
            BankName.GetInputElement().readOnly = false;
            BranchName.GetInputElement().readOnly = false;

            $("ChequeNo_I").val("");
          //  $("ChequeDate_I").val(" ");
            $("BankName_I").val("");
            $("BranchName_I").val("");
        }
    },


    SelectionChangeDiscountType: function (s, e) {
        var mode = s.GetValue();
        if (mode == "P") {
            if (isNaN(SubTotal.GetValue())) {
                $("#SubTotal_I").val(0);
            }
            if (isNaN(DiscountPercent.GetValue())) {
                $("#DiscountPercent_I").val(0);
            }

            var disAmount = (parseInt(SubTotal.GetValue()) * parseInt(DiscountPercent.GetValue())) / 100;
            $("#DiscountAmount_I").val(disAmount);
            var finalamt = (parseInt(SubTotal.GetValue()) - parseInt(DiscountAmount.GetValue()));
            $("#FinalAmount_I").val(finalamt);

        }
        else {

            if (isNaN(SubTotal.GetValue())) {
                $("#SubTotal_I").val(0);
            }
            if (isNaN(DiscountPercent.GetValue())) {
                $("#DiscountPercent_I").val(0);
            }

            var disAmount = parseInt(DiscountPercent.GetValue());
            $("#DiscountAmount_I").val(disAmount);

            var finalamt = (parseInt(SubTotal.GetValue()) - parseInt(DiscountAmount.GetValue()));
            $("#FinalAmount_I").val(finalamt);


        }
      
    },

    GetDiscount: function (s, e) {
        var mode = DiscountType.GetValue();

        if (mode == "P") {
            if (isNaN(SubTotal.GetValue())) {
                $("#SubTotal_I").val(0);
            }
            if (isNaN(DiscountPercent.GetValue())) {
                $("#DiscountPercent_I").val(0);
            }

            var disAmount = (parseInt(SubTotal.GetValue()) * parseInt(DiscountPercent.GetValue())) / 100;
            $("#DiscountAmount_I").val(disAmount);
            var finalamt = (parseInt(SubTotal.GetValue()) - parseInt(DiscountAmount.GetValue()));

            $("#FinalAmount_I").val(finalamt);
        }
        else {


            if (isNaN(SubTotal.GetValue())) {
                $("#SubTotal_I").val(0);
            }
            if (isNaN(DiscountPercent.GetValue())) {
                $("#DiscountPercent_I").val(0);
            }

            var disAmount = parseInt(DiscountPercent.GetValue());
            $("#DiscountAmount_I").val(disAmount);
            var finalamt = (parseInt(SubTotal.GetValue()) - parseInt(DiscountAmount.GetValue()));

            $("#FinalAmount_I").val(finalamt);


        }

    },


    SelectionChangedForStudent: function (s, e) {

        /*alert('dfdf');*/
        loadingPanelFeestructure.Show();
        s.GetRowValues(s.GetFocusedRowIndex(), 'StudentID', feesTransactionController.GetSelectedFieldValuesCallbackForStudentID);

        
        //s.GetSelectedFieldValues("StudentID", feesTransactionController.GetSelectedFieldValuesCallbackForStudentID);

    },
    GetSelectedFieldValuesCallbackForStudentID: function (values) {

        txtStudentID.SetText(values);
        txtstudentname.SetText(values);

        var studentID;
       
        if (values != null) {
            studentID = values;
        }


        $.ajax({
            url: "/FeesTransaction/GetAllTermForClasswise",
            type: "POST",
            data: { RegID: studentID },
            beforeSend: (function (data) {
                
            }),
            success: function (data) {
                //alert(data);
                $("#GridTermListForFeeID").html(data);
            },
            error: function () {
            }
        }).done(function (data) {

        });

        $.ajax({
            url: "/FeesTransaction/GetStudentData",
            type: "POST",
            data: { mStudentID: studentID },
            beforeSend: (function (data) {

            }),
            success: function (data) {

                
                EnrollmentNo.SetText(data.studentEnrollno);
                FatherName.SetText(data.studentFathername);
                MobileNo.SetText(data.studentMobileno);
                Class.SetText(data.studentClass);

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelFeestructure.Hide();
        });
       
    },


}