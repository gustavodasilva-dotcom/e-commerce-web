using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
using Loja.Web.Presentation.Models.Registration.Address;
using System.Diagnostics;

namespace Loja.Web.Domain.Entities.Registration.Address
{
    [Table("States")]
    public class States : Repository
    {
        #region << PROPERTIES >>
        public int ID { get; private set; }
        public Guid GuidID { get; private set; }
        public string Initials { get; private set; }
        public int CountryID { get; private set; }
        public bool Active { get; private set; }
        public bool Deleted { get; private set; }
        public DateTime Created_at { get; private set; }
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<IEnumerable<States>> GetAllAsync()
        {
            try
            {
                var connect = await ConnectAsync();
                return await connect.GetAllAsync<States>();
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
                id = await connect.InsertAsync(new States
                {
                    GuidID = Guid.NewGuid(),
                    Initials = model.State,
                    CountryID = (int)model.CountryID,
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
