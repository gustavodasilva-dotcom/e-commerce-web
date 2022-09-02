$(document).ready(function () {
    GetShoppingCartItems();
    SetCards();
});

function SetCards() {
    let url = window.location.origin;

    let htmlCard = '<div class="shop-cart-card">';

    $.each(window.ShoppingCart, function (i, item) {
        htmlCard += '<a class="shop-cart-title" href="' + url + '/Products/Details?guidID='
            + window.ShoppingCart[i].productGuid + '">'
            + window.ShoppingCart[i].name + '</a>';

        htmlCard += '<p class="shop-cart-price">' + window.ShoppingCart[i].price + '</p>';

        htmlCard += '<p class="shop-cart-descr">' + window.ShoppingCart[i].description + '</p>';

        htmlCard += '<hr>';

        htmlCard += '</div>';
    });

    $('.shop-cart-card').html(htmlCard);
}