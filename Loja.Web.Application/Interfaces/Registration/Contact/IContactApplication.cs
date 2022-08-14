using Loja.Web.Presentation.Models.Registration.Contact;

namespace Loja.Web.Application.Interfaces.Registration.Contact
{
    public interface IContactApplication
    {
        Task<long?> InsertAsync(ContactsModel model);
    }
}
