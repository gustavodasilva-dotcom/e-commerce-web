$(document).ready(function () {

    var manufacturers = GetManufacturers();

    if (manufacturers != null || manufacturers != undefined)
        SetTableElements(manufacturers);
});

function SetTableElements(manufacturers) {

    let htmlCode = '';

    htmlCode += '<table class="register-table" id="table_Categories">';

    htmlCode += '<tr class="register-table-tr">';
    htmlCode +=     '<th class="register-table-th">Name</th>';
    htmlCode += '</tr>';

    htmlCode += '<tr class="register-table-tr">';

    if (manufacturers.length == 0) {

        htmlCode += '<tr class="register-table-tr">';
        htmlCode +=     '<td class="register-table-td" id="manufacturer-name" data-guid="">No data registered.</td>';
        htmlCode += '</tr>';
    } else {

        for (let i = 0; i < manufacturers.length; i++) {
            htmlCode += '<tr class="register-table-tr">';
            htmlCode +=     '<td class="register-table-td" name="manufacturer-name">';
            htmlCode +=         `<a href="/Manufacturers/Details?guid=${manufacturers[i].guidID}">`;
            htmlCode +=             `<div style="text-transform: uppercase;">${CapitalizeFirstLetter(manufacturers[i].name)}`;
            htmlCode +=             '</div>';
            htmlCode +=         '</a>';
            htmlCode +=     '</td>';
            htmlCode += '</tr>';
        }
    }

    htmlCode += '</table>';

    $('#content').html(htmlCode);

    window.Manufacturers = manufacturers;
}