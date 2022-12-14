using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
using Loja.Web.Presentation.Models.Registration.Product.Model;
using System.Diagnostics;

namespace Loja.Web.Domain.Entities.Registration.Product
{
    [Table("Subcategories")]
    public class Subcategories : Repository
    {
        #region << PROPERTIES >>
        [Key]
		public int ID { get; private set; }
		public Guid GuidID { get; private set; }
		public string? Name { get; private set; }
		public int? CategoryID { get; private set; }
		public bool Active { get; private set; }
		public bool Deleted { get; private set; }
		public DateTime Created_at { get; private set; }
		public int? Created_by { get; private set; }
		public DateTime? Deleted_at { get; private set; }
		public int? Deleted_by { get; private set; }
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<IEnumerable<Subcategories>> GetAllAsync()
        {
            try
            {
                var connect = await ConnectAsync();
                return await connect.GetAllAsync<Subcategories>();
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine(e);
#endif
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region InsertAsync
        public async Task<long?> InsertAsync(SubcategoriesModel model)
        {
            long? id = null;
            try
            {
                var connect = await ConnectAsync();
                id = await connect.InsertAsync(new Subcategories
                {
                    GuidID = Guid.NewGuid(),
                    Name = model?.Name?.Trim(),
                    CategoryID = model?.CategoryID,
                    Active = true,
                    Deleted = false,
                    Created_at = DateTime.Now,
                    Created_by = model?.Created_by,
                    Deleted_at = null,
                    Deleted_by = null
                });
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine(e);
#endif
                throw new Exception(e.Message);
            }
            return id;
        }
        #endregion

        #region UpdateAsync
        public async Task<bool> UpdateAsync(Subcategories subcategory, SubcategoriesModel model)
        {
            var updated = false;
            try
            {
                var connect = await ConnectAsync();
                updated = await connect.UpdateAsync(new Subcategories
                {
                    ID = subcategory.ID,
                    GuidID = subcategory.GuidID,
                    Name = model?.Name?.Trim(),
                    CategoryID = model?.CategoryID,
                    Active = subcategory.Active,
                    Deleted = subcategory.Deleted,
                    Created_at = subcategory.Created_at,
                    Created_by = subcategory.Created_by,
                    Deleted_at = subcategory.Deleted_at,
                    Deleted_by = subcategory.Deleted_by
                });
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine(e);
#endif
                throw new Exception(e.Message);
            }
            return updated;
        }
        #endregion

        #endregion
    }
}
