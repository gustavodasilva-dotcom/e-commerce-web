let orderID;

$(document).ready(function () {

    let params = (new URL(window.location.href)).searchParams;
    orderID = params.get('orderID');

    GetOrderDetails(orderID);

    GetUserAddresses();
    SetAddressesCards();
});

$('#btn-register-address').click(function () {

    $('#register-new-address').css('display', 'block');
    $('#btn-move-next').css('display', 'none');
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

$('#btn-move-next').click(function () {

    let addressID = $('input[name="address-chk"]:checked').val();

    SetDeliveryAddress(orderID, addressID);

    window.location.href = '/Orders/Overview?orderID=' + orderID;
});

function SetAddressesCards() {

    if (window.Addresses.length > 0) {

        let addresses = window.Addresses;

        let htmlCards = '';

        $.each(addresses, function (i, item) {
            htmlCards += '<div class="card-address">';
            htmlCards +=    `<input type="radio" name="address-chk" class="address-chk" data-guid="${addresses[i].guidID}" value="${addresses[i].guidID}">`;
            htmlCards +=    `<p class="address-name">${CapitalizeFirstLetter(addresses[i].street.name)}</p>`;
            htmlCards +=    `<p class="address-number">${CapitalizeFirstLetter(addresses[i].number)}`;
            htmlCards +=    addresses[i].comment == null || CapitalizeFirstLetter(addresses[i].comment) == '' ?
                                '' : ', ' + CapitalizeFirstLetter(addresses[i].comment) + '</p>';
            htmlCards +=    `<p class="address-city">${CapitalizeFirstLetter(addresses[i].city.name)} - 
                                ${CapitalizeFirstLetter(addresses[i].state.initials)}</p>`;
            htmlCards +=    `<p class="address-country">${addresses[i].street.postalCode} - ${CapitalizeFirstLetter(addresses[i].country.name)}</p>`;
            htmlCards += '</div>';
            htmlCards += '<hr />';
        });

        $('#user-addresses').html(htmlCards);

        if (window.Order.deliveryAddress != null) {

            let deliveryAddress = window.Addresses.find(x => x.guidID === window.Order.deliveryAddress.guidID);
            $(`[data-guid="${deliveryAddress.guidID}"]`).prop('checked', true);
        }

    }
}