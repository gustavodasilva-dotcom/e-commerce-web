using Loja.Web.Application.Interfaces.Registration.Product;
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

        private readonly IProductApplication _productApplication;

        public ShoppingCartApplication(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }
        #endregion

        #region << METHODS >>

        #region PUBLIC

        #region GetShoppingCartByUserGuidAsync
        public async Task<List<ShoppingCartsViewModel>> GetShoppingCartByUserGuidAsync(Guid? userGuid)
        {
            List<ShoppingCartsViewModel> shoppingCartReturn = new();

            int? shoppingCartID = null;

            var users = await _users.GetAllAsync();
            
            var user = users.FirstOrDefault(x => x.GuidID == userGuid) ?? throw new Exception("User invalid.");

            var shoppingCarts = await _shoppingCarts.GetAllAsync();

            var userShoppingCart = shoppingCarts.FirstOrDefault(x => x.UserID == user.ID);
            
            if (userShoppingCart is null)
                shoppingCartID = (int)await _shoppingCarts.InsertAsync(user.ID);

            shoppingCartID = userShoppingCart?.ID;

            var productsCart = await _shoppingCartsProducts.GetAllAsync();

            if (productsCart.Any())
            {
                var shoppingCartProducts = productsCart.Select(x => new ShoppingCartsViewModel
                {
                    ID = x?.ID,
                    GuidID = x?.GuidID,
                    Quantity = x.Quantity,
                    ProductID = x.ProductID,
                    ShoppingCartID = x.ShoppingCartID,
                    Active = x.Active,
                    Deleted = x.Deleted,
                    Created_at = x.Created_at
                }).Where(x => x.ShoppingCartID == shoppingCartID &&
                              x.Active && !x.Deleted);

                var products = await _productApplication.GetAllAsync();
                
                foreach (var cartProduct in shoppingCartProducts)
                {
                    var productDetails = products.FirstOrDefault(x => x?.ID == cartProduct.ProductID);
                    cartProduct.ProductGuid = productDetails?.GuidID;
                    cartProduct.Name = productDetails?.Name;
                    cartProduct.Description = productDetails?.Description;
                    cartProduct.Price = productDetails?.Price;

                    shoppingCartReturn.Add(cartProduct);
                }
            }

            return shoppingCartReturn;
        }
        #endregion

        #region AddToCartAsync
        public async Task<List<ShoppingCartsViewModel>> AddToCartAsync(ShoppingCartsModel model)
        {
            Validate(model);

            ShoppingCarts? shoppingCart = null;
            
            var users = await _users.GetAllAsync();
            
            var shoppingCarts = await _shoppingCarts.GetAllAsync();
            
            var user = users.FirstOrDefault(x => x.GuidID == model.UserGuid) ?? throw new Exception("User invalid.");

            model.UserID = user.ID;
            model.Created_by = user.ID;

            shoppingCart = shoppingCarts.FirstOrDefault(x => x.UserID == user.ID);

            if (shoppingCart is null)
            {
                var id = await _shoppingCarts.InsertAsync(model);

                shoppingCarts = await _shoppingCarts.GetAllAsync();

                shoppingCart = shoppingCarts.FirstOrDefault(x => x.UserID == user.ID) ??
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }

            var products = await _products.GetAllAsync();
            
            if (!products.Any(x => x.ID == model.ProductID))
                throw new Exception("The product informed does not exists. Please, contact the system administrator.");

            model.ShoppingCartID = shoppingCart.ID;

            var cartProductID = await _shoppingCartsProducts.InsertAsync(model);

            var shoppingCartsProducts = await _shoppingCartsProducts.GetAllAsync();
            
            var cartProduct = shoppingCartsProducts.FirstOrDefault(x => x.ID == cartProductID) ??
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");

            return await GetShoppingCartByUserGuidAsync(model.UserGuid);
        }
        #endregion

        #region EmptyShoppingCartAsync
        public async Task<bool> EmptyShoppingCartAsync(int shoppingCartID)
        {
            var shoppingCartsProducts = await _shoppingCartsProducts.GetAllAsync();

            var cartProducts = shoppingCartsProducts.Where(x => x.ShoppingCartID == shoppingCartID);

            if (!cartProducts.Any())
                throw new Exception("Shopping cart is alredy empty.");

            return await _shoppingCartsProducts.DeleteAsync(cartProducts.ToList());
        }
        #endregion

        #endregion

        #region PRIVATE

        #region Validate
        private static void Validate(ShoppingCartsModel model)
        {
            if (model.Quantity <= 0)
                throw new Exception("The product's quantity cannot be less or equal to zero.");

            if (model.ProductID == 0)
                throw new Exception("The product informed is invalid. Please, contact the system administrator.");
        }
        #endregion

        #endregion

        #endregion
    }
}
