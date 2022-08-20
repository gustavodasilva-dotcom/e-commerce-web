using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Domain.Entities.Registration.Product;

namespace Loja.Web.Application.Applications.Registration.Product
{
    public class CategoryApplication : ICategoryApplication
    {
        #region << PROPERTIES >>
        private readonly Categories _categories = new();
        #endregion

        #region << METHODS >>

        #region PUBLIC

        #region GetAllAsync
        public async Task<IEnumerable<Categories?>> GetAllAsync()
        {
            return await _categories.GetAllAsync();
        }
        #endregion

        #endregion

        #endregion
    }
}
