let orderID;

$(document).ready(function () {
    let params = (new URL(window.location.href)).searchParams;
    orderID = params.get('orderID');

    GetOrderDetails(orderID);
});