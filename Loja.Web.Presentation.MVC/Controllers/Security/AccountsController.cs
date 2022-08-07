using Loja.Web.Application.Security;
using Microsoft.AspNetCore.Mvc;

namespace Loja.Web.Presentation.MVC.Controllers.Security
{
    public class AccountsController : Controller
    {
        private readonly SecurityApplication _securityApplication;

        public AccountsController(SecurityApplication securityApplication)
        {
            _securityApplication = securityApplication;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string emailUsername, string password)
        {
            try
            {
                _securityApplication.Login(emailUsername, password);
            }
            catch (Exception e)
            {

            }
            return View();
        }
    }
}
