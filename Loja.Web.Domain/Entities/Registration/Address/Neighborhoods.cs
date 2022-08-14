using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
using Loja.Web.Presentation.Models.Registration.Address;
using System.Diagnostics;

namespace Loja.Web.Domain.Entities.Registration.Address
{
    [Table("Neighborhoods")]
    public class Neighborhoods : Repository
    {
        #region << PROPERTIES >>
        public int ID { get; private set; }
        public Guid GuidID { get; private set; }
        public string Name { get; private set; }
        public int CityID { get; private set; }
        public bool Active { get; private set; }
        public bool Deleted { get; private set; }
        public DateTime Created_at { get; private set; }
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<IEnumerable<Neighborhoods>> GetAllAsync()
        {
            try
            {
                var connect = await ConnectAsync();
                return await connect.GetAllAsync<Neighborhoods>();
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
                id = await connect.InsertAsync(new Neighborhoods
                {
                    GuidID = model.GuidID,
                    Name = model.State,
                    CityID = (int)model.CityID,
                    Active = model.Active,
                    Deleted = model.Deleted,
                    Created_at = model.Created_at
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
