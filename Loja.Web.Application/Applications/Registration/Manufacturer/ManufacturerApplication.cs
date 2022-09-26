using Loja.Web.Application.Interfaces.Registration.Address;
using Loja.Web.Application.Interfaces.Registration.Contact;
using Loja.Web.Application.Interfaces.Registration.Manufacturer;
using Loja.Web.Domain.Entities.Registration.Address;
using Loja.Web.Domain.Entities.Registration.Contact;
using Loja.Web.Domain.Entities.Registration.Manufacturer;
using Loja.Web.Domain.Entities.Security;
using Loja.Web.Presentation.Models.Registration.Contact.ViewModel;
using Loja.Web.Presentation.Models.Registration.Manufacturer.Model;
using Loja.Web.Presentation.Models.Registration.Manufacturer.ViewModel;
using System.Text.RegularExpressions;

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
        private readonly Users _users = new();
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

            foreach (var manufacturer in manufacturers.OrderBy(x => x.Name).Where(x => x.Active && !x.Deleted))
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
                    StateTaxpayerRegistrationNumber = manufacturer.StateTaxpayerRegistrationNumber,
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

        #region GetByIDAsync
        public async Task<ManufacturerViewModel> GetByIDAsync(Guid guid)
        {
            var manufacturers = await _manufacturer.GetAllAsync() ??
                throw new Exception("There's no manufacturers registered.");

            var manufacturer = manufacturers.FirstOrDefault(x => x.GuidID == guid && x.Active && !x.Deleted) ??
                throw new Exception("The manufacturer was not found.");

            var addresses = await _addresses.GetAllAsync();
            var contacts = await _contacts.GetAllAsync();

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

            return new ManufacturerViewModel
            {
                ID = manufacturer.ID,
                GuidID = manufacturer.GuidID,
                Name = manufacturer.Name,
                BrazilianCompany = manufacturer.BrazilianCompany,
                CAGE = manufacturer.CAGE,
                NCAGE = manufacturer.NCAGE,
                SEC = manufacturer.SEC,
                FederalTaxpayerRegistrationNumber = manufacturer.FederalTaxpayerRegistrationNumber,
                StateTaxpayerRegistrationNumber = manufacturer.StateTaxpayerRegistrationNumber,
                Contact = contactModel,
                Address = addressModel,
                Active = manufacturer.Active,
                Deleted = manufacturer.Deleted,
                Created_at = manufacturer.Created_at,
                Created_by = manufacturer.Created_by,
                Deleted_at = manufacturer.Deleted_at,
                Deleted_by = manufacturer.Deleted_by
            };
        }
        #endregion

        #region InsertAsync
        public async Task<ManufacturerViewModel> SaveAsync(ManufacturersModel model)
        {
            Validate(model);

            var users = await _users.GetAllAsync();

            var manufacturers = await _manufacturer.GetAllAsync();

            Manufacturers? manufacturer = null;

            manufacturer = manufacturers.FirstOrDefault(x => x.GuidID == model.GuidID && x.Active && !x.Deleted);

            if (model.GuidID != Guid.Empty && manufacturer != null)
            {
                model.ID = manufacturer.ID;
                model.GuidID = manufacturer.GuidID;
                model.Created_by = manufacturer.Created_by;
            }
            else
                model.Created_by = users?.Where(x => x.GuidID == model.UserGuid && x.Active && !x.Deleted).FirstOrDefault()?.ID;

            if (model.BrazilianCompany)
            {
                if (manufacturers.Any(x => x.GuidID != model.GuidID
                    && x?.FederalTaxpayerRegistrationNumber == model?.FederalTaxpayerRegistrationNumber))
                    throw new Exception("There's already a manufacturer registered with the federal taxpayer registration number informed.");

                if (manufacturers.Any(x => x.GuidID != model.GuidID
                    && !string.IsNullOrEmpty(x?.StateTaxpayerRegistrationNumber)
                    && x?.StateTaxpayerRegistrationNumber == model?.StateTaxpayerRegistrationNumber))
                    throw new Exception("There's already a manufacturer registered with the state taxpayer registration number informed.");
            }
            else
            {
                if (manufacturers.Any(x => x.GuidID != model.GuidID && x?.CAGE == model?.CAGE))
                    throw new Exception("There's already a manufacturer registered with the CAGE informed.");

                if (manufacturers.Any(x => x.GuidID != model.GuidID && x?.SEC == model?.SEC))
                    throw new Exception("There's already a manufacturer registered with the SEC informed.");
            }

            var street = await _addressApplication.GetStreetByPostalCodeAsync(model?.Addresses?.PostalCode ??
                throw new Exception("The address or the postal code was not informed."));

            if (street == null)
            {
                await _addressApplication.InsertAsync(model.Addresses);
                street = await _addressApplication.GetStreetByPostalCodeAsync(model?.Addresses?.PostalCode ??
                    throw new Exception("The address or the postal code was not informed."));
            }

            if (model.Addresses != null)
            {
                if (manufacturer != null)
                {
                    var addresses = await _addresses.GetAllAsync();

                    var manufacturerAddress = addresses.FirstOrDefault(x => x.ID == manufacturer?.AddressID && x.Active && !x.Deleted);

                    if (manufacturerAddress != null)
                    {
                        model.Addresses.ID = manufacturerAddress.ID;

                        if (street?.ID != manufacturerAddress?.StreetID)
                        {
                            model.Addresses.StreetID = street?.ID;

                            if (!await _addresses.UpdateAsync(model.Addresses, manufacturerAddress ??
                                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.")))
                                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
                        }
                    }
                }
                else
                {
                    model.Addresses.StreetID = street?.ID;
                    model.Addresses.ID = (int)await _addressApplication.InsertAddressAsync(model.Addresses);
                }
            }
            
            if (model.Contacts != null)
            {
                var contacts = await _contacts.GetAllAsync();

                var manufacturerContact = contacts.FirstOrDefault(x => x.ID == manufacturer?.ContactID && x.Active && !x.Deleted);

                if (manufacturerContact != null)
                {
                    model.Contacts.ID = manufacturerContact.ID;

                    if (!await _contacts.UpdateAsync(model.Contacts, manufacturerContact))
                        throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
                }
                else
                    model.Contacts.ID = (int)await _contactApplication.InsertAsync(model.Contacts);
            }

            long? manufacturerID = null;

            if (model.GuidID == Guid.Empty)
            {
                manufacturerID = await _manufacturer.InsertAsync(model) ??
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }
            else
            {
                if (!await _manufacturer.UpdateAsync(model, manufacturer ??
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.")))
                    manufacturerID = manufacturers.First(x => x.GuidID == model.GuidID && x.Active && !x.Deleted).ID;
            }
            
            manufacturers = await _manufacturer.GetAllAsync();

            if (model.BrazilianCompany)
            {
                manufacturer = manufacturers.FirstOrDefault(
                    x => x?.FederalTaxpayerRegistrationNumber == model?.FederalTaxpayerRegistrationNumber &&
                    x?.StateTaxpayerRegistrationNumber == model?.StateTaxpayerRegistrationNumber) ??
                        throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }
            else
            {
                manufacturer = manufacturers.FirstOrDefault(x => x?.CAGE == model?.CAGE) ??
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }

            return await GetByIDAsync(manufacturer.GuidID);
        }
        #endregion

        #endregion

        #region PRIVATE

        #region Validate
        private static void Validate(ManufacturersModel model)
        {
            if (model.BrazilianCompany)
            {
                if (!string.IsNullOrEmpty(model.FederalTaxpayerRegistrationNumber))
                    model.FederalTaxpayerRegistrationNumber = Regex.Replace(model.FederalTaxpayerRegistrationNumber, @"[^0-9a-zA-Z]+", "");
                
                if (!string.IsNullOrEmpty(model.StateTaxpayerRegistrationNumber))
                    model.StateTaxpayerRegistrationNumber = Regex.Replace(model.StateTaxpayerRegistrationNumber, @"[^0-9a-zA-Z]+", "");

                if (string.IsNullOrEmpty(model.FederalTaxpayerRegistrationNumber) &&
                    !int.TryParse(model.FederalTaxpayerRegistrationNumber, out int _))
                    throw new Exception("Federal taxpayer registration number cannot be null or empty.");

                if (!string.IsNullOrEmpty(model.StateTaxpayerRegistrationNumber) &&
                    !int.TryParse(model.StateTaxpayerRegistrationNumber, out int _))
                    throw new Exception("State taxpayer registration number cannot be null or empty.");
            }
            else
            {
                model.CAGE = Regex.Replace(model.CAGE, @"[^0-9a-zA-Z]+", "");
                model.NCAGE = Regex.Replace(model.NCAGE, @"[^0-9a-zA-Z]+", "");

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
