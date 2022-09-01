using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
using Loja.Web.Presentation.Models.Registration.ShoppingCart;
using System.Diagnostics;

namespace Loja.Web.Domain.Entities.Registration.ShoppingCart
{
    [Table("ShoppingCartsProducts")]
    public class ShoppingCartsProducts : Repository
    {
        #region << PROPERTIES >>
        [Key]
        public int ID { get; private set; }
        public Guid GuidID { get; private set; }
        public int Quantity { get; private set; }
        public int ProductID { get; private set; }
        public int? ShoppingCartID { get; private set; }
        public bool Active { get; private set; }
        public bool Deleted { get; private set; }
        public DateTime Created_at { get; private set; }
        public int? Created_by { get; private set; }
        public DateTime? Deleted_at { get; private set; }
        public int? Deleted_by { get; private set; }
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<IEnumerable<ShoppingCartsProducts>> GetAllAsync()
        {
            try
            {
                var connect = await ConnectAsync();
                return await connect.GetAllAsync<ShoppingCartsProducts>();
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
        public async Task<long?> InsertAsync(ShoppingCartsModel model)
        {
            long? id = null;
            try
            {
                var connect = await ConnectAsync();
                id = await connect.InsertAsync(new ShoppingCartsProducts
                {
                    GuidID = Guid.NewGuid(),
                    Quantity = model.Quantity,
                    ProductID = model.ProductID,
                    ShoppingCartID = model.ShoppingCartID,
                    Active = model.Active,
                    Deleted = model.Deleted,
                    Created_at = model.Created_at,
                    Created_by = model.Created_by,
                    Deleted_at = model.Deleted_at,
                    Deleted_by = model.Deleted_by
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

        #endregion
    }
}
