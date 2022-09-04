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

function StepOne(stepOne) {
    $.ajax({
        async: false,
        type: "POST",
        dataType: "json",
        data: { model: stepOne },
        url: "/Orders/StepOne",
        success: function (result) {
            if (result.Code == 1) {
                window.StepOne = {};
                window.StepOne = result.Order;
            }
            else {
                alert(result.Message);
            }
        }
    });
}