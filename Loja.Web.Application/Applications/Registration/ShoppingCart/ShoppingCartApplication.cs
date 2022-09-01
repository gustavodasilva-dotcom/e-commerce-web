using Loja.Web.Application.Interfaces.Registration.ShoppingCart;
using Loja.Web.Domain.Entities.Registration.ShoppingCart;
using Loja.Web.Domain.Entities.Security;
using Loja.Web.Presentation.Models.Registration.ShoppingCart;

namespace Loja.Web.Application.Applications.Registration.ShoppingCart
{
    public class ShoppingCartApplication : IShoppingCartApplication
    {
        #region << PROPERTIES >>
        private readonly Users _users = new();
        private readonly ShoppingCarts _shoppingCarts = new();
        private readonly ShoppingCartsProducts _shoppingCartsProducts = new();
        #endregion

        public async Task<ShoppingCartsProducts?> AddToCartAsync(ShoppingCartsModel model)
        {
            ShoppingCarts? shoppingCart = null;
            var users = await _users.GetAllAsync();
            var shoppingCarts = await _shoppingCarts.GetAllAsync();
            var user = users.FirstOrDefault(x => x.GuidID == model.UserGuid);
            if (user is null)
            {
                throw new Exception("User invalid.");
            }
            model.UserID = user.ID;
            model.Created_by = user.ID;
            shoppingCart = shoppingCarts.FirstOrDefault(x => x.UserID == user.ID);
            if (shoppingCart is null)
            {
                var id = await _shoppingCarts.InsertAsync(model);
                shoppingCarts = await _shoppingCarts.GetAllAsync();
                shoppingCart = shoppingCarts.FirstOrDefault(x => x.UserID == user.ID);
                if (shoppingCart is null)
                {
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
                }
            }
            model.ShoppingCartID = shoppingCart.ID;
            var cartProductID = await _shoppingCartsProducts.InsertAsync(model);
            var shoppingCartsProducts = await _shoppingCartsProducts.GetAllAsync();
            var cartProduct = shoppingCartsProducts.FirstOrDefault(x => x.ID == cartProductID);
            if (cartProduct is null)
            {
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }
            return cartProduct;
        }
    }
}
