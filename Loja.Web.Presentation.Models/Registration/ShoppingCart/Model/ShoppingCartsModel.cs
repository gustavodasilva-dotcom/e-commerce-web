namespace Loja.Web.Presentation.Models.Registration.ShoppingCart.Model
{
    public class ShoppingCartsModel
    {
        public int Quantity { get; set; }

        public int ProductID { get; set; }

        public int? ShoppingCartID { get; set; } = null;

        public int? UserID { get; set; } = null;
        public Guid? UserGuid { get; set; } = null;

        public bool Active { get; set; } = true;

        public bool Deleted { get; set; } = false;

        public DateTime Created_at { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));

        public int? Created_by { get; set; } = null;
        public Guid? Created_by_Guid { get; set; } = null;

        public DateTime? Deleted_at { get; set; } = null;

        public int? Deleted_by { get; set; } = null;
    }
}
