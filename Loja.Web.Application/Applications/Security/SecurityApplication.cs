using Loja.Web.Application.Interfaces.Security;
using Loja.Web.Domain.Entities.Security;
using Loja.Web.DTO.Security;
using Loja.Web.Tools.String.Extensions;

namespace Loja.Web.Application.Applications.Security
{
    public class SecurityApplication : ISecurityApplication
    {
        #region << PROPERTIES >>
        private readonly Users _users = new();
        private readonly UserRoles _userRoles = new();
        #endregion

        #region << METHODS >>

        #region Login
        public async Task<UsersDTO> Login(UsersDTO user)
        {
            var users = await _users.GetAll();
            if (users is null || !users.Any())
            {
                throw new Exception("Please, sign a user.");
            }
            Users? userDb = null;
            if (string.IsNullOrEmpty(user?.Login))
            {
                userDb = users.Where(x => x.Email == user?.Email && x.Active && !x.Deleted).FirstOrDefault();
            }
            else
            {
                userDb = users.Where(x => x.Login == user?.Login && x.Active && !x.Deleted).FirstOrDefault();
            }
            if (userDb is null)
            {
                throw new Exception("Invalid username / e-mail.");
            }
            if (!userDb.Password.Decrypt().Equals(user?.Password))
            {
                throw new Exception("Invalid password.");
            }
            return _ = new UsersDTO(userDb);
        }
        #endregion

        #region GetUserRoles
        public async Task<List<UserRolesDTO>> GetUserRoles()
        {
            var roles = await _userRoles.GetAll();
            if (!roles.Any())
            {
                throw new Exception("There's no user roles registered.");
            }
            var rolesDTO = new List<UserRolesDTO>();
            foreach (var role in roles)
            {
                rolesDTO.Add(_ = new UserRolesDTO(role));
            }
            return rolesDTO;
        }
        #endregion

        #endregion
    }
}
