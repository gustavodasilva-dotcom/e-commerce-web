using Loja.Web.Domain.Entities.Registration.Product;

namespace Loja.Web.Application.Interfaces.Registration.Product
{
    public interface IProductApplication
    {
        Task<IEnumerable<Products?>> GetAllAsync();
    }
}
