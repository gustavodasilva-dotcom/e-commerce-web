let domain;
let isEdit = false;

$(document).ready(function () {
    let params = (new URL(window.location.href)).searchParams;

    ValidateParams(params);
    SetHtmlElementsByProcess(process);
});

$('#register').click(function () {
    let model = {};

    model.Name = $('#edt_Name').val();

    if (process === '2') model.CategoryGuid = $('#select_Categories').val();

    $.ajax({
        async: false,
        type: "POST",
        dataType: "json",
        data: { model: model },
        url: `/${domain}/Register`,
        success: function (result) {
            if (result.Code == 1) {
                let modelResult = result[`${domain}`];

                if (modelResult != null || modelResult != undefined)
                    window.location.href = `/${domain}/Index`;

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
});

//#region ValidateParams
function ValidateParams(params) {
    process = params.get('process');
    let method = params.get('method');

    if (process == null || process == undefined)
        window.location.href = '/Default/Select?statusCode=400';

    if (method != null || method != undefined
        && method === '1' || method === '0')
        isEdit = method === '1' ? true : false;
    else
        window.location.href = '/Default/Select?statusCode=400';
}
//#endregion

//#region SetHtmlElementsByProcess
function SetHtmlElementsByProcess(process) {
    $('.2').hide();

    switch (process) {
        case '1':
            domain = 'Categories';
            break;
        case '2':
            domain = 'Subcategories';
            $('.2').show();
            break;
        case '3':
            domain = 'Currencies';
            break;
        default:
            window.location.href = '/Default/Select?statusCode=400';
            break;
    }

    document.title = domain + ' - Loja';
    $('.register-title').text(domain);

    var btnText = isEdit ? 'Alter' : 'Register';
    $('#register').text(btnText);

    if (process === '2') {
        var categories = GetCategories();

        if (categories != null || categories != undefined)
            SetComboOptions(categories, 'select_Categories');
    }
}
//#endregion