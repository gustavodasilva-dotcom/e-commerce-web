namespace Loja.Web.Application.Interfaces.Security
{
    public interface ISecurityApplication
    {
        Task Login(string emailUsername, string password);
    }
}
