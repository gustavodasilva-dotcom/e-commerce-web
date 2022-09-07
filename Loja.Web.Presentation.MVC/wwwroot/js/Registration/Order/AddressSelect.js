﻿let orderID;

$(document).ready(function () {
    let params = (new URL(window.location.href)).searchParams;
    orderID = params.get('orderID');

    GetUserAddresses();
    SetAddressesCards();
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

function SetAddressesCards() {
    if (window.Addresses.length > 0) {
        let addresses = window.Addresses;

        let htmlCards = '';

        $.each(addresses, function (i, item) {
            htmlCards += '<div class="card-address">';
            htmlCards += '<input type="radio" name="address-chk" class="address-chk" value="' + addresses[i].guidID + '">';
            htmlCards += '<p class="address-name">' + addresses[i].street.name + '</p>';
            htmlCards += '<p class="address-number">' + addresses[i].number;
            htmlCards += addresses[i].comment == null || addresses[i].comment == '' ? '' : ', ' + addresses[i].comment + '</p>';
            htmlCards += '<p class="address-city">' + addresses[i].city.name + ' - ' + addresses[i].state.initials + '</p>';
            htmlCards += '<p class="address-country">' + addresses[i].street.postalCode + ' - ' + addresses[i].country.name + '</p>';
            htmlCards += '</div>';
            htmlCards += '<hr />';
        });

        $('#user-addresses').html(htmlCards);
    }
}

/*$('input[name="address-chk"]:checked').val();*/