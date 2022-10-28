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

$('#edit').click(function () {

    isEdit = !isEdit;

    ReadonlyElements(isEdit);
    SetContactMasks();
    SetAddressMasks();
    ReadonlyAddressInputs(isBrazilianCompany, isEdit);

    if (isEdit)
        $('#btn-register-update').css('display', 'none');
    else
        $('#btn-register-update').css('display', 'block');

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

        $('#btn-register-update').css('display', 'block');

        SetVisibilityByLocation();
    }
}
//#endregion

//#region ReadonlyElements
function ReadonlyElements(readonly) {

    $('#edt_Name').prop('disabled', readonly);

    $('input[name="localition"]').prop('disabled', readonly);
    
    $('#edt_Cage').prop('disabled', readonly);
    $('#edt_Sec').prop('disabled', readonly);
    $('#edt_FedTaxPayer').prop('disabled', readonly);
    $('#edt_SttTaxPayer').prop('disabled', readonly);
    
    $('#edt_Phone').prop('disabled', readonly);
    $('#edt_Cellphone').prop('disabled', readonly);
    $('#edt_Email').prop('disabled', readonly);
    $('#edt_Website').prop('disabled', readonly);
    
    $('#address-postal-code').prop('disabled', readonly);
    $('#address-number').prop('disabled', readonly);
    $('#address-comment').prop('disabled', readonly);
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