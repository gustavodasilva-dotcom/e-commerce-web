$(document).ready(function () {

    var products = GetMostSolds();
    SetMostSolds(products);

});

function SetMostSolds(products) {

    let htmlCode = '';

    if (products.length > 0) {

        for (let i = 0; i < products.length; i++) {

            htmlCode += '<div class="card-gallery-small">';
            htmlCode += `<a href="/Products/Details?guidID=${products[i].guidID}">`;

            if (products[i].bases64.length > 0)
                htmlCode += `<img src="data:image/png;base64,${products[i].bases64[0]}">`;
            else
                htmlCode += '<img src="/media/default.png">';

            htmlCode += `<h3>${CapitalizeFirstLetter(products[i].name)}</h3>`;
            htmlCode += `<h6>${products[i].currency.symbol} ${products[i].price}</h6>`;
            htmlCode += '</a>';
            htmlCode += '</div>';

        }

    } else {

        htmlCode += 'No content';
    }

    $('#gallery').html(htmlCode);
}