using Loja.Web.DTO.Security;
using Loja.Web.Presentation.Models.Security;

namespace Loja.Web.Application.Interfaces.Security
{
    public interface ISecurityApplication
    {
        Task<UsersDTO> LoginAsync(string emailUsername, string password);
        Task<List<UserRolesDTO>> GetUserRolesAsync();
        Task InsertAsync(UsersModel model);
    }
}
