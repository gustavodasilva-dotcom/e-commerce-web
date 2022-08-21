using Loja.Web.Application.Interfaces.Registration.Finance;
using Loja.Web.Presentation.Models.Registration.Finance;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Finance
{
    public class CurrenciesController : Controller
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
            try
            {
                var currencies = await _currencyApplication.GetAllAsync();
                if (currencies.Any())
                {
                    var currenciesObj = new List<CurrenciesModel>();
                    foreach (var currency in currencies)
                    {
                        currenciesObj.Add(new CurrenciesModel
                        {
                            ID = currency?.ID,
                            GuidID = currency.GuidID,
                            Name = currency.Name,
                            Symbol = currency.Symbol,
                            USExchangeRate = currency.USExchangeRate,
                            LastUpdated = currency.LastUpdated,
                            Active = currency.Active,
                            Deleted = currency.Deleted,
                            Created_at = currency.Created_at,
                            Created_by = currency.Created_by,
                            Deleted_at = currency.Deleted_at,
                            Deleted_by = currency.Deleted_by
                        });
                    }
                    result.Code = 1;
                    result.Currencies = currenciesObj.OrderBy(x => x.Name);
                }
                else
                {
                    result.Code = 0;
                    result.Message = "There's no subcategories registered.";
                }
            }
            catch (Exception e)
            {
                result.Code = 0;
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #endregion
    }
}
