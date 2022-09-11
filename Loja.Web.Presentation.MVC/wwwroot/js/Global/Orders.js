function GetOrderDetails(orderID) {
    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        data: { orderGuid: orderID },
        url: "/Orders/GetOrderDetails",
        success: function (result) {
            if (result.Code == 1) {
                window.Order = {};
                window.Order = result.Order;
            } else {
                if (result.RedirectToLogin) window.location.href = '/Accounts/Login';
                alert(result.Message);
            }
        }
    });
}

function FinishOrder(orderID, orderTotal, finishOrder) {
    $.ajax({
        async: false,
        type: "POST",
        dataType: "json",
        data: {
            orderGuid: orderID,
            total: orderTotal,
            finishOrder: finishOrder
        },
        url: "/Orders/FinishOrder",
        success: function (result) {
            if (result.Code == 1) {
                window.TrackingNumber = null;
                window.TrackingNumber = result.TrackingNumber;
            } else {
                if (result.RedirectToLogin) window.location.href = '/Accounts/Login';
                alert(result.Message);
            }
        }
    });
}

function CalculateOrderTotal(order) {
    let orderTotal = 0;

    for (let i = 0; i < order.products.length; i++)
        orderTotal += order.products[i].price != null ? order.products[i].price * order.products[i].quantity : 0;

    return orderTotal;
}