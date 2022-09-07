using Loja.Web.Application.Interfaces.Security;
using Loja.Web.Presentation.Models.Security;
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
            return View();
        }
        #endregion

        #region Logout
        [HttpPost]
        public JsonResult Logout()
        {
            dynamic result = new ExpandoObject();
            try
            {
                result.Code = 1;
                HttpContext.Session.Clear();
            }
            catch (Exception e)
            {
                result.Code = 0;
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UsersModel model)
        {
            try
            {
                if (HttpContext.Session.Keys.Any(k => k == SessionUserID))
                {
                    model.Created_by_Guid = Guid.Parse(HttpContext.Session.GetString(SessionUserID));
                }
                var role = Request.Form["roles"].ToString().ToLower();
                var roles = await _securityApplication.GetUserRolesAsync();
                if (string.IsNullOrEmpty(role))
                {
                    model.UserRoleID_Guid = roles?.LastOrDefault()?.GuidID;
                }
                else
                {
                    model.UserRoleID_Guid = roles?.Where(x => x?.Name?.ToLower() == role).FirstOrDefault()?.GuidID;
                }
                await _securityApplication.InsertAsync(model);
                ViewBag.SuccessMessage = "User created successfully.";
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
