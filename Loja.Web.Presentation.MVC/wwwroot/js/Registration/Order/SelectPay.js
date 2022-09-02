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

    if (oneItemOnly) {
        singleItem = shoppingCart.find(x => x.productGuid == productGuidID.toLowerCase());
    }
}