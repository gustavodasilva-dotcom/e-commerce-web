$(document).ready(function () {
    let urlLocation = window.location.href;
    let params = (new URL(urlLocation)).searchParams;
    let productID = params.get('guidID');

    GetProductDetails(productID);
});

let bigImage = document.querySelector('.product-details-big-image img');

function popImage(image) {
    bigImage.src = image;
}

function GetProductDetails(guidID) {
    $.ajax({
        async: true,
        type: "GET",
        dataType: "json",
        url: "/Products/GetDetails",
        data: { productID: guidID },
        success: function (result) {
            if (result.Code == 1) {
                SetDetails(result.Product);
            }
            else {
                alert(result.Message);
            }
        },
        error: function (req, status, error) {
            alert('Error: ', error);
        }
    });
}

function SetDetails(product) {
    $('.product-details-name').text(product.name);
    $('.product-details-description').text(product.description);

    if (product.discount > 0) {
        let priceDiscounted = (product.price / 100) * product.discount;
        $('.product-details-price').text(priceDiscounted.toFixed(2));
        $('.product-details-original-price').text(product.price);
    }
    else {
        $('#product-details-price-hidden').val(product.price);
        $('#product-details-price-hidden').maskMoney().trigger('mask.maskMoney');
        $('.product-details-price').text($('#product-details-price-hidden').val());
    }

    if (product.stock >= 1) {
        $('.product-details-stock').text('In stock');
        $('.product-details-stock').css('color', '#0081A7');
    }
    else {
        $('.product-details-stock').text('Not in stock');
        $('.product-details-stock').css('color', '#F07167');
    }

    if (product.discount > 0) {
        $('.product-details-discount').text('-' + product.discount + '%');
    }
}