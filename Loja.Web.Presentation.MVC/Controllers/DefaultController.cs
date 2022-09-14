using Microsoft.AspNetCore.Mvc;

namespace Loja.Web.Presentation.MVC.Controllers
{
    public class DefaultController : Controller
    {
        #region SessionKeys
        public const string SessionName = "Name";
        public const string SessionEmail = "Email";
        public const string SessionLogin = "Login";
        public const string SessionUserID = "UserID";
        public const string SessionRole = "Role";
        #endregion

        #region Select
        public IActionResult Select()
        {
            return View();
        }
        #endregion
    }
}
