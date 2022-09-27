using Loja.Web.Application.Interfaces.Registration.Contact;
using Loja.Web.Domain.Entities.Registration.Contact;
using Loja.Web.Presentation.Models.Registration.Contact.Model;
using Loja.Web.Tools.String.Extensions;
using System.Text.RegularExpressions;

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
        public async Task<long?> SaveAsync(ContactsModel model, int? contactID = null)
        {
            Validate(model);

            long? id = null;

            var contacts = await _contacts.GetAllAsync();

            var contact = contacts.FirstOrDefault(x => x.ID == contactID && x.Active && !x.Deleted);

            if (model.GuidID == Guid.Empty && contact == null)
            {
                id = await _contacts.InsertAsync(model) ??
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }
            else
            {
                if (!await _contacts.UpdateAsync(model, contact))
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");

                id = contact.ID;
            }

            return contactID;
        }
        #endregion

        #endregion

        #region PRIVATE

        #region Validate
        private static void Validate(ContactsModel model)
        {
            if (!string.IsNullOrEmpty(model.Phone) && model.Phone.Length > 12) throw new Exception("The phone number cannot be greater than 12.");
            if (!string.IsNullOrEmpty(model.Phone) && model?.Cellphone?.Length > 13) throw new Exception("The cellphone number cannot be greater than 12.");
            if (!string.IsNullOrEmpty(model?.Email) && !model.Email.IsEmail()) throw new Exception("The email informed is not valid.");

            if (!string.IsNullOrEmpty(model.Phone))
                model.Phone = Regex.Replace(model.Phone, @"[^0-9a-zA-Z]+", "");

            if (!string.IsNullOrEmpty(model.Cellphone))
                model.Cellphone = Regex.Replace(model.Cellphone, @"[^0-9a-zA-Z]+", "");
        }
        #endregion

        #endregion

        #endregion
    }
}
