let singleItem;
let productGuidID;
let paymentTypes = [];
let shoppingCart = [];
let oneItemOnly = false;

$(document).ready(function () {
    let params = (new URL(window.location.href)).searchParams;
    oneItemOnly = params.get('oneItemOnly') == '1' ? true : false;
    productGuidID = params.get('productGuidID');

    GetShoppingCartItems();
    SetShoppingCartItems();
    SetComboBoxPaymentTypes();
});

function SetShoppingCartItems() {
    shoppingCart = window.ShoppingCart;

    let htmlCards;

    if (oneItemOnly) {
        singleItem = shoppingCart.find(x => x.productGuid == productGuidID.toLowerCase());

        htmlCards = '<div class="item-card">';
        htmlCards += '<a class="item-title" href="#">' + singleItem.name + '</a><br/ >';
        htmlCards += '<p class="item-price">' + singleItem.price + '</p>';
        htmlCards += '<p class="item-qtd">#' + singleItem.quantity + '</p>';
        htmlCards += '<p class="item-amount">' + (singleItem.price * singleItem.quantity) + '</p>';
        htmlCards += '</div>';
    } else {
        $.each(shoppingCart, function (i, item) {
            htmlCards = '<div class="item-card">';
            htmlCards += '<a class="item-title" href="#">' + shoppingCart[i].name + '</a>';
            htmlCards += '<p class="item-price">' + shoppingCart[i].price + '</p>';
            htmlCards += '<p class="item-qtd">' + shoppingCart[i].quantity + '</p>';
            htmlCards += '<p class="item-amount">' + (shoppingCart[i].price * shoppingCart[i].quantity) + '</p>';
            htmlCards += '<hr>';
            htmlCards += '</div>';
        });
    }

    $('#register-items-card').html(htmlCards);
}

function SetComboBoxPaymentTypes() {
    GetPaymentTypes();

    paymentTypes = window.PaymentTypes;

    $.each(paymentTypes, function (i, item) {
        $('#register-select-payment-types').append(`<option value="${paymentTypes[i].guidID}">${paymentTypes[i].name}</option>`);
    });
}

$('#register-select-payment-types').change(function () {
    let paymentSelected = paymentTypes.find(x => x.guidID == $(this).val());

    if (paymentSelected != null && paymentSelected != undefined) {
        if (paymentSelected.isCard == true) {
            $('#card-details').css('display', 'block');
        } else {
            $('#card-details').css('display', 'none');
        }
    } else {
        $('#card-details').css('display', 'none');
    }
});

$('#btn-move-next').click(function () {
    alert('Ok.');
});