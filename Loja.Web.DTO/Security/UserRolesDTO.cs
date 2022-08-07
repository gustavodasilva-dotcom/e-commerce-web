using Loja.Web.Domain.Entities.Security;

namespace Loja.Web.DTO.Security
{
    public class UserRolesDTO
    {
        public UserRolesDTO(UserRoles userRole)
        {
            ID = userRole.ID;
            Name = userRole.Name;
            Active = userRole.Active;
            Deleted = userRole.Deleted;
        }

        private Guid _ID;
        public Guid ID
        {
            get { return _ID; }
            private set
            {
                _ID = value;
            }
        }

        private string? _name;
        public string? Name
        {
            get { return _name; }
            private set
            {
                _name = value;
            }
        }

        private bool _active;
        public bool Active
        {
            get { return _active; }
            private set
            {
                _active = value;
            }
        }

        private bool _deleted;
        public bool Deleted
        {
            get { return _deleted; }
            private set
            {
                _deleted = value;
            }
        }
    }
}