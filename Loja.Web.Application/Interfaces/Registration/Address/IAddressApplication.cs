using Loja.Web.Domain.Entities.Registration.Address;
using Loja.Web.Presentation.Models.Registration.Address;

namespace Loja.Web.Application.Interfaces.Registration.Address
{
    public interface IAddressApplication
    {
        Task<Streets?> GetAddressByPostalCodeAsync(string postalCode);
        Task InsertAsync(AddressesModel model);
        Task InsertAsync(string postalCode);
        Task<long?> InsertAddressAsync(AddressesModel model);
    }
}
