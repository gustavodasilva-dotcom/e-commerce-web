using Loja.Web.Application.Interfaces.Registration.Finance;
using Loja.Web.Domain.Entities.Registration.Finance;

namespace Loja.Web.Application.Applications.Registration.Finance
{
    public class CurrencyApplication : ICurrencyApplication
    {
        #region << PROPERTIES >>
        private readonly Currencies _currencies = new();
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<IEnumerable<Currencies?>> GetAllAsync()
        {
            return await _currencies.GetAllAsync();
        }
        #endregion

        #endregion
    }
}
