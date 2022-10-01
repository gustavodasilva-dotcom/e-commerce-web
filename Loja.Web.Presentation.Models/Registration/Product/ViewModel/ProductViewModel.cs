using Loja.Web.Presentation.Models.Registration.Manufacturer.ViewModel;

namespace Loja.Web.Presentation.Models.Registration.Product.ViewModel
{
    public class ProductViewModel
    {
        public Guid GuidID { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int Discount { get; set; }

        public SubcategoryViewModel? Subcategory { get; set; }

        public ManufacturerViewModel? Manufacturer { get; set; } 

        public MeasurementViewModel? Weight { get; set; }

        public MeasurementViewModel? Height { get; set; }

        public MeasurementViewModel? Width { get; set; }

        public MeasurementViewModel? Length { get; set; }
        
        public int Quantity { get; set; }

        public int? Stock { get; set; }
        
        public bool Active { get; set; }
        
        public bool Deleted { get; set; }
        
        public DateTime Created_at { get; set; }
    }
}
