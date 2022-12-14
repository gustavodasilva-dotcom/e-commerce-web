using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
using Loja.Web.Presentation.Models.Registration.Order.Model;
using System.Diagnostics;

namespace Loja.Web.Domain.Entities.Registration.Payment
{
    [Table("CardsInfos")]
    public class CardsInfos : Repository
    {
        #region << PROPERTIES >>
        [Key]
        public int ID { get; private set; }
        public Guid GuidID { get; private set; }
        public string? CardNumber { get; private set; }
        public string? NameAtTheCard { get; private set; }
        public int? ExpMonth { get; private set; }
        public int? ExpYear { get; private set; }
        public string? CVV { get; private set; }
        public int? Quantity { get; private set; }
        public int? UserID { get; private set; }
        public bool Active { get; private set; }
        public bool Deleted { get; private set; }
        public DateTime Created_at { get; private set; }
        public int? Created_by { get; private set; }
        public DateTime? Deleted_at { get; private set; }
        public int? Deleted_by { get; private set; }
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<IEnumerable<CardsInfos>> GetAllAsync()
        {
            try
            {
                var connect = await ConnectAsync();
                return await connect.GetAllAsync<CardsInfos>();
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
        public async Task<long?> InsertAsync(CardInfoModel model)
        {
            long? id = null;
            try
            {
                var connect = await ConnectAsync();
                id = await connect.InsertAsync(new CardsInfos
                {
                    GuidID = Guid.NewGuid(),
                    CardNumber = model?.CardNumber?.Trim(),
                    NameAtTheCard = model?.NameAtTheCard?.Trim(),
                    ExpMonth = model?.Month,
                    ExpYear = model?.Year,
                    CVV = model?.CVV?.Trim(),
                    Quantity = model?.Quantity,
                    UserID = model?.UserID,
                    Active = true,
                    Deleted = false,
                    Created_at = DateTime.Now,
                    Created_by = model?.UserID,
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
        public async Task<bool> UpdateAsync(CardsInfos cardInfo, int? quantity = null)
        {
            var updated = false;
            try
            {
                var connect = await ConnectAsync();
                updated = await connect.UpdateAsync(new CardsInfos
                {
                    ID = cardInfo.ID,
                    GuidID = cardInfo.GuidID,
                    CardNumber = cardInfo.CardNumber,
                    NameAtTheCard = cardInfo.NameAtTheCard,
                    ExpMonth = cardInfo.ExpMonth,
                    ExpYear = cardInfo.ExpYear,
                    CVV = cardInfo.CVV,
                    Quantity = quantity != null ? quantity : cardInfo.Quantity,
                    UserID = cardInfo.UserID,
                    Active = cardInfo.Active,
                    Deleted = cardInfo.Deleted,
                    Created_at = cardInfo.Created_at,
                    Created_by = cardInfo.Created_by,
                    Deleted_at = cardInfo.Deleted_at,
                    Deleted_by = cardInfo.Deleted_by
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
