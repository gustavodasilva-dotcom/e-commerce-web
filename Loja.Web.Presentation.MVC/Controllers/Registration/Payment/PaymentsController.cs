using Loja.Web.Application.Interfaces.Registration.Payment;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Payment
{
    public class PaymentsController : Controller
    {
        #region << PROPERTIES >>
        private readonly IPaymentApplication _paymentApplication;
        #endregion

        #region << CONSTRUCTOR >>
        public PaymentsController(IPaymentApplication paymentApplication)
        {
            _paymentApplication = paymentApplication;
        }
        #endregion

        #region << METHODS >>

        #region GetPaymentTypes
        [HttpGet]
        public async Task<JsonResult> GetPaymentTypes()
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                var paymentTypes = await _paymentApplication.GetAllAsync();
                if (paymentTypes.Any())
                {
                    result.PaymentTypes = paymentTypes.Select(x => new
                    {
                        x?.GuidID,
                        x?.Name,
                        x?.IsCard,
                        x?.Active,
                        x?.Deleted,
                        x?.Created_at
                    });
                    result.Code = 1;
                }
                else
                {
                    result.Message = "There's no payment types registered.";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #endregion
    }
}
