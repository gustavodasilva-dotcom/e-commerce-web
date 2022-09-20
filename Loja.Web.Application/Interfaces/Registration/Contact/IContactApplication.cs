using Loja.Web.Domain.Entities.Registration.Contact;
using Loja.Web.Presentation.Models.Registration.Contact.Model;

namespace Loja.Web.Application.Interfaces.Registration.Contact
{
    public interface IContactApplication
    {
        Task<IEnumerable<Contacts?>> GetAllAsync();
        Task<long?> InsertAsync(ContactsModel model);
    }
}
