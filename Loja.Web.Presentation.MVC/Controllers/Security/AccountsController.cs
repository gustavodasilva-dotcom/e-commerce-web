using Loja.Web.Application.Interfaces.Security;
using Loja.Web.DTO.Security;
using Microsoft.AspNetCore.Mvc;

namespace Loja.Web.Presentation.MVC.Controllers.Security
{
    public class AccountsController : Controller
    {
        #region << PROPERTIES >>
        private readonly ISecurityApplication _securityApplication;

        const string SessionName = "Name";
        const string SessionEmail = "Email";
        const string SessionLogin = "Login";
        const string SessionRole = "Role";
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
                    var roles = await _securityApplication.GetUserRoles();

                    var role = roles?.Where(x => x.ID == user.UserRoleID).FirstOrDefault()?.Name;
                    var defaultRole = roles?.LastOrDefault()?.Name;

                    HttpContext.Session.SetString(SessionName, string.IsNullOrEmpty(user.Name) ?
                        throw new ArgumentException("Session value cannot be null.", nameof(user.Name)) : user.Name);

                    HttpContext.Session.SetString(SessionEmail, string.IsNullOrEmpty(user.Email) ?
                        throw new ArgumentException("Session value cannot be null.", nameof(user.Email)) : user.Email);

                    HttpContext.Session.SetString(SessionLogin, string.IsNullOrEmpty(user.Login) ?
                        throw new ArgumentException("Session value cannot be null.", nameof(user.Login)) : user.Login);

                    if (user.UserRoleID is null)
                    {
                        HttpContext.Session.SetString(SessionRole, string.IsNullOrEmpty(defaultRole) ?
                            throw new ArgumentException("Session value cannot be null.", nameof(defaultRole)) : defaultRole);
                    }
                    else
                    {
                        HttpContext.Session.SetString(SessionRole, string.IsNullOrEmpty(role) ?
                            throw new ArgumentException("Session value cannot be null.", nameof(role)) : role);
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
