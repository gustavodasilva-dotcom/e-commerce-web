using Microsoft.AspNetCore.Mvc;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Order
{
    public class OrdersController : Controller
    {
        public IActionResult SelectPay()
        {
            return View();
        }
    }
}
