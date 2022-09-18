using Microsoft.AspNetCore.Mvc;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Generic
{
    public class GenericsController : DefaultController
    {
        #region View
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString(SessionRole) == "Employee")
            {
                return View();
            }
            return Redirect("/Default/Select?statusCode=401");
        }
        #endregion
    }
}
