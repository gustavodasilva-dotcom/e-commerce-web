using Loja.Web.Domain.Entities.Registration.Manufacturer;
using Loja.Web.Presentation.Models.Registration.Manufacturer;

namespace Loja.Web.Application.Interfaces.Registration.Manufacturer
{
    public interface IManufacturerApplication
    {
        Task<Manufacturers> InsertAsync(ManufacturersModel model);
    }
}
