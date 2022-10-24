let cardIssuers = {};
let validIssuer = false;
let paymentTypes = [];


$(document).ready(function () {

    GetCardIssuers();

});

    
//#region GetUserCards
function GetUserCards() {

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Payments/GetUserCards",
        success: function (result) {
            if (result.Code == 1) {

                window.CardsInfos = {};
                window.CardsInfos = result.CardsInfos;
            }
            else {
                ShowMessageDiv(result.Message);
            }
        }
    });

}
//#endregion


//#region GetPaymentTypes
function GetPaymentTypes() {

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Payments/GetPaymentTypes",
        success: function (result) {
            if (result.Code == 1) {

                window.PaymentTypes = {};
                window.PaymentTypes = result.PaymentTypes;
            }
            else {
                ShowMessageDiv(result.Message);
            }
        }
    });

}
//#endregion


//#region SetComboBoxPaymentTypes
function SetComboBoxPaymentTypes() {

    GetPaymentTypes();

    paymentTypes = window.PaymentTypes;

    $.each(paymentTypes, function (i, item) {
        $('#register-select-payment-types').append(`<option value="${paymentTypes[i].guidID}">${paymentTypes[i].name}</option>`);
    });

}
//#endregion


//#region SetUserCardsList
function SetUserCardsList() {

    if (editOrder) {
        $('#register-select-payment-types').val(model.paymentMethod.guidID);
        PaymentSelected(model.paymentMethod.guidID);
    }

    let card = window.CardsInfos;

    let htmlCode = '<h2>Previous cards</h2>';

    $.each(card, function (i, item) {

        htmlCode += '<div class="card-content">';
        htmlCode +=     `<input type="radio" name="card-chk" class="chkCard" data-card="${card[i].guidID}"`;
        htmlCode +=         `onclick="SetCardInfos(this, null, 'card-issuer-img')">`
        htmlCode +=     '<p style="margin-left: 18px;"><strong>Card number</strong>:';
        htmlCode +=     `${"*".repeat(card[i].cardNumber.length - 4) + card[i].cardNumber.slice(-4)}</p>`;
        htmlCode +=     `<p><strong>Name at the card:</strong> ${card[i].nameAtTheCard}</p>`;
        htmlCode +=     `<p><strong>CVV:</strong> ${card[i].cvv}<span style="margin-left: 20px;"></span>`;
        htmlCode +=     `<strong>Expiration:</strong> ${card[i].month}/${card[i].year}</p>`;
        htmlCode += '</div>';
        htmlCode += '<hr />';

    });

    $('#user-cards').html(htmlCode);

}
//#endregion


//#region SetCardInfos
function SetCardInfos(input = null, model = null, field = null) {

    if (input != null)
        model = window.CardsInfos.find(x => x.guidID == input.dataset.card);

    $(`[data-card="${model.guidID}"]`).prop('checked', true);

    $('#card-number').val(model.cardNumber);
    $('#card-name').val(model.nameAtTheCard);
    $('#card-month').val(model.month);
    $('#card-year').val(model.year);
    $('#card-cvv').val(model.cvv);
    $('#card-quantity').val(model.quantity);

    if (field != null)
        SetCardIssuer(model.cardNumber, field);
    
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
            $('#user-cards').css('display', 'block');

            if (paymentSelected.name.toLowerCase().includes('credit'))
                SetQuantitySelect();
            else
                $('#card-quantity-field').css('display', 'none');
        } else {

            $('#card-details').css('display', 'none');
            $('#user-cards').css('display', 'block');
        }
    } else {

        $('#card-details').css('display', 'none');
        $('#user-cards').css('display', 'block');
    }
}
//#endregion


//#region StepOne
function StepOne(stepOne) {

    console.log('Sending object', stepOne);

    $.ajax({
        async: false,
        type: "POST",
        dataType: "json",
        data: { model: stepOne },
        url: "/Orders/StepOne",
        success: function (result) {
            if (result.Code == 1) {

                window.StepOne = {};
                window.StepOne = result.Order;

                requestSuccessed = true;
            }
            else {

                requestSuccessed = false;

                ShowMessageDiv(result.Message);
                return;
            }
        }
    });

}
//#endregion


//#region Card Validation

function ValidateCards(cardInfo) {

    if (!validIssuer)
        return response(false, 'The card issuer is invalid or unkown.');

    if (cardInfo.CardNumber == '' || cardInfo.CardNumber == null)
        return response(false, 'Please, inform the card number.');

    if (cardInfo.CardNumber == '5490997771092064')
        return response(false, 'This credit card number is associated with a scam attempt.');

    if (!ValidateCardNumber(cardInfo.CardNumber))
        return response(false, 'Card number invalid.');

    if (cardInfo.NameAtTheCard == '' || cardInfo.NameAtTheCard == null)
        return response(false, 'Please, inform the name at the card.');

    if (cardInfo.Month == '' || cardInfo.Month == null)
        return response(false, 'Please, inform the expiration month of the card.');

    if (cardInfo.Year == '' || cardInfo.Year == null)
        return response(false, 'Please, inform the expiration year of the card.');

    if (cardInfo.CVV == '' || cardInfo.CVV == null)
        return response(false, 'Please, inform the CVV of the card.');

    return response(true);
}

const ValidateCardNumber = number => {

    if (!regex.test(number))
        return false;

    return CardNumberCheck(number);

}

const CardNumberCheck = val => {

    let checkSum = 0;
    let j = 1;

    for (let i = val.length - 1; i >= 0; i--) {

        let calc = 0;
        calc = Number(val.charAt(i)) * j;

        if (calc > 9) {
            checkSum++;
            calc -= 10;
        }

        checkSum += calc;

        if (j == 1)
            j = 2;
        else
            j = 1;

    }

    return (checkSum % 10) == 0;

}

const CheckCreditCardIssuer = cardNumber => {

    let lengthValid = false;
    let prefixValid = false;

    validIssuer = false;

    for (let i = 0; i < cardIssuers.length; i++) {

        const prefix = cardIssuers[i].prefixes.split(',');

        for (let j = 0; j < prefix.length; j++) {

            const exp = new RegExp('^' + prefix[j]);

            if (exp.test(cardNumber))
                prefixValid = true;

        }

        if (prefixValid) {

            const lengths = cardIssuers[i].length.split(',');

            for (let j = 0; j < lengths.length; j++) {

                if (cardNumber.length == lengths[j])
                    lengthValid = true;

            }
            
        }

        if (prefixValid && lengthValid) {
            validIssuer = true;
            return response(true, null, cardIssuers[i]);
        }
        
    }

    return response(false);

}

function GetCardIssuers() {

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/CardIssuers/Get",
        success: function (result) {
            if (result.Code == 1) {

                cardIssuers = {};
                cardIssuers = result.CardIssuers;
            }
            else {

                if (result.RedirectToLogin) {
                    window.location.href = '/Accounts/Login';
                } else {
                    alert(result.Message);
                }
            }
        }
    });

}

function SetCardIssuer(cardNumber, field) {

    let validIssuer = CheckCreditCardIssuer(cardNumber);

    if (validIssuer.success)
        SetCardIssuerImg(validIssuer.obj, field);

}

function SetCardIssuerImg(obj, field) {

    $(`#${field}`).attr('src', `/media/${obj.name.toLowerCase()}-logo.png`);

}
//#endregion