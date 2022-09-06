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
    if ($('input[id="localition_brazil"]:checked').length > 0) {
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
                    DisplayAddressErrorMessage(false, '');
                }
                else {
                    alert(result.message);
                    DisplayAddressErrorMessage(true, result.message);
                    ReadonlyAddressInputs(true);
                    CleanAddressInputValues();
                }
            }
        });
    }
});

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
    $('#address-name').val(model.street.name);
    $('#address-name').prop('readonly', true);

    $('#address-neighborhood').val(model.neighborhood.name);
    $('#address-neighborhood').prop('readonly', true);

    $('#address-city').val(model.city.name);
    $('#address-city').prop('readonly', true);

    $('#address-state').val(model.state.initials);
    $('#address-state').prop('readonly', true);

    $('#address-country').val(model.country.name);
    $('#address-country').prop('readonly', true);
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
                alert(result.Message);
                return;
            } else {
                if (result.RedirectToLogin) window.location.href = '/Accounts/Login';
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
                if (result.RedirectToLogin) window.location.href = '/Accounts/Login';
                alert(result.Message);
            }
        }
    });
}