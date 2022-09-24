using Loja.Web.Application.Interfaces.Registration.Address;
using Loja.Web.Application.Interfaces.Registration.Contact;
using Loja.Web.Application.Interfaces.Registration.Manufacturer;
using Loja.Web.Presentation.Models.Registration.Manufacturer.Model;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Manufacturer
{
    public class ManufacturersController : DefaultController
    {
        #region << PROPERTIES >>
        private readonly IManufacturerApplication _manufacturerApplication;
        private readonly IContactApplication _contactApplication;
        private readonly IAddressApplication _addressApplication;
        #endregion

        #region << CONSTRUCTOR >>
        public ManufacturersController(
            IManufacturerApplication manufacturerApplication,
            IContactApplication contactApplication,
            IAddressApplication addressApplication)
        {
            _manufacturerApplication = manufacturerApplication;
            _contactApplication = contactApplication;
            _addressApplication = addressApplication;
        }
        #endregion

        #region << METHODS >>

        #region Views
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString(SessionRole) == "Employee")
            {
                return View();
            }
            return Redirect("/Default/Select?statusCode=401");
        }

        public IActionResult Details()
        {
            if (HttpContext.Session.GetString(SessionRole) == "Employee")
            {
                return View();
            }
            return Redirect("/Default/Select?statusCode=401");
        }
        #endregion

        #region Get
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                result.Manufacturers = await _manufacturerApplication.GetAllAsync();
                result.Code = 1;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region GetByID
        [HttpGet]
        public async Task<JsonResult> GetByID(Guid guid)
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                result.Manufacturers = await _manufacturerApplication.GetByIDAsync(guid);
                result.Code = 1;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region Save
        [HttpPost]
        public async Task<JsonResult> Save(ManufacturersModel model)
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

                result.Manufacturers = await _manufacturerApplication.SaveAsync(model);
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
