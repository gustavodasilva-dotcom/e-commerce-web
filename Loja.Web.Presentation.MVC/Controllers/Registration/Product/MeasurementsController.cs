using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Product
{
    public class MeasurementsController : Controller
    {
        #region << PROPERTIES >>
        private readonly IMeasurementApplication _measurementApplication;
        #endregion

        #region << CONSTRUCTOR >>
        public MeasurementsController(IMeasurementApplication measurementApplication)
        {
            _measurementApplication = measurementApplication;
        }
        #endregion

        #region << METHODS >>

        #region GetHeightMeasurements
        [HttpGet]
        public async Task<JsonResult> GetHeightMeasurements()
        {
            dynamic result = new ExpandoObject();
            try
            {
                var measurements = await _measurementApplication.GetAllMeasurementsAsync();
                var measurementTypes = await _measurementApplication.GetAllMeasurementTypesAsync();
                measurementTypes = measurementTypes.OrderBy(x => x?.Name);
                if (measurements.Any())
                {
                    var measurementsObj = new List<MeasurementsModel>();
                    foreach (var measurement in measurements)
                    {
                        measurementsObj.Add(new MeasurementsModel
                        {
                            ID = measurement?.ID,
                            GuidID = measurement.GuidID,
                            Name = measurement.Name,
                            MeasurementTypeID = measurement.MeasurementTypeID,
                            Active = measurement.Active,
                            Deleted = measurement.Deleted,
                            Created_at = measurement.Created_at,
                            Created_by = measurement.Created_by,
                            Deleted_at = measurement.Deleted_at,
                            Deleted_by = measurement.Deleted_by
                        });
                    }
                    result.Code = 1;
                    result.Measurements = measurementsObj.Where(x => x.MeasurementTypeID == measurementTypes?.First()?.ID);
                }
                else
                {
                    result.Code = 0;
                    result.Message = "There's no height measurements registered.";
                }
            }
            catch (Exception e)
            {
                result.Code = 0;
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region GetMassMeasurements
        [HttpGet]
        public async Task<JsonResult> GetMassMeasurements()
        {
            dynamic result = new ExpandoObject();
            try
            {
                var measurements = await _measurementApplication.GetAllMeasurementsAsync();
                var measurementTypes = await _measurementApplication.GetAllMeasurementTypesAsync();
                measurementTypes = measurementTypes.OrderBy(x => x?.Name);
                if (measurements.Any())
                {
                    var measurementsObj = new List<MeasurementsModel>();
                    foreach (var measurement in measurements)
                    {
                        measurementsObj.Add(new MeasurementsModel
                        {
                            ID = measurement?.ID,
                            GuidID = measurement.GuidID,
                            Name = measurement.Name,
                            MeasurementTypeID = measurement.MeasurementTypeID,
                            Active = measurement.Active,
                            Deleted = measurement.Deleted,
                            Created_at = measurement.Created_at,
                            Created_by = measurement.Created_by,
                            Deleted_at = measurement.Deleted_at,
                            Deleted_by = measurement.Deleted_by
                        });
                    }
                    result.Code = 1;
                    result.Measurements = measurementsObj.Where(x => x.MeasurementTypeID == measurementTypes?.Last()?.ID);
                }
                else
                {
                    result.Code = 0;
                    result.Message = "There's no mass measurements registered.";
                }
            }
            catch (Exception e)
            {
                result.Code = 0;
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #endregion
    }
}
