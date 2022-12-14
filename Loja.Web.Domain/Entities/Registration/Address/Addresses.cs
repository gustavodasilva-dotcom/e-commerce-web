using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
using Loja.Web.Presentation.Models.Registration.Address.Model;
using System.Diagnostics;

namespace Loja.Web.Domain.Entities.Registration.Address
{
    [Table("Addresses")]
    public class Addresses : Repository
    {
        #region << PROPERTIES >>
        [Key]
        public int ID { get; private set; }
        public Guid GuidID { get; private set; }
        public string? Number { get; private set; }
        public string? Comment { get; private set; }
        public int? StreetID { get; private set; }
        public bool Active { get; private set; }
        public bool Deleted { get; private set; }
        public DateTime Created_at { get; private set; }
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<IEnumerable<Addresses>> GetAllAsync()
        {
            try
            {
                var connect = await ConnectAsync();
                return await connect.GetAllAsync<Addresses>();
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
        public async Task<long?> InsertAsync(AddressesModel model)
        {
            long? id = null;
            try
            {
                var connect = await ConnectAsync();
                id = await connect.InsertAsync(new Addresses
                {
                    GuidID = Guid.NewGuid(),
                    Number = model.Number.Trim(),
                    Comment = model?.Comment?.Trim(),
                    StreetID = (int)model.StreetID,
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

        #region UpdateAsync
        public async Task<bool> UpdateAsync(AddressesModel model, Addresses address)
        {
            var updated = false;
            try
            {
                var connect = await ConnectAsync();
                updated = await connect.UpdateAsync(new Addresses
                {
                    ID = address.ID,
                    GuidID = address.GuidID,
                    Number = model.Number,
                    Comment = model.Comment,
                    StreetID = model.StreetID,
                    Active = address.Active,
                    Deleted = address.Deleted,
                    Created_at = address.Created_at
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
