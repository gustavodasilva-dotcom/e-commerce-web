using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
using Loja.Web.Presentation.Models.Registration.Product.Model;
using System.Diagnostics;

namespace Loja.Web.Domain.Entities.Registration.Product
{
    [Table("ProductsRatings")]
    public class ProductsRatings : Repository
    {
        #region << PROPERTIES >>
        [Key]
        public int ID { get; private set; }
        public Guid GuidID { get; private set; }
        public int Rating { get; private set; }
        public int ProductID { get; private set; }
        public DateTime Created_at { get; private set; }
        public int? Created_by { get; private set; }
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<IEnumerable<ProductsRatings>> GetAllAsync()
        {
            try
            {
                var connect = await ConnectAsync();
                return await connect.GetAllAsync<ProductsRatings>();
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
        public async Task<long?> InsertAsync(ProductsRatingsModel model)
        {
            long? id = null;
            try
            {
                var connect = await ConnectAsync();
                id = await connect.InsertAsync(new ProductsRatings
                {
                    GuidID = Guid.NewGuid(),
                    Rating = model.Rating,
                    ProductID = model.ProductID,
                    Created_at = DateTime.Now,
                    Created_by = model?.Created_by,
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
        public async Task<bool> UpdateAsync(ProductsRatings productRating, ProductsRatingsModel model)
        {
            var updated = false;
            try
            {
                var connect = await ConnectAsync();
                updated = await connect.UpdateAsync(new ProductsRatings
                {
                    ID = productRating.ID,
                    GuidID = productRating.GuidID,
                    Rating = model.Rating,
                    ProductID = productRating.ProductID,
                    Created_at = productRating.Created_at,
                    Created_by = productRating.Created_by,
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
