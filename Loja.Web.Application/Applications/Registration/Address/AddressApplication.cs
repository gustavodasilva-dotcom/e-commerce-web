using Loja.Web.Application.Interfaces.Registration.Address;
using Loja.Web.Domain.Entities.Registration.Address;
using Loja.Web.Infra.CrossCutting.Config;
using Loja.Web.Presentation.Models.Registration.Address;
using Newtonsoft.Json;

namespace Loja.Web.Application.Applications.Registration.Address
{
    public class AddressApplication : IAddressAplication
    {
        #region << PROPERTIES >>
        private readonly Addresses _addresses = new();
        private readonly Countries _countries = new();

        private class ViaCep
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

        #region << METHODS >>

        #region GetAddressByPostalCodeAsync
        public async Task<Addresses?> GetAddressByPostalCodeAsync(string postalCode)
        {
            var addresses = await _addresses.GetAllAsync();
            return addresses?.FirstOrDefault(x => x?.PostalCode == postalCode);
        }
        #endregion

        #region RegisterAddressAsync
        public async Task RegisterAddressAsync(AddressesModel model)
        {
            var viaCep = await RequestViaCepAsync(model.PostalCode);
            if (viaCep is null)
            {
                await RequestPositionStackAsync(model);
            }
            var countries = await _countries.GetAllAsync();
        }
        #endregion

        #region RequestViaCepAsync
        private async Task<ViaCep?> RequestViaCepAsync(string postalCode)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(string.Format(
                "{0}/{1}/{2}",
                Settings.Configuration["Api:ViaCep:BaseUrl"],
                postalCode,
                Settings.Configuration["Api:ViaCep:Return"]));
            if (response.StatusCode.ToString() == "400")
            {
                throw new Exception("The format of the postal code informed is invalid.");
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeObject<ViaCep>(responseContent);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region RequestPositionStackAsync
        public async Task RequestPositionStackAsync(AddressesModel model)
        {

        }
        #endregion

        #endregion
    }
}
