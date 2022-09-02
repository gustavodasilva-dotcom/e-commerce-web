using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Application.Interfaces.Registration.ShoppingCart;
using Loja.Web.Presentation.Models.Registration.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.ShoppingCart
{
    public class ShoppingCartsController : Controller
    {
        #region << PROPERTIES >>
        private readonly IProductApplication _productApplication;
        private readonly IShoppingCartApplication _shoppingCartApplication;
        #endregion

        #region << CONSTRUCTOR >>
        public ShoppingCartsController(
            IProductApplication productApplication,
            IShoppingCartApplication shoppingCartApplication)
        {
            _productApplication = productApplication;
            _shoppingCartApplication = shoppingCartApplication;
        }
        #endregion

        #region << METHODS >>

        #region Details
        public IActionResult Details()
        {
            return View();
        }
        #endregion

        #region GetByUserID
        [HttpGet]
        public async Task<JsonResult> GetByUserID()
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            result.RedirectToLogin = false;
            try
            {
                if (HttpContext.Session.Keys.Any(k => k == "UserID"))
                {
                    var userID = HttpContext.Session.GetString("UserID") ??
                        throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
                    if (Guid.TryParse(userID, out Guid userGuid))
                    {
                        result.Products = null;
                        var productsCart = await _shoppingCartApplication.GetShoppingCartByUserGuidAsync(userGuid);
                        if (productsCart.Any())
                        {
                            var shoppingCartProducts = productsCart.Select(x => new ShoppingCartsViewModel
                            {
                                ID = x?.ID,
                                GuidID = x?.GuidID,
                                Quantity = x.Quantity,
                                ProductID = x.ProductID,
                                ShoppingCartID = x.ShoppingCartID,
                                Active = x.Active,
                                Deleted = x.Deleted,
                                Created_at = x.Created_at
                            }).ToList();
                            var products = await _productApplication.GetAllAsync();
                            foreach (var cartProduct in shoppingCartProducts)
                            {
                                var productDetails = products.FirstOrDefault(x => x?.ID == cartProduct.ProductID);
                                cartProduct.ProductGuid = productDetails?.GuidID;
                                cartProduct.Name = productDetails?.Name;
                                cartProduct.Description = productDetails?.Description;
                                cartProduct.Price = productDetails?.Price;
                            }
                            result.Products = shoppingCartProducts;
                        }
                        result.Code = 1;
                    }
                    else
                    {
                        throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
                    }
                }
                else
                {
                    result.RedirectToLogin = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region AddToCart
        [HttpPost]
        public async Task<JsonResult> AddToCart(ShoppingCartsModel model)
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            result.RedirectToLogin = false;
            try
            {
                if (HttpContext.Session.Keys.Any(k => k == "UserID"))
                {
                    var createdByGuid = HttpContext.Session.GetString("UserID");
                    model.UserGuid = Guid.Parse(
                        createdByGuid != null ? createdByGuid :
                        throw new Exception("An error occurred while executing the process. Please, contact the system administrator."));
                    var shoppingCart = await _shoppingCartApplication.AddToCartAsync(model);
                    result.Product = new
                    {
                        shoppingCart?.ID,
                        shoppingCart?.GuidID,
                        shoppingCart?.Quantity,
                        shoppingCart?.ProductID,
                        shoppingCart?.ShoppingCartID,
                        shoppingCart?.Active,
                        shoppingCart?.Deleted,
                        shoppingCart?.Created_at
                    };
                    result.Code = 1;
                }
                else
                {
                    result.RedirectToLogin = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region EmptyShoppingCart
        [HttpPost]
        public async Task<JsonResult> EmptyShoppingCart(int shoppingCartID)
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                if (await _shoppingCartApplication.EmptyShoppingCartAsync(shoppingCartID))
                {
                    result.Code = 1;
                    result.Deleted = true;
                }
                else
                {
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
                }
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
