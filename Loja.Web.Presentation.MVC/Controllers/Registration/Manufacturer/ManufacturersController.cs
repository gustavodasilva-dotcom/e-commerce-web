using Loja.Web.Application.Interfaces.Registration.Manufacturer;
using Loja.Web.Presentation.Models.Registration.Address;
using Loja.Web.Presentation.Models.Registration.Contact;
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
        public async Task<ActionResult<IEnumerable<ManufacturersModel>>> Index()
        {
            if (HttpContext.Session.GetString("Role") == "Employee")
            {
                var manufacturers = await _manufacturerApplication.GetAllAsync();
                if (manufacturers.Any())
                {
                    return View(manufacturers.Where(x => !x.Deleted).Select(x => new ManufacturersModel
                    {
                        GuidID = x.GuidID,
                        Name = x.Name,
                        BrazilianCompany = x.BrazilianCompany,
                        CAGE = x.CAGE,
                        NCAGE = x.NCAGE,
                        SEC = x.SEC,
                        Contacts = new ContactsModel
                        {
                            ID = x.ContactID
                        },
                        Addresses = new AddressesModel
                        {
                            ID = x.AddressID
                        },
                        FederalTaxpayerRegistrationNumber = x.FederalTaxpayerRegistrationNumber,
                        StateTaxpayerRegistrationNumber = x.StateTaxpayerRegistrationNumber,
                        Active = x.Active,
                        Deleted = x.Deleted
                    }).ToList());
                }
                return BadRequest();
            }
            return Unauthorized();
        }
        #endregion

        #region Register
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("Role") == "Employee")
            {
                return View();
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Register(ManufacturersModel model)
        {
            try
            {
                var localition = Request.Form["localition"].ToString().ToLower().Equals("true") ? true : false;
                model.BrazilianCompany = localition;
                model.Addresses.IsForeign = !localition;
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

        #region Details
        public async Task<ActionResult<ManufacturersModel>> Details(Guid guid)
        {
            var manufacturers = await _manufacturerApplication.GetAllAsync();
            return View(manufacturers.FirstOrDefault(x => x.GuidID == guid));
        }
        #endregion

        #endregion
    }
}
