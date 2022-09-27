using Microsoft.AspNetCore.Mvc;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Contact
{
    public class ContactsController : Controller
    {
        #region Views
        public IActionResult ContactsStructure()
        {
            return PartialView();
        }
        #endregion
    }
}
