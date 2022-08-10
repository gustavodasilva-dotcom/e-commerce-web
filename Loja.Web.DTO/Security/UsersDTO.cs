namespace Loja.Web.DTO.Security
{
    public class UsersDTO
    {
        #region << CONSTRUCTORS >>
        public UsersDTO(
            Guid guidID,
            string? name,
            string? email,
            string? login,
            string password,
            bool active,
            bool deleted,
            DateTime created_at,
            int? created_by,
            DateTime? deleted_at,
            int? deleted_by,
            int? userRoleID
            )
        {
            GuidID = guidID;
            Name = name;
            Email = email;
            Login = login;
            Password = password;
            Active = active;
            Deleted = deleted;
            Created_at = created_at;
            Created_by = created_by;
            Deleted_at = deleted_at;
            Deleted_by = deleted_by;
            UserRoleID = userRoleID;
        }
        #endregion

        #region << GETTERS SETTERS >>
        private Guid _guidID;
        public Guid GuidID
        {
            get { return _guidID; }
            private set
            {
                _guidID = value;
            }
        }

        private string? _name;
        public string? Name
        {
            get { return _name; }
            private set
            {
                this._name = string.IsNullOrEmpty(value) ?
                    throw new ArgumentException("Property cannot be empty.", nameof(Name)) : value.Trim();
            }
        }

        private string? _email;
        public string? Email
        {
            get { return _email; }
            private set
            {
                this._email = string.IsNullOrEmpty(value) ?
                    throw new ArgumentException("Property cannot be empty.", nameof(Email)) : value.Trim();
            }
        }

        private string? _login;
        public string? Login
        {
            get { return _login; }
            private set
            {
                this._login = string.IsNullOrEmpty(value) ?
                    throw new ArgumentException("Property cannot be empty.", nameof(Login)) : value.Trim();
            }
        }

        private string? _password;
        public string? Password
        {
            get { return _password; }
            private set
            {
                this._password = string.IsNullOrEmpty(value) ?
                    throw new ArgumentException("Property cannot be empty.", nameof(Password)) : value.Trim();
            }
        }

        private bool _active;
        public bool Active
        {
            get { return _active; }
            private set
            {
                this._active = value;
            }
        }

        private bool _deleted;
        public bool Deleted
        {
            get { return _deleted; }
            private set
            {
                this._deleted = value;
            }
        }

        private DateTime _created_at;
        public DateTime Created_at
        {
            get { return _created_at; }
            private set
            {
                this._created_at = value;
            }
        }

        public int? _created_by;
        public int? Created_by
        {
            get { return _created_by; }
            private set
            {
                this._created_by = value;
            }
        }

        public DateTime? _deleted_at;
        public DateTime? Deleted_at
        {
            get { return _deleted_at; }
            private set
            {
                this._deleted_at = value;
            }
        }

        private int? _deleted_by;
        public int? Deleted_by
        {
            get { return _deleted_by; }
            private set
            {
                this._deleted_by = value;
            }
        }

        private int? _userRoleID;
        public int? UserRoleID
        {
            get { return _userRoleID; }
            private set
            {
                this._userRoleID = value;
            }
        }
        #endregion
    }
}
