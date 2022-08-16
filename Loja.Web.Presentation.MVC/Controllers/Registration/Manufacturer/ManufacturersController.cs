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
            Cities? city;
            States? state;
            Streets? street;
            Countries? country;
            Neighborhoods? neighborhood;
            try
            {
                street = await _addressApplication.GetStreetByPostalCodeAsync(postalCode);
                if (street is null)
                {
                    await _addressApplication.InsertAsync(postalCode);
                    street = await _addressApplication.GetStreetByPostalCodeAsync(postalCode);
                    if (street is null)
                    {
                        throw new Exception("An error ocurred while executing the process.");
                    }
                }
                neighborhood = await _addressApplication.GetNeighborhoodAsync(street.NeighborhoodID);
                if (neighborhood is null)
                {
                    await _addressApplication.InsertAsync(postalCode);
                    neighborhood = await _addressApplication.GetNeighborhoodAsync(street.NeighborhoodID);
                    if (neighborhood is null)
                    {
                        throw new Exception("An error ocurred while executing the process.");
                    }
                }
                city = await _addressApplication.GetCityAsync(neighborhood.CityID);
                if (city is null)
                {
                    await _addressApplication.InsertAsync(postalCode);
                    city = await _addressApplication.GetCityAsync(neighborhood.CityID);
                    if (city is null)
                    {
                        throw new Exception("An error ocurred while executing the process.");
                    }
                }
                state = await _addressApplication.GetStateAsync(city.StateID);
                if (state is null)
                {
                    await _addressApplication.InsertAsync(postalCode);
                    state = await _addressApplication.GetStateAsync(city.StateID);
                    if (state is null)
                    {
                        throw new Exception("An error ocurred while executing the process.");
                    }
                }
                country = await _addressApplication.GetCountriesAsync(state.CountryID);
                if (country is null)
                {
                    await _addressApplication.InsertAsync(postalCode);
                    country = await _addressApplication.GetCountriesAsync(state.CountryID);
                    if (country is null)
                    {
                        throw new Exception("An error ocurred while executing the process.");
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new
                {
                    Code = 0,
                    e.Message
                });
            }
            return Json(new
            {
                Code = 1,
                Street = street,
                Neighborhood = neighborhood,
                City = city,
                State = state,
                Country = country
            });
        }
        #endregion

        #endregion
    }
}
