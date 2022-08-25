using Loja.Web.Application.Interfaces.Registration.Finance;
using Loja.Web.Application.Interfaces.Registration.Manufacturer;
using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Product
{
    public class ProductsController : Controller
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

        #region Register
        public IActionResult Process()
        {
            //if (HttpContext.Session.GetString("Role") == "Employee")
            //{
                return View();
            //}
            //return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Process(ProductsModel model)
        {
            try
            {
                if (HttpContext.Session.GetString("Role") == "Employee")
                {
                    if (HttpContext.Session.Keys.Any(k => k == "UserID"))
                    {
                        model.Created_by_Guid = Guid.Parse(HttpContext.Session.GetString("UserID"));
                    }
                    await ValidateKeys(model);
                    var product = await _productApplication.InsertAsync(model);
                    if (model != null)
                    {
                        return Redirect(string.Format("~/Products/Details?guidID={0}", model.GuidID));
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "An error occurred while executing the process. Please, contact the system administrator.";
                    }
                }
                return Unauthorized();
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
            }
            return View();
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

        #region << VALIDATIONS >>

        #region ValidateKeys
        private async Task ValidateKeys(ProductsModel model)
        {
            var currencies = await _currenciesApplication.GetAllAsync();
            try
            {
                model.CurrencyID = currencies?.FirstOrDefault(x => x?.GuidID == Guid.Parse(Request.Form["currencies"]))?.ID;
            }
            catch (Exception)
            {
                throw new Exception("Please, select a currency.");
            }
            var manufacturers = await _manufacturerApplication.GetAllAsync();
            try
            {
                model.ManufacturerID = manufacturers?.FirstOrDefault(x => x?.GuidID == Guid.Parse(Request.Form["manufacturers"]))?.ID;
            }
            catch (Exception)
            {
                throw new Exception("Please, select a manufacturer.");
            }
            var subcategories = await _subcategoryApplication.GetAllAsync();
            try
            {
                model.SubcategoryID = subcategories?.FirstOrDefault(x => x?.GuidID == Guid.Parse(Request.Form["subcategories"]))?.ID;
            }
            catch (Exception)
            {
                throw new Exception("Please, select a subcategory.");
            }
            var measurements = await _measurementApplication.GetAllMeasurementsAsync();
            try
            {
                model.WeightMeasurementTypeID = measurements?.FirstOrDefault(x => x?.GuidID == Guid.Parse(Request.Form["weight-measure"]))?.ID;
            }
            catch (Exception)
            {
                throw new Exception("Please, select a weight.");
            }
            try
            {
                model.HeightMeasurementTypeID = measurements?.FirstOrDefault(x => x?.GuidID == Guid.Parse(Request.Form["height-measure"]))?.ID;
            }
            catch (Exception)
            {
                throw new Exception("Please, select a height.");
            }
            try
            {
                model.WidthMeasurementTypeID = measurements?.FirstOrDefault(x => x?.GuidID == Guid.Parse(Request.Form["width-measure"]))?.ID;
            }
            catch (Exception)
            {
                throw new Exception("Please, select a width.");
            }
            try
            {
                model.LengthMeasurementTypeID = measurements?.FirstOrDefault(x => x?.GuidID == Guid.Parse(Request.Form["length-measure"]))?.ID;
            }
            catch (Exception)
            {
                throw new Exception("Please, select a length.");
            }
        }
        #endregion

        #endregion
    }
}
