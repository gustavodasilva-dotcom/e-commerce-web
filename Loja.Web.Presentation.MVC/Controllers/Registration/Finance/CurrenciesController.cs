using Loja.Web.Application.Interfaces.Registration.Finance;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Finance
{
    public class CurrenciesController : DefaultController
    {
        #region << PROPERTIES >>
        private readonly ICurrencyApplication _currencyApplication;
        #endregion

        #region << CONSTRUCTOR >>
        public CurrenciesController(ICurrencyApplication currencyApplication)
        {
            _currencyApplication = currencyApplication;
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
                result.Currencies = await _currencyApplication.GetAllAsync();
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
