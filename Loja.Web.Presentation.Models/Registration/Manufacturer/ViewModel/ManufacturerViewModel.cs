using Loja.Web.Presentation.Models.Registration.Address.ViewModel;
using Loja.Web.Presentation.Models.Registration.Contact.ViewModel;

namespace Loja.Web.Presentation.Models.Registration.Manufacturer.ViewModel
{
    public class ManufacturerViewModel
    {
        public int ID { get; set; }

        public Guid GuidID { get; set; }

        public string Name { get; set; }

        public bool BrazilianCompany { get; set; }

        public string? CAGE { get; set; }

        public string? NCAGE { get; set; }

        public string? SEC { get; set; }

        public string? FederalTaxpayerRegistrationNumber { get; set; }

        public string? StateTaxpayerRegistrationNumber { get; set; }

        public ContactViewModel? Contact { get; set; }

        public AddressViewModel? Address { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public DateTime Created_at { get; set; }

        public int? Created_by { get; set; }

        public DateTime? Deleted_at { get; set; }

        public int? Deleted_by { get; set; }
    }
}
