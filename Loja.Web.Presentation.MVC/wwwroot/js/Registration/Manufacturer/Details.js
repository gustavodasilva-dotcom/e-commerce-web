let guid;
let isEdit = false;

$(document).ready(function () {
    let params = (new URL(window.location.href)).searchParams;

    ValidateParams(params);
    SetHtmlElements();
});

//#region ValidateParams
function ValidateParams(params) {
    guid = params.get('guid');

    if (guid != null && guid === '') window.location.href = '/Default/Select?statusCode=400';
    if (guid != null && guid !== '') isEdit = true;
}
//#endregion

//#region SetHtmlElementsByProcess
function SetHtmlElements() {
    if (isEdit) {
        ReadonlyElements(true);

        var manufacturer = GetManufacturerByID(guid);

        if (manufacturer != null || manufacturer != undefined) {
            $('#edt_Name').val(manufacturer.name);

            if (manufacturer.brazilianCompany) {
                $('#localition_brazil').prop('checked', true);
                $('#register-input-brazilian').css('display', 'block');
            } else {
                $('#localition_foreign').prop('checked', true);
                $('#register-input-foreigh').css('display', 'block');
            }

            $('#edt_Cage').val(manufacturer.cage);
            $('#edt_Sec').val(manufacturer.sec);
            $('#edt_FedTaxPayer').val(manufacturer.federalTaxpayerRegistrationNumber);
            $('#edt_SttTaxPayer').val(manufacturer.stateTaxpayerRegistrationNumber);

            $('#edt_Phone').val(manufacturer.contact.phone);
            $('#edt_Cellphone').val(manufacturer.contact.cellphone);
            $('#edt_Email').val(manufacturer.contact.email);
            $('#edt_Website').val(manufacturer.contact.website);

            $('#address-postal-code').val(manufacturer.address.street.postalCode);
            $('#address-number').val(manufacturer.address.number);
            $('#address-comment').val(manufacturer.address.comment);

            SetAddressInputValues(manufacturer.address);
        }
    }
}
//#endregion

//#region ReadonlyElements
function ReadonlyElements(readonly) {
    if (readonly) {
        $('#edt_Name').prop('readonly', 'true');

        $('input[name="localition"]').prop('disabled', 'true');

        $('#edt_Cage').prop('readonly', 'true');
        $('#edt_Sec').prop('readonly', 'true');
        $('#edt_FedTaxPayer').prop('readonly', 'true');
        $('#edt_SttTaxPayer').prop('readonly', 'true');

        $('#edt_Phone').prop('readonly', 'true');
        $('#edt_Cellphone').prop('readonly', 'true');
        $('#edt_Email').prop('readonly', 'true');
        $('#edt_Website').prop('readonly', 'true');

        $('#address-postal-code').prop('readonly', 'true');
        $('#address-number').prop('readonly', 'true');
        $('#address-comment').prop('readonly', 'true');
    } else {
        $('#edt_Name').prop('readonly', 'false');

        $('input[name="localition"]').prop('disabled', 'false');

        $('#edt_Cage').prop('readonly', 'false');
        $('#edt_Sec').prop('readonly', 'false');
        $('#edt_FedTaxPayer').prop('readonly', 'false');
        $('#edt_SttTaxPayer').prop('readonly', 'false');

        $('#edt_Phone').prop('readonly', 'false');
        $('#edt_Cellphone').prop('readonly', 'false');
        $('#edt_Email').prop('readonly', 'false');
        $('#edt_Website').prop('readonly', 'false');

        $('#address-postal-code').prop('readonly', 'false');
        $('#address-number').prop('readonly', 'false');
        $('#address-comment').prop('readonly', 'false');
    }
}
//#endregion