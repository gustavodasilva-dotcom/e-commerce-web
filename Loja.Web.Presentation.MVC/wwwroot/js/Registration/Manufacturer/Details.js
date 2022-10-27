let guid;
let isEdit = false;
let editEnabled = false;
let isBrazilianCompany = false;

$(document).ready(function () {

    let params = (new URL(window.location.href)).searchParams;

    ValidateParams(params);
    SetHtmlElements();
    SetMasks();
});

$('input[name="localition"]').click(function () {

    SetVisibilityByLocation();
    SetContactMasks();
    SetAddressMasks();
    ReadonlyAddressInputs(isBrazilianCompany);
});

$('#btn-edit').click(function () {

    if (editEnabled) {

        ReadonlyElements(false);
        SetContactMasks();
        SetAddressMasks();
        ReadonlyAddressInputs(isBrazilianCompany);

        $('#btn-register-update').css('display', 'block');
        $(this).css('display', 'none');
    }
})

$('#btn-register-update').click(function () {

    let model = {};

    model.GuidID = guid;
    model.Name = $('#edt_Name').val();
    model.BrazilianCompany = $('#localition_brazil:checked').length > 0 ? true : false;

    if (model.BrazilianCompany) {

        model.FederalTaxpayerRegistrationNumber = $('#edt_FedTaxPayer').val();
        model.StateTaxpayerRegistrationNumber = $('#edt_SttTaxPayer').val();
    } else {

        model.CAGE = $('#edt_Cage').val();;
        model.NCAGE = model.CAGE;
        model.SEC = $('#edt_Sec').val();
    }

    model.Contacts = SetContatModel();

    model.Addresses = SetAddressModel();
    model.Addresses.IsForeign = !model.BrazilianCompany;

    var modelSaved = SaveManufacturer(model);

    if (modelSaved != null) {
        if (isEdit) {
            window.location.reload();
        } else {
            window.location.href = '/Manufacturers/Details?guid=' + modelSaved.guidID;
        }
    }
});

//#region ValidateParams
function ValidateParams(params) {

    guid = params.get('guid');

    if (guid != null && guid === '') window.location.href = '/Default/Select?statusCode=400';
    if (guid != null && guid !== '') isEdit = true;
}
//#endregion

//#region SetHtmlElements
function SetHtmlElements() {

    if (isEdit) {

        ReadonlyElements(true);

        $('#btn-register-update').text('Update');

        var manufacturer = GetManufacturerByID(guid);

        if (manufacturer != null || manufacturer != undefined) {
            $('#edt_Name').val(manufacturer.name);

            isBrazilianCompany = manufacturer.brazilianCompany;

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

            SetContactHtmlElements(manufacturer.contact);

            SetAddressInputValues(manufacturer.address);
        }
    } else {

        $('#btn-edit').css('display', 'none');
        $('#btn-register-update').css('display', 'block');

        SetVisibilityByLocation();
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

//#region SetMasks
function SetMasks() {

    if ($('#localition_brazil:checked').length > 0) {
        $('#edt_FedTaxPayer').mask('00.000.000/0000-00');
    }

    SetContactMasks();
    SetAddressMasks();
}
//#endregion

//#region SetVisibilityByLocation
function SetVisibilityByLocation() {

    if ($('#localition_brazil:checked').length > 0) {

        $('#localition_brazil').prop('checked', true);

        $('#register-input-brazilian').css('display', 'block');
        $('#register-input-foreigh').css('display', 'none');

        isBrazilianCompany = true;
    } else {

        $('#localition_foreign').prop('checked', true);

        $('#register-input-foreigh').css('display', 'block');
        $('#register-input-brazilian').css('display', 'none');

        isBrazilianCompany = false;
    }
}
//#endregion