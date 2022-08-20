using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Product
{
    public class SubcategoriesController : Controller
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
            if (HttpContext.Session.GetString("Role") == "Employee")
            {
                try
                {
                    var subcategories = await _subcategoryApplication.GetAllAsync();
                    if (subcategories.Any())
                    {
                        return View(subcategories.Select(x => new SubcategoriesModel
                        {
                            ID = x?.ID,
                            GuidID = x.GuidID,
                            Name = x.Name,
                            CategoryID = x.CategoryID,
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
            return Unauthorized();
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
        public async Task<IActionResult> Register(SubcategoriesModel model)
        {
            try
            {
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
