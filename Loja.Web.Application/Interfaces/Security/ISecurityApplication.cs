using Loja.Web.Presentation.Models.Security.Model;
using Loja.Web.Presentation.Models.Security.ViewModel;

namespace Loja.Web.Application.Interfaces.Security
{
    public interface ISecurityApplication
    {
        Task<List<UserRolesViewModel>> GetUserRolesAsync();
        Task<UserViewModel> LoginAsync(string emailUsername, string password);
        Task<UserViewModel> InsertAsync(UserModel model);
    }
}
