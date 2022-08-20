using Loja.Web.Domain.Entities.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product;

namespace Loja.Web.Application.Interfaces.Registration.Product
{
    public interface ISubcategoryApplication
    {
        Task<IEnumerable<Subcategories?>> GetAllAsync();
        Task<Subcategories> InsertAsync(SubcategoriesModel model);
    }
}
