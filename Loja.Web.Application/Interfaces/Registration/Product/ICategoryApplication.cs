using Loja.Web.Domain.Entities.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product.Model;

namespace Loja.Web.Application.Interfaces.Registration.Product
{
    public interface ICategoryApplication
    {
        Task<IEnumerable<Categories?>> GetAllAsync();
        Task<Categories> InsertAsync(CategoriesModel model);
    }
}
