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