using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Domain.Entities.Registration.Product;
using Loja.Web.Domain.Entities.Security;
using Loja.Web.Presentation.Models.Registration.Product.Model;
using Loja.Web.Presentation.Models.Registration.Product.ViewModel;

namespace Loja.Web.Application.Applications.Registration.Product
{
    public class SubcategoryApplication : ISubcategoryApplication
    {
        #region << PROPERTIES >>
        private readonly Subcategories _subcategories = new();
        private readonly Categories _categories = new();
        private readonly Users _users = new();
        #endregion

        #region << METHODS >>

        #region PUBLIC

        #region GetAllAsync
        public async Task<List<SubcategoryViewModel>> GetAllAsync()
        {
            var subcategories = await _subcategories.GetAllAsync();

            if (!subcategories.Any()) throw new Exception("There's no subcategories registered.");

            var categories = await _categories.GetAllAsync();

            var result = new List<SubcategoryViewModel>();

            foreach (var subcategory in subcategories)
            {
                var category = categories.FirstOrDefault(x => x.ID == subcategory.CategoryID);

                var categoryModel = category == null ? null :
                    new CategoryViewModel
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

                result.Add(new SubcategoryViewModel
                {
                    ID = subcategory.ID,
                    GuidID = subcategory.GuidID,
                    Name = subcategory.Name,
                    Category = categoryModel,
                    Active = subcategory.Active,
                    Deleted = subcategory.Deleted,
                    Created_at = subcategory.Created_at,
                    Created_by = subcategory.Created_by,
                    Deleted_at = subcategory.Deleted_at,
                    Deleted_by = subcategory.Deleted_by
                });
            }

            return result;
        }
        #endregion

        #region InsertAsync
        public async Task<SubcategoryViewModel> InsertAsync(SubcategoriesModel model)
        {
            Validate(model);
            
            var users = await _users.GetAllAsync();
            model.Created_by = users?.Where(x => x.GuidID == model.UserGuid).FirstOrDefault()?.ID;

            var categories = await _categories.GetAllAsync();

            var category = categories.First(x => x.GuidID == model.CategoryGuid) ??
                throw new Exception("The category was not found. Please, contact the system administrator.");

            model.CategoryID = category?.ID;
            
            var subcategories = await _subcategories.GetAllAsync();

            if (subcategories.Any(x => x.Name == model?.Name?.Trim() && x.CategoryID == model?.CategoryID))
                throw new Exception("There's alredy a subcategory registered with the same name and category.");

            var subcategoryID = await _subcategories.InsertAsync(model) ??
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");

            subcategories = await _subcategories.GetAllAsync();
            var subcategory = subcategories.FirstOrDefault(x => x.ID == subcategoryID) ??
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");

            return new SubcategoryViewModel
            {
                ID = subcategory.ID,
                GuidID = subcategory.GuidID,
                Name = subcategory.Name,
                Category = new CategoryViewModel
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
                },
                Active = subcategory.Active,
                Deleted = subcategory.Deleted,
                Created_at = subcategory.Created_at,
                Created_by = subcategory.Created_by,
                Deleted_at = subcategory.Deleted_at,
                Deleted_by = subcategory.Deleted_by
            };
        }
        #endregion

        #endregion

        #region PRIVATE

        #region Validate
        private static void Validate(SubcategoriesModel model)
        {
            if (string.IsNullOrEmpty(model.Name)) throw new Exception("The category name cannot be null or empty.");
            if (model.CategoryGuid == Guid.Empty) throw new Exception("Please, select a subcategory.");
        }
        #endregion

        #endregion

        #endregion
    }
}
