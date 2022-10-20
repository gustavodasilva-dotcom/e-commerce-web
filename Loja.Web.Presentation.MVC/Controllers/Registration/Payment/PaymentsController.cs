using Loja.Web.Application.Interfaces.Registration.Payment;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Payment
{
    public class PaymentsController : DefaultController
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

        #region Views
        public IActionResult PaymentsStructure()
        {
            return PartialView();
        }
        #endregion

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

        #region GetUserCards
        public async Task<JsonResult> GetUserCards()
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                if (HttpContext.Session.Keys.Any(k => k == SessionUserID))
                {
                    var createdByGuid = HttpContext.Session.GetString(SessionUserID);
                    var userGuid = Guid.Parse(
                        createdByGuid != null ? createdByGuid :
                        throw new Exception("An error occurred while executing the process. Please, contact the system administrator."));
                    result.CardsInfos = await _paymentApplication.GetUserCardsAsync(userGuid);
                    result.Code = 1;
                }
                else
                {
                    result.RedirectToLogin = true;
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
