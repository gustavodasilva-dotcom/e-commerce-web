using Loja.Web.Application.Interfaces.Security;
using Loja.Web.DTO.Security;
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
                var user = new UsersDTO(emailUsername, password);
                try
                {
                    user = await _securityApplication.Login(user);
                    var role = await _securityApplication.GetUserRoles();
                    if (role.Where(x => x.ID == user.UserRoleID && x.Name == "Employee").Any())
                    {

                    }
                    else
                    {

                    }
                    return Redirect("~/Home/Index");
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = e.Message;
                    // TODO: create log at the database.
                }
            }
            catch (ArgumentException e)
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
