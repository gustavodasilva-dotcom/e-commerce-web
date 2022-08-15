using Loja.Web.Presentation.Models.Registration.Address;
using Loja.Web.Presentation.Models.Registration.Contact;

namespace Loja.Web.Presentation.Models.Registration.Manufacturer
{
    public class ManufacturersModel
    {
        public int? ID { get; set; } = null;

        public Guid GuidID { get; set; } = Guid.NewGuid();

        public string? Name { get; set; }

        public bool BrazilianCompany { get; set; } = true;

        #region Foreigh manufacturer
        public string? CAGE { get; set; } = null;

        public string? NCAGE { get; set; } = null;

        public string? SEC { get; set; } = null;
        #endregion

        #region Brazilian manufacturer
        public string? FederalTaxpayerRegistrationNumber { get; set; } = null;

        public string? StateTaxpayerRegistrationNumber { get; set; } = null;
        #endregion

        public ContactsModel? Contacts { get; set; }

        public AddressesModel? Addresses { get; set; }

        public bool Active { get; set; } = true;

        public bool Deleted { get; set; } = false;

        public DateTime Created_at { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));

        public int? Created_by { get; set; } = null;

        public DateTime? Deleted_at { get; set; } = null;

        public int? Deleted_by { get; set; } = null;

        public int? UserRoleID { get; set; } = null;
    }
}
