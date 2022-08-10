namespace Loja.Web.DTO.Security
{
    public class UserRolesDTO
    {
        #region << CONSTRUCTORS >>
        public UserRolesDTO(int id, Guid guidID, string? code, string? name, bool active, bool deleted)
        {
            ID = id;
            GuidID = guidID;
            Code = code;
            Name = name;
            Active = active;
            Deleted = deleted;
        }
        #endregion

        #region << GETTERS SETTERS >>
        private int _id;
        public int ID
        {
            get { return _id; }
            private set
            {
                _id = value;
            }
        }

        private Guid _guidID;
        public Guid GuidID
        {
            get { return _guidID; }
            private set
            {
                _guidID = value;
            }
        }

        private string? _code;
        public string? Code
        {
            get { return _code; }
            private set
            {
                _code = value;
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
        #endregion
    }
}