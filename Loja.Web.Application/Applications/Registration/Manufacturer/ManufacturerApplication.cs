using Loja.Web.Application.Interfaces.Registration.Address;
using Loja.Web.Application.Interfaces.Registration.Contact;
using Loja.Web.Application.Interfaces.Registration.Manufacturer;
using Loja.Web.Domain.Entities.Registration.Address;
using Loja.Web.Domain.Entities.Registration.Contact;
using Loja.Web.Domain.Entities.Registration.Manufacturer;
using Loja.Web.Presentation.Models.Registration.Contact.ViewModel;
using Loja.Web.Presentation.Models.Registration.Manufacturer.Model;
using Loja.Web.Presentation.Models.Registration.Manufacturer.ViewModel;

namespace Loja.Web.Application.Applications.Registration.Manufacturer
{
    public class ManufacturerApplication : IManufacturerApplication
    {
        #region << PROPERTIES >>
        private readonly IAddressApplication _addressApplication;
        private readonly IContactApplication _contactApplication;

        private readonly Manufacturers _manufacturer = new();
        private readonly Addresses _addresses = new();
        private readonly Contacts _contacts = new();
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

        #region GetAllAsync
        public async Task<List<ManufacturerViewModel>> GetAllAsync()
        {
            var manufacturers = await _manufacturer.GetAllAsync();

            if (!manufacturers.Any()) throw new Exception("There's no manufacturers registered.");

            var addresses = await _addresses.GetAllAsync();
            var contacts = await _contacts.GetAllAsync();

            var result = new List<ManufacturerViewModel>();

            foreach (var manufacturer in manufacturers.Where(x => x.Active && !x.Deleted))
            {
                var contact = contacts.FirstOrDefault(x => x.ID == manufacturer.ContactID && x.Active && !x.Deleted);
                
                var address = addresses.FirstOrDefault(x => x.ID == manufacturer.AddressID && x.Active && !x.Deleted) ??
                    throw new Exception("No addresses was found. Please, contact the system administrator.");

                var addressModel = await _addressApplication.GetAddressesAsync(address);

                var contactModel = contact == null ? null : new ContactViewModel
                {
                    ID = contact.ID,
                    GuidID = contact.GuidID,
                    Phone = contact.Phone,
                    Cellphone = contact.Cellphone,
                    Email = contact.Email,
                    Website = contact.Website,
                    Active = contact.Active,
                    Deleted = contact.Deleted,
                    Created_at = contact.Created_at
                };

                result.Add(new ManufacturerViewModel
                {
                    ID = manufacturer.ID,
                    GuidID = manufacturer.GuidID,
                    Name = manufacturer.Name,
                    BrazilianCompany = manufacturer.BrazilianCompany,
                    CAGE = manufacturer.CAGE,
                    NCAGE = manufacturer.NCAGE,
                    SEC = manufacturer.SEC,
                    FederalTaxpayerRegistrationNumber = manufacturer.FederalTaxpayerRegistrationNumber,
                    StateTaxpayerRegistrationNumber = manufacturer.FederalTaxpayerRegistrationNumber,
                    Contact = contactModel,
                    Address = addressModel,
                    Active = manufacturer.Active,
                    Deleted = manufacturer.Deleted,
                    Created_at = manufacturer.Created_at,
                    Created_by = manufacturer.Created_by,
                    Deleted_at = manufacturer.Deleted_at,
                    Deleted_by = manufacturer.Deleted_by
                });
            }

            return result;
        }
        #endregion

        #region InsertAsync
        public async Task<Manufacturers> InsertAsync(ManufacturersModel model)
        {
            Validate(model);
            Manufacturers? manufacturer = null;
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
            var street = await _addressApplication.GetStreetByPostalCodeAsync(model?.Addresses?.PostalCode);
            if (street == null)
            {
                await _addressApplication.InsertAsync(model.Addresses);
                street = await _addressApplication.GetStreetByPostalCodeAsync(model?.Addresses?.PostalCode);
            }
            model.Addresses.StreetID = street?.ID;
            model.Addresses.ID = (int)await _addressApplication.InsertAddressAsync(model.Addresses);
            model.Contacts.ID = (int)await _contactApplication.InsertAsync(model.Contacts);
            var manufacturerID = await _manufacturer.InsertAsync(model);
            if (manufacturerID is null)
            {
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }
            manufacturers = await _manufacturer.GetAllAsync();
            if (model.BrazilianCompany)
            {
                manufacturer = manufacturers.FirstOrDefault(
                    x => x?.FederalTaxpayerRegistrationNumber == model?.FederalTaxpayerRegistrationNumber &&
                    x?.StateTaxpayerRegistrationNumber == model?.StateTaxpayerRegistrationNumber);
            }
            else
            {
                manufacturer = manufacturers.FirstOrDefault(x => x?.CAGE == model?.CAGE);
            }
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
                if (string.IsNullOrEmpty(model.FederalTaxpayerRegistrationNumber) &&
                    !int.TryParse(model.FederalTaxpayerRegistrationNumber, out int _))
                {
                    throw new Exception("Federal taxpayer registration number cannot be null or empty.");
                }
                if (!string.IsNullOrEmpty(model.StateTaxpayerRegistrationNumber) &&
                    !int.TryParse(model.StateTaxpayerRegistrationNumber, out int _))
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
