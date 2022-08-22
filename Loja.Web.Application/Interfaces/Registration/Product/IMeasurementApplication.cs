using Loja.Web.Domain.Entities.Registration.Product;

namespace Loja.Web.Application.Interfaces.Registration.Product
{
    public interface IMeasurementApplication
    {
        Task<IEnumerable<Measurements?>> GetAllMeasurementsAsync();
        Task<IEnumerable<MeasurementTypes?>> GetAllMeasurementTypesAsync();
    }
}
