using Loja.Web.Application.Interfaces.Registration.Manufacturer;
using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product;
using Microsoft.AspNetCore.Mvc;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Product
{
    public class ProductsController : Controller
    {
        #region << PROPERTIES >>
        private readonly IProductApplication _productApplication;
        private readonly IManufacturerApplication _manufacturerApplication;
        private readonly ISubcategoryApplication _subcategoryApplication;
        #endregion

        #region << CONSTRUCTOR >>
        public ProductsController(
            IProductApplication productApplication,
            IManufacturerApplication manufacturerApplication,
            ISubcategoryApplication subcategoryApplication)
        {
            _productApplication = productApplication;
            _manufacturerApplication = manufacturerApplication;
            _subcategoryApplication = subcategoryApplication;
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
                        Discount = product.Discount,
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
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(ProductsModel model)
        {
            try
            {
                if (HttpContext.Session.Keys.Any(k => k == "UserID"))
                {
                    model.Created_by_Guid = Guid.Parse(HttpContext.Session.GetString("UserID"));
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
                if (await _productApplication.InsertAsync(model) != null)
                {
                    return Redirect("~/Home/Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "An error occurred while executing the process. Please, contact the system administrator.";
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
            }
            return View();
        }
        #endregion

        #endregion
    }
}
