$('#logout-button').click(function () {
    $.ajax({
        async: false,
        type: "POST",
        dataType: "json",
        url: "/Accounts/Logout",
        success: function (result) {
            if (result.Code == 1) {
                window.location.href = '/Home/Index';
            }
            else {
                alert(result.Message);
            }
        }
    });
});

function ShowMessageError(message) {
    $('.register-alert').show();
    $('.register-alert p').text(message);
}