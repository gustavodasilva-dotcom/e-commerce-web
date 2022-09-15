using System.ComponentModel.DataAnnotations;

namespace Loja.Web.Presentation.Models.Security.Model
{
    public class UserModel
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public Guid UserGuid { get; set; } = Guid.Empty;
        public int? Created_by { get; set; } = null;

        public Guid? Deleted_by_Guid { get; set; } = null;
        public int? Deleted_by { get; set; } = null;

        public Guid UserRoleGuid { get; set; } = Guid.Empty;
        public int? UserRoleID { get; set; } = null;
    }
}
