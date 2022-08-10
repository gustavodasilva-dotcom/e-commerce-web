using System.ComponentModel.DataAnnotations;

namespace Loja.Web.Presentation.MVC.Models.Security
{
    public class Users
    {
        public Guid? ID { get; set; } = null;

        [Required(ErrorMessage = "The name must be informed.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The e-mail must be informed.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The login must be informed.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The password must be informed.")]
        public string Password { get; set; }
        
        public bool Active { get; set; } = true;
        
        public bool Deleted { get; set; } = false;
        
        public DateTime Created_at { get; set; } = DateTime.Now;
        
        public Guid? Created_by { get; set; } = null;

        public DateTime? Deleted_at { get; set; } = null;

        public Guid? Deleted_by { get; set; } = null;

        public Guid? UserRoleID { get; set; } = null;
    }
}
