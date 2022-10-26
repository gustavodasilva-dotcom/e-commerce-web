namespace Loja.Web.Presentation.Models.Registration.ShoppingCart.ViewModel
{
    public class ShoppingCartViewModel
    {
        public int ID { get; set; }

        public Guid GuidID { get; set; }

        public List<ShoppingCartProductViewModel>? ShoppingCartProducts { get; set; }
    }
}
