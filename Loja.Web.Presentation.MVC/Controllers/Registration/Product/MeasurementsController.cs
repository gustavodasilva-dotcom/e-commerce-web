using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product.Model;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Product
{
    public class MeasurementsController : DefaultController
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
            result.Code = 0;
            try
            {
                result.Measurements = await _measurementApplication.GetAllMeasurementsAsync();
                result.Code = 1;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #endregion
    }
}
