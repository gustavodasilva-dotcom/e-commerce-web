using Loja.Web.Application.Interfaces.Registration.Manufacturer;
using Loja.Web.Presentation.Models.Registration.Manufacturer;
using Microsoft.AspNetCore.Mvc;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Manufacturer
{
    public class ManufacturersController : Controller
    {
        #region << PROPERTIES >>
        private readonly IManufacturerApplication _manufacturerApplication;
        #endregion

        #region << CONSTRUCTOR >>
        public ManufacturersController(IManufacturerApplication manufacturerApplication)
        {
            _manufacturerApplication = manufacturerApplication;
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

        #endregion
    }
}
