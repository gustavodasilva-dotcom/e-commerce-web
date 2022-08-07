using Loja.Web.DTO.Security;

namespace Loja.Web.Application.Interfaces.Security
{
    public interface ISecurityApplication
    {
        Task<UsersDTO> Login(UsersDTO user);
        Task<List<UserRolesDTO>> GetUserRoles();
    }
}
