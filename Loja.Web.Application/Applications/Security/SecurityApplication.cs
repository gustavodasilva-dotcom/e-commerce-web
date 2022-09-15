using Loja.Web.Application.Interfaces.Security;
using Loja.Web.Domain.Entities.Security;
using Loja.Web.Tools.String.Extensions;
using Loja.Web.Presentation.Models.Security.Model;
using Loja.Web.Presentation.Models.Security.ViewModel;

namespace Loja.Web.Application.Applications.Security
{
    public class SecurityApplication : ISecurityApplication
    {
        #region << PROPERTIES >>
        private readonly Users _users = new();
        private readonly UserRoles _userRoles = new();
        #endregion

        #region << METHODS >>

        #region GetUserRolesAsync
        public async Task<List<UserRolesViewModel>> GetUserRolesAsync()
        {
            var roles = await _userRoles.GetAllAsync();
            
            if (!roles.Any()) throw new Exception("There's no user roles registered.");

            return roles.Select(x => new UserRolesViewModel
            {
                ID = x.ID,
                GuidID = x.GuidID,
                Code = x.Code,
                Name = x.Name,
                Active = x.Active,
                Deleted = x.Deleted
            }).OrderBy(x => x.Name).ToList();
        }
        #endregion

        #region LoginAsync
        public async Task<UserViewModel> LoginAsync(string emailUsername, string password)
        {
            Validate(emailUsername, password);

            var users = await _users.GetAllAsync();
            
            if (users is null || !users.Any()) throw new Exception("Please, sign a user.");
            
            Users? user = null;

            if (emailUsername.IsEmail())
                user = users.Where(x => x.Email == emailUsername && x.Active && !x.Deleted).FirstOrDefault() ??
                    throw new Exception("Invalid username / e-mail.");
            else
                user = users.Where(x => x.Login == emailUsername && x.Active && !x.Deleted).FirstOrDefault() ??
                    throw new Exception("Invalid username / e-mail.");

            var userPassword = user.Password ??
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");

            if (!userPassword.Decrypt().Equals(password)) throw new Exception("Invalid password.");

            return new UserViewModel
            {
                ID = user.ID,
                GuidID = user.GuidID,
                Name = user.Name,
                Email = user.Email,
                Login = user.Login,
                Active = user.Active,
                Deleted = user.Deleted,
                Created_at = user.Created_at,
                Deleted_at = user.Deleted_at,
                Created_by = user.Created_by,
                Deleted_by = user.Deleted_by,
                UserRoleID = user.UserRoleID
            };
        }
        #endregion

        #region InsertAsync
        public async Task<UserViewModel> InsertAsync(UserModel model)
        {
            Validate(model);

            var users = await _users.GetAllAsync();
            var roles = await GetUserRolesAsync();

            if (users.Where(x => x.Login == model.Login).Any())
                throw new Exception(string.Format("There's already an user registered with the username {0}.", model.Login));

            if (model.UserGuid != Guid.Empty)
                model.Created_by = users?.Where(x => x.GuidID == model.UserGuid).FirstOrDefault()?.ID;

            if (model.UserRoleGuid == Guid.Empty)
                model.UserRoleID = roles?.OrderByDescending(x => x.Name).First().ID;
            else
                model.UserRoleID = roles?.OrderByDescending(x => x.Name).Last().ID;

            model.Password = model?.Password?.Encrypt();

            var userID = await _users.InsertAsync(model ??
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.")) ??
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");

            users = await _users.GetAllAsync();
            var user = users.FirstOrDefault(x => x.ID == userID) ??
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");

            return new UserViewModel
            {
                ID = user.ID,
                GuidID = user.GuidID,
                Name = user.Name,
                Email = user.Email,
                Login = user.Login,
                Active = user.Active,
                Deleted = user.Deleted,
                Created_at = user.Created_at,
                Deleted_at = user.Deleted_at,
                Created_by = user.Created_by,
                Deleted_by = user.Deleted_by,
                UserRoleID = user.UserRoleID
            };
        }
        #endregion

        #region Validate
        private static void Validate(UserModel model)
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
