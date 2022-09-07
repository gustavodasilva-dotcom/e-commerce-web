using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product.Model;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Product
{
    public class CategoriesController : Controller
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

        #region Index
        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _categoryApplication.GetAllAsync();
                if (categories.Any())
                {
                    return View(categories.Select(x => new CategoriesModel
                    {
                        ID = x?.ID,
                        GuidID = x.GuidID,
                        Name = x.Name,
                        Active = x.Active,
                        Deleted = x.Deleted,
                        Created_at = x.Created_at,
                        Created_by = x.Created_by,
                        Deleted_at = x.Deleted_at,
                        Deleted_by = x.Deleted_by
                    }));
                }
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        #endregion

        #region Get
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            dynamic result = new ExpandoObject();
            try
            {
                var categories = await _categoryApplication.GetAllAsync();
                if (categories.Any())
                {
                    var categoriesObj = new List<CategoriesModel>();
                    foreach (var category in categories)
                    {
                        categoriesObj.Add(new CategoriesModel
                        {
                            ID = category?.ID,
                            GuidID = category.GuidID,
                            Name = category.Name,
                            Active = category.Active,
                            Deleted = category.Deleted,
                            Created_at = category.Created_at,
                            Created_by = category.Created_by,
                            Deleted_at = category.Deleted_at,
                            Deleted_by = category.Deleted_by
                        });
                    }
                    result.Code = 1;
                    result.Categories = categoriesObj;
                }
                else
                {
                    result.Code = 0;
                    result.Message = "There's no categories registered.";
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

        #region Register
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("Role") == "Employee")
            {
                return View();
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CategoriesModel model)
        {
            try
            {
                if (HttpContext.Session.Keys.Any(k => k == "UserID"))
                {
                    model.Created_by_Guid = Guid.Parse(HttpContext.Session.GetString("UserID"));
                }
                if (await _categoryApplication.InsertAsync(model) != null)
                {
                    ViewBag.SuccessMessage = "Category created successfully.";
                }
                else
                {
                    ViewBag.ErrorMessage = "An error occurred while executing the process. Please, contact the system administrator.";
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
            }
            return View();
        }
        #endregion

        #endregion
    }
}
