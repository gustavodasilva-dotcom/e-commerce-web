$(document).ready(function () {
    $('#localition_brazil').prop('checked', true);
    $('#address-postal-code').mask('00000-000');

    ReadonlyAddressInputs(true);
});

$('#localition_brazil').change(function () {
    $('#address-postal-code').mask('00000-000');

    ReadonlyAddressInputs(true);
});

$('#localition_foreign').change(function () {
    $('#address-postal-code').unmask();

    ReadonlyAddressInputs(false);
});

$('#address-postal-code').focusout(function () {
    if ($('input[id="localition_brazil"]:checked').length > 0
        && !$('#address-postal-code').is('[readonly]')
        && $('#address-postal-code').val().length >= 9) {

        var postal_code = $(this).val();

        $.ajax({
            async: true,
            type: "GET",
            dataType: "json",
            url: "/Addresses/Get",
            data: { postalCode: postal_code },
            success: function (result) {
                if (result.code == 1) {
                    SetAddressInputValues(result);
                }
                else {
                    ShowMessageDiv(result.message);
                    ReadonlyAddressInputs(true);
                    CleanAddressInputValues();
                }
            }
        });

    }

    SetAddressMasks();
});

function SetAddressMasks() {
    if (isBrazilianCompany) {
        $('#address-postal-code').mask('00000-000');
        $('#address-postal-code').trigger('input');
    }
}

function ReadonlyAddressInputs(readonly) {
    if (readonly) {
        $('#address-name').prop('readonly', true);
        $('#address-neighborhood').prop('readonly', true);
        $('#address-city').prop('readonly', true);
        $('#address-state').prop('readonly', true);
        $('#address-country').prop('readonly', true);
    }
    else {
        $('#address-name').prop('readonly', false);
        $('#address-neighborhood').prop('readonly', false);
        $('#address-city').prop('readonly', false);
        $('#address-state').prop('readonly', false);
        $('#address-country').prop('readonly', false);
    }
}

function SetAddressInputValues(model) {
    $('#address-postal-code').val(model.street.postalCode);
    $('#address-name').val(model.street.name);
    $('#address-number').val(model.number);
    $('#address-comment').val(model.comment);
    $('#address-neighborhood').val(model.neighborhood.name);
    $('#address-city').val(model.city.name);
    $('#address-state').val(model.state.initials);
    $('#address-country').val(model.country.name);
}

function SetAddressModel() {
    let addresses = {};

    addresses.PostalCode = $('#address-postal-code').val();
    addresses.Name = $('#address-name').val();
    addresses.Number = $('#address-number').val();
    addresses.Comment = $('#address-comment').val();
    addresses.Neighborhood = $('#address-neighborhood').val();
    addresses.City = $('#address-city').val();
    addresses.State = $('#address-state').val();
    addresses.Country = $('#address-country').val();

    return addresses;
}

function RegisterUserAddress(addressModel) {

    $.ajax({
        async: false,
        type: "POST",
        dataType: "json",
        url: "/Addresses/RegisterUserAddress",
        data: { model: addressModel },
        success: function (result) {
            if (result.Code != 1) {

                ShowMessageDiv(result.message);
                return;
            } else {

                if (result.RedirectToLogin)
                    window.location.href = '/Accounts/Login';
            }
        }
    });
}

function GetUserAddresses() {

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Addresses/GetUserAddresses",
        success: function (result) {
            if (result.Code == 1) {

                window.Addresses = {};
                window.Addresses = result.Addresses;
            } else {

                if (result.RedirectToLogin)
                    window.location.href = '/Accounts/Login';

                ShowMessageDiv(result.message);
            }
        }
    });
}

function SetDeliveryAddress(orderID, addressID) {

    $.ajax({
        async: false,
        type: "POST",
        dataType: "json",
        data: { orderGuid: orderID, addressGuid: addressID },
        url: "/Orders/StepTwo",
        success: function (result) {
            if (result.Code != 1) {

                if (result.RedirectToLogin)
                    window.location.href = '/Accounts/Login';

                ShowMessageDiv(result.message);
                return;
            }
        }
    });
}

function GetOrderAddress(orderID) {

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        data: { orderGuid: orderID },
        url: "/Addresses/GetOrderAddress",
        success: function (result) {
            if (result.Code == 1) {

                window.Address = {};
                window.Address = result.Address;
            } else {

                ShowMessageDiv(result.message);
            }
        }
    });
}