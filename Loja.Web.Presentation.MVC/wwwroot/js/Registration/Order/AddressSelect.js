let orderID;

$(document).ready(function () {
    let params = (new URL(window.location.href)).searchParams;
    orderID = params.get('orderID');
});

$('#btn-register-address').click(function () {
    $('#register-new-address').css('display', 'block');
});

$('#register-address').click(function () {
    RegisterUserAddress($('#address-postal-code').val());
});