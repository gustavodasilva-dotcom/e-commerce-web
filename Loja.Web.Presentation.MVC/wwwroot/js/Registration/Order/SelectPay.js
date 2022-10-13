//#region Global variables
let singleItem;
let orderGuid;
let productGuidID;
let paymentSelected;
let model = [];
let products = [];
let paymentTypes = [];
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

        singleItem = shoppingCart.find(x => x.productGuid == productGuidID.toLowerCase());

        htmlCards = '<div class="item-card">';

        htmlCards += `<a class="item-title" href="#">${singleItem.name}</a><br/ >`;

        htmlCards += `<p class="item-price">${singleItem.currency.symbol} ${singleItem.price}</p>`;

        htmlCards += '<input type="number" class="card-info-input" style="width: 50px; margin-right: 20px" ' +
                     `data-quantity="${singleItem.guidID}" value="${singleItem.quantity}" />`;

        htmlCards += `<p class="item-amount">${singleItem.currency.symbol} ${(singleItem.price * singleItem.quantity)}</p>`;
        htmlCards += '</div>';

    } else {

        products = editOrder ? model.products : model;

        $.each(products, function (i, item) {

            htmlCards = '<div class="item-card">';

            htmlCards += `<a class="item-title" href="#">${products[i].name}</a></br>`;

            htmlCards += `<p class="item-price">${products[i].currency.symbol} ${products[i].price}</p>`;

            htmlCards += '<input type="number" class="card-info-input" style="width: 50px; margin-right: 20px" ' +
                         `data-quantity="${products[i].guidID}" value="${products[i].quantity}" />`;

            htmlCards += `<p class="item-amount">${products[i].currency.symbol} ${(products[i].price * products[i].quantity)}</p>`;
            htmlCards += '<hr>';
            htmlCards += '</div>';

        });
    }

    $('#register-items-card').html(htmlCards);
}
//#endregion

//#region SetComboBoxPaymentTypes
function SetComboBoxPaymentTypes() {

    GetPaymentTypes();

    paymentTypes = window.PaymentTypes;

    $.each(paymentTypes, function (i, item) {
        $('#register-select-payment-types').append(`<option value="${paymentTypes[i].guidID}">${paymentTypes[i].name}</option>`);
    });

    if (editOrder) {

        $('#register-select-payment-types').val(model.paymentMethod.guidID);
        PaymentSelected(model.paymentMethod.guidID);
    }
}
//#endregion

//#region SetQuantitySelect
function SetQuantitySelect() {

    $('#card-quantity-field').css('display', 'block');

    if (!($('#card-quantity')[0].length > 1)) {
        for (let i = 1; i <= 12; i++) $('#card-quantity').append(`<option value="${i}">${i}x</option>`);
    }    
}
//#endregion

//#region PaymentSelected
function PaymentSelected(paymentGuid) {

    paymentSelected = paymentTypes.find(x => x.guidID == paymentGuid);

    if (paymentSelected != null && paymentSelected != undefined) {

        if (paymentSelected.isCard) {

            $('#card-details').css('display', 'block');
            $('#card-details').css('display', 'block');

            if (paymentSelected.name.toLowerCase().includes('credit'))
                SetQuantitySelect();
            else
                $('#card-quantity-field').css('display', 'none');
        } else {

            $('#card-details').css('display', 'none');
        }
    } else {

        $('#card-details').css('display', 'none');
    }
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

    if (oneItemOnly) {

        let quanity = $(`input[data-quantity="${productGuidID}"]`).val();

        stepOneModel.ProductGuid.push(productGuidID);
        stepOneModel.Quantity.push(quanity);
    }
    else {

        for (let i = 0; i < products.length; i++) {

            let quanity = $(`input[data-quantity="${products[i].guidID}"]`).val();

            stepOneModel.ProductGuid.push(products[i].guidID);
            stepOneModel.Quantity.push(quanity);
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

    var validation = ValidateModel(model);

    if (!validation.Valide) {
        ShowMessageDiv(validation.Message);
        return;
    }

    StepOne(stepOneModel);

    window.location.href = '/Orders/AddressSelect?orderID=' + window.StepOne.guidID;
});
//#endregion

//#region ValidateModel
function ValidateModel(model) {

    let validation = {};
    validation.Valide = false;

    if (model.PaymentGuid == '' ||
        model.PaymentGuid == null) {

        validation.Message = 'Please, select the payment method.';
        return;
    }

    if (mode.IsCard) {

        if (model.CardInfo.CardNumber == '' ||
            model.CardInfo.CardNumber == null) {

            validation.Message = 'Please, inform the card number.';
            return;
        }

        if (model.CardInfo.NameAtTheCard == '' ||
            model.CardInfo.NameAtTheCard == null) {

            validation.Message = 'Please, inform the name at the card.';
            return;
        }

        if (model.CardInfo.Month == '' ||
            model.CardInfo.Month == null) {

            validation.Message = 'Please, inform the expiration month of the card.';
            return;
        }

        if (model.CardInfo.Year == '' ||
            model.CardInfo.Year == null) {

            validation.Message = 'Please, inform the expiration year of the card.';
            return;
        }

        if (model.CardInfo.CVV == '' ||
            model.CardInfo.CVV == null) {

            validation.Message = 'Please, inform the CVV of the card.';
            return;
        }
    }

    validation.Valide = true;
}
//#endregion