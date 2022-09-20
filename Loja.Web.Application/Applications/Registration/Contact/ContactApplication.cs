using Loja.Web.Application.Interfaces.Registration.Contact;
using Loja.Web.Domain.Entities.Registration.Contact;
using Loja.Web.Presentation.Models.Registration.Contact.Model;
using Loja.Web.Tools.String.Extensions;

namespace Loja.Web.Application.Applications.Registration.Contact
{
    public class ContactApplication : IContactApplication
    {
        #region << PROPERTIES >>
        private readonly Contacts _contacts = new();
        #endregion

        #region << METHODS >>

        #region PUBLIC

        #region GetAllAsync
        public async Task<IEnumerable<Contacts?>> GetAllAsync()
        {
            return await _contacts.GetAllAsync();
        }
        #endregion

        #region InsertAsync
        public async Task<long?> InsertAsync(ContactsModel model)
        {
            Validate(model);
            var contactID = await _contacts.InsertAsync(model);
            if (contactID is null)
            {
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }
            return contactID;
        }
        #endregion

        #endregion

        #region PRIVATE

        #region Validate
        private static void Validate(ContactsModel model)
        {
            if (!string.IsNullOrEmpty(model.Phone) && model.Phone.Length > 12)
            {
                throw new Exception("The phone number cannot be greater than 12.");
            }
            if (!string.IsNullOrEmpty(model.Phone) && model?.Cellphone?.Length > 13)
            {
                throw new Exception("The cellphone number cannot be greater than 12.");
            }
            if (!string.IsNullOrEmpty(model?.Email) && !model.Email.IsEmail())
            {
                throw new Exception("The email informed is not valid.");
            }
        }
        #endregion

        #endregion

        #endregion
    }
}
