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
            if (HttpContext.Session.Keys.Any(k => k == "UserID"))
            {
                return View();
            }
            return Unauthorized();
        }
        #endregion

        #region AddressSelect
        public IActionResult AddressSelect()
        {
            //if (HttpContext.Session.Keys.Any(k => k == "UserID"))
            //{
                return View();
            //}
            //return Unauthorized();
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
                if (HttpContext.Session.Keys.Any(k => k == "UserID"))
                {
                    var createdByGuid = HttpContext.Session.GetString("UserID");
                    model.UserGuid = Guid.Parse(
                        createdByGuid != null ? createdByGuid :
                        throw new Exception("An error occurred while executing the process. Please, contact the system administrator."));
                    var order = await _orderApplication.StepOneAsync(model);
                    if (order is not null)
                    {
                        result.Code = 1;
                        result.Order = new
                        {
                            order.GuidID,
                            order.Total,
                            order.UserID,
                            order.PaymentMethodID,
                            order.OrderStatusID,
                            order.Active,
                            order.Deleted,
                            order.Created_at
                        };
                    }
                    else
                    {
                        throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
                    }
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
