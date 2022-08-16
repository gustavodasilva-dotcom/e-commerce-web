using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
using Loja.Web.Presentation.Models.Registration.Address;
using System.Diagnostics;

namespace Loja.Web.Domain.Entities.Registration.Address
{
    [Table("Streets")]
    public class Streets : Repository
    {
        #region << PROPRIEDADES >>
        [Key]
        public int ID { get; private set; }
	    public Guid GuidID { get; private set; }
        public string PostalCode { get; private set; }
        public string Name { get; private set; }
        public int NeighborhoodID { get; private set; }
        public bool Active { get; private set; }
        public bool Deleted { get; private set; }
        public DateTime Created_at { get; private set; }
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<IEnumerable<Streets>> GetAllAsync()
        {
            try
            {
                var connect = await ConnectAsync();
                return await connect.GetAllAsync<Streets>();
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
                id = await connect.InsertAsync(new Streets
                {
                    GuidID = Guid.NewGuid(),
                    PostalCode = model.PostalCode,
                    Name = model.Name,
                    NeighborhoodID = (int)model.NeighborhoodID,
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
