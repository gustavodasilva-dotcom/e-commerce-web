using Dapper.Contrib.Extensions;
using Loja.Web.Domain.Entities.Registration.ShoppingCart;
using Loja.Web.Infra.Data.Repositories;
using Loja.Web.Presentation.Models.Registration.Order.Model;
using System.Diagnostics;

namespace Loja.Web.Domain.Entities.Registration.Order
{
    [Table("OrdersProducts")]
    public class OrdersProducts : Repository
    {
        #region << PROPERTIES >>
        [Key]
		public int ID { get; private set; }
		public Guid GuidID { get; private set; }
		public int Quantity { get; private set; }
		public decimal Amount { get; private set; }
		public decimal Unitary { get; private set; }
		public int OrderID { get; private set; }
		public int ProductID { get; private set; }
		public bool Active { get; private set; }
		public bool Deleted { get; private set; }
		public DateTime Created_at { get; private set; }
		public int? Created_by { get; private set; }
		public DateTime? Deleted_at { get; private set; }
		public int? Deleted_by { get; private set; }
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<IEnumerable<OrdersProducts>> GetAllAsync()
        {
            try
            {
                var connect = await ConnectAsync();
                return await connect.GetAllAsync<OrdersProducts>();
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
        public async Task<long?> InsertAsync(ShoppingCartsProducts product, StepOneModel model, int orderID, decimal price)
        {
            long? id = null;
            try
            {
                var connect = await ConnectAsync();
                id = await connect.InsertAsync(new OrdersProducts
                {
                    GuidID = Guid.NewGuid(),
                    Quantity = product.Quantity,
                    Amount = price * product.Quantity,
                    Unitary = price,
                    OrderID = orderID,
                    ProductID = product.ProductID,
                    Active = true,
                    Deleted = false,
                    Created_at = DateTime.Now,
                    Created_by = model.UserID,
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
        public async Task<bool> UpdateAsync(OrdersProducts orderProduct, int quantity)
        {
            var updated = false;
            try
            {
                var connect = await ConnectAsync();
                updated = await connect.UpdateAsync(new OrdersProducts
                {
                    ID = orderProduct.ID,
                    GuidID = orderProduct.GuidID,
                    Quantity = quantity,
                    Amount = orderProduct.Unitary * quantity,
                    Unitary = orderProduct.Unitary,
                    OrderID = orderProduct.OrderID,
                    ProductID = orderProduct.ProductID,
                    Active = orderProduct.Active,
                    Deleted = orderProduct.Deleted,
                    Created_at = orderProduct.Created_at,
                    Created_by = orderProduct.Created_by,
                    Deleted_at = orderProduct.Deleted_at,
                    Deleted_by = orderProduct.Deleted_by
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
