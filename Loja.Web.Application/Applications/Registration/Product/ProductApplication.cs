using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Domain.Entities.Registration.Product;

namespace Loja.Web.Application.Applications.Registration.Product
{
    public class ProductApplication : IProductApplication
    {
        #region << PROPERTIES >>
        private readonly Products _products = new();
        #endregion

        #region << METHODS >>

        #region PUBLIC

        #region GetAllAsync
        public async Task<IEnumerable<Products?>> GetAllAsync()
        {
            return await _products.GetAllAsync();
        }
        #endregion

        #endregion

        #region PRIVATE

        #endregion

        #endregion
    }
}
