using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product;
using Microsoft.AspNetCore.Mvc;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Product
{
    public class ProductsController : Controller
    {
        #region << PROPERTIES >>
        private readonly IProductApplication _productApplication;
        #endregion

        #region << CONSTRUCTOR >>
        public ProductsController(IProductApplication productApplication)
        {
            _productApplication = productApplication;
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
                        Price = product.Price,
                        Discount = product.Discount,
                        SubcategoryID = product.SubcategoryID,
                        ManufacturerID = product.ManufacturerID,
                        Weight = product.Weight,
                        Height = product.Height,
                        Width = product.Width,
                        Length = product.Length,
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
        #endregion

        #endregion
    }
}
