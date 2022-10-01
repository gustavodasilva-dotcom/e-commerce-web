using Loja.Web.Application.Interfaces.Registration.Finance;
using Loja.Web.Domain.Entities.Registration.Finance;
using Loja.Web.Presentation.Models.Registration.Finance.ViewModel;

namespace Loja.Web.Application.Applications.Registration.Finance
{
    public class CurrencyApplication : ICurrencyApplication
    {
        #region << PROPERTIES >>
        private readonly Currencies _currencies = new();
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<List<CurrencyViewModel>> GetAllAsync()
        {
            var currencies = await _currencies.GetAllAsync() ??
                throw new Exception("There's no currencies registered.");

            return currencies.Select(x => new CurrencyViewModel
            {
                ID = x.ID,
                GuidID = x.GuidID,
                Name = x.Name,
                Symbol = x.Symbol,
                USExchangeRate = x.USExchangeRate,
                LastUpdated = x.LastUpdated,
                Active = x.Active,
                Deleted = x.Deleted,
                Created_at = x.Created_at,
                Created_by = x.Created_by,
                Deleted_at = x.Deleted_at,
                Deleted_by = x.Deleted_by
            }).ToList();
        }
        #endregion

        #endregion
    }
}
