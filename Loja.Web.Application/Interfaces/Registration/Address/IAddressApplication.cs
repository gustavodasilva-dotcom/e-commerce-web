using Loja.Web.Domain.Entities.Registration.Address;
using Loja.Web.Presentation.Models.Registration.Address;

namespace Loja.Web.Application.Interfaces.Registration.Address
{
    public interface IAddressApplication
    {
        Task<Streets?> GetStreetByPostalCodeAsync(string postalCode);
        Task<dynamic?> GetUserAddressesAsync(Guid userGuid);
        Task<IEnumerable<Addresses?>> GetAllAddressesAsync();
        Task<IEnumerable<Streets?>> GetAllStreetsAsync();
        Task<Neighborhoods?> GetNeighborhoodAsync(int neighborhoodID);
        Task<Cities?> GetCityAsync(int cityID);
        Task<States?> GetStateAsync(int stateID);
        Task<Countries?> GetCountriesAsync(int countryID);
        Task InsertAsync(AddressesModel model);
        Task InsertAsync(string postalCode);
        Task<Addresses?> InsertUsersAddressesAsync(AddressesModel model);
        Task<long?> InsertAddressAsync(AddressesModel model);
    }
}
