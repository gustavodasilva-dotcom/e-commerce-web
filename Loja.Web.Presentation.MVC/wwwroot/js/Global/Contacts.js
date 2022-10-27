function SetContactMasks() {

    if (isBrazilianCompany) {

        $('#edt_Phone').mask('(00) 0000-0000');
        $('#edt_Cellphone').mask('(00) 00000-0000');
    } else {

        $('#edt_Phone').unmask();
        $('#edt_Cellphone').unmask();
    }
}

function SetContactHtmlElements(model) {

    $('#edt_Phone').val(model.phone);
    $('#edt_Cellphone').val(model.cellphone);
    $('#edt_Email').val(model.email);
    $('#edt_Website').val(model.website);
}

function SetContatModel() {

    let contacts = {};

    contacts.Phone = $('#edt_Phone').val();
    contacts.Cellphone = $('#edt_Cellphone').val();
    contacts.Email = $('#edt_Email').val();
    contacts.Website = $('#edt_Website').val();

    return contacts;
}