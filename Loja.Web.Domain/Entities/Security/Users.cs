﻿using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
using System.Diagnostics;

namespace Loja.Web.Domain.Entities.Security
{
    [Table("Users")]
    public class Users : Repository
    {
        #region << PROPERTIES >>
        public Guid ID { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public bool Active { get; private set; }
        public bool Deleted { get; private set; }
        public DateTime Created_at { get; private set; }
        public Guid? Created_by { get; private set; }
        public DateTime? Deleted_at { get; private set; }
        public Guid? Deleted_by { get; private set; }
        public Guid? UserRoleID { get; private set; }
        #endregion

        #region << METHODS >>

        #region GetAll
        public async Task<IEnumerable<Users>> GetAll()
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

        #endregion
    }
}
