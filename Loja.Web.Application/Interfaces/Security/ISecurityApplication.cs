using Loja.Web.Domain.Entities.Security;
using Loja.Web.Presentation.Models.Security;

namespace Loja.Web.Application.Interfaces.Security
{
    public interface ISecurityApplication
    {
        Task<Users> LoginAsync(string emailUsername, string password);
        Task<List<UserRoles>> GetUserRolesAsync();
        Task InsertAsync(UsersModel model);
    }
}
