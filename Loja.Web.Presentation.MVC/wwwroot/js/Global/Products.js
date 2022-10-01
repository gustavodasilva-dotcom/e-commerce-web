function GetProductByID(guid) {
    let products = null;

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        data: { guid: guid },
        url: "/Products/Get",
        success: function (result) {
            if (result.Code == 1) {
                products = result.Products;

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

    return products;
}