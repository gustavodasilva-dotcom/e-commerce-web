using Loja.Web.Domain.Entities.Registration.Payment;
using Loja.Web.Presentation.Models.Registration.Order.ViewModel;

namespace Loja.Web.Application.Interfaces.Registration.Payment
{
    public interface IPaymentApplication
    {
        Task<IEnumerable<PaymentMethods?>> GetAllAsync();
        Task<List<CardInfoViewModel>> GetUserCardsAsync(Guid userGuid);
    }
}
