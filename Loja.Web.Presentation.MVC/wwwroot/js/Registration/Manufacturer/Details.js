let guid;
let isEdit = false;
let editEnabled = false;

$(document).ready(function () {
    let params = (new URL(window.location.href)).searchParams;

    ValidateParams(params);
    SetHtmlElements();
});

$('#btn-edit').click(function () {
    if (editEnabled) {
        ReadonlyElements(false);
        $('#btn-register-update').css('display', 'block');
    }
})

$('#btn-register-update').click(function () {
    let model = {};

    model.GuidID = guid;
    model.Name = $('#edt_Name').val();
    model.BrazilianCompany = $('input[name="localition"]').val();

    if (model.BrazilianCompany) {
        model.FederalTaxpayerRegistrationNumber = $('#edt_FedTaxPayer').val();
        model.StateTaxpayerRegistrationNumber = $('#edt_SttTaxPayer').val();
    } else {
        model.CAGE = $('#edt_Cage').val();;
        model.NCAGE = model.CAGE;
        model.SEC = $('#edt_Sec').val();
    }

    model.Contacts.Phone = $('#edt_Phone').val();
    model.Contacts.Cellphone = $('#edt_Cellphone').val();
    model.Contacts.Email = $('#edt_Email').val();
    model.Contacts.Website = $('#edt_Website').val();

    model.Addresses.PostalCode = $('#address-postal-code').val();
    model.Addresses.Name = $('#address-name').val();
    model.Addresses.Number = $('#address-number').val();
    model.Addresses.Comment = $('#address-comment').val();
    model.Addresses.Neighborhood = $('#address-neighborhood').val();
    model.Addresses.City = $('#address-city').val();
    model.Addresses.State = $('#address-state').val();
    model.Addresses.Country = $('#address-country').val();
    model.Addresses.IsForeign = model.BrazilianCompany;

    if (SaveManufacturer(model) != null)
        window.location.reload();
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

        $('#btn-register-update').text('Update');

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
    $('#edt_Name').prop('readonly', readonly);

    $('input[name="localition"]').prop('disabled', readonly);
    
    $('#edt_Cage').prop('readonly', readonly);
    $('#edt_Sec').prop('readonly', readonly);
    $('#edt_FedTaxPayer').prop('readonly', readonly);
    $('#edt_SttTaxPayer').prop('readonly', readonly);
    
    $('#edt_Phone').prop('readonly', readonly);
    $('#edt_Cellphone').prop('readonly', readonly);
    $('#edt_Email').prop('readonly', readonly);
    $('#edt_Website').prop('readonly', readonly);
    
    $('#address-postal-code').prop('readonly', readonly);
    $('#address-number').prop('readonly', readonly);
    $('#address-comment').prop('readonly', readonly);
}
//#endregion