namespace Loja.Web.Presentation.Models.Registration.ShoppingCart.ViewModel
{
    public class ShoppingCartProductViewModel
    {
        public int? ID { get; set; }

        public Guid? GuidID { get; set; }

        public Guid? ProductGuid { get; set; }

        public int ProductID { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int Quantity { get; set; }

        public int? ShoppingCartID { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public DateTime Created_at { get; set; }
    }
}
