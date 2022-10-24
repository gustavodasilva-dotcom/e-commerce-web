$(document).ready(function () {

    GetShoppingCartItems();
    SetCards();

});

function SetCards() {

    let htmlCard = '<div class="shop-cart-card">';

    for (let i = 0; i < window.ShoppingCart.shoppingCartProducts.length; i++) {

        htmlCard += '<a class="shop-cart-title" href="' + window.location.origin + '/Products/Details?guidID='
            + window.ShoppingCart.shoppingCartProducts[i].productGuid + '">'
            + CapitalizeFirstLetter(window.ShoppingCart.shoppingCartProducts[i].name) + '</a>';

        htmlCard += `<p class="shop-cart-quantity">Quantity: ${window.ShoppingCart.shoppingCartProducts[i].quantity}</p>`;

        htmlCard += `<p class="shop-cart-price-un">Price (unitary): ${window.ShoppingCart.shoppingCartProducts[i].price}</p>`;

        htmlCard += '<p class="shop-cart-price-am">Price (amount): ' + (window.ShoppingCart.shoppingCartProducts[i].price *
                                                                        window.ShoppingCart.shoppingCartProducts[i].quantity) + '</p>';

        htmlCard += '<hr>';

        htmlCard += '</div>';

    };

    $('.shop-cart-card').html(htmlCard);
    $('#shopping-cart-id').val(window.ShoppingCart.id);
    $('#shopping-cart-guid').val(window.ShoppingCart.guidID);

}

$('#btn-ept-cart').click(function () {

    EmptyShoppingCart(parseInt($('#shopping-cart-id').val()));
});

$('#btn-buy-cart').click(function () {

    window.location.href = '/Orders/SelectPay?shoppingCart=' + $('#shopping-cart-guid').val();
;});