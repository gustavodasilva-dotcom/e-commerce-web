namespace Loja.Web.Presentation.Models.Registration.Contact.ViewModel
{
    public class ContactViewModel
    {
		public int ID { get; set; }

		public Guid GuidID { get; set; }

		public string? Phone { get; set; }

		public string? Cellphone { get; set; }

		public string? Email { get; set; }

		public string? Website { get; set; }

		public bool Active { get; set; }

		public bool Deleted { get; set; }

		public DateTime Created_at { get; set; }
	}
}
