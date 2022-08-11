using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
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
        public string PostalCode { get; private set; }
        public string Name { get; private set; }
        public int Number { get; private set; }
        public string? Comment { get; private set; }
        public int NeighborhoodID { get; private set; }
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

        #endregion
    }
}
