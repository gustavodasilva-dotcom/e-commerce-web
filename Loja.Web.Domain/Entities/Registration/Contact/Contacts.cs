using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
using Loja.Web.Presentation.Models.Registration.Contact;
using System.Diagnostics;

namespace Loja.Web.Domain.Entities.Registration.Contact
{
	[Table("Contacts")]
	public class Contacts : Repository
	{
        #region << PROPRIEDADES >>
        [Key]
		public int ID { get; private set; }
		public Guid GuidID { get; private set; }
		public string? Phone { get; private set; }
		public string? Cellphone { get; private set; }
		public string? Email { get; private set; }
		public string? Website { get; private set; }
		public bool Active { get; private set; }
		public bool Deleted { get; private set; }
		public DateTime Created_at { get; private set; }
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<IEnumerable<Contacts>> GetAllAsync()
        {
            try
            {
                var connect = await ConnectAsync();
                return await connect.GetAllAsync<Contacts>();
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
        public async Task<long?> InsertAsync(ContactsModel model)
        {
            long? id = null;
            try
            {
                var connect = await ConnectAsync();
                id = await connect.InsertAsync(new Contacts
                {
                    GuidID = Guid.NewGuid(),
                    Phone = model.Phone,
                    Cellphone = model.Cellphone,
                    Email = model.Email,
                    Website = model.Website,
                    Active = true,
                    Deleted = false,
                    Created_at = DateTime.Now
                }); ;
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
