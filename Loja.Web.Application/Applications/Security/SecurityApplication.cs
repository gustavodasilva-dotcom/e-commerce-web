using Loja.Web.Application.Interfaces.Security;
using Loja.Web.Domain.Entities.Security;
using Loja.Web.Tools.String.Extensions;

namespace Loja.Web.Application.Applications.Security
{
    public class SecurityApplication : ISecurityApplication
    {
        public async Task Login(string emailUsername, string password)
        {
            var users = await new Users().GetAll();
            if (users is null || !users.Any())
            {
                throw new Exception("Please, sign a user.");
            }
            Users? user = null;
            if (emailUsername.IsEmail())
            {
                user = users.Where(x => x.Email == emailUsername && x.Active && !x.Deleted).FirstOrDefault();
            }
            else
            {
                user = users.Where(x => x.Login == emailUsername && x.Active && !x.Deleted).FirstOrDefault();
            }
            if (user is null)
            {
                throw new Exception("Invalid username / e-mail.");
            }
            if (!user.Password.Decrypt().Equals(password))
            {
                throw new Exception("Invalid password.");
            }
        }
    }
}
