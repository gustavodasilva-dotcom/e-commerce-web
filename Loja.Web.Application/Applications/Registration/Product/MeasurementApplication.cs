using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Domain.Entities.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product.ViewModel;

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
        public async Task<List<MeasurementViewModel>> GetAllAsync()
        {
            var measurements = await _measurements.GetAllAsync() ??
                throw new Exception("There's no measurements registered.");

            measurements = measurements.OrderBy(x => x.MeasurementTypeID);

            var measurementTypes = await _measurementTypes.GetAllAsync() ??
                throw new Exception("There's no measurement types registered.");

            var result = new List<MeasurementViewModel>();

            foreach (var measurement in measurements.Where(x => x.Active && !x.Deleted))
            {
                var measurementType = measurementTypes.FirstOrDefault(x => x.ID == measurement.MeasurementTypeID);

                var measurementTypeModel = measurementType == null ? null : new MeasurementTypeViewModel
                {
                    ID = measurementType.ID,
                    GuidID = measurementType.GuidID,
                    Name = measurementType.Name,
                    Active = measurementType.Active,
                    Deleted = measurementType.Deleted,
                    Created_at = measurementType.Created_at
                };

                result.Add(new MeasurementViewModel
                {
                    ID = measurement.ID,
                    GuidID = measurement.GuidID,
                    Name = measurement.Name,
                    MeasurementType = measurementTypeModel,
                    Active = measurement.Active,
                    Deleted = measurement.Deleted,
                    Created_at = measurement.Created_at,
                    Created_by = measurement.Created_by,
                    Deleted_at = measurement.Deleted_at,
                    Deleted_by = measurement.Deleted_by
                });
            }

            return result;
        }
        #endregion

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
