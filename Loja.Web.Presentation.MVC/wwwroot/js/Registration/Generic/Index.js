let guid;
let domain;
let isEdit = false;

$(document).ready(function () {

    let params = (new URL(window.location.href)).searchParams;

    ValidateParams(params);
    SetHtmlElementsByProcess(process);
});

$('#register').click(function () {

    let model = {};

    if (isEdit) model.GuidID = guid;

    model.Name = $('#edt_Name').val();

    if (process === '2') model.CategoryGuid = $('#select_Categories').val();

    $.ajax({
        async: false,
        type: "POST",
        dataType: "json",
        data: { model: model },
        url: `/${domain}/${isEdit ? 'Update' : 'Register'}`,
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
                    ShowModal(result.Message);
                    return;
                }
            }
        }
    });
});

//#region ValidateParams
function ValidateParams(params) {

    let method = params.get('method');
    process = params.get('process');
    guid = params.get('guid');

    if (process == null || process == undefined)
        window.location.href = '/Default/Select?statusCode=400';

    if (method != null || method != undefined
        && method === '1' || method === '0')
        isEdit = method === '1' ? true : false;
    else
        window.location.href = '/Default/Select?statusCode=400';

    if (isEdit && (guid == null || guid == undefined || guid == ''))
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

    var btnText = isEdit ? 'Update' : 'Register';
    $('#register').text(btnText);

    var categories = GetCategories();
    var subcategories = GetSubcategories();

    if (!isEdit) {
        switch (process) {
            case '1':
                break;
            case '2':
                categories = GetCategories();
                if (categories != null || categories != undefined) {
                    $('#select_Categories').append('<option value="" selected disabled>Select a category</option>');
                    SetComboOptions(categories, 'select_Categories');
                }
                break;
        }
    } else {
        switch (process) {
            case '1':
                var category = categories.find(x => x.guidID == guid);
                $('#edt_Name').val(category.name);
                break;
            case '2':
                var subcategory = subcategories.find(x => x.guidID == guid);
                $('#edt_Name').val(subcategory.name);
                if (categories != null || categories != undefined) {
                    $('#select_Categories').append('<option value="" selected disabled>Select a category</option>');
                    SetComboOptions(categories, 'select_Categories');
                    $('#select_Categories').val(subcategory.category.guidID);
                }
                break;
        }
    }
}
//#endregion