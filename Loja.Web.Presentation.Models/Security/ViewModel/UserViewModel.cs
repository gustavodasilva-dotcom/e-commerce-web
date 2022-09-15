namespace Loja.Web.Presentation.Models.Security.ViewModel
{
    public class UserViewModel
    {
        public int ID { get; set; }

        public Guid GuidID { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Login { get; set; }

        public string Password { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public DateTime Created_at { get; set; }

        public int? Created_by { get; set; }

        public DateTime? Deleted_at { get; set; }

        public int? Deleted_by { get; set; }

        public int? UserRoleID { get; set; }
    }
}
