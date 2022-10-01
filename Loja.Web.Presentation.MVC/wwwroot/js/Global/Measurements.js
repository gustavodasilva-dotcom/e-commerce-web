function GetMeasurements() {
    let measurements = null;

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Measurements/Get",
        success: function (result) {
            if (result.Code == 1) {
                measurements = result.Measurements;

                if (result.RedirectToHome)
                    window.location.href = '/Home/Index';
            }
            else {
                if (result.RedirectToLogin) {
                    window.location.href = '/Accounts/Login';
                } else if (result.RedirectToHome) {
                    window.location.href = '/Home/Index';
                } else {
                    ShowMessageDiv(result.Message);
                    return;
                }
            }
        }
    });

    return measurements;
}