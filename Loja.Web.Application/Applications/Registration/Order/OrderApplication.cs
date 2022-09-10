using Loja.Web.Application.Interfaces.Registration.Order;
using Loja.Web.Domain.Entities.Registration.Address;
using Loja.Web.Domain.Entities.Registration.Order;
using Loja.Web.Domain.Entities.Registration.Payment;
using Loja.Web.Domain.Entities.Registration.Product;
using Loja.Web.Domain.Entities.Registration.ShoppingCart;
using Loja.Web.Domain.Entities.Security;
using Loja.Web.Presentation.Models.Registration.Order.Model;
using Loja.Web.Presentation.Models.Registration.Order.ViewModel;
using Loja.Web.Presentation.Models.Registration.Product.ViewModel;

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
        private readonly Addresses _addresses = new();
        #endregion

        #region << METHODS >>

        #region GetOrderDetailsAsync
        public async Task<OrderViewModel> GetOrderDetailsAsync(Guid orderGuid)
        {
            var orders = await _orders.GetAllAsync() ??
                throw new Exception("No order was found. Please, contact the system administrator.");

            var order = orders.FirstOrDefault(x => x.GuidID == orderGuid && x.Active && !x.Deleted) ??
                throw new Exception("The order was not found. Please, contact the system administrator.");

            var paymentMethods = await _paymentMethods.GetAllAsync() ??
                throw new Exception("No payment method was found. Please, contact the system administrator.");

            var paymentMethod = paymentMethods.FirstOrDefault(x => x.ID == order.PaymentMethodID && x.Active && !x.Deleted) ??
                throw new Exception("The payment method was not found. Please, contact the system administrator.");

            CardsInfos cardInfo = new();

            if (paymentMethod.IsCard)
            {
                var ordersCardsInfos = await _ordersCardsInfos.GetAllAsync() ??
                    throw new Exception("No orders cards infos was found. Please, contact the system administrator.");

                var orderCardInfo = ordersCardsInfos.FirstOrDefault(x => x.OrderID == order.ID && x.Active && !x.Deleted) ??
                    throw new Exception("The order's card info was not found. Please, contact the system administrator.");

                var cardsInfos = await _cardsInfos.GetAllAsync() ??
                    throw new Exception("No cards infos was found. Please, contact the system administrator.");

                cardInfo = cardsInfos.FirstOrDefault(x => x.ID == orderCardInfo.CardInfoID && x.Active && !x.Deleted) ??
                    throw new Exception("The card info was not found. Please, contact the system administrator.");
            }

            var ordersStatus = await _ordersStatus.GetAllAsync() ??
                throw new Exception("No order's status was found. Please, contact the system administrator.");

            var orderStatus = ordersStatus.FirstOrDefault(x => x.ID == order.OrderStatusID && x.Active && !x.Deleted) ??
                throw new Exception("The order status was not found. Please, contact the system administrator.");

            var ordersProducts = await _ordersProducts.GetAllAsync() ??
                throw new Exception("No orders products was found. Please, contact the system administrator.");

            var orderProducts = ordersProducts.Where(x => x.OrderID == order.ID && x.Active && !x.Deleted) ??
                throw new Exception("No order's products was found. Please, contact the system administrator.");

            var products = await _products.GetAllAsync() ??
                throw new Exception("No products was found. Please, contact the system administrator.");

            List<ProductViewModel>? productsModel = new(); 

            foreach (OrdersProducts prod in orderProducts.ToList())
            {
                var product = products.FirstOrDefault(x => x.ID == prod.ProductID && x.Active && !x.Deleted) ??
                    throw new Exception("No product was found. Please, contact the system administrator.");
                
                productsModel.Add(new ProductViewModel
                {
                    GuidID = product.GuidID,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Discount = product.Discount,
                    Weight = product.Weight,
                    Height = product.Height,
                    Width = product.Width,
                    Length = product.Length,
                    Quantity = prod.Quantity,
                    Stock = product.Stock,
                    Active = product.Active,
                    Deleted = product.Deleted,
                    Created_at = product.Created_at
                });
            }

            return new OrderViewModel
            {
                GuidID = order.GuidID,
                Total = order.Total,
                PaymentMethod = new PaymentMethodViewModel
                {
                    GuidID = paymentMethod.GuidID,
                    Name = paymentMethod.Name,
                    IsCard = paymentMethod.IsCard
                },
                CardInfo = new CardInfoModel
                {
                    CardNumber = cardInfo?.CardNumber,
                    NameAtTheCard = cardInfo?.NameAtTheCard,
                    Month = cardInfo?.ExpMonth,
                    Year = cardInfo?.ExpYear,
                    CVV = cardInfo?.CVV,
                    Quantity = cardInfo?.Quantity
                },
                OrderStatus = new OrderStatusViewModel
                {
                    GuidID = orderStatus.GuidID,
                    Name = orderStatus.Name
                },
                Products = productsModel,
                Active = order.Active,
                Deleted = order.Deleted,
                Created_at = order.Created_at
            };
        }
        #endregion

        #region StepOneAsync -- Payment
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

        #region StepTwoAsync -- DeliveryAddress
        public async Task<bool> StepTwoAsync(Guid orderGuid, Guid addressGuid, Guid userGuid)
        {
            var users = await _users.GetAllAsync();
            var user = users.FirstOrDefault(x => x.GuidID == userGuid && x.Active && !x.Deleted) ??
                throw new Exception("No user was found with the session data. Please, contact the system administrator.");

            var addresses = await _addresses.GetAllAsync();
            var userAddress = addresses.FirstOrDefault(x => x.GuidID == addressGuid && x.Active && !x.Deleted) ??
                throw new Exception("No address was found. Please, contact the system administrator.");

            var orders = await _orders.GetAllAsync();
            var order = orders.FirstOrDefault(x => x.GuidID == orderGuid && x.Active && !x.Deleted) ??
                throw new Exception("No order was found. Please, contact the system administrator.");

            return await _orders.UpdateAsync(order, userAddress.ID);
        }
        #endregion

        #endregion
    }
}
