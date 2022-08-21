using Loja.Web.Domain.Entities.Registration.Finance;

namespace Loja.Web.Application.Interfaces.Registration.Finance
{
    public interface ICurrencyApplication
    {
        Task<IEnumerable<Currencies?>> GetAllAsync();
    }
}
