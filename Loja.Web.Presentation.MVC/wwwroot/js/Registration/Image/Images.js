let bases64 = [];

function UploadImages() {

    let ids = [];

    if (window.FormData !== undefined) {

        var fileUpload = $('input[type="file"]').get(0);
        var files = fileUpload.files;

        if (files.length == 6) {

            var fileData = new FormData();

            for (let i = 0; i < files.length; i++) fileData.append(files[i].name, files[i]);

            $.ajax({
                async: false,
                url: "/Images/UploadImages",
                type: "POST",
                contentType: false,
                processData: false,
                data: fileData,
                success: function (result) {
                    if (result.Code == 1) {
                        for (let i = 0; i < result.Images.length; i++)
                            ids[i] = result.Images[i].id;
                    } else {
                        ShowMessageDiv(result.Message);
                    }
                },
            });
        }
    } else {
        ShowMessageDiv('Not inserting images, because FormData is not supported by the browser.');
    }

    return ids;
}

function InsertProductsImages(productId, imagesId) {

    let model = {};

    model.ObjectID = productId;
    model.ImagesIDs = imagesId;

    $.ajax({
        async: false,
        type: "POST",
        dataType: "json",
        url: "/Images/InsertProductsImages",
        data: { defaultObjectImages: model },
        success: function (result) {
            if (result.Code != 1) {
                ShowMessageDiv(result.Message);
                return;
            }
        },
    });
}

async function GetBases64ByProductIDAsync(productID) {

    bases64 = {};

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Images/GetBases64ByProductID",
        data: { productID: productID },
        success: function (result) {
            if (result.Code == 1) {
                bases64 = [];
                bases64 = result.Bases64;
            } else {
                ShowMessageDiv(result.Message);
            }
        },
    });
}