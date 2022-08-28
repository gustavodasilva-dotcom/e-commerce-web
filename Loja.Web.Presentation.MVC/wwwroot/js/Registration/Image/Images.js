function UploadImages() {
    let ids = [];

    if (window.FormData !== undefined) {
        var fileUpload = $('input[type="file"]').get(0);
        var files = fileUpload.files;

        if (files.length == 6) {
            var fileData = new FormData();

            for (let i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }

            $.ajax({
                async: false,
                url: "/Images/UploadImages",
                type: "POST",
                contentType: false,
                processData: false,
                data: fileData,
                success: function (result) {
                    if (result.Code == 1) {
                        for (let i = 0; i < result.Images.length; i++) {
                            ids[i] = result.Images[i].id;
                        }
                    } else {
                        ShowMessageError(result.Message);
                    }
                },
            });
        }
    } else {
        ShowMessageError('Not inserting images, because FormData is not supported by the browser.');
    }

    return ids;
}

function InsertProductsImages(productId, imagesId) {
    let defaultObjectImagesModel = {};

    defaultObjectImagesModel.ObjectID = productId;
    defaultObjectImagesModel.ImagesIDs = imagesId;

    $.ajax({
        async: false,
        type: "POST",
        dataType: "json",
        url: "/Images/InsertProductsImages",
        data: { defaultObjectImages: defaultObjectImagesModel },
        success: function (result) {
            if (result.Code != 1) {
                ShowMessageError(result.Message);
                return;
            }
        },
    });
}