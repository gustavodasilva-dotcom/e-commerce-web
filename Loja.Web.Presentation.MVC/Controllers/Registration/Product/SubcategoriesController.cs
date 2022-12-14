using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product.Model;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Product
{
    public class SubcategoriesController : DefaultController
    {
        #region << PROPERTIES >>
        private readonly ISubcategoryApplication _subcategoryApplication;
        private readonly ICategoryApplication _categoryApplication;
        #endregion

        #region << CONSTRUCTOR >>
        public SubcategoriesController(ISubcategoryApplication subcategoryApplication, ICategoryApplication categoryApplication)
        {
            _subcategoryApplication = subcategoryApplication;
            _categoryApplication = categoryApplication;
        }
        #endregion

        #region << METHODS >>

        #region Views
        public IActionResult Index()
        {
            if (HttpContext.Session.Keys.Any(k => k == SessionUserID))
            {
                return View();
            }
            return Redirect("/Default/Select?statusCode=401");
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
                result.Subcategories = await _subcategoryApplication.GetAllAsync();
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
        public async Task<JsonResult> Register(SubcategoriesModel model)
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

                result.Subcategories = await _subcategoryApplication.InsertAsync(model);
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
        public async Task<JsonResult> Update(SubcategoriesModel model)
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                result.Subcategories = await _subcategoryApplication.UpdateAsync(model);
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
