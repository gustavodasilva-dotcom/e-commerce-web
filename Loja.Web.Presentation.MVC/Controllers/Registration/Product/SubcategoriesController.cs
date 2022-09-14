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

        #region Index
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString(SessionRole) == "Employee")
            {
                List<SubcategoriesModel>? result = new();
                try
                {
                    var subcategories = await _subcategoryApplication.GetAllAsync();
                    var categories = await _categoryApplication.GetAllAsync();
                    foreach (var subcategory in subcategories)
                    {
                        var sub = new SubcategoriesModel
                        {
                            ID = subcategory?.ID,
                            GuidID = subcategory.GuidID,
                            Name = subcategory.Name,
                            CategoryID = subcategory.CategoryID,
                            Active = subcategory.Active,
                            Deleted = subcategory.Deleted,
                            Created_at = subcategory.Created_at,
                            Created_by = subcategory.Created_by,
                            Deleted_at = subcategory.Deleted_at,
                            Deleted_by = subcategory.Deleted_by
                        };
                        foreach (var category in categories)
                        {
                            if (sub.ID == category?.ID)
                            {
                                sub.Category = new CategoriesModel
                                {
                                    ID = category?.ID,
                                    GuidID = category.GuidID,
                                    Name = category.Name,
                                    Active = category.Active,
                                    Deleted = category.Deleted,
                                    Created_at = category.Created_at,
                                    Deleted_at = category.Deleted_at,
                                    Deleted_by = category.Deleted_by
                                };
                            }
                        }
                        result?.Add(sub);
                    }
                    return View(result);
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }
            return Redirect("/Default/Select?statusCode=401");
        }
        #endregion

        #region Get
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            dynamic result = new ExpandoObject();
            try
            {
                var subcategories = await _subcategoryApplication.GetAllAsync();
                if (subcategories.Any())
                {
                    var subcategoriesObj = new List<SubcategoriesModel>();
                    foreach (var subcategory in subcategories)
                    {
                        subcategoriesObj.Add(new SubcategoriesModel
                        {
                            ID = subcategory?.ID,
                            GuidID = subcategory.GuidID,
                            Name = subcategory.Name,
                            CategoryID = subcategory.CategoryID,
                            Active = subcategory.Active,
                            Deleted = subcategory.Deleted,
                            Created_at = subcategory.Created_at,
                            Created_by = subcategory.Created_by,
                            Deleted_at = subcategory.Deleted_at,
                            Deleted_by = subcategory.Deleted_by
                        });
                    }
                    result.Code = 1;
                    result.Subcategories = subcategoriesObj;
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

        #region Register
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString(SessionRole) == "Employee")
            {
                return View();
            }
            return Redirect("/Default/Select?statusCode=401");
        }

        [HttpPost]
        public async Task<IActionResult> Register(SubcategoriesModel model)
        {
            try
            {
                if (HttpContext.Session.Keys.Any(k => k == SessionUserID))
                {
                    model.Created_by_Guid = Guid.Parse(HttpContext.Session.GetString(SessionUserID));
                }
                var categories = await _categoryApplication.GetAllAsync();
                try
                {
                    model.CategoryID = categories?.FirstOrDefault(x => x?.GuidID == Guid.Parse(Request.Form["categories"]))?.ID;
                }
                catch (Exception)
                {
                    throw new Exception("Please, select a category.");
                }
                if (await _subcategoryApplication.InsertAsync(model) != null)
                {
                    ViewBag.SuccessMessage = "Subcategory created successfully.";
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
