using Loja.Web.Presentation.Models.Registration.Finance.ViewModel;

namespace Loja.Web.Application.Interfaces.Registration.Finance
{
    public interface ICardIssuerApplication
    {
        Task<List<CardIssuerViewModel>> GetAllAsync();
    }
}
