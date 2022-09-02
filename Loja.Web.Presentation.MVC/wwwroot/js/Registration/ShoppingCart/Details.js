$(document).ready(function () {
    GetShoppingCartItems();
    SetCards();
});

function SetCards() {
    let htmlCard = '<div class="shop-cart-card">';

    $.each(window.ShoppingCart, function (i, item) {
        htmlCard += '<a class="shop-cart-title" href="' + window.location.origin + '/Products/Details?guidID='
            + window.ShoppingCart[i].productGuid + '">'
            + window.ShoppingCart[i].name + '</a>';

        htmlCard += '<p class="shop-cart-quantity">Quantity: ' + window.ShoppingCart[i].quantity + '</p>';

        htmlCard += '<p class="shop-cart-price-un">Price (unitary): ' + window.ShoppingCart[i].price + '</p>';

        htmlCard += '<p class="shop-cart-price-am">Price (amount): ' + (window.ShoppingCart[i].price * window.ShoppingCart[i].quantity) + '</p>';

        htmlCard += '<hr>';

        htmlCard += '</div>';
    });

    $('.shop-cart-card').html(htmlCard);
    $('#shopping-cart-id').val(window.ShoppingCart[0].shoppingCartID);
}

$('#btn-ept-cart').click(function () {
    EmptyShoppingCart(parseInt($('#shopping-cart-id').val()));
});