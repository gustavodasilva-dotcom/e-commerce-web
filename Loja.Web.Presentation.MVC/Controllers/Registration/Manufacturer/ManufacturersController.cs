using Loja.Web.Application.Interfaces.Registration.Address;
using Loja.Web.Application.Interfaces.Registration.Contact;
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

        #region Index
        public async Task<ActionResult<IEnumerable<ManufacturersModel>>> Index()
        {
            if (HttpContext.Session.GetString("Role") == "Employee")
            {
                try
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
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
                }
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
            if (HttpContext.Session.GetString("Role") == "Employee")
            {
                try
                {
                    var manufacturers = await _manufacturerApplication.GetAllAsync();
                    var contacts = await _contactApplication.GetAllAsync();
                    var addresses = await _addressApplication.GetAllAddressesAsync();
                    var streets = await _addressApplication.GetAllStreetsAsync();
                    var manufacturer = manufacturers.FirstOrDefault(x => x?.GuidID == guid);
                    var contact = contacts.FirstOrDefault(x => x?.ID == manufacturer?.ContactID);
                    var address = addresses.FirstOrDefault(x => x?.ID == manufacturer?.AddressID);
                    var street = streets.FirstOrDefault(x => x?.ID == address?.StreetID);
                    var neighborhood = await _addressApplication.GetNeighborhoodAsync(street.NeighborhoodID);
                    var city = await _addressApplication.GetCityAsync(neighborhood.CityID);
                    var state = await _addressApplication.GetStateAsync(city.StateID);
                    var country = await _addressApplication.GetCountriesAsync(state.CountryID);
                    return View(new ManufacturersModel
                    {
                        GuidID = manufacturer.GuidID,
                        Name = manufacturer.Name,
                        BrazilianCompany = manufacturer.BrazilianCompany,
                        CAGE = manufacturer.CAGE,
                        NCAGE = manufacturer.NCAGE,
                        SEC = manufacturer.SEC,
                        Contacts = new ContactsModel
                        {
                            ID = contact?.ID,
                            Phone = contact?.Phone,
                            Cellphone = contact?.Cellphone,
                            Email = contact?.Email,
                            Website = contact?.Website
                        },
                        Addresses = new AddressesModel
                        {
                            ID = address?.ID,
                            PostalCode = street?.PostalCode,
                            Name = street?.Name,
                            Number = address?.Number,
                            Comment = address?.Comment,
                            Neighborhood = neighborhood?.Name,
                            City = city?.Name,
                            State = state?.Initials,
                            Country = country?.Name
                        },
                        FederalTaxpayerRegistrationNumber = manufacturer.FederalTaxpayerRegistrationNumber,
                        StateTaxpayerRegistrationNumber = manufacturer.StateTaxpayerRegistrationNumber,
                        Active = manufacturer.Active,
                        Deleted = manufacturer.Deleted
                    });
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }
            return Unauthorized();
        }
        #endregion

        #endregion
    }
}
