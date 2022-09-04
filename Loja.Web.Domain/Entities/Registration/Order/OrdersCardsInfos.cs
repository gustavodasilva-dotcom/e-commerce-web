using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
using System.Diagnostics;

namespace Loja.Web.Domain.Entities.Registration.Order
{
    [Table("OrdersCardsInfos")]
    public class OrdersCardsInfos : Repository
    {
        #region << METHODS >>
        [Key]
        public int ID { get; private set; }
        public int OrderID { get; private set; }
        public int CardInfoID { get; private set; }
        public bool Active { get; private set; }
        public bool Deleted { get; private set; }
        public DateTime Created_at { get; private set; }
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<IEnumerable<OrdersCardsInfos>> GetAllAsync()
        {
            try
            {
                var connect = await ConnectAsync();
                return await connect.GetAllAsync<OrdersCardsInfos>();
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
        public async Task<long?> InsertAsync(int orderID, int cardInfoID)
        {
            long? id = null;
            try
            {
                var connect = await ConnectAsync();
                id = await connect.InsertAsync(new OrdersCardsInfos
                {
                    OrderID = orderID,
                    CardInfoID = cardInfoID,
                    Active = true,
                    Deleted = false,
                    Created_at = DateTime.Now
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
