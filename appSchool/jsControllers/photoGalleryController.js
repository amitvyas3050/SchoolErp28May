var photoGalleryController = {

    splitterResized: function (s, e) {
        switch (e.pane.name) {
            
            case 'PhotoGalleryBody':
           
                break;
            case 'PhotoGalleryFooter':
                //fileManagerGallery.SetHeight(e.pane.GetClientHeight() - 10);
              
                var winHeight = document.getElementById('PhotoGallerySplitter_1_CC').offsetHeight;
                fileManagerGallery.SetHeight(winHeight - 10);

                break;

        }
    },

    CreateFolder: function(s, e)
    {
        var FolderName = txtFolderName.GetValue();
        if (FolderName == null)
        {
            alert("Please Enter FolderName.");
            return;
        }

        var EventName = txtEvent.GetValue();
        if (EventName == null) {
            alert("Please Enter Event.");
            return;
        }

        var Remark = txtRemark.GetValue();
        if (Remark == null) {
            alert("Please Enter Remark.");
            return;
        }

        $.ajax({
            url: "/PhotoGallery/CreateFolderView",
            type:"POST",
            datatype: "json",
            data: { PFolderName: FolderName.toString(), PEventName: EventName.toString(), PRemark: Remark.toString() },
            success: function (data) {
                alert(data.ErrorMsg);
                if (data.Result) {
                    txtFolderName.SetText("");
                    txtEvent.SetText("");
                    txtRemark.SetText("");
                }

                $("#PhotoGallerySplitter_1_CC").html(data.FileMgrData);
                var winHeight = document.getElementById('PhotoGallerySplitter_1_CC').offsetHeight;
                fileManagerGallery.SetHeight(winHeight - 10);
            },
            error: function ()
            {
            }
        });

    },


    UpdatePhotoDetail: function (s, e) {
    
        var PGalleryID = GalleryID.GetValue();
        if (PGalleryID == null) {
            alert("Please Select Gallery Folder.");
            return;
        }

        $.ajax({
            url: "/PhotoGallery/UpdatePhotoDetail",
            type: "POST",
            datatype: "json",
            data: { mGalleryID: PGalleryID },
            beforeSend: (function (data) {
                loadingPanelPhotoGallery.Show();
            }),
            success: function (data) {
              
                alert(data.ErrorMsg);

            },
            error: function () {
            }
        }).done(function (data) {
            loadingPanelPhotoGallery.Hide();

        });


    }


}