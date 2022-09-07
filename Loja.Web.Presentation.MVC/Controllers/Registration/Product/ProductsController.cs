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
        public async Task<IActionResult> Index()
        {
            List<ProductsModel>? result = new();
            try
            {
                var products = await _productApplication.GetAllAsync();
                foreach (var product in products.Where(x => x.Active && !x.Deleted))
                {
                    result?.Add(new ProductsModel
                    {
                        ID = product?.ID,
                        GuidID = product.GuidID,
                        Name = product.Name,
                        Description = product.Description,
                        //Price = product.Price,
                        //Discount = product.Discount,
                        SubcategoryID = product.SubcategoryID,
                        ManufacturerID = product.ManufacturerID,
                        //Weight = product.Weight,
                        //Height = product.Height,
                        //Width = product.Width,
                        //Length = product.Length,
                        Stock = product.Stock,
                        Active = product.Active,
                        Deleted = product.Deleted,
                        Created_at = product.Created_at,
                        Created_by = product.Created_by,
                        Deleted_at = product.Deleted_at,
                        Deleted_by = product.Deleted_by
                    });
                }
                return View(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        #endregion

        #region Process
        public IActionResult Process()
        {
            if (HttpContext.Session.GetString(SessionRole) == "Employee")
            {
                return View();
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<JsonResult> Process(ProductsModel model)
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                if (HttpContext.Session.Keys.Any(k => k == SessionUserID))
                {
                    var createdByGuid = HttpContext.Session.GetString(SessionUserID);
                    model.Created_by_Guid = Guid.Parse(
                        createdByGuid != null ? createdByGuid :
                        throw new Exception("An error occurred while executing the process. Please, contact the system administrator."));
                }
                var product = await _productApplication.ProcessAsync(model);
                if (product != null)
                {
                    result.Code = 1;
                    result.Product = new
                    {
                        product.ID,
                        product.GuidID,
                        product.Name,
                        product.Description,
                        product.Price,
                        product.CurrencyID,
                        product.Discount,
                        product.SubcategoryID,
                        product.ManufacturerID,
                        product.WeightMeasurementTypeID,
                        product.Weight,
                        product.HeightMeasurementTypeID,
                        product.Height,
                        product.WidthMeasurementTypeID,
                        product.Width,
                        product.LengthMeasurementTypeID,
                        product.Length,
                        product.Stock
                    };
                }
                else
                {
                    result.Message = "An error occurred while executing the process. Please, contact the system administrator.";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region Details
        public IActionResult Details(Guid guidID)
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetDetails(Guid productID)
        {
            dynamic result = new ExpandoObject();
            try
            {
                var products = await _productApplication.GetAllAsync();
                var product = products.FirstOrDefault(x => x?.GuidID == productID);
                if (product is null || !product.Active || product.Deleted)
                {
                    throw new Exception("There's no product with the id informed.");
                }
                result.Code = 1;
                result.Product = new
                {
                    product.ID,
                    product.GuidID,
                    product.Name,
                    product.Description,
                    product.Price,
                    product.CurrencyID,
                    product.Discount,
                    product.SubcategoryID,
                    product.ManufacturerID,
                    product.Weight,
                    product.WeightMeasurementTypeID,
                    product.Height,
                    product.HeightMeasurementTypeID,
                    product.Width,
                    product.WidthMeasurementTypeID,
                    product.Length,
                    product.LengthMeasurementTypeID,
                    product.Stock
                };
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
