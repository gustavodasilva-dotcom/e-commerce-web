using Loja.Web.Application.Interfaces.Security;
using Microsoft.AspNetCore.Mvc;

namespace Loja.Web.Presentation.MVC.Controllers.Security
{
    public class AccountsController : Controller
    {
        #region << PROPERTIES >>
        private readonly ISecurityApplication _securityApplication;
        #endregion

        #region << CONSTRUCTOR >>
        public AccountsController(ISecurityApplication securityApplication)
        {
            _securityApplication = securityApplication;
        }
        #endregion

        #region << METHODS >>

        #region Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string emailUsername, string password)
        {
            try
            {
                await _securityApplication.Login(emailUsername, password);
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
                // TODO: create log at the database.
            }
            return View();
        }
        #endregion

        #endregion
    }
}
