using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Application.Interfaces.Registration.ShoppingCart;
using Loja.Web.Presentation.Models.Registration.ShoppingCart.Model;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.ShoppingCart
{
    public class ShoppingCartsController : DefaultController
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
            try
            {
                if (HttpContext.Session.Keys.Any(k => k == SessionUserID))
                {
                    var createdByGuid = HttpContext.Session.GetString(SessionUserID);
                    var userGuid = Guid.Parse(createdByGuid ??
                        throw new Exception("An error occurred while executing the process. Please, contact the system administrator."));
                    
                    result.Products = await _shoppingCartApplication.GetShoppingCartByUserGuidAsync(userGuid);
                    result.Code = 1;
                }
                else
                    result.RedirectToLogin = true;
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
                if (HttpContext.Session.Keys.Any(k => k == SessionUserID))
                {
                    var createdByGuid = HttpContext.Session.GetString(SessionUserID);
                    model.UserGuid = Guid.Parse(createdByGuid ??
                        throw new Exception("An error occurred while executing the process. Please, contact the system administrator."));

                    var shoppingCart = await _shoppingCartApplication.AddToCartAsync(model);
                    result.Code = 1;
                }
                else
                    result.RedirectToLogin = true;
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
