let items = {};
let singleItem;
let productGuidID;
let shoppingCart = [];
let oneItemOnly = false;

$(document).ready(function () {
    let params = (new URL(window.location.href)).searchParams;
    oneItemOnly = params.get('oneItemOnly') == '1' ? true : false;
    productGuidID = params.get('productGuidID');

    GetShoppingCartItems();
    SetShoppingCartItems();
});

function SetShoppingCartItems() {
    shoppingCart = window.ShoppingCart;
    let htmlCards;

    if (oneItemOnly) {
        singleItem = shoppingCart.find(x => x.productGuid == productGuidID.toLowerCase());

        htmlCards = '<div class="item-card">';
        htmlCards += '<a class="item-title" href="#">' + singleItem.name + '</a>';
        htmlCards += '<p class="item-price">' + singleItem.price + '</p>';
        htmlCards += '<p class="item-qtd">' + singleItem.quantity + '</p>';
        htmlCards += '<p class="item-amount">' + (singleItem.price * singleItem.quantity) + '</p>';
        htmlCards += '</div>';
    } else {
        $.each(shoppingCart, function (i, item) {
            htmlCards = '<div class="item-card">';
            htmlCards += '<a class="item-title" href="#">' + shoppingCart[i].name + '</a>';
            htmlCards += '<p class="item-price">' + shoppingCart[i].price + '</p>';
            htmlCards += '<p class="item-qtd">' + shoppingCart[i].quantity + '</p>';
            htmlCards += '<p class="item-amount">' + (shoppingCart[i].price * shoppingCart[i].quantity) + '</p>';
            htmlCards += '</div>';
        });
    }

    $('#register-items-card').html(htmlCards);
}