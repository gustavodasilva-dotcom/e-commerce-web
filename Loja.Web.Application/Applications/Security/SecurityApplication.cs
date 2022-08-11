using Loja.Web.Application.Interfaces.Security;
using Loja.Web.Domain.Entities.Security;
using Loja.Web.Tools.String.Extensions;
using Loja.Web.Presentation.Models.Security;

namespace Loja.Web.Application.Applications.Security
{
    public class SecurityApplication : ISecurityApplication
    {
        #region << PROPERTIES >>
        private readonly Users _users = new();
        private readonly UserRoles _userRoles = new();
        #endregion

        #region << METHODS >>

        #region LoginAsync
        public async Task<Users> LoginAsync(string emailUsername, string password)
        {
            Validate(emailUsername, password);
            var users = await _users.GetAllAsync();
            if (users is null || !users.Any())
            {
                throw new Exception("Please, sign a user.");
            }
            Users? domain = null;
            if (emailUsername.IsEmail())
            {
                domain = users.Where(x => x.Email == emailUsername && x.Active && !x.Deleted).FirstOrDefault();
            }
            else
            {
                domain = users.Where(x => x.Login == emailUsername && x.Active && !x.Deleted).FirstOrDefault();
            }
            if (domain is null)
            {
                throw new Exception("Invalid username / e-mail.");
            }
            if (!domain.Password.Decrypt().Equals(password))
            {
                throw new Exception("Invalid password.");
            }
            return domain;
        }
        #endregion

        #region GetUserRolesAsync
        public async Task<List<UserRoles>> GetUserRolesAsync()
        {
            var roles = await _userRoles.GetAllAsync();
            if (!roles.Any())
            {
                throw new Exception("There's no user roles registered.");
            }
            return roles.OrderBy(x => x.Name).ToList();
        }
        #endregion

        #region Insert
        public async Task InsertAsync(UsersModel model)
        {
            Validate(model);
            var users = await _users.GetAllAsync();
            if (users.Where(x => x.Login == model.Login).Any())
            {
                throw new Exception(string.Format("There's alredy an user registered with the username {0}.", model.Login));
            }
            if (model.Created_by_Guid != null)
            {
                model.Created_by = users?.Where(x => x.GuidID == model.Created_by_Guid).FirstOrDefault()?.ID;
            }
            if (model.UserRoleID_Guid != null)
            {
                var roles = await GetUserRolesAsync();
                model.UserRoleID = roles?.Where(x => x.GuidID == model.UserRoleID_Guid).FirstOrDefault()?.ID;
            }
            model.Password = model?.Password?.Encrypt();
            if (await _users.InsertAsync(model) is null)
            {
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }
        }
        #endregion

        #region Validate
        private static void Validate(UsersModel model)
        {
            if (string.IsNullOrEmpty(model.Name)) throw new Exception("Please, inform the name.");
            if (string.IsNullOrEmpty(model.Email)) throw new Exception("Please, inform the e-mail.");
            if (string.IsNullOrEmpty(model.Login)) throw new Exception("Please, inform the login.");
            if (string.IsNullOrEmpty(model.Password)) throw new Exception("Please, inform the password.");
        }

        private static void Validate(string emailUsername, string password)
        {
            if (string.IsNullOrEmpty(emailUsername)) throw new Exception("Please, inform the username / e-mail.");
            if (string.IsNullOrEmpty(password)) throw new Exception("Please, inform the password.");
        }
        #endregion

        #endregion
    }
}
