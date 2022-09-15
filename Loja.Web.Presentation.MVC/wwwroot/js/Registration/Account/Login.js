$('#login').click(function () {
    let emailUsername = $('#emailUsername').val();

    let password = $('#password').val();

    Login(emailUsername, password);
});