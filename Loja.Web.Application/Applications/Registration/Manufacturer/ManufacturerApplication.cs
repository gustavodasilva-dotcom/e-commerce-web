using Loja.Web.Application.Interfaces.Registration.Address;
using Loja.Web.Application.Interfaces.Registration.Contact;
using Loja.Web.Application.Interfaces.Registration.Manufacturer;
using Loja.Web.Domain.Entities.Registration.Manufacturer;
using Loja.Web.Presentation.Models.Registration.Manufacturer;

namespace Loja.Web.Application.Applications.Registration.Manufacturer
{
    public class ManufacturerApplication : IManufacturerApplication
    {
        #region << PROPERTIES >>
        private readonly IAddressApplication _addressApplication;
        private readonly IContactApplication _contactApplication;

        private readonly Manufacturers _manufacturer = new();
        #endregion

        #region << CONSTRUCTOR >>
        public ManufacturerApplication(IAddressApplication addressApplication, IContactApplication contactApplication)
        {
            _addressApplication = addressApplication;
            _contactApplication = contactApplication;
        }
        #endregion

        #region << METHODS >>

        #region PUBLIC

        #region InsertAsync
        public async Task<Manufacturers> InsertAsync(ManufacturersModel model)
        {
            Validate(model);
            var manufacturers = await _manufacturer.GetAllAsync();
            if (model.BrazilianCompany)
            {
                if (manufacturers.Any(x => x?.FederalTaxpayerRegistrationNumber == model?.FederalTaxpayerRegistrationNumber
                    || x?.StateTaxpayerRegistrationNumber == model?.StateTaxpayerRegistrationNumber))
                {
                    throw new Exception("There's already a manufacturer registered with the federal or state taxpayer registration number.");
                }
            }
            else
            {
                if (manufacturers.Any(x => x?.CAGE == model?.CAGE || x?.SEC == model?.SEC))
                {
                    throw new Exception("There's already a manufacturer registered with the CAGE or SEC informed.");
                }
            }
            var address = await _addressApplication.GetAddressByPostalCodeAsync(model?.Addresses?.PostalCode);
            if (address == null)
            {
                await _addressApplication.InsertAsync(model.Addresses);
                await _addressApplication.InsertAddressAsync(model.Addresses);
            }
            else
            {
                model.Addresses.ID = address.ID;
            }
            model.Contacts.ID = (int)await _contactApplication.InsertAsync(model.Contacts);
            var manufacturerID = await _manufacturer.InsertAsync(model);
            if (manufacturerID is null)
            {
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }
            manufacturers = await _manufacturer.GetAllAsync();
            var manufacturer = manufacturers.FirstOrDefault(x => x?.CAGE == model?.CAGE);
            if (manufacturer is null)
            {
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }
            return manufacturer;
        }
        #endregion

        #endregion

        #region PRIVATE

        #region Validate
        private static void Validate(ManufacturersModel model)
        {
            if (model.BrazilianCompany)
            {
                if (string.IsNullOrEmpty(model.FederalTaxpayerRegistrationNumber))
                {
                    throw new Exception("Federal taxpayer registration number cannot be null or empty.");
                }
                if (string.IsNullOrEmpty(model.StateTaxpayerRegistrationNumber))
                {
                    throw new Exception("State taxpayer registration number cannot be null or empty.");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(model.Name)) throw new Exception("Name cannot be null or empty.");
                if (string.IsNullOrEmpty(model.CAGE)) throw new Exception("CAGE cannot be null or empty.");
                if (string.IsNullOrEmpty(model.NCAGE)) throw new Exception("NCAGE cannot be null or empty.");
                if (model.SEC is null) throw new Exception("SEC number cannot be null or empty.");
            }
        }
        #endregion

        #endregion

        #endregion
    }
}
