using Loja.Web.Application.Interfaces.Registration.Address;
using Loja.Web.Application.Interfaces.Registration.Manufacturer;
using Loja.Web.Domain.Entities.Registration.Address;
using Loja.Web.Presentation.Models.Registration.Manufacturer;
using Microsoft.AspNetCore.Mvc;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Manufacturer
{
    public class ManufacturersController : Controller
    {
        #region << PROPERTIES >>
        private readonly IManufacturerApplication _manufacturerApplication;
        private readonly IAddressApplication _addressApplication;
        #endregion

        #region << CONSTRUCTOR >>
        public ManufacturersController(IManufacturerApplication manufacturerApplication, IAddressApplication addressApplication)
        {
            _manufacturerApplication = manufacturerApplication;
            _addressApplication = addressApplication;
        }
        #endregion

        #region << METHODS >>

        #region Index
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(ManufacturersModel model)
        {
            try
            {
                model.BrazilianCompany = Request.Form["localition"].ToString().ToLower().Equals("true") ? true : false;
                if (await _manufacturerApplication.InsertAsync(model) != null)
                {
                    ViewBag.SuccessMessage = "Manufacturer created successfully.";
                }
                else
                {
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
            }
            return View();
        }
        #endregion

        #region GetAddress
        [HttpGet]
        public async Task<JsonResult> GetAddress(string postalCode)
        {
            Streets? street = null;
            try
            {
                street = await _addressApplication.GetAddressByPostalCodeAsync(postalCode);
                if (street is null)
                {
                    await _addressApplication.InsertAsync(postalCode);
                    street = await _addressApplication.GetAddressByPostalCodeAsync(postalCode);
                    if (street is null)
                    {
                        throw new Exception("An error ocurred while executing the process.");
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
            }
            return Json(new
            {
                Street = street
            });
        }
        #endregion

        #endregion
    }
}
