let cardIssuers = {};
let validIssuer = false;


$(document).ready(function () {

    GetCardIssuers();

});


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


//#region SetPaymentInfos
function SetPaymentInfos() {

    $('#card-number').val(model.cardInfo.cardNumber);
    $('#card-name').val(model.cardInfo.nameAtTheCard);
    $('#card-month').val(model.cardInfo.month);
    $('#card-year').val(model.cardInfo.year);
    $('#card-cvv').val(model.cardInfo.cvv);
    $('#card-quantity').val(model.cardInfo.quantity);

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
            }
            else {
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
    let cardCompany = '';

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
            cardCompany = cardIssuers[i].name;
            validIssuer = true;
            return response(true, null, cardCompany);
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

//#endregion