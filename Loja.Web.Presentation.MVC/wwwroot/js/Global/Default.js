let statusCode;
let process;
let pageTitle;
let checkProcess = false;

$(document).ready(function () {
    let params = (new URL(window.location.href)).searchParams;

    ValidateParams(params);
    document.title = pageTitle + ' - Loja';

    SetHtmlElements();
});

function ValidateParams(params) {
    statusCode = params.get('statusCode');
    process = params.get('process');

    switch (statusCode) {
        case '400':
            pageTitle = 'Bad Request';
            checkProcess = false;
            break;
        case '401':
            pageTitle = 'Unauthorized';
            checkProcess = false;
            break;
        case '201':
            pageTitle = 'OK';
            checkProcess = true;
            break;
        default:
            window.location.href = '/Home/Index';
            break;
    }

    if (checkProcess) {
        switch (process) {
            case '1':
                pageTitle = 'Thank you!';
                break;
            default:
                window.location.href = '/Home/Index';
        }
    }
}

function SetHtmlElements() {
    let htmlCode = '';

    $('#page-title').text(pageTitle);

    if (statusCode === '201' && process === '1') {
        htmlCode += '<p>Your order was created sucessfully.</p>';

        if (window.Tracking == null || window.Tracking == undefined) window.location.href = '/Home/Index';

        htmlCode += `<p>Order tracking: <b>${window.Tracking}</b></p>`;
    }

    if (statusCode === '400') {
        htmlCode += '<p>The resource was not requested the way the serve expected.</p>';
        htmlCode += '<p>Please, contact the system administrator.</p>';
    }

    if (statusCode === '401') {
        htmlCode += '<p>The user logged does not have authorization to access the page requested.</p>';
    }

    $('#content').html(htmlCode);
}