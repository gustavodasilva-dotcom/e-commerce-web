using Loja.Web.Domain.Entities.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product;

namespace Loja.Web.Application.Interfaces.Registration.Product
{
    public interface IProductApplication
    {
        Task<IEnumerable<Products?>> GetAllAsync();
        Task<Products> ProcessAsync(ProductsModel model);
    }
}
