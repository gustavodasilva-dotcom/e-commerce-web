using Loja.Web.Application.Interfaces.Registration.Order;
using Loja.Web.Domain.Entities.Registration.Order;
using Loja.Web.Domain.Entities.Registration.Payment;
using Loja.Web.Domain.Entities.Registration.ShoppingCart;
using Loja.Web.Domain.Entities.Security;
using Loja.Web.Presentation.Models.Registration.Order;

namespace Loja.Web.Application.Applications.Registration.Order
{
    public class OrderApplication : IOrderApplication
    {
        #region << PROPERTIES >>
        private readonly Users _users = new();
        private readonly Orders _orders = new();
        private readonly OrdersStatus _ordersStatus = new();
        private readonly ShoppingCarts _shoppingCarts = new();
        private readonly PaymentMethods _paymentMethods = new();
        private readonly OrdersProducts _ordersProducts = new();
        private readonly OrdersCardsInfos _ordersCardsInfos = new();
        #endregion

        #region << METHODS >>

        #region StepOneAsync
        public async Task StepOneAsync(StepOneModel model)
        {
            var users = await _users.GetAllAsync();
            if (users is null)
            {
                throw new Exception("There's no users registered. Please, contact the system administrator.");
            }
            var user = users.FirstOrDefault(x => x.GuidID == model.UserGuid);
            if (user is null)
            {
                throw new Exception("No user was found with the session data. Please, contact the system administrator.");
            }
            var paymentMethods = await _paymentMethods.GetAllAsync();
            if (paymentMethods is null)
            {
                throw new Exception("There's no payment methods registered. Please, contact the system administrator.");
            }
            var paymentMethod = paymentMethods.FirstOrDefault(x => x.GuidID == model?.CardInfo?.GuidID);
            if (paymentMethod is null)
            {
                throw new Exception("The payment selected does not exists. Please, contact the system administrator.");
            }
            var shoppingCarts = await _shoppingCarts.GetAllAsync();
            if (shoppingCarts is null)
            {
                throw new Exception("There's no shopping carts registered. Please, contact the system administrator.");
            }
            var shoppingCart = shoppingCarts.FirstOrDefault(x => x.UserID == user?.ID);
            if (shoppingCart is null)
            {
                // await _shoppingCarts.InsertAsync(user.ID);
                // TODO: create function to insert product at the shopping cart in case said shopping cart is none
                // and there's no products.
            }
            var ordersStatus = await _ordersStatus.GetAllAsync();
            if (ordersStatus is null)
            {
                throw new Exception("There's no order status registered. Please, contact the system administrator.");
            }
            await _orders.InsertAsync(user.ID, paymentMethod.ID, ordersStatus.First().ID);
        }
        #endregion

        #endregion
    }
}
