let orderID;

$(document).ready(function () {
    let params = (new URL(window.location.href)).searchParams;
    orderID = params.get('orderID');

    GetUserAddresses();
});

$('#btn-register-address').click(function () {
    $('#register-new-address').css('display', 'block');
});

$('#register-address').click(function () {
    let addressModel = {};

    addressModel.IsForeign = $('input[name="localition"]').val() == 'false' ? true : false;

    addressModel.PostalCode = $('#address-postal-code').val();
    addressModel.Number = $('#address-number').val();
    addressModel.Comment = $('#address-comment').val();

    if (addressModel.IsForeign) {
        addressModel.Name = $('#address-name').val();
        addressModel.Neighborhood = $('#address-neighborhood').val();
        addressModel.State = $('#address-state').val();
        addressModel.Country = $('#address-country').val();
    }

    RegisterUserAddress(addressModel);

    window.location.reload();
});