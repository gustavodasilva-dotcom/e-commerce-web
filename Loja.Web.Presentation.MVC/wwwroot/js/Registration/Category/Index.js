$(document).ready(function () {
    var categories = GetCategories();

    if (categories != null || categories != undefined)
        SetTableElements(categories);
});

function SetTableElements(categories) {
    let htmlCode = '';

    htmlCode += '<table class="register-table" id="table_Categories">';
    
    htmlCode += '<tr class="register-table-tr">';
    htmlCode +=     '<th class="register-table-th">Name</th>';
    htmlCode += '</tr>';

    htmlCode += '<tr class="register-table-tr">';

    if (categories.length == 0) {
        htmlCode += '<tr class="register-table-tr">';
        htmlCode +=     '<td class="register-table-td" id="manufacturer-name" data-guid="">No data registered.</td>';
        htmlCode += '</tr>';
    } else {
        for (let i = 0; i < categories.length; i++) {
            htmlCode += '<tr class="register-table-tr">';
            htmlCode +=     `<td class="register-table-td" name="manufacturer-name" data-guid="${categories[i].guidID}">${categories[i].name}</td>`;
            htmlCode += '</tr>';
        }
    }

    htmlCode += '</table>';
    
    $('#content').html(htmlCode);
}