using Loja.Web.Domain.Entities.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product.Model;
using Loja.Web.Presentation.Models.Registration.Product.ViewModel;

namespace Loja.Web.Application.Interfaces.Registration.Product
{
    public interface ISubcategoryApplication
    {
        Task<List<SubcategoryViewModel>> GetAllAsync();
        Task<SubcategoryViewModel> InsertAsync(SubcategoriesModel model);
    }
}
