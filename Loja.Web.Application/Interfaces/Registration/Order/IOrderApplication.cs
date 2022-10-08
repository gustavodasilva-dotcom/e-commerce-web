using Loja.Web.Domain.Entities.Registration.Order;
using Loja.Web.Presentation.Models.Registration.Order.Model;
using Loja.Web.Presentation.Models.Registration.Order.ViewModel;

namespace Loja.Web.Application.Interfaces.Registration.Order
{
    public interface IOrderApplication
    {
        Task<OrderViewModel> GetOrderDetailsAsync(Guid orderGuid);
        Task<List<OrderViewModel>> GetByUserAsync(Guid userGuid);
        Task<Orders?> StepOneAsync(StepOneModel model);
        Task<bool> StepTwoAsync(Guid orderGuid, Guid addressGuid, Guid userGuid);
        Task<string> ProcessOrderAsync(Guid orderGuid, string total, bool finishOrder);
    }
}
