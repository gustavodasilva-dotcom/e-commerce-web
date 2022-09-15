namespace Loja.Web.Presentation.Models.Security.ViewModel
{
    public class UserRolesViewModel
    {
        public int ID { get; set; }

        public Guid GuidID { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }
    }
}
