using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
using Loja.Web.Presentation.Models.Security.Model;
using System.Diagnostics;

namespace Loja.Web.Domain.Entities.Security
{
    [Table("Users")]
    public class Users : Repository
    {
        #region << PROPERTIES >>
        [Key]
        public int ID { get; private set; }
        public Guid GuidID { get; private set; }
        public string? Name { get; private set; }
        public string? Email { get; private set; }
        public string? Login { get; private set; }
        public string? Password { get; private set; }
        public bool Active { get; private set; }
        public bool Deleted { get; private set; }
        public DateTime Created_at { get; private set; }
        public int? Created_by { get; private set; }
        public DateTime? Deleted_at { get; private set; }
        public int? Deleted_by { get; private set; }
        public int? UserRoleID { get; private set; }
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            try
            {
                var connect = await ConnectAsync();
                return await connect.GetAllAsync<Users>();
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
        public async Task<long?> InsertAsync(UserModel model)
        {
            long? id = null;
            try
            {
                var connect = await ConnectAsync();
                id = await connect.InsertAsync(new Users
                {
                    GuidID = Guid.NewGuid(),
                    Name = model.Name,
                    Email = model.Email,
                    Login = model.Login,
                    Password = model.Password,
                    Active = true,
                    Deleted = false,
                    Created_at = DateTime.Now,
                    Deleted_at = null,
                    Created_by = model.Created_by,
                    Deleted_by = null,
                    UserRoleID = model.UserRoleID
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
