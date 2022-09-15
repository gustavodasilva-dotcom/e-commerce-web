using Loja.Web.Application.Interfaces.Security;
using Loja.Web.Presentation.Models.Security.Model;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Security
{
    public class AccountsController : DefaultController
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

        #region Views
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        #endregion

        #region GetUserRoles
        [HttpGet]
        public async Task<JsonResult> GetUserRoles()
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                result.UserRoles = await _securityApplication.GetUserRolesAsync();
                result.Code = 1;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region Login
        [HttpPost]
        public async Task<JsonResult> Login(string emailUsername, string password)
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                var user = await _securityApplication.LoginAsync(emailUsername, password);
                var roles = await _securityApplication.GetUserRolesAsync();

                var role = roles?.Where(x => x.ID == user.UserRoleID).FirstOrDefault()?.Name;
                var defaultRole = roles?.LastOrDefault()?.Name;

                HttpContext.Session.SetString(SessionName, string.IsNullOrEmpty(user.Name) ?
                    throw new ArgumentException("Session value cannot be null.", nameof(user.Name)) : user.Name);

                HttpContext.Session.SetString(SessionEmail, string.IsNullOrEmpty(user.Email) ?
                    throw new ArgumentException("Session value cannot be null.", nameof(user.Email)) : user.Email);

                HttpContext.Session.SetString(SessionLogin, string.IsNullOrEmpty(user.Login) ?
                    throw new ArgumentException("Session value cannot be null.", nameof(user.Login)) : user.Login);

                HttpContext.Session.SetString(SessionUserID, user.GuidID.ToString());

                if (user.UserRoleID is null)
                    HttpContext.Session.SetString(SessionRole, string.IsNullOrEmpty(defaultRole) ?
                        throw new ArgumentException("Session value cannot be null.", nameof(defaultRole)) : defaultRole);
                else
                    HttpContext.Session.SetString(SessionRole, string.IsNullOrEmpty(role) ?
                        throw new ArgumentException("Session value cannot be null.", nameof(role)) : role);

                result.Code = 1;
                result.RedirectToHome = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region Logout
        [HttpPost]
        public JsonResult Logout()
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                HttpContext.Session.Clear();

                result.Code = 1;
                result.RedirectToHome = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region Register
        [HttpPost]
        public async Task<JsonResult> Register(UserModel model)
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                if (HttpContext.Session.Keys.Any(k => k == SessionUserID))
                {
                    var createdByGuid = HttpContext.Session.GetString(SessionUserID);
                    model.UserGuid = Guid.Parse(createdByGuid ??
                        throw new Exception("An error occurred while executing the process. Please, contact the system administrator."));
                }

                result.User = await _securityApplication.InsertAsync(model);
                result.Code = 1;
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
