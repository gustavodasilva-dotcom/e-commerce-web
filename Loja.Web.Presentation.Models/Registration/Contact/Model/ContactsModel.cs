namespace Loja.Web.Presentation.Models.Registration.Contact.Model
{
    public class ContactsModel
    {
        public int? ID { get; set; } = null;

        public Guid GuidID { get; set; } = Guid.NewGuid();

        public string? Phone { get; set; }

        public string? Cellphone { get; set; }

        public string? Email { get; set; }

        public string? Website { get; set; }

        public bool Active { get; set; } = true;

        public bool Deleted { get; set; } = false;

        public DateTime Created_at { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
    }
}
