using Loja.Web.Domain.Entities.Registration.Address;
using Loja.Web.Presentation.Models.Registration.Address;

namespace Loja.Web.Application.Interfaces.Registration.Address
{
    public interface IAddressApplication
    {
        Task<Addresses?> GetAddressByPostalCodeAsync(string postalCode);
        Task<long?> InsertAsync(AddressesModel model);
    }
}
