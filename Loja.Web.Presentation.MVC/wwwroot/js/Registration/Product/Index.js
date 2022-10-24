let param;
let products = {};
let subcategories = {};

let checkParam = false;


$(document).ready(function () {

    param = new URL(window.location.href).searchParams.get('subcategory');

    if (jQuery.isEmptyObject(products))
        products = GetProducts();

    if (jQuery.isEmptyObject(subcategories)) {
        subcategories = GetSubcategories();
        SetComboOptions(subcategories, 'register-select-subcategories');
    }
    
    if (param != null) {
        checkParam = true;
        $('#register-select-subcategories').val(param);
        $('#register-select-subcategories').trigger('change');
    }

});

$('#register-select-subcategories').on('change', function () {

    let subcategProducts;

    if (param != null && checkParam)
        subcategProducts = products.filter(x => { return x.subcategory.guidID === param });
    else
        subcategProducts = products.filter(x => { return x.subcategory.guidID === $(this).val() });

    let htmlCode = '';

    for (let i = 0; i < subcategProducts.length; i++) {

        htmlCode += '<div class="card-gallery">';
        htmlCode +=     `<a href="/Products/Details?guidID=${subcategProducts[i].guidID}">`;

        if (subcategProducts[i].bases64.length > 0)
            htmlCode += `<img src="data:image/png;base64,${subcategProducts[i].bases64[0]}">`;
        else
            htmlCode += '<img src="/media/default.png">';

        htmlCode +=         `<h3>${CapitalizeFirstLetter(subcategProducts[i].name)}</h3>`;
        htmlCode +=         `<h6>${subcategProducts[i].currency.symbol} ${subcategProducts[i].price}</h6>`;
        htmlCode +=         '<ul>';
        htmlCode +=             '<li>';
        htmlCode +=                 '<i class="fa fa-star checked"></i>';
        htmlCode +=             '</li>';
        htmlCode +=             '<li>';
        htmlCode +=                 '<i class="fa fa-star checked"></i>';
        htmlCode +=             '</li>';
        htmlCode +=             '<li>';
        htmlCode +=                 '<i class="fa fa-star checked"></i>';
        htmlCode +=             '</li>';
        htmlCode +=             '<li>';
        htmlCode +=                 '<i class="fa fa-star checked"></i>';
        htmlCode +=             '</li>';
        htmlCode +=             '<li>';
        htmlCode +=                 '<i class="fa fa-star"></i>';
        htmlCode +=             '</li>';
        htmlCode +=         '</ul>';

        htmlCode +=     '</a>';
        htmlCode += '</div>';

    }

    $('#gallery').html(htmlCode);

    if (checkParam) {
        checkParam = false;
        window.history.pushState(null, null, '/Products/Index?subcategory=' + subcategParam);
    } else
        window.history.pushState(null, null, '/Products/Index?subcategory=' + $(this).val());
    
});