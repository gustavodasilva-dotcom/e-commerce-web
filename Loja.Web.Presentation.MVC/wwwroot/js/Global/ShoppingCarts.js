function AddToCart() {
    if (parseInt($('#product-quantity').val()) > 0) {
        let shoppingCart = {};

        shoppingCart.Quantity = parseInt($('#input-product-quantity').val());
        shoppingCart.ProductID = parseInt($('#product-id').val());

        $.ajax({
            async: false,
            type: "POST",
            dataType: "json",
            url: "/ShoppingCarts/AddToCart",
            data: { model: shoppingCart },
            success: function (result) {
                if (result.Code == 1) {
                    alert('Item add sucessfully.');
                }
                else {
                    if (result.RedirectToLogin) {
                        window.location.href = '/Accounts/Login';
                    } else {
                        alert(result.Message);
                    }
                }
            }
        });
    } else {
        alert('A product that is not in stock cannot be added to the shopping cart.');
    }
}

function GetShoppingCartItems() {
    $.ajax({
        async: false,
        type: "GET",
        dataType: "json",
        url: "/ShoppingCarts/GetByUserID",
        success: function (result) {
            if (result.Code == 1) {
                window.ShoppingCart = { };
                window.ShoppingCart = result.Products;
            }
            else {
                if (result.RedirectToLogin) {
                    window.location.href = '/Accounts/Login';
                } else {
                    alert(result.Message);
                }
            }
        }
    });
}