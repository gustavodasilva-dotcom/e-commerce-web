using Loja.Web.Application.Interfaces.Registration.Address;
using Loja.Web.Domain.Entities.Registration.Address;
using Loja.Web.Presentation.Models.Registration.Address.Model;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Address
{
    public class AddressesController : DefaultController
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

        #region Views
        public IActionResult AddressesStructure(AddressesModel fields)
        {
            return PartialView(fields);
        }
        #endregion

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

        #region GetUserAddresses
        [HttpGet]
        public async Task<JsonResult> GetUserAddresses()
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                if (HttpContext.Session.Keys.Any(k => k == SessionUserID))
                {
                    var createdByGuid = HttpContext.Session.GetString(SessionUserID);
                    var userGuid = Guid.Parse(
                        createdByGuid != null ? createdByGuid :
                        throw new Exception("An error occurred while executing the process. Please, contact the system administrator."));
                    result.Addresses = await _addressApplication.GetUserAddressesAsync(userGuid);
                    result.Code = 1;
                }
                else
                {
                    result.RedirectToLogin = true;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region GetOrderAddress
        [HttpGet]
        public async Task<JsonResult> GetOrderAddress(Guid orderGuid)
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                result.Address = await _addressApplication.GetOrderAddressAsync(orderGuid);
                result.Code = 1;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region RegisterUserAddress
        [HttpPost]
        public async Task<JsonResult> RegisterUserAddress(AddressesModel model)
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                if (HttpContext.Session.Keys.Any(k => k == SessionUserID))
                {
                    var createdByGuid = HttpContext.Session.GetString(SessionUserID);
                    model.UserGuid = Guid.Parse(
                        createdByGuid != null ? createdByGuid :
                        throw new Exception("An error occurred while executing the process. Please, contact the system administrator."));
                    var address = await _addressApplication.InsertUsersAddressesAsync(model);
                    result.Address = new
                    {
                        address?.ID,
                        address?.GuidID,
                        address?.StreetID,
                        address?.Active,
                        address?.Deleted
                    };
                    result.Code = 1;
                }
                else
                {
                    result.RedirectToLogin = true;
                }
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
