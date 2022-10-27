function GetCurrencies() {

    let currencies = null;

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Currencies/Get",
        success: function (result) {
            if (result.Code == 1) {
                currencies = result.Currencies;

                if (result.RedirectToHome)
                    window.location.href = '/Home/Index';
            }
            else {
                if (result.RedirectToLogin) {
                    window.location.href = '/Accounts/Login';
                } else if (result.RedirectToHome) {
                    window.location.href = '/Home/Index';
                } else {
                    ShowModal(result.Message);
                    return;
                }
            }
        }
    });

    return currencies;
}