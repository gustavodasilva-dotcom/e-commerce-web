using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product.Model;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Product
{
    public class CategoriesController : DefaultController
    {
        #region << PROPERTIES >>
        private readonly ICategoryApplication _categoryApplication;
        #endregion

        #region << CONSTRUCTOR >>
        public CategoriesController(ICategoryApplication categoryApplication)
        {
            _categoryApplication = categoryApplication;
        }
        #endregion

        #region << METHODS >>

        #region Views
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Get
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                result.Categories = await _categoryApplication.GetAllAsync();
                result.Code = 1;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region Register
        [HttpPost]
        public async Task<JsonResult> Register(CategoriesModel model)
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                if (HttpContext.Session.Keys.Any(k => k == SessionUserID))
                {
                    var createdByGuid = HttpContext.Session.GetString(SessionUserID);
                    model.UserGuid = Guid.Parse(createdByGuid ??
                        throw new Exception("An error occurred while executing the process. Please, contact the system administrator."));
                }

                result.Categories = await _categoryApplication.InsertAsync(model);
                result.Code = 1;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region Update
        [HttpPost]
        public async Task<JsonResult> Update(CategoriesModel model)
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                result.Categories = await _categoryApplication.UpdateAsync(model);
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
