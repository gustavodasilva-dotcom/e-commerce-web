using Loja.Web.Application.Interfaces.Registration.ShoppingCart;
using Loja.Web.Domain.Entities.Registration.Product;
using Loja.Web.Domain.Entities.Registration.ShoppingCart;
using Loja.Web.Domain.Entities.Security;
using Loja.Web.Presentation.Models.Registration.ShoppingCart;

namespace Loja.Web.Application.Applications.Registration.ShoppingCart
{
    public class ShoppingCartApplication : IShoppingCartApplication
    {
        #region << PROPERTIES >>
        private readonly Users _users = new();
        private readonly Products _products = new();
        private readonly ShoppingCarts _shoppingCarts = new();
        private readonly ShoppingCartsProducts _shoppingCartsProducts = new();
        #endregion

        #region << METHODS >>

        #region PUBLIC

        #region GetShoppingCartByUserGuidAsync
        public async Task<IEnumerable<ShoppingCartsProducts?>> GetShoppingCartByUserGuidAsync(Guid userGuid)
        {
            int? shoppingCartID = null;
            var users = await _users.GetAllAsync();
            var user = users.FirstOrDefault(x => x.GuidID == userGuid);
            if (user is null)
            {
                throw new Exception("User invalid.");
            }
            var shoppingCarts = await _shoppingCarts.GetAllAsync();
            var userShoppingCart = shoppingCarts.FirstOrDefault(x => x.UserID == user.ID);
            if (userShoppingCart is null)
            {
                shoppingCartID = (int)await _shoppingCarts.InsertAsync(user.ID);
            }
            shoppingCartID = userShoppingCart?.ID;
            return await _shoppingCartsProducts.GetAllAsync();
        }
        #endregion

        #region AddToCartAsync
        public async Task<ShoppingCartsProducts?> AddToCartAsync(ShoppingCartsModel model)
        {
            Validate(model);
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
            var products = await _products.GetAllAsync();
            if (!products.Any(x => x.ID == model.ProductID))
            {
                throw new Exception("The product informed does not exists. Please, contact the system administrator.");
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
        #endregion

        #region EmptyShoppingCartAsync
        public async Task<bool> EmptyShoppingCartAsync(int shoppingCartID)
        {
            var shoppingCartsProducts = await _shoppingCartsProducts.GetAllAsync();
            var cartProducts = shoppingCartsProducts.Where(x => x.ShoppingCartID == shoppingCartID);
            if (!cartProducts.Any())
            {
                throw new Exception("Shopping cart is alredy empty.");
            }
            return await _shoppingCartsProducts.DeleteAsync(cartProducts.ToList());
        }
        #endregion

        #endregion

        #region PRIVATE

        #region Validate
        private void Validate(ShoppingCartsModel model)
        {
            if (model.Quantity <= 0)
            {
                throw new Exception("The product's quantity cannot be less or equal to zero.");
            }
            if (model.ProductID == 0)
            {
                throw new Exception("The product informed is invalid. Please, contact the system administrator.");
            }
            if (model.ShoppingCartID == null || model.ShoppingCartID == 0)
            {
                throw new Exception("The shopping cart informed is invalid. Please, contact the system administrator.");
            }
        }
        #endregion

        #endregion

        #endregion
    }
}
