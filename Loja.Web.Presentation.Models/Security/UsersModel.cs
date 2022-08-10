using System.ComponentModel.DataAnnotations;

namespace Loja.Web.Presentation.Models.Security
{
    public class UsersModel
    {
        public int? ID { get; set; } = null;

        public Guid GuidID { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "The name must be informed.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The e-mail must be informed.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "The login must be informed.")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "The password must be informed.")]
        public string? Password { get; set; }

        public bool Active { get; set; } = true;

        public bool Deleted { get; set; } = false;

        public DateTime Created_at { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));

        public Guid? Created_by_Guid { get; set; } = null;
        public int? Created_by { get; set; } = null;

        public DateTime? Deleted_at { get; set; } = null;

        public Guid? Deleted_by_Guid { get; set; } = null;
        public int? Deleted_by { get; set; } = null;

        public Guid? UserRoleID_Guid { get; set; } = null;
        public int? UserRoleID { get; set; } = null;
    }
}
