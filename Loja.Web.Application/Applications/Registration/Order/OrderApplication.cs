using Loja.Web.Application.Interfaces.Registration.Order;
using Loja.Web.Domain.Entities.Registration.Order;
using Loja.Web.Domain.Entities.Registration.Payment;
using Loja.Web.Domain.Entities.Registration.Product;
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
        private readonly OrdersProducts _ordersProducts = new();
        private readonly OrdersCardsInfos _ordersCardsInfos = new();
        private readonly ShoppingCarts _shoppingCarts = new();
        private readonly ShoppingCartsProducts _shoppingCartsProducts = new();
        private readonly PaymentMethods _paymentMethods = new();
        private readonly CardsInfos _cardsInfos = new();
        private readonly Products _products = new();
        #endregion

        #region << METHODS >>

        #region StepOneAsync
        public async Task<Orders?> StepOneAsync(StepOneModel model)
        {
            var users = await _users.GetAllAsync();

            var user = users.FirstOrDefault(x => x.GuidID == model.UserGuid) ??
                throw new Exception("No user was found with the session data. Please, contact the system administrator.");

            var paymentMethods = await _paymentMethods.GetAllAsync();

            var paymentMethod = paymentMethods.FirstOrDefault(x => x.GuidID == model?.PaymentGuid) ??
                throw new Exception("The payment selected does not exists. Please, contact the system administrator.");

            var shoppingCarts = await _shoppingCarts.GetAllAsync() ??
                throw new Exception("There's no shopping carts registered. Please, contact the system administrator.");

            var shoppingCart = shoppingCarts.FirstOrDefault(x => x.UserID == user?.ID);

            if (shoppingCart is null)
            {
                // await _shoppingCarts.InsertAsync(user.ID);
                // TODO: create function to insert product at the shopping cart in case said shopping cart is none
                // and there's no products.
            }

            var ordersStatus = await _ordersStatus.GetAllAsync() ??
                throw new Exception("There's no order status registered. Please, contact the system administrator.");

            var products = await _products.GetAllAsync() ??
                throw new Exception("There's no products registered. Please, contact the system administrator.");

            model.UserID = user.ID;
            model.CardInfo.UserID = user.ID;
            model.PaymentMethodID = paymentMethod.ID;
            model.OrderStatusID = ordersStatus.Last().ID;

            var orderID = await _orders.InsertAsync(model) ??
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");

            var shoppingCartsProducts = await _shoppingCartsProducts.GetAllAsync();

            var userShoppingCarProds = shoppingCartsProducts.Where(x => x.ShoppingCartID == shoppingCart?.ID && x.Active && !x.Deleted) ??
                throw new Exception("The user's shopping cart is empty. Please, contact the system administrator.");

            foreach (var cartProd in userShoppingCarProds)
            {
                await _ordersProducts.InsertAsync(cartProd, model, (int)orderID, products.First(x => x.ID == cartProd.ProductID).Price ?? 0);
            }
            
            if (model.IsCard)
            {
                var cardInfoID = await _cardsInfos.InsertAsync(model.CardInfo ??
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator."));

                if (cardInfoID is null)
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
                
                await _ordersCardsInfos.InsertAsync((int)orderID, (int)cardInfoID);
            }

            var orders = await _orders.GetAllAsync() ??
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");

            if (!await _shoppingCartsProducts.DeleteAsync(shoppingCartsProducts.ToList()))
            {
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }

            return orders.FirstOrDefault(x => x.ID == orderID);
        }
        #endregion

        #endregion
    }
}
