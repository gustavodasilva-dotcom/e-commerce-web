using Loja.Web.Domain.Entities.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product.Model;
using Loja.Web.Presentation.Models.Registration.Product.ViewModel;

namespace Loja.Web.Application.Interfaces.Registration.Product
{
    public interface ICategoryApplication
    {
        Task<List<CategoryViewModel>> GetAllAsync();
        Task<CategoryViewModel> InsertAsync(CategoriesModel model);
    }
}
