using Loja.Web.Application.Interfaces.Registration.Order;
using Loja.Web.Presentation.Models.Registration.Order;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Order
{
    public class OrdersController : Controller
    {
        #region << PROPERTIES >>
        private readonly IOrderApplication _orderApplication;
        #endregion

        #region << CONSTRUCTOR >>
        public OrdersController(IOrderApplication orderApplication)
        {
            _orderApplication = orderApplication;
        }
        #endregion

        #region << METHODS >>

        #region SelectPay
        public IActionResult SelectPay()
        {
            return View();
        }
        #endregion

        #region StepOne
        [HttpPost]
        public async Task<JsonResult> StepOne(StepOneModel model)
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {

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
