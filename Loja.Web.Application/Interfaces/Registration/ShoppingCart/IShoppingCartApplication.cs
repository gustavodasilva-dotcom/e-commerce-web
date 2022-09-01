using Loja.Web.Domain.Entities.Registration.ShoppingCart;
using Loja.Web.Presentation.Models.Registration.ShoppingCart;

namespace Loja.Web.Application.Interfaces.Registration.ShoppingCart
{
    public interface IShoppingCartApplication
    {
        Task<ShoppingCartsProducts?> AddToCartAsync(ShoppingCartsModel model);
    }
}
