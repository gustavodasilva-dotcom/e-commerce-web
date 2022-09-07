namespace Loja.Web.Presentation.Models.Registration.Product.ViewModel
{
    public class ProductViewModel
    {
        public Guid GuidID { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int Discount { get; set; }

        public decimal? Weight { get; set; }

        public decimal? Height { get; set; }

        public decimal? Width { get; set; }

        public decimal? Length { get; set; }
        
        public int Stock { get; set; }
        
        public bool Active { get; set; }
        
        public bool Deleted { get; set; }
        
        public DateTime Created_at { get; set; }
    }
}
