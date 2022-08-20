using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Domain.Entities.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product;

namespace Loja.Web.Application.Applications.Registration.Product
{
    public class SubcategoryApplication : ISubcategoryApplication
    {
        #region << PROPERTIES >>
        private readonly Subcategories _subcategories = new();
        #endregion

        #region << METHODS >>

        #region PUBLIC

        #region GetAllAsync
        public async Task<IEnumerable<Subcategories?>> GetAllAsync()
        {
            return await _subcategories.GetAllAsync();
        }
        #endregion

        #region InsertAsync
        public async Task<Subcategories> InsertAsync(SubcategoriesModel model)
        {
            Subcategories? subcategory = null;
            var subcategories = await _subcategories.GetAllAsync();
            if (subcategories.Any(x => x.Name == model.Name.Trim() && x.CategoryID == model.CategoryID))
            {
                throw new Exception("There's alredy a category registered with the same name and category.");
            }
            var subcategoryID = await _subcategories.InsertAsync(model);
            if (subcategoryID is null)
            {
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }
            subcategories = await _subcategories.GetAllAsync();
            subcategory = subcategories.FirstOrDefault(x => x.ID == subcategoryID);
            if (subcategory is null)
            {
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }
            return subcategory;
        }
        #endregion

        #endregion

        #endregion
    }
}
