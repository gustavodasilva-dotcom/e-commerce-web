﻿//#region Global variables
let singleItem;
let orderGuid;
let productGuidID;
let paymentSelected;
let model = [];
let products = [];
let shoppingCart = [];
let editOrder = false;
let oneItemOnly = false;
//#endregion


$(document).ready(function () {

    CheckUrlParameters();

    if (!editOrder)
        GetShoppingCartItems();
    else
        GetOrderDetails(orderGuid);

    SetShoppingCartItems();
    SetComboBoxPaymentTypes();
    GetUserCards();
    SetUserCardsList();

    if (editOrder)
        SetCardInfos(null, window.Order.cardInfo, 'card-issuer-img');

});

$('#card-number').change(function () {

    if ($(this).val() != '' &&
        $(this).val() != null) {

        SetCardIssuer($(this).val(), 'card-issuer-img');

    } else {

        $('#card-issuer-img').attr('src', null);
        $(this).val('');

    }

});


//#region CheckUrlParameters
function CheckUrlParameters() {

    let params = (new URL(window.location.href)).searchParams;

    let oneItemOnlyParam = params.get('oneItemOnly');
    let productGuidParam = params.get('productGuidID');
    let orderParam = params.get('order');

    if (!oneItemOnlyParam && !orderParam ||
        !oneItemOnlyParam && !productGuidParam && !orderParam)
        window.location.href = '/Default/Select?statusCode=400';

    oneItemOnly = oneItemOnlyParam == '1' ? true : false;
    productGuidID = productGuidParam;

    editOrder = orderParam != null ? true : false;
    orderGuid = orderParam;
}
//#endregion


//#region SetShoppingCartItems
function SetShoppingCartItems() {

    model = editOrder ? window.Order : window.ShoppingCart;

    let htmlCards;

    if (oneItemOnly) {

        singleItem = window.ShoppingCart.find(x => x.productGuid == productGuidID.toLowerCase());

        htmlCards = '<div class="item-card">';

        htmlCards += `<a class="item-title" href="#">${singleItem.name}</a><br/ >`;

        htmlCards += `<p class="item-price">$ ${singleItem.price}</p>`;

        htmlCards += '<input type="number" class="card-info-input" style="width: 50px; margin-right: 20px" ' +
            `data-quantity="${singleItem.guidID}" value="${singleItem.quantity}" />`;

        htmlCards += '</div>';

    } else {

        products = editOrder ? model.products : model;

        $.each(products, function (i, item) {

            htmlCards = '<div class="item-card">';

            htmlCards += `<a class="item-title" href="#">${products[i].name}</a></br>`;

            htmlCards += `<p class="item-price">${products[i].currency.symbol} ${products[i].price}</p>`;

            htmlCards += '<input type="number" class="card-info-input" style="width: 50px; margin-right: 20px" ' +
                `data-quantity="${products[i].guidID}" value="${products[i].quantity}" />`;

            htmlCards += '<hr>';
            htmlCards += '</div>';

        });
    }

    $('#register-items-card').html(htmlCards);
}
//#endregion


//#region HTML elements' events
$('#register-select-payment-types').change(function () {

    PaymentSelected($(this).val());
});

$('#btn-move-next').click(function () {

    let stepOneModel = {};
    let cardInfoModel = {};

    if (editOrder) stepOneModel.OrderGuid = orderGuid;

    stepOneModel.IsCard = paymentSelected.isCard;

    stepOneModel.ProductGuid = [];
    stepOneModel.ProductQuantity = [];

    if (oneItemOnly) {

        let quanity = $(`input[data-quantity="${productGuidID}"]`).val();

        stepOneModel.ProductGuid.push(productGuidID);
        stepOneModel.Quantity.push(quanity);
    }
    else {

        for (let i = 0; i < products.length; i++) {

            let quanity = $(`input[data-quantity="${products[i].guidID}"]`).val();

            stepOneModel.ProductGuid.push(products[i].guidID);
            stepOneModel.ProductQuantity.push(quanity);
        }

    }

    stepOneModel.PaymentGuid = $('#register-select-payment-types').val();

    cardInfoModel.CardNumber = $('#card-number').val();
    cardInfoModel.NameAtTheCard = $('#card-name').val();
    cardInfoModel.Month = parseInt($('#card-month').val());
    cardInfoModel.Year = parseInt($('#card-year').val());
    cardInfoModel.CVV = $('#card-cvv').val();

    if (paymentSelected.name.toLowerCase().includes('credit')) cardInfoModel.Quantity = parseInt($('#card-quantity').val());

    stepOneModel.CardInfo = cardInfoModel;


    if (stepOneModel.PaymentGuid == '' ||
        stepOneModel.PaymentGuid == null) {

        ShowMessageDiv('Please, select the payment method.');
        return;
    }

    if (stepOneModel.IsCard) {
        let validation = ValidateCards(stepOneModel.CardInfo);

        if (!validation.success) {
            ShowMessageDiv(validation.message);
            return;
        }
    }


    StepOne(stepOneModel);

    if (requestSuccessed)
        window.location.href = '/Orders/AddressSelect?orderID=' + window.StepOne.guidID;

});
//#endregion