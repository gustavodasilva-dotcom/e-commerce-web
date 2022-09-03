function GetPaymentTypes() {
    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Payments/GetPaymentTypes",
        success: function (result) {
            if (result.Code == 1) {
                window.PaymentTypes = {};
                window.PaymentTypes = result.PaymentTypes;
            }
            else {
                alert(result.Message);
            }
        }
    });
}