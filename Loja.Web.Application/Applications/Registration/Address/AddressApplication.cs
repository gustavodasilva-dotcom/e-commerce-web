using Loja.Web.Application.Interfaces.Registration.Address;
using Loja.Web.Domain.Entities.Registration.Address;
using Loja.Web.Infra.CrossCutting.Config;
using Loja.Web.Presentation.Models.Registration.Address;
using Newtonsoft.Json;

namespace Loja.Web.Application.Applications.Registration.Address
{
    public class AddressApplication : IAddressApplication
    {
        #region << PROPERTIES >>
        private readonly Streets _streets = new();
        private readonly Addresses _addresses = new();
        private readonly Countries _countries = new();
        private readonly States _states = new();
        private readonly Cities _cities = new();
        private readonly Neighborhoods _neighborhoods = new();

        #region ViaCep
        public class ViaCEP
        {
            public string? cep { get; set; }
            public string? logradouro { get; set; }
            public string? complemento { get; set; }
            public string? bairro { get; set; }
            public string? localidade { get; set; }
            public string? uf { get; set; }
            public string? ibge { get; set; }
            public string? gia { get; set; }
            public string? ddd { get; set; }
            public string? siafi { get; set; }
        }
        #endregion

        #endregion

        #region << METHODS >>

        #region PUBLIC

        #region Gets

        #region GetStreetByPostalCodeAsync
        public async Task<Streets?> GetStreetByPostalCodeAsync(string postalCode)
        {
            postalCode = ValidatePostalCode(postalCode);
            var streets = await _streets.GetAllAsync();
            return streets?.FirstOrDefault(x => x?.PostalCode == postalCode);
        }
        #endregion

        #region GetNeighborhoodAsync
        public async Task<Neighborhoods?> GetNeighborhoodAsync(int neighborhoodID)
        {
            var neighborhoods = await _neighborhoods.GetAllAsync();
            return neighborhoods?.FirstOrDefault(x => x?.ID == neighborhoodID);
        }
        #endregion

        #region GetCityAsync
        public async Task<Cities?> GetCityAsync(int cityID)
        {
            var cities = await _cities.GetAllAsync();
            return cities?.FirstOrDefault(x => x?.ID == cityID);
        }
        #endregion

        #region GetStateAsync
        public async Task<States?> GetStateAsync(int stateID)
        {
            var states = await _states.GetAllAsync();
            return states?.FirstOrDefault(x => x?.ID == stateID);
        }
        #endregion

        #region GetCountriesAsync
        public async Task<Countries?> GetCountriesAsync(int countryID)
        {
            var countries = await _countries.GetAllAsync();
            return countries?.FirstOrDefault(x => x?.ID == countryID);
        }
        #endregion

        #endregion

        #region InsertAsync
        public async Task InsertAsync(string postalCode)
        {
            postalCode = ValidatePostalCode(postalCode);
            var model = new AddressesModel
            {
                PostalCode = postalCode,
                IsForeign = false
            };
            await InsertAddressByRequestAsync(model);
        }

        public async Task InsertAsync(AddressesModel model)
        {
            Validate(ref model);
            await InsertAddressByRequestAsync(model);
        }

        public async Task<long?> InsertAddressAsync(AddressesModel model)
        {
            return await _addresses.InsertAsync(model);
        }

        private async Task InsertAddressByRequestAsync(AddressesModel model)
        {
            ViaCEP? viaCEP = null;
            long? countryID, stateID, cityID, neighborhoodID, streetID, addressID = null;
            if (!model.IsForeign)
            {
                viaCEP = await RequestViaCepAsync(model.PostalCode);
                if (viaCEP is null)
                {
                    throw new Exception("The address informed is invalid.");
                }
                model.Country = "Brazil";
            }
            var countries = await _countries.GetAllAsync();
            if (countries is null || !countries.Any(x => x.Name.ToLower().Contains(model.Country.ToLower())))
            {
                countryID = await _countries.InsertAsync(model);
                if (countryID == null)
                {
                    throw new Exception("An error ocurred while executing the process.");
                }
                model.CountryID = (int)countryID;
            }
            else
            {
                model.CountryID = countries?.FirstOrDefault(x => x.Name.ToLower().Contains(model.Country.ToLower())).ID;
            }
            if (!model.IsForeign)
            {
                model.State = viaCEP?.uf;
            }
            var states = await _states.GetAllAsync();
            if (states is null || !states.Any(x => x.Initials.ToLower().Contains(model.State.ToLower()) && x.CountryID == model.CountryID))
            {
                stateID = await _states.InsertAsync(model);
                if (stateID == null)
                {
                    throw new Exception("An error ocurred while executing the process.");
                }
                model.StateID = (int)stateID;
            }
            else
            {
                model.StateID = states?.FirstOrDefault(x => x.Initials.ToLower().Contains(model.State.ToLower())).ID;
            }
            if (!model.IsForeign)
            {
                model.City = viaCEP?.localidade;
            }
            var cities = await _cities.GetAllAsync();
            if (cities is null || !cities.Any(x => x.Name.ToLower().Contains(model.City.ToLower()) && x.StateID == model.StateID))
            {
                cityID = await _cities.InsertAsync(model);
                if (cityID == null)
                {
                    throw new Exception("An error ocurred while executing the process.");
                }
                model.CityID = (int)cityID;
            }
            else
            {
                model.CityID = cities?.FirstOrDefault(x => x.Name.ToLower().Contains(model.City.ToLower())).ID;
            }
            if (!model.IsForeign)
            {
                model.Neighborhood = viaCEP?.bairro;
            }
            var neighborhoods = await _neighborhoods.GetAllAsync();
            if (neighborhoods is null || !neighborhoods.Any(x => x.Name.ToLower().Contains(model.Neighborhood.ToLower()) && x.CityID == model.CityID))
            {
                neighborhoodID = await _neighborhoods.InsertAsync(model);
                if (neighborhoodID == null)
                {
                    throw new Exception("An error ocurred while executing the process.");
                }
                model.NeighborhoodID = (int)neighborhoodID;
            }
            else
            {
                model.NeighborhoodID = neighborhoods?.FirstOrDefault(x => x.Name.ToLower().Contains(model.Neighborhood.ToLower())).ID;
            }
            if (!model.IsForeign)
            {
                model.Name = viaCEP?.logradouro;
            }
            var streets = await _streets.GetAllAsync();
            if (streets is null || !streets.Any(x => x.Name.ToLower().Equals(model?.Name?.ToLower()) && x.NeighborhoodID == model.NeighborhoodID))
            {
                streetID = await _streets.InsertAsync(model);
                if (streetID == null)
                {
                    throw new Exception("An error ocurred while executing the process.");
                }
            }
        }
        #endregion

        #endregion

        #region PRIVATE

        #region ValidateModel
        private static string ValidatePostalCode(string postalCode)
        {
            if (string.IsNullOrEmpty(postalCode))
            {
                throw new Exception("Postal code cannot be null or empty.");
            }
            postalCode = postalCode.Replace("-", "");
            if (int.Parse(postalCode) == 0 || !int.TryParse(postalCode, out int _))
            {
                throw new Exception("Invalid postal code.");
            }
            return postalCode;
        }

        private static void ValidatePostalCode(ref AddressesModel model)
        {
            if (string.IsNullOrEmpty(model.PostalCode))
            {
                throw new Exception("Postal code cannot be null or empty.");
            }
            model.PostalCode = model?.PostalCode?.Replace("-", "");
            if (!model.IsForeign)
            {
                if (int.Parse(model.PostalCode) == 0 || !int.TryParse(model.PostalCode, out int _))
                {
                    throw new Exception("Invalid postal code.");
                }
            }
        }

        private static void Validate(ref AddressesModel model)
        {
            ValidatePostalCode(ref model);
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new Exception("Invalid address name.");
            }
            if (!int.TryParse(model.Number, out int _))
            {
                throw new Exception("Invalid address code.");
            }
            if (model.IsForeign)
            {
                if (string.IsNullOrEmpty(model.Neighborhood)) throw new Exception("Neighborhood cannot be null or empty.");
                if (string.IsNullOrEmpty(model.City)) throw new Exception("City cannot be null or empty.");
                if (string.IsNullOrEmpty(model.State)) throw new Exception("State cannot be null or empty.");
                if (string.IsNullOrEmpty(model.Country)) throw new Exception("State cannot be null or empty.");
            }
        }
        #endregion

        #region RequestViaCepAsync
        private static async Task<ViaCEP?> RequestViaCepAsync(string postalCode)
        {
            var endpoint =
                Settings.Configuration["Api:ViaCep:BaseUrl"]
                + postalCode
                + Settings.Configuration["Api:ViaCep:Return"];
            using var client = new HttpClient();
            var response = await client.GetAsync(endpoint);
            if (response.StatusCode.ToString() == "400")
            {
                throw new Exception("The format of the postal code informed is invalid.");
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeObject<ViaCEP>(responseContent);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #endregion

        #endregion
    }
}
