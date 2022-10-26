using Loja.Web.Presentation.Models.Registration.ShoppingCart.Model;
using Loja.Web.Presentation.Models.Registration.ShoppingCart.ViewModel;

namespace Loja.Web.Application.Interfaces.Registration.ShoppingCart
{
    public interface IShoppingCartApplication
    {
        Task<ShoppingCartViewModel?> GetShoppingCartByUserGuidAsync(Guid? userGuid);
        Task<ShoppingCartViewModel> AddToCartAsync(ShoppingCartsModel model);
        Task<bool> EmptyShoppingCartAsync(int shoppingCartID);
    }
}
