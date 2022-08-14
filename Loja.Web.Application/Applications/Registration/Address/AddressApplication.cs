using Loja.Web.Application.Interfaces.Registration.Address;
using Loja.Web.Domain.Entities.Registration.Address;
using Loja.Web.Infra.CrossCutting.Config;
using Loja.Web.Presentation.Models.Registration.Address;
using Newtonsoft.Json;
using System.Web;

namespace Loja.Web.Application.Applications.Registration.Address
{
    public class AddressApplication : IAddressApplication
    {
        #region << PROPERTIES >>
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

        #region positionstack
        private class positionstack
        {
            public double? latitude { get; set; }
            public double? longitude { get; set; }
            public string? type { get; set; }
            public string? name { get; set; }
            public string? number { get; set; }
            public string? postal_code { get; set; }
            public string? street { get; set; }
            public int? confidence { get; set; }
            public string? region { get; set; }
            public string? region_code { get; set; }
            public string? county { get; set; }
            public string? locality { get; set; }
            public string? administrative_area { get; set; }
            public string? neighbourhood { get; set; }
            public string? country { get; set; }
            public string? country_code { get; set; }
            public string? continent { get; set; }
            public string? label { get; set; }
            public string? map_url { get; set; }
        }
        #endregion

        #endregion

        #region << METHODS >>

        #region PUBLIC

        #region GetAddressByPostalCodeAsync
        public async Task<Addresses?> GetAddressByPostalCodeAsync(string postalCode)
        {
            postalCode = ValidatePostalCode(postalCode);
            var addresses = await _addresses.GetAllAsync();
            return addresses?.FirstOrDefault(x => x?.PostalCode == postalCode);
        }
        #endregion

        #region InsertAsync
        public async Task<long?> InsertAsync(AddressesModel model)
        {
            ValidateModel(ref model);
            long? countryID, stateID, cityID, neighborhoodID;
            ViaCEP? viaCEP;
            List<positionstack>? positionstack;
            positionstack? validPositionstack = null;
            viaCEP = await RequestViaCepAsync(model.PostalCode);
            if (viaCEP is null)
            {
                // TODO: RequestPositionStackAsync to be corrected.
                positionstack = await RequestPositionStackAsync(model);
                if (positionstack is null)
                {
                    throw new Exception("The address informed is invalid.");
                }
                else
                {
                    validPositionstack = positionstack.FirstOrDefault(x => x.postal_code.Equals(model));
                    if (validPositionstack == null)
                    {
                        throw new Exception("The address informed is invalid.");
                    }
                }
            }
            var countries = await _countries.GetAllAsync();
            if (countries is null || !countries.Any(x => x.Name.Contains(model.Country)))
            {
                if (viaCEP != null)
                {
                    model.Country = "Brazil";
                }
                else
                {
                    model.Country = validPositionstack?.country;
                }
                countryID = await _countries.InsertAsync(model);
                if (countryID == null)
                {
                    throw new Exception("An error ocurred while executing the process.");
                }
                model.CountryID = (int)countryID;
            }
            var states = await _states.GetAllAsync();
            if (states is null || !states.Any(x => x.Initials.Contains(model.State)))
            {
                if (viaCEP != null)
                {
                    model.State = viaCEP.uf;
                }
                else
                {
                    model.State = validPositionstack?.region_code;
                }
                stateID = await _states.InsertAsync(model);
                if (stateID == null)
                {
                    throw new Exception("An error ocurred while executing the process.");
                }
                model.StateID = (int)stateID;
            }
            var cities = await _cities.GetAllAsync();
            if (cities is null || !cities.Any(x => x.Name.Contains(model.City)))
            {
                if (viaCEP != null)
                {
                    model.City = viaCEP.localidade;
                }
                else
                {
                    model.City = validPositionstack?.locality;
                }
                cityID = await _cities.InsertAsync(model);
                if (cityID == null)
                {
                    throw new Exception("An error ocurred while executing the process.");
                }
                model.CityID = (int)cityID;
            }
            var neighborhoods = await _neighborhoods.GetAllAsync();
            if (neighborhoods is null || !neighborhoods.Any(x => x.Name.Contains(model.Neighborhood)))
            {
                if (viaCEP != null)
                {
                    model.Neighborhood = viaCEP.bairro;
                }
                else
                {
                    model.Neighborhood = validPositionstack?.neighbourhood;
                }
                neighborhoodID = await _neighborhoods.InsertAsync(model);
                if (neighborhoodID == null)
                {
                    throw new Exception("An error oc0urred while executing the process.");
                }
                model.NeighborhoodID = (int)neighborhoodID;
            }
            var addressID = await _addresses.InsertAsync(model);
            if (addressID == null)
            {
                throw new Exception("An error oc0urred while executing the process.");
            }
            return addressID;
        }
        #endregion

        #endregion

        #region PRIVATE

        #region ValidateModel
        private string ValidatePostalCode(string postalCode)
        {
            postalCode = postalCode.Replace("-", "");
            if (int.Parse(postalCode) == 0 || !int.TryParse(postalCode, out int _))
            {
                throw new Exception("Invalid postal code.");
            }
            return postalCode;
        }

        private void ValidateModel(ref AddressesModel model)
        {
            model.PostalCode = ValidatePostalCode(model.PostalCode);
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new Exception("Invalid address name.");
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

        #region RequestPositionStackAsync
        private async Task<List<positionstack>?> RequestPositionStackAsync(AddressesModel model)
        {
            var builder = new UriBuilder(Settings.Configuration["Api:positionstack:BaseUrl"])
            {
                Port = -1
            };
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["access_key"] = Settings.Configuration["Api:positionstack:AccessKey"];
            var nameSplited = model.Name?.Split(" ").ToList();
            query["query"] = model.Number.ToString() + " " + string.Join(" ", nameSplited)
                .Replace("-", "")
                .Replace(".", "");
            builder.Query = string.Concat(query.ToString());
            var endpoint = builder.ToString();
            using var client = new HttpClient();
            var response = await client.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("The format of the postal code informed is invalid.");
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeObject<List<positionstack>>(responseContent);
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
