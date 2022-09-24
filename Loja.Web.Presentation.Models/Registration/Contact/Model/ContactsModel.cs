namespace Loja.Web.Presentation.Models.Registration.Contact.Model
{
    public class ContactsModel
    {
        public int? ID { get; set; }

        public Guid GuidID { get; set; } = Guid.NewGuid();

        public string? Phone { get; set; }

        public string? Cellphone { get; set; }

        public string? Email { get; set; }

        public string? Website { get; set; }
    }
}
