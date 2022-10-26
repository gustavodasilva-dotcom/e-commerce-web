namespace Loja.Web.Presentation.Models.Registration.Product.Model
{
    public class ProductsRatingsModel
    {
        public int Rating { get; set; }

        public Guid ProductGuid { get; set; }

        public int ProductID { get; set; }

        public Guid UserGuid { get; set; }

        public int? Created_by { get; set; }
    }
}
