using Loja.Web.Application.Interfaces.Registration.Address;
using Loja.Web.Domain.Entities.Registration.Address;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Address
{
    public class AddressesController : Controller
    {
        #region << PROPERTIES >>
        private readonly IAddressApplication _addressApplication;
        #endregion

        #region << CONSTRUCTOR >>
        public AddressesController(IAddressApplication addressApplication)
        {
            _addressApplication = addressApplication;
        }
        #endregion

        #region << METHODS >>

        #region Get
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> Get(string postalCode)
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

        #region RegisterUserAddress
        [HttpPost]
        public async Task<JsonResult> RegisterUserAddress(string postalCode)
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                // TODO: create table UsersAddress.
                // TODO: add column DeliveryAddressID at the Order table.
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
