function AddToCart(quantity, productID, productQuantity) {

    if (productQuantity > 0) {

        let shoppingCart = {};

        shoppingCart.Quantity = quantity;
        shoppingCart.ProductID = productID;

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
                        ShowModal(result.Message);
                        return;
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
                    ShowModal(result.Message);
                    return;
                }
            }
        }
    });
}

function EmptyShoppingCart(shoppingCartID) {

    $.ajax({
        async: false,
        type: "POST",
        dataType: "json",
        data: { shoppingCartID: shoppingCartID },
        url: "/ShoppingCarts/EmptyShoppingCart",
        success: function (result) {
            if (result.Code == 1) {
                window.location.reload();
            }
            else {
                if (result.RedirectToLogin) {
                    window.location.href = '/Accounts/Login';
                } else {
                    ShowModal(result.Message);
                    return;
                }
            }
        }
    });
}