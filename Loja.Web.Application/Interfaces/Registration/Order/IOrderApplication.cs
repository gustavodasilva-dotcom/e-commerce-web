using Loja.Web.Domain.Entities.Registration.Order;
using Loja.Web.Presentation.Models.Registration.Order;

namespace Loja.Web.Application.Interfaces.Registration.Order
{
    public interface IOrderApplication
    {
        Task<Orders?> StepOneAsync(StepOneModel model);
    }
}
