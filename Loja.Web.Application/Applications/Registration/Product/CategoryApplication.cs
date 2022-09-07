using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Domain.Entities.Registration.Product;
using Loja.Web.Domain.Entities.Security;
using Loja.Web.Presentation.Models.Registration.Product.Model;

namespace Loja.Web.Application.Applications.Registration.Product
{
    public class CategoryApplication : ICategoryApplication
    {
        #region << PROPERTIES >>
        private readonly Categories _categories = new();
        private readonly Users _users = new();
        #endregion

        #region << METHODS >>

        #region PUBLIC

        #region GetAllAsync
        public async Task<IEnumerable<Categories?>> GetAllAsync()
        {
            return await _categories.GetAllAsync();
        }
        #endregion

        #region InsertAsync
        public async Task<Categories> InsertAsync(CategoriesModel model)
        {
            Validate(model);
            Categories? category = null;
            var users = await _users.GetAllAsync();
            if (model.Created_by_Guid != null)
            {
                model.Created_by = users?.Where(x => x.GuidID == model.Created_by_Guid).FirstOrDefault()?.ID;
            }
            var categories = await _categories.GetAllAsync();
            if (categories.Any(x => x.Name == model.Name.Trim()))
            {
                throw new Exception("There's alredy a category registered with the same name.");
            }
            var categoryID = await _categories.InsertAsync(model);
            if (categoryID is null)
            {
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }
            categories = await _categories.GetAllAsync();
            category = categories.FirstOrDefault(x => x.ID == categoryID);
            if (category is null)
            {
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }
            return category;
        }
        #endregion

        #endregion

        #region PRIVATE

        #region Validate
        private static void Validate(CategoriesModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new Exception("The category name cannot be null or empty.");
            }
        }
        #endregion

        #endregion

        #endregion
    }
}
