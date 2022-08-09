using Loja.Web.Domain.Entities.Security;
using Loja.Web.Tools.String.Extensions;

namespace Loja.Web.DTO.Security
{
    public class UsersDTO
    {
        #region << CONSTRUCTORS >>
        public UsersDTO(Users users)
        {
            ID = users.ID;
            Name = users.Name;
            Email = users.Email;
            Login = users.Login;
            Password = users.Password;
            Active = users.Active;
            Deleted = users.Deleted;
            Created_at = users.Created_at;
            Created_by = users.Created_by;
            Deleted_at = users.Deleted_at;
            Deleted_by = users.Deleted_by;
            UserRoleID = users.UserRoleID;
        }

        public UsersDTO(string name, string email, string login, string password)
        {
            Name = name;
            Email = email;
            Login = login;
            Password = password;
        }

        public UsersDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public UsersDTO(UsersDTO usersDTO)
        {
            _usersDTO = usersDTO;
        }
        #endregion

        #region << GETTERS SETTERS >>
        private UsersDTO? _usersDTO;
        public UsersDTO? usersDTO
        {
            get { return _usersDTO; }
            private set
            {
                _usersDTO = value;
            }
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
                if (!this._email.IsEmail())
                {
                    this._login = value;
                    this._email = null;
                }
                else
                {
                    this._login = null;
                }
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

        public Guid? _created_by;
        public Guid? Created_by
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

        private Guid? _deleted_by;
        public Guid? Deleted_by
        {
            get { return _deleted_by; }
            private set
            {
                this._deleted_by = value;
            }
        }

        public Guid? _userRoleID;
        public Guid? UserRoleID
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
