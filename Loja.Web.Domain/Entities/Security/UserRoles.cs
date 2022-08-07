using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
using System.Diagnostics;

namespace Loja.Web.Domain.Entities.Security
{
    [Table("UserRoles")]
    public class UserRoles : Repository
    {
        #region << PROPERTIES >>
        [Key]
        public Guid ID { get; private set; }
	    public string Name { get; private set; }
	    public bool Active { get; private set; }
	    public bool Deleted { get; private set; }
        #endregion

        #region << METHODS >>

        #region GetAll
        public async Task<IEnumerable<UserRoles>> GetAll()
        {
            try
            {
                var connect = await ConnectAsync();
                return await connect.GetAllAsync<UserRoles>();
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
