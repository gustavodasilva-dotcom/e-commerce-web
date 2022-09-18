$(document).ready(function () {
    var subcategories = GetSubcategories();

    if (subcategories != null || subcategories != undefined)
        SetTableElements(subcategories);
});

function SetTableElements(subcategories) {
    let htmlCode = '';

    var categories = GetCategories();

    htmlCode += '<table class="register-table" id="table_Categories">';

    if (categories != null || categories != undefined) {
        htmlCode += '<table class="register-table" id="table_Categories">';

        for (let i = 0; i < categories.length; i++) {
            var catSub = subcategories.filter(x => x.category.name == categories[i].name);

            htmlCode += '<tr class="register-table-tr">';
            htmlCode +=     `<th class="register-table-th">${categories[i].name}</th>`;
            htmlCode += '</tr>';

            for (let i = 0; i < catSub.length; i++) {
                htmlCode += '<tr class="register-table-tr">';
                htmlCode +=     `<td class="register-table-td" name="manufacturer-name" data-guid="${catSub[i].guidID}">${catSub[i].name}</td>`;
                htmlCode += '</tr>';
            }
        }
    } else {
        htmlCode += '<tr class="register-table-tr">';
        htmlCode +=     '<th class="register-table-th">Name</th>';
        htmlCode += '</tr>';

        htmlCode += '<tr class="register-table-tr">';

        if (subcategories.length == 0) {
            htmlCode += '<tr class="register-table-tr">';
            htmlCode +=     '<td class="register-table-td" id="manufacturer-name" data-guid="">No data registered.</td>';
            htmlCode += '</tr>';
        } else {
            for (let i = 0; i < subcategories.length; i++) {
                htmlCode += '<tr class="register-table-tr">';
                htmlCode +=     `<td class="register-table-td" name="manufacturer-name" data-guid="${subcategories[i].guidID}">${subcategories[i].name}</td>`;
                htmlCode += '</tr>';
            }
        }
    }

    htmlCode += '</table>';

    $('#content').html(htmlCode);
}