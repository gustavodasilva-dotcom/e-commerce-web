let orderID;

$(document).ready(function () {
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

    for (let i = 0; i < order.products.length; i++) {
        htmlCode += '<h2 class="overview-title-2">Products:</h2>';

        htmlCode += '<div class="product-card">';
        
        htmlCode +=     `<input type="hidden" id="${order.products[i].guidID}" value="${order.products[i].guidID}" >`;
        htmlCode +=     `<h3 class="overview-title-3">${order.products[i].name}</h3>`;

        htmlCode +=     `<p>Unitary: ${order.products[i].price}</p>`;
        htmlCode +=     `<p>Amount: ${order.products[i].price * order.products[i].quantity}</p>`;
        htmlCode +=     `<label for="product-${i}">Quantity:</label>`;
        htmlCode +=     `<input type="number" id="product-${i}" value="${order.products[i].quantity}">`;

        htmlCode += '</div>';

        if (order.products.length > 1) htmlCode += '<hr />';
    }

    $('.product-details-left').html(htmlCode);
}

function SetHtmlElementsRight(address, order) {
    let htmlCode = '';

    htmlCode += '<h2 class="overview-title-2">Delivery address:</h2>';

    htmlCode += '<div class="address-card">';
    
    htmlCode +=     `<h3 class="overview-title-3">${address.street.name}</h3>`;

    htmlCode +=     `<p>${address.number}`;
    htmlCode +=     address.comment == null || address.comment == '' ? '' : ', ' + address.comment + '</p>';
    htmlCode +=     `<p>${address.city.name} - ${address.state.initials}</p>`;
    htmlCode +=     `<p>${address.street.postalCode} - ${address.country.name}</p>`;

    htmlCode += '</div>';

    htmlCode += '<h2 class="overview-title-2">Payment method:</h2>';

    htmlCode += `<h3 class="overview-title-3">${order.paymentMethod.name}</h3>`;

    if (order.paymentMethod.isCard) {

    }

    $('.product-details-right').html(htmlCode);
}