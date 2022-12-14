using Loja.Web.Presentation.Models.Registration.Manufacturer.Model;
using Loja.Web.Presentation.Models.Registration.Manufacturer.ViewModel;

namespace Loja.Web.Application.Interfaces.Registration.Manufacturer
{
    public interface IManufacturerApplication
    {
        Task<List<ManufacturerViewModel>> GetAllAsync();
        Task<ManufacturerViewModel> GetByIDAsync(Guid guid);
        Task<ManufacturerViewModel> SaveAsync(ManufacturersModel model);
    }
}
