using Loja.Web.Application.Interfaces.Registration.Finance;
using Loja.Web.Application.Interfaces.Registration.Manufacturer;
using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product.Model;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Product
{
    public class ProductsController : DefaultController
    {
        #region << PROPERTIES >>
        private readonly IProductApplication _productApplication;
        private readonly IManufacturerApplication _manufacturerApplication;
        private readonly ISubcategoryApplication _subcategoryApplication;
        private readonly ICurrencyApplication _currenciesApplication;
        private readonly IMeasurementApplication _measurementApplication;
        #endregion

        #region << CONSTRUCTOR >>
        public ProductsController(
            IProductApplication productApplication,
            IManufacturerApplication manufacturerApplication,
            ISubcategoryApplication subcategoryApplication,
            ICurrencyApplication currencyApplication,
            IMeasurementApplication measurementApplication)
        {
            _productApplication = productApplication;
            _manufacturerApplication = manufacturerApplication;
            _subcategoryApplication = subcategoryApplication;
            _currenciesApplication = currencyApplication;
            _measurementApplication = measurementApplication;
        }
        #endregion

        #region << METHODS >>

        #region Index
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Process()
        {
            if (HttpContext.Session.GetString(SessionRole) == "Employee")
            {
                return View();
            }
            return Redirect("/Default/Select?statusCode=401");
        }

        public IActionResult Details(Guid guidID)
        {
            return View();
        }
        #endregion

        #region GetAll
        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                result.Products = await _productApplication.GetAllAsync();
                result.Code = 1;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region Get
        [HttpGet]
        public async Task<JsonResult> Get(Guid guid)
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                result.Products = await _productApplication.GetByIDAsync(guid);
                result.Code = 1;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region GetMostSolds
        [HttpGet]
        public async Task<JsonResult> GetMostSolds()
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                result.Products = await _productApplication.GetMostSoldsAsync();
                result.Code = 1;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region Save
        [HttpPost]
        public async Task<JsonResult> Save(ProductsModel model)
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                if (HttpContext.Session.Keys.Any(k => k == SessionUserID))
                {
                    var createdByGuid = HttpContext.Session.GetString(SessionUserID);
                    model.UserGuid = Guid.Parse(createdByGuid ??
                        throw new Exception("An error occurred while executing the process. Please, contact the system administrator."));
                }

                result.Products = await _productApplication.SaveAsync(model);
                result.Code = 1;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region SaveProductRating
        [HttpPost]
        public async Task<JsonResult> SaveProductRating(ProductsRatingsModel model)
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                if (HttpContext.Session.Keys.Any(k => k == SessionUserID))
                {
                    var createdByGuid = HttpContext.Session.GetString(SessionUserID);
                    model.UserGuid = Guid.Parse(createdByGuid ??
                        throw new Exception("An error occurred while executing the process. Please, contact the system administrator."));
                }

                result.Rating = await _productApplication.SaveProductRatingAsync(model);
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
