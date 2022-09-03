using Loja.Web.Application.Interfaces.Registration.Payment;
using Loja.Web.Domain.Entities.Registration.Payment;

namespace Loja.Web.Application.Applications.Registration.Payment
{
    public class PaymentApplication : IPaymentApplication
    {
        #region << PROPERTIES >>
        private readonly PaymentMethods _paymentMethods = new();
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<IEnumerable<PaymentMethods?>> GetAllAsync()
        {
            return await _paymentMethods.GetAllAsync();
        }
        #endregion

        #endregion
    }
}
