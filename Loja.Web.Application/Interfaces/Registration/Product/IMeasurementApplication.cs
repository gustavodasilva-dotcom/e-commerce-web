using Loja.Web.Domain.Entities.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product.ViewModel;

namespace Loja.Web.Application.Interfaces.Registration.Product
{
    public interface IMeasurementApplication
    {
        Task<List<MeasurementViewModel>> GetAllAsync();
        Task<IEnumerable<Measurements?>> GetAllMeasurementsAsync();
        Task<IEnumerable<MeasurementTypes?>> GetAllMeasurementTypesAsync();
    }
}
