$(document).ready(function () {
    var userRoles = GetUserRoles();

    if (userRoles != null || userRoles != undefined)
        SetComboOptions(userRoles, 'select_userRoles');
});

$('#register-user').click(function () {
    let userModel = {};

    userModel.Name = $('#edt_Name').val();

    userModel.Email = $('#edt_Email').val();

    userModel.Login = $('#edt_Login').val();

    userModel.Password = $('#edt_Password').val();

    userModel.UserRoleGuid = $('#select_userRoles').val();

    var user = RegisterUser(userModel);

    if (user != null || user != undefined) {
        ShowMessageDiv('User registered sucessfully.');

        ClearFields();
    }
});

function ClearFields() {
    $('#edt_Name').val(null);

    $('#edt_Email').val(null);

    $('#edt_Login').val(null);

    $('#edt_Password').val('');

    $('#select_userRoles').val('');
}