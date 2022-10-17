using Loja.Web.Application.Interfaces.Registration.Finance;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Finance
{
    public class CardIssuersController : Controller
    {
        #region << PROPERTIES >>
        private readonly ICardIssuerApplication _cardIssuerApplication;
        #endregion

        #region << CONSTRUCTOR >>
        public CardIssuersController(ICardIssuerApplication cardIssuerApplication)
        {
            _cardIssuerApplication = cardIssuerApplication;
        }
        #endregion

        #region << METHODS >>

        #region Get
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                result.CardIssuers = await _cardIssuerApplication.GetAllAsync();
                result.Code = 1;
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
