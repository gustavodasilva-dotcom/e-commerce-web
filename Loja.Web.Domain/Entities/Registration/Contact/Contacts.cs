using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
using Loja.Web.Presentation.Models.Registration.Contact.Model;
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
                    Phone = model?.Phone?.Trim(),
                    Cellphone = model?.Cellphone?.Trim(),
                    Email = model?.Email?.Trim(),
                    Website = model?.Website?.Trim(),
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
        public async Task<bool> UpdateAsync(ContactsModel model, Contacts contact)
        {
            var updated = false;
            try
            {
                var connect = await ConnectAsync();
                updated = await connect.UpdateAsync(new Contacts
                {
                    ID = contact.ID,
                    GuidID = contact.GuidID,
                    Phone = model?.Phone?.Trim(),
                    Cellphone = model?.Cellphone?.Trim(),
                    Email = model?.Email?.Trim(),
                    Website = model?.Website?.Trim(),
                    Active = contact.Active,
                    Deleted = contact.Deleted,
                    Created_at = contact.Created_at
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
