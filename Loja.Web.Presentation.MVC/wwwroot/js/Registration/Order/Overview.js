let orderID;
let orderTotal;

$(document).ready(function () {

    $(document.body).css('overflow-x', 'hidden');

    let params = (new URL(window.location.href)).searchParams;
    orderID = params.get('orderID');

    GetOrderDetails(orderID);
    GetOrderAddress(orderID);
    SetHtmlElements();
});

function SetHtmlElements() {
    let order = window.Order;
    let address = window.Address;

    $('#orderGuid').val(order.guidID);
    $('#addressGuid').val(address.guidID);
    $('#paymentGuid').val(order.paymentMethod.guidID);

    if (order != null || order != undefined) {
        SetHtmlElementsLeft(order);
        SetHtmlElementsRight(address, order);
    }
}

function SetHtmlElementsLeft(order) {
    let htmlCode = '';

    orderTotal = CalculateOrderTotal(order);

    htmlCode += '<h2 class="overview-title-2">Products:</h2>';

    for (let i = 0; i < order.products.length; i++) {
        htmlCode += '<div class="product-card">';
        
        htmlCode +=     `<input type="hidden" id="${order.products[i].guidID}" value="${order.products[i].guidID}" >`;
        htmlCode +=     `<h3 class="overview-title-3">${CapitalizeFirstLetter(order.products[i].name)}</h3>`;

        htmlCode +=     '<div class="product-details">';

        htmlCode +=         '<br />';

        htmlCode +=         '<label>Unitary: </label>';
        htmlCode +=         `<p>${order.products[i].currency.symbol} ${order.products[i].price}</p>`;

        htmlCode +=         '<label style="margin-left: 20px;">Amount: </label>';
        htmlCode +=         `<p>${order.products[i].currency.symbol} ${order.products[i].price * order.products[i].quantity}</p>`;

        htmlCode +=         `<label style="margin-left: 20px;">Quantity:</label>`;
        htmlCode +=         `<p>${order.products[i].quantity}</p>`;

        htmlCode +=     '</div>'

        htmlCode += '</div>';

        if (order.products.length > 1) htmlCode += '<hr />';
    }

    htmlCode += '<div class="overview-price">';

    htmlCode +=     `<label>Order total:</label><p>${order.products[0].currency.symbol} ${orderTotal}</p>`;

    htmlCode += '</div>'

    $('.product-details-left').html(htmlCode);
}

function SetHtmlElementsRight(address, order) {
    let htmlCode = '';

    htmlCode += '<h2 class="overview-title-2">Status:</h2>';

    htmlCode += `<p>${CapitalizeFirstLetter(order.orderStatus.name)}</p>`;

    if (order.orderStatus.name === 'Completed')
        DisableButton('btn-finish-order', true);

    if (order.orderStatus.name === 'Cancelled')
        DisableButton('btn-cancel-order', true);

    htmlCode += '<h2 class="overview-title-2">Delivery address:</h2>';

    htmlCode += '<div class="address-card">';
    
    htmlCode +=     `<h3 class="overview-title-3">${CapitalizeFirstLetter(address.street.name)}, ${CapitalizeFirstLetter(address.number)}</h3>`;
    htmlCode +=     `<p>${CapitalizeFirstLetter(address.comment)}</p>`;
    htmlCode +=     `<p>${CapitalizeFirstLetter(address.city.name)} - ${CapitalizeFirstLetter(address.state.initials)}</p>`;
    htmlCode +=     `<p><span class="postal-code">${address.street.postalCode}</span> - ${CapitalizeFirstLetter(address.country.name)}</p>`;

    htmlCode += '</div>';

    htmlCode += '<h2 class="overview-title-2" style="margin-top: 20px;">Payment method:</h2>';

    htmlCode += `<p style="font-size: 20px;"><b>${CapitalizeFirstLetter(order.paymentMethod.name)}</b></p>`;

    $('.product-details-right').html(htmlCode);

    $('.postal-code').mask('00000-000');
}

$('#btn-finish-order').click(function () {
    ProcessOrder(orderID, orderTotal, true);
});

$('#btn-cancel-order').click(function () {
    ProcessOrder(orderID, orderTotal, false);
});

$('#btn-alter-order').click(function () {
    alert('To be developed.');
});