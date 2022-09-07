using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product.Model;
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

        #region Get
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            dynamic result = new ExpandoObject();
            try
            {
                var measurements = await _measurementApplication.GetAllMeasurementsAsync();
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
                    result.Measurements = measurementsObj.OrderBy(x => x.MeasurementTypeID);
                }
                else
                {
                    result.Code = 0;
                    result.Message = "There's no measurements registered.";
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
