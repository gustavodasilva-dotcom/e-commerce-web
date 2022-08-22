using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Domain.Entities.Registration.Product;

namespace Loja.Web.Application.Applications.Registration.Product
{
    public class MeasurementApplication : IMeasurementApplication
    {
        #region << PROPERTIES >>
        private readonly Measurements _measurements = new();
        private readonly MeasurementTypes _measurementTypes = new();
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<IEnumerable<Measurements?>> GetAllMeasurementsAsync()
        {
            return await _measurements.GetAllAsync();
        }
        #endregion

        #region GetAllMeasurementTypesAsync
        public async Task<IEnumerable<MeasurementTypes?>> GetAllMeasurementTypesAsync()
        {
            return await _measurementTypes.GetAllAsync();
        }
        #endregion

        #endregion
    }
}
