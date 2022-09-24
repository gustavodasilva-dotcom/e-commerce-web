using Loja.Web.Presentation.Models.Registration.Address.Model;
using Loja.Web.Presentation.Models.Registration.Contact.Model;

namespace Loja.Web.Presentation.Models.Registration.Manufacturer.Model
{
    public class ManufacturersModel
    {
        public int? ID { get; set; }

        public Guid GuidID { get; set; } = Guid.Empty;

        public string? Name { get; set; }

        public bool BrazilianCompany { get; set; }

        #region Foreigh manufacturer
        public string? CAGE { get; set; }

        public string? NCAGE { get; set; }

        public string? SEC { get; set; }
        #endregion

        #region Brazilian manufacturer
        public string? FederalTaxpayerRegistrationNumber { get; set; } = null;

        public string? StateTaxpayerRegistrationNumber { get; set; } = null;
        #endregion

        public ContactsModel? Contacts { get; set; }

        public AddressesModel? Addresses { get; set; }

        public Guid UserGuid { get; set; } = Guid.Empty;
        public int? Created_by { get; set; } = null;

        public DateTime? Deleted_at { get; set; } = null;

        public int? Deleted_by { get; set; } = null;
    }
}
