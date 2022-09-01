using Loja.Web.Application.Interfaces.Registration.ShoppingCart;
using Loja.Web.Presentation.Models.Registration.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.ShoppingCart
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartApplication _shoppingCartApplication;

        public ShoppingCartsController(IShoppingCartApplication shoppingCartApplication)
        {
            _shoppingCartApplication = shoppingCartApplication;
        }

        public IActionResult Details()
        {
            return View();
        }

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
    }
}
