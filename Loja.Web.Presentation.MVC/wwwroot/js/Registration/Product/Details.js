let productID;
let bigImage = document.querySelector('.product-details-big-image img');

$(document).ready(function () {

    $(document.body).css('overflow-x', 'hidden');

    let params = (new URL(window.location.href)).searchParams;
    productID = params.get('guidID');

    var product = GetProductByID(productID);

    if (product != null) {

        SetDetails(product);
        SetDescription(product.description);
    }

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

function SetDetails(product) {

    $('#product-id').val(product.id);
    $('#product-guid').val(product.guidID);

    SetProductRating(product.rating);

    $('.product-details-name').text(product.name);
    document.title = product.name;

    if (product.discount > 0) {

        let priceDiscounted = (product.price / 100) * product.discount;
        priceDiscounted = product.price - priceDiscounted;
        $('.product-details-price').text(`${product.currency.symbol} ${priceDiscounted.toFixed(2) }`);
        $('.product-details-original-price').text(product.price.toFixed(2));
    }
    else
        $('.product-details-price').text(`${product.currency.symbol} ${product.price}`);

    if (product.stock >= 1) {

        $('.product-details-stock').text('In stock');
        $('.product-details-stock').css('color', '#0081A7');
    }
    else {

        $('.product-details-stock').text('Not in stock');
        $('.product-details-stock').css('color', '#F07167');
    }

    $('#product-quantity').val(product.stock);

    if (product.discount > 0) $('.product-details-discount').text('-' + product.discount + '%');
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

function AddItemToCart() {

    let quantity = parseInt($('#input-product-quantity').val());
    let productID = parseInt($('#product-id').val());

    let productQuantity = parseInt($('#product-quantity').val());

    AddToCart(quantity, productID, productQuantity);
}

function ConvertBase64ToImage() {

    if (bases64.length > 0) {

        for (let i = 0; i < bases64.length; i++) {

            var image = document.getElementById('img-' + (i + 1));
            image.src = 'data:image/png;base64,' + bases64[i];

            if (i == 0) {

                image.className = "product-details-big-image";

                var smallImage1 = document.getElementById('img-1-small');
                smallImage1.src = 'data:image/png;base64,' + bases64[i];
            } else
                image.className = "product-details-small-image";

        }
    } else {

        for (let i = 0; i < 6; i++) {

            var image = document.getElementById('img-' + (i + 1));
            image.src = '/media/default.png';

            if (i == 0) {

                image.className = "product-details-big-image";

                var smallImage1 = document.getElementById('img-1-small');
                smallImage1.src = '/media/default.png';
            } else
                image.className = "product-details-small-image";

        }

    }

}