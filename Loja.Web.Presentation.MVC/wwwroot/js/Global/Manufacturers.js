function GetManufacturers() {
    let manufacturers = null;

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Manufacturers/Get",
        success: function (result) {
            if (result.Code == 1) {
                manufacturers = result.Manufacturers;

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

    return manufacturers;
}

function GetManufacturerByID(id) {
    let manufacturer = null;

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        data: { guid: id },
        url: "/Manufacturers/GetByID",
        success: function (result) {
            if (result.Code == 1) {
                manufacturer = result.Manufacturers;

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

    return manufacturer;
}