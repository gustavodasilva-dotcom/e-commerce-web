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

                requestSuccessed = true;
            } else {

                requestSuccessed = false;

                if (result.RedirectToLogin) window.location.href = '/Accounts/Login';
                ShowMessageDiv(result.Message);
            }
        }
    });
}

function GetByUser() {

    let orders = {};

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Orders/GetByUser",
        success: function (result) {
            if (result.Code == 1) {

                orders = result.Order;

                requestSuccessed = true;
            } else {

                requestSuccessed = false;

                if (result.RedirectToLogin) window.location.href = '/Accounts/Login';
                ShowMessageDiv(result.Message);
            }
        }
    });

    return orders;
}

function ProcessOrder(orderID, orderTotal, finishOrder) {

    $.ajax({
        async: false,
        type: "POST",
        dataType: "json",
        data: {
            orderGuid: orderID,
            total: orderTotal,
            finishOrder: finishOrder
        },
        url: "/Orders/ProcessOrder",
        success: function (result) {
            if (result.Code == 1) {

                window.TrackingNumber = { };
                window.TrackingNumber = result.TrackingNumber;

                requestSuccessed = true;
            } else {

                requestSuccessed = false;

                if (result.RedirectToLogin) window.location.href = '/Accounts/Login';
                ShowMessageDiv(result.Message);
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