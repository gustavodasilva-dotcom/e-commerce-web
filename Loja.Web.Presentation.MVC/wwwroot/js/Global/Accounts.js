function GetUserRoles() {
    let userRoles = null;

    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/Accounts/GetUserRoles",
        success: function (result) {
            if (result.Code == 1) {
                userRoles = result.UserRoles;

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

    return userRoles;
}

function RegisterUser(userModel) {
    let user = null;

    $.ajax({
        async: false,
        type: "POST",
        dataType: "json",
        data: { model: userModel },
        url: "/Accounts/Register",
        success: function (result) {
            if (result.Code == 1) {
                user = result.User;

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

    return user;
}

function Login(emailUsername, password) {
    $.ajax({
        async: false,
        type: "POST",
        dataType: "json",
        data: { emailUsername: emailUsername, password: password },
        url: "/Accounts/Login",
        success: function (result) {
            if (result.Code == 1) {
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
}

function Logout() {
    $.ajax({
        async: false,
        type: "POST",
        dataType: "json",
        url: "/Accounts/Logout",
        success: function (result) {
            if (result.Code == 1) {
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
}