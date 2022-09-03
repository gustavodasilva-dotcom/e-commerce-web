using Loja.Web.Domain.Entities.Registration.Payment;

namespace Loja.Web.Application.Interfaces.Registration.Payment
{
    public interface IPaymentApplication
    {
        Task<IEnumerable<PaymentMethods?>> GetAllAsync();
    }
}
