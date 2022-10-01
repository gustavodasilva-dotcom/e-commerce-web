let productID;
let bigImage = document.querySelector('.product-details-big-image img');

$(document).ready(function () {
    let params = (new URL(window.location.href)).searchParams;
    productID = params.get('guidID');

    GetProductDetails(productID);

    GetBases64ByProductIDAsync(productID);
    ConvertBase64ToImage();
});

$('#btn-add-to-cart').click(function () {
    AddItemToCart();
});

$('#btn-buy-now').click(function () {
    AddItemToCart();
    window.location.href = '/Orders/SelectPay?oneItemOnly=1&productGuidID=' + productID;
});

$('#btn-edit').click(function () {
    window.location.href = '/Products/Process?edit=1&guidID=' + productID;
});

function popImage(image) {
    bigImage.src = image;
}

function GetProductDetails(guidID) {
    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Products/Get",
        data: { productID: guidID },
        success: function (result) {
            if (result.Code == 1) {
                SetDetails(result.Product);
                SetDescription(result.Product.description);
            }
            else {
                alert(result.Message);
            }
        }
    });
}

function SetDetails(product) {
    $('#product-id').val(product.id);

    $('.product-details-name').text(product.name);
    document.title = product.name;

    if (product.discount > 0) {
        let priceDiscounted = (product.price / 100) * product.discount;
        priceDiscounted = product.price - priceDiscounted;
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

    $('#product-quantity').val(product.stock);

    if (product.discount > 0) {
        $('.product-details-discount').text('-' + product.discount + '%');
    }
}

function SetDescription(description) {
    if (description.length > 200) {
        let firstPart = description.substring(0, 200);
        let secondPart = description.substring(200, description.length - 1);

        let htmlDescription = firstPart + '<span id="dots">...</span><span id="more">' + secondPart;

        $('.product-details-description').html(htmlDescription);

        SetReadMore();
    }
}

function ConvertBase64ToImage() {
    if (window.Bases64.length > 0) {
        for (let i = 0; i < window.Bases64.length; i++) {
            var image = document.getElementById('img-' + (i + 1));
            image.src = 'data:image/png;base64,' + window.Bases64[i];

            if (i == 0) {
                image.className = "product-details-big-image";

                var smallImage1 = document.getElementById('img-1-small');
                smallImage1.src = 'data:image/png;base64,' + window.Bases64[i];
            } else {
                image.className = "product-details-small-image";
            }
        }
    }
}

function AddItemToCart() {
    let quantity = parseInt($('#input-product-quantity').val());
    let productID = parseInt($('#product-id').val());

    let productQuantity = parseInt($('#product-quantity').val());

    AddToCart(quantity, productID, productQuantity);
}