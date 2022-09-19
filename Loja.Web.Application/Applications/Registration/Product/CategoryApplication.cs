using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Domain.Entities.Registration.Product;
using Loja.Web.Domain.Entities.Security;
using Loja.Web.Presentation.Models.Registration.Product.Model;
using Loja.Web.Presentation.Models.Registration.Product.ViewModel;

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
        public async Task<List<CategoryViewModel>> GetAllAsync()
        {
            var categories = await _categories.GetAllAsync();

            if (!categories.Any()) throw new Exception("There's no categories registered.");

            return categories.Select(x => new CategoryViewModel
            {
                ID = x.ID,
                GuidID = x.GuidID,
                Name = x.Name,
                Active = x.Active,
                Deleted = x.Deleted,
                Created_at = x.Created_at,
                Created_by = x.Created_by,
                Deleted_at = x.Deleted_at,
                Deleted_by = x.Deleted_by
            }).Where(x => x.Active && !x.Deleted).OrderBy(x => x.Name).ToList();
        }
        #endregion

        #region InsertAsync
        public async Task<CategoryViewModel> InsertAsync(CategoriesModel model)
        {
            Validate(model);

            var users = await _users.GetAllAsync();

            if (model.UserGuid != Guid.Empty)
                model.Created_by = users?.Where(x => x.GuidID == model.UserGuid && x.Active && !x.Deleted).FirstOrDefault()?.ID;
            
            var categories = await _categories.GetAllAsync();

            if (categories.Any(x => x.Name == model?.Name?.Trim()))
                throw new Exception("There's alredy a category registered with the same name.");
            
            var categoryID = await _categories.InsertAsync(model) ??
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");

            categories = await _categories.GetAllAsync();

            var category = categories.FirstOrDefault(x => x.ID == categoryID && x.Active && !x.Deleted) ??
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");

            return new CategoryViewModel
            {
                ID = category.ID,
                GuidID = category.GuidID,
                Name = category.Name,
                Active = category.Active,
                Deleted = category.Deleted,
                Created_at = category.Created_at,
                Created_by = category.Created_by,
                Deleted_at = category.Deleted_at,
                Deleted_by = category.Deleted_by
            };
        }
        #endregion

        #region UpdateAsync
        public async Task<CategoryViewModel> UpdateAsync(CategoriesModel model)
        {
            Validate(model, isEdit: true);

            var categories = await _categories.GetAllAsync();

            var category = categories.FirstOrDefault(x => x.GuidID == model.GuidID && x.Active && !x.Deleted) ??
                throw new Exception("The category was not found. Please, contact the system administrator.");

            if (!await _categories.UpdateAsync(category, model))
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");

            categories = await _categories.GetAllAsync();

            category = categories.FirstOrDefault(x => x.ID == category.ID && x.Active && !x.Deleted) ??
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");

            return new CategoryViewModel
            {
                ID = category.ID,
                GuidID = category.GuidID,
                Name = category.Name,
                Active = category.Active,
                Deleted = category.Deleted,
                Created_at = category.Created_at,
                Created_by = category.Created_by,
                Deleted_at = category.Deleted_at,
                Deleted_by = category.Deleted_by
            };
        }
        #endregion

        #endregion

        #region PRIVATE

        #region Validate
        private static void Validate(CategoriesModel model, bool isEdit = false)
        {
            if (isEdit)
            {
                if (model.GuidID == Guid.Empty)
                    throw new Exception("The id was not sent in the request. Please, contact the system administrator.");
            }

            if (string.IsNullOrEmpty(model.Name)) throw new Exception("The category name cannot be null or empty.");
        }
        #endregion

        #endregion

        #endregion
    }
}
