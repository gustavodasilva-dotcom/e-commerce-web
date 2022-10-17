using Loja.Web.Application.Interfaces.Registration.Finance;
using Loja.Web.Domain.Entities.Registration.Finance;
using Loja.Web.Presentation.Models.Registration.Finance.ViewModel;

namespace Loja.Web.Application.Applications.Registration.Finance
{
    public class CardIssuerApplication : ICardIssuerApplication
    {
        #region << PROPERTIES >>
        private readonly CardIssuers _cardIssuers = new();
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<List<CardIssuerViewModel>> GetAllAsync()
        {
            var currencies = await _cardIssuers.GetAllAsync() ??
                throw new Exception("There's no card issuers registered.");

            return currencies.Select(x => new CardIssuerViewModel
            {
                ID = x.ID,
                GuidID = x.GuidID,
                Name = x.Name,
                Length = x.Length,
                Prefixes = x.Prefixes,
                CheckDigit = x.CheckDigit,
                Active = x.Active,
                Deleted = x.Deleted,
                Created_at = x.Created_at,
                Deleted_at = x.Deleted_at
            }).ToList();
        }
        #endregion

        #endregion
    }
}
