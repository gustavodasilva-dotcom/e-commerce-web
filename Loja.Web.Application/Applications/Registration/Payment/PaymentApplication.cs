using Loja.Web.Application.Interfaces.Registration.Payment;
using Loja.Web.Domain.Entities.Registration.Payment;
using Loja.Web.Domain.Entities.Security;
using Loja.Web.Presentation.Models.Registration.Order.Model;
using Loja.Web.Presentation.Models.Registration.Order.ViewModel;

namespace Loja.Web.Application.Applications.Registration.Payment
{
    public class PaymentApplication : IPaymentApplication
    {
        #region << PROPERTIES >>
        private readonly PaymentMethods _paymentMethods = new();
        private readonly CardsInfos _cardsInfos = new();
        private readonly Users _users = new();
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<IEnumerable<PaymentMethods?>> GetAllAsync()
        {
            return await _paymentMethods.GetAllAsync();
        }
        #endregion

        #region GetUserCardsAsync
        public async Task<List<CardInfoViewModel>> GetUserCardsAsync(Guid userGuid)
        {
            var cardsInfosReturn = new List<CardInfoViewModel>();

            var users = await _users.GetAllAsync() ??
                throw new Exception("No users was found. Please, contact the system administrator.");

            var user = users.FirstOrDefault(x => x.GuidID == userGuid && x.Active && !x.Deleted) ??
                throw new Exception("The user was not found. Please, contact the system administrator.");

            var cardsInfos = await _cardsInfos.GetAllAsync();

            var userCardsInfos = cardsInfos.Where(x => x.UserID == user.ID &&
                                                       x.Active && !x.Deleted);

            foreach (var cardInfo in userCardsInfos)
            {
                cardsInfosReturn.Add(new CardInfoViewModel
                {
                    ID = cardInfo.ID,
                    GuidID = cardInfo.GuidID,
                    CardNumber = cardInfo.CardNumber,
                    NameAtTheCard = cardInfo.NameAtTheCard,
                    Month = cardInfo.ExpMonth,
                    Year = cardInfo.ExpYear,
                    CVV = cardInfo.CVV,
                    Quantity = cardInfo.Quantity,
                    UserID = cardInfo.UserID,
                    Active = cardInfo.Active,
                    Deleted = cardInfo.Deleted,
                    Created_at = cardInfo.Created_at
                });
            }

            return cardsInfosReturn;
        }
        #endregion

        #endregion
    }
}
