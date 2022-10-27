let orderGuid;

let orders = [];
let cancelleds = [];
let completeds = [];
let pendings = [];

$(document).ready(function () {

    orders = GetByUser();

    if (orders != null) {
        GroupByOrderStatus(orders);
        SetHtmlElements();
    }
});

function GroupByOrderStatus(orders) {

    const orderStatus = [
        'Cancelled',
        'Completed',
        'Pending'
    ];

    for (let i = 0; i < orders.length; i++) {

        if (orders[i].orderStatus.name == orderStatus[0])
            cancelleds.push(orders[i])
        else if (orders[i].orderStatus.name == orderStatus[1])
            completeds.push(orders[i]);
        else
            pendings.push(orders[i]);
    }

}

function SetHtmlElements() {

    if (completeds.length == 0) $('.tabCompletedOrders').text('No data');
    if (pendings.length == 0) $('.tabPending').text('No data');
    if (cancelleds.length == 0) $('.tabCancelledReturned').text('No data');

    let htmlCode = '';


    /****************** Completed orders tab ******************/
    if (completeds.length > 0) {

        for (let i = 0; i < completeds.length; i++) {

            htmlCode += `<a onclick="OpenOrderDetailsModal($(this));" href="#" style="text-decoration: none; color: #000000;" id="${completeds[i].guidID}">`;

            if (!completeds[i].tracking)
                htmlCode += '<p><strong>Order not finished</strong></p>';
            else
                htmlCode += `<p>Order <strong>${completeds[i].tracking}</strong></p>`;

            htmlCode += `<p>Created at: <strong>${new Date(completeds[i].created_at).toISOString().split('T')[0]}</strong></p>`;
            htmlCode += '<hr>';
            htmlCode += '</a>';
        }
    } else {

        htmlCode += 'No content';
    }

    $('.tabCompletedOrders').html(htmlCode);
    /****************** Completed orders tab ******************/


    htmlCode = '';

    /******************* Pending orders tab *******************/
    if (pendings.length > 0) {

        for (let i = 0; i < pendings.length; i++) {
            htmlCode += `<a onclick="OpenOrderDetailsModal($(this));" href="#" style="text-decoration: none; color: #000000;" id="${pendings[i].guidID}">`;

            if (!pendings[i].tracking)
                htmlCode += '<p><strong>Order not finished</strong></p>';
            else
                htmlCode += `<p>Order <strong>${pendings[i].tracking}</strong></p>`;

            htmlCode += `<p>Created at: <strong>${new Date(pendings[i].created_at).toISOString().split('T')[0]}</strong></p>`;
            htmlCode += '<hr>';
            htmlCode += '</a>';
        }
    } else {

        htmlCode += 'No content';
    }
    
    
    $('.tabPending').html(htmlCode);
    /******************* Pending orders tab *******************/


    htmlCode = '';

    /****************** Cancelleds orders tab *****************/
    if (cancelleds.length > 0) {

        for (let i = 0; i < cancelleds.length; i++) {
            htmlCode += `<a onclick="OpenOrderDetailsModal($(this));" href="#" style="text-decoration: none; color: #000000;" id="${cancelleds[i].guidID}">`;

            if (!cancelleds[i].tracking)
                htmlCode += '<p><strong>Order not finished</strong></p>';
            else
                htmlCode += `<p>Order <strong>${cancelleds[i].tracking}</strong></p>`;

            htmlCode += `<p>Created at: <strong>${new Date(cancelleds[i].created_at).toISOString().split('T')[0]}</strong></p>`;
            htmlCode += '<hr>';
            htmlCode += '</a>';
        }
    } else {

        htmlCode += 'No content';
    }
    

    $('.tabCancelledReturned').html(htmlCode);
    /****************** Cancelleds orders tab *****************/

}

function OpenOrderDetailsModal(order) {

    ShowModal();

    $(document.body).css('overflow', 'hidden');

    let orderObj = orders.find(x => x.guidID == order[0].id);
    orderGuid = orderObj.guidID;

    htmlCode = '<div style="padding: 60px; margin-top: -10px;">'

    if (orderObj.tracking)
        htmlCode += `<p><strong>Tracking code:</strong> ${orderObj.tracking}</p>`;
    else
        htmlCode += '<p><strong>Order not finished</strong></p>';

    htmlCode += `<p><strong>Created at:</strong> ${new Date(orderObj.created_at).toISOString().split('T')[0]}</p>`;
    htmlCode += `<p><strong>Order status:</strong> ${CapitalizeFirstLetter(orderObj.orderStatus.name)}</p>`;
    htmlCode += '<hr></br>';
    htmlCode += '<h3>Total & payment:</h3>';

    if (orderObj.total)
        htmlCode += `<p><strong>Total:</strong> $ ${orderObj.total}</p>`;
    else
        htmlCode += '<p><strong>Order not finished</strong></p>';

    htmlCode += `<p><strong>Payment method:</strong> ${CapitalizeFirstLetter(orderObj.paymentMethod.name)}</p>`;
    htmlCode += '</br>';

    if (orderObj.orderStatus.name !== 'Completed') {
        htmlCode += `<button type="button" class="default-blue-button" onclick="window.location.href = '/Orders/SelectPay?order=${orderGuid}';">Edit order</button>`;
        htmlCode += '<button type="button" class="default-white-button">Cancel order</button>';
    }
    
    htmlCode += '<button type="button" class="default-white-button">Return order</button>';

    htmlCode += '</div>';

    $('.modal-content p').html(htmlCode);
}