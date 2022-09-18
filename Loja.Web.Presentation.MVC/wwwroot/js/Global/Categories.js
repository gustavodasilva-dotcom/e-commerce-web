function GetCategories() {
    let categories = null;

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Categories/Get",
        success: function (result) {
            if (result.Code == 1) {
                categories = result.Categories;

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

    return categories;
}

function GetSubcategories() {
    let subcategories = null;

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Subcategories/Get",
        success: function (result) {
            if (result.Code == 1) {
                subcategories = result.Subcategories;

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

    return subcategories;
}