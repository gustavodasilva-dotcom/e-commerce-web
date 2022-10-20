using Loja.Web.Application.Interfaces.Registration.Order;
using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Domain.Entities.Registration.Address;
using Loja.Web.Domain.Entities.Registration.Order;
using Loja.Web.Domain.Entities.Registration.Payment;
using Loja.Web.Domain.Entities.Registration.Product;
using Loja.Web.Domain.Entities.Registration.ShoppingCart;
using Loja.Web.Domain.Entities.Security;
using Loja.Web.Presentation.Models.Registration.Order.Model;
using Loja.Web.Presentation.Models.Registration.Order.ViewModel;
using Loja.Web.Presentation.Models.Registration.Product.ViewModel;
using System.Globalization;

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

        private readonly IProductApplication _productApplication;
        #endregion

        #region << CONSTRUCTOR >>
        public OrderApplication(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }
        #endregion

        #region << METHODS >>

        #region PUBLIC

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
            CardInfoViewModel? cardInfoModel = null;

            if (paymentMethod.IsCard)
            {
                var ordersCardsInfos = await _ordersCardsInfos.GetAllAsync() ??
                    throw new Exception("No orders cards infos was found. Please, contact the system administrator.");

                var orderCardInfo = ordersCardsInfos.FirstOrDefault(x => x.OrderID == order.ID && x.Active && !x.Deleted);

                if (orderCardInfo != null)
                {
                    var cardsInfos = await _cardsInfos.GetAllAsync() ??
                        throw new Exception("No cards infos was found. Please, contact the system administrator.");

                    cardInfo = cardsInfos.FirstOrDefault(x => x.ID == orderCardInfo.CardInfoID && x.Active && !x.Deleted) ??
                        throw new Exception("The card info was not found. Please, contact the system administrator.");

                    cardInfoModel = new CardInfoViewModel
                    {
                        ID = cardInfo.ID,
                        GuidID = cardInfo.GuidID,
                        CardNumber = cardInfo?.CardNumber,
                        NameAtTheCard = cardInfo?.NameAtTheCard,
                        Month = cardInfo?.ExpMonth,
                        Year = cardInfo?.ExpYear,
                        CVV = cardInfo?.CVV,
                        Quantity = cardInfo?.Quantity,
                        UserID = cardInfo?.UserID,
                        Active = cardInfo.Active,
                        Deleted = cardInfo.Deleted,
                        Created_at = cardInfo.Created_at
                    };
                }
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

                var orderProduct = orderProducts.FirstOrDefault(x => x.ProductID == product.ID && x.Active && !x.Deleted);

                var productDetails = await _productApplication.GetByIDAsync(product.GuidID);
                productDetails.Quantity = orderProduct == null ? 0 : orderProduct.Quantity;

                productsModel.Add(productDetails);
            }

            return new OrderViewModel
            {
                GuidID = order.GuidID,
                Tracking = order.Tracking,
                Total = order.Total,
                PaymentMethod = new PaymentMethodViewModel
                {
                    GuidID = paymentMethod.GuidID,
                    Name = paymentMethod.Name,
                    IsCard = paymentMethod.IsCard
                },
                CardInfo = cardInfoModel ?? null,
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

        #region GetByUserAsync
        public async Task<List<OrderViewModel>> GetByUserAsync(Guid userGuid)
        {
            var orders = await _orders.GetAllAsync() ??
                throw new Exception("No order was found. Please, contact the system administrator.");

            var users = await _users.GetAllAsync() ??
                throw new Exception("No users was found. Please, contact the system administrator.");

            var user = users.FirstOrDefault(x => x.GuidID == userGuid && x.Active && !x.Deleted) ??
                throw new Exception("The user was not found. Please, contact the system administrator.");

            var userOrders = orders.Where(x => x.UserID == user.ID).OrderBy(x => x.OrderStatusID);

            List<OrderViewModel> ordersModel = new();

            foreach (var order in userOrders) ordersModel.Add(await GetOrderDetailsAsync(order.GuidID));

            return ordersModel;
        }
        #endregion

        #region StepOneAsync -- Payment
        public async Task<OrderViewModel> StepOneAsync(StepOneModel model)
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

            var ordersStatus = await _ordersStatus.GetAllAsync() ??
                throw new Exception("There's no order status registered. Please, contact the system administrator.");

            var products = await _products.GetAllAsync() ??
                throw new Exception("There's no products registered. Please, contact the system administrator.");

            var ordersProducts = await _ordersProducts.GetAllAsync() ??
                throw new Exception("There's no order products registered. Please, contact the system administrator.");

            var orders = await _orders.GetAllAsync();
            var order = orders.FirstOrDefault(x => x.GuidID == model.OrderGuid && x.Active && !x.Deleted);

            model.UserID = user.ID;
            model.CardInfo.UserID = user.ID;
            model.PaymentMethodID = paymentMethod.ID;
            model.OrderStatusID = ordersStatus.Last().ID;

            long? orderID;

            if (model.OrderGuid == Guid.Empty && order is not null)
            {
                orderID = await _orders.InsertAsync(model) ??
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }
            else
            {
                if (!await _orders.UpdateAsync(order, model))
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");

                orderID = order.ID;
            }

            var shoppingCartsProducts = await _shoppingCartsProducts.GetAllAsync();

            var userShoppingCarProds = shoppingCartsProducts.Where(x => x.ShoppingCartID == shoppingCart?.ID).ToList() ??
                throw new Exception("The user's shopping cart is empty. Please, contact the system administrator.");

            if (model.OrderGuid == Guid.Empty && order is not null)
            {
                foreach (var cartProd in userShoppingCarProds.Where(x => x.Active && !x.Deleted))
                    await _ordersProducts.InsertAsync(cartProd, model, (int)orderID, products.First(x => x.ID == cartProd.ProductID).Price ?? 0);
            }
            else
            {
                foreach (ShoppingCartsProducts cartProd in userShoppingCarProds)
                {
                    var orderProduct = ordersProducts.FirstOrDefault(x => x.OrderID == order?.ID &&
                                                                          x.ProductID == cartProd.ProductID &&
                                                                          x.Active && !x.Deleted);

                    if (orderProduct is not null)
                    {
                        var product = products.First(x => x.ID == orderProduct?.ProductID);

                        var index = model.ProductGuid.IndexOf(model.ProductGuid.First(x => x == product.GuidID));

                        await _ordersProducts.UpdateAsync(orderProduct, model.ProductQuantity[index]);
                    }
                }
            }

            if (model.IsCard)
            {
                long? cardInfoID = null;

                var cardsInfos = await _cardsInfos.GetAllAsync() ??
                    throw new Exception("There's no cards infos registered. Please, contact the system administrator.");

                var cardInfo = cardsInfos.FirstOrDefault(x => x.UserID == model.UserID &&
                                                              x.CardNumber == model?.CardInfo?.CardNumber?.Trim() &&
                                                              x.NameAtTheCard == model?.CardInfo?.NameAtTheCard?.Trim() &&
                                                              x.ExpMonth == model?.CardInfo?.Month &&
                                                              x.ExpYear == model?.CardInfo.Year &&
                                                              x.CVV == model?.CardInfo.CVV);

                if (cardInfo is null)
                {
                    cardInfoID = await _cardsInfos.InsertAsync(model.CardInfo ??
                        throw new Exception("An error occurred while executing the process. Please, contact the system administrator."));
                }
                else
                {
                    cardInfoID = cardInfo.ID;

                    if (!await _cardsInfos.UpdateAsync(cardInfo, model.CardInfo.Quantity))
                        throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
                }

                if (cardInfoID is null)
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");

                var ordersCardsInfos = await _ordersCardsInfos.GetAllAsync();

                var cardsInfosToBeDeleted = ordersCardsInfos.Where(x => x.OrderID == orderID &&
                                                                        x.CardInfoID != cardInfoID &&
                                                                        x.Active && !x.Deleted).ToList();

                if (cardsInfosToBeDeleted.Any())
                    await _ordersCardsInfos.DeleteAsync(cardsInfosToBeDeleted);

                if (!ordersCardsInfos.Where(x => x.OrderID == orderID &&
                                                 x.CardInfoID == cardInfoID &&
                                                 x.Active && !x.Deleted).Any())
                    await _ordersCardsInfos.InsertAsync((int)orderID, (int)cardInfoID);
            }

            orders = await _orders.GetAllAsync() ??
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");

            if (shoppingCartsProducts.Where(x => x.Active && !x.Deleted).Any())
            {
                if (!await _shoppingCartsProducts.DeleteAsync(shoppingCartsProducts.Where(x => x.Active && !x.Deleted).ToList()))
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }
            
            return await GetOrderDetailsAsync(orders.First(x => x.ID == orderID).GuidID);
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

        #region FinishOrderAsync
        public async Task<string> ProcessOrderAsync(Guid orderGuid, string total, bool finishOrder)
        {
            int? orderStatusID;

            var orders = await _orders.GetAllAsync() ??
                throw new Exception("No order was found. Please, contact the system administrator.");

            var order = orders.FirstOrDefault(x => x.GuidID == orderGuid && x.Active && !x.Deleted) ??
                throw new Exception("The order was not found. Please, contact the system administrator.");

            var ordersStatus = await _ordersStatus.GetAllAsync() ??
                throw new Exception("There's no order status registered. Please, contact the system administrator.");

            if (finishOrder && !string.IsNullOrEmpty(order.Tracking) && order.OrderStatusID == ordersStatus.First().ID)
                throw new Exception("The order is already cancelled.");

            if (!finishOrder && !string.IsNullOrEmpty(order.Tracking) && order.OrderStatusID == ordersStatus.OrderBy(x => x.Name).First().ID)
                throw new Exception("The order is already finished.");

            var ordersProducts = await _ordersProducts.GetAllAsync() ??
                throw new Exception("No orders products was found. Please, contact the system administrator.");

            var orderProducts = ordersProducts.Where(x => x.OrderID == order.ID && x.Active && !x.Deleted) ??
                throw new Exception("No order's products was found. Please, contact the system administrator.");

            var products = await _products.GetAllAsync() ??
                throw new Exception("No products was found. Please, contact the system administrator.");

            if (finishOrder)
                orderStatusID = ordersStatus.First().ID;
            else
                orderStatusID = ordersStatus.OrderBy(x => x.Name).First().ID;

            foreach (OrdersProducts orderProduct in orderProducts.ToList())
            {
                var product = products.FirstOrDefault(x => x.ID == orderProduct.ProductID && x.Active && !x.Deleted) ??
                    throw new Exception("No product was found. Please, contact the system administrator.");

                if (!await _products.UpdateAsync(product, quantity: product.Stock - orderProduct.Quantity))
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }

            var tracking = GenerateTracking();

            ConvertPriceStringToDecimal(total, out decimal totalConverted);

            if (!await _orders.UpdateAsync(order, tracking: tracking, orderStatusID: orderStatusID, total: totalConverted))
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");

            return tracking;
        }
        #endregion

        #endregion

        #region PRIVATE

        #region GenerateTracking
        private static string GenerateTracking()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var random = new Random();

            return new string(Enumerable.Repeat(chars, 15).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static decimal ConvertPriceStringToDecimal(string priceString, out decimal price)
        {
            if (!decimal.TryParse(
                priceString.Replace(",", ""),
                NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture,
                out decimal convertedPrice))
            {
                throw new Exception("The product's price is not numeric.");
            }
            return price = convertedPrice;
        }
        #endregion

        #endregion

        #endregion
    }
}
