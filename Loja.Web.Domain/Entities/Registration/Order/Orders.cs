using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
using Loja.Web.Presentation.Models.Registration.Order;
using System.Diagnostics;

namespace Loja.Web.Domain.Entities.Registration.Order
{
    [Table("Orders")]
    public class Orders : Repository
    {
        #region << PROPERTIES >>
        [Key]
		public int ID { get; private set; }
		public Guid GuidID { get; private set; }
		public decimal? Total { get; private set; }
		public int? UserID { get; private set; }
		public int? PaymentMethodID { get; private set; }
		public int? OrderStatusID { get; private set; }
        public int? DeliveryAddressID { get; private set; }
		public bool Active { get; private set; }
		public bool Deleted { get; private set; }
		public DateTime Created_at { get; private set; }
		public int? Created_by { get; private set; }
		public DateTime? Deleted_at { get; private set; }
		public int? Deleted_by { get; private set; }
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<IEnumerable<Orders>> GetAllAsync()
        {
            try
            {
                var connect = await ConnectAsync();
                return await connect.GetAllAsync<Orders>();
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
        public async Task<long?> InsertAsync(StepOneModel model)
        {
            long? id = null;
            try
            {
                var connect = await ConnectAsync();
                id = await connect.InsertAsync(new Orders
                {
                    GuidID = Guid.NewGuid(),
                    Total = null,
                    UserID = model.UserID,
                    PaymentMethodID = model.PaymentMethodID,
                    OrderStatusID = model.OrderStatusID,
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
        public async Task<bool> UpdateAsync(Orders order, int? deliveryAddressID = null, decimal? total = null)
        {
            var updated = false;
            try
            {
                var connect = await ConnectAsync();
                updated = await connect.UpdateAsync(new Orders
                {
                    ID = order.ID,
                    GuidID = order.GuidID,
                    Total = total != null ? total : order.Total,
                    UserID = order.UserID,
                    PaymentMethodID = order.PaymentMethodID,
                    OrderStatusID = order.OrderStatusID,
                    DeliveryAddressID = deliveryAddressID != null ? deliveryAddressID : order.DeliveryAddressID,
                    Active = order.Active,
                    Deleted = order.Deleted,
                    Created_at = order.Created_at,
                    Created_by = order.Created_by,
                    Deleted_at = order.Deleted_at,
                    Deleted_by = order.Deleted_by
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
