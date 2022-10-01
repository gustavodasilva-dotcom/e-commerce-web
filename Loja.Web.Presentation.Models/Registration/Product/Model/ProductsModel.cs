namespace Loja.Web.Presentation.Models.Registration.Product.Model
{
    public class ProductsModel
    {
        public int? ID { get; set; }

        public Guid GuidID { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? CurrencyID { get; set; }
        public Guid CurrencyGuid { get; set; }

        public int Discount { get; set; } = 0;
        
        public int? SubcategoryID { get; set; }
        public Guid SubcategoryGuid { get; set; }

        public int? ManufacturerID { get; set; }
        public Guid ManufacturerGuid { get; set; }

        public decimal? Weight { get; set; }
        public int? WeightMeasurementTypeID { get; set; }
        public Guid WeightGuid { get; set; }

        public decimal? Height { get; set; }
        public int? HeightMeasurementTypeID { get; set; }
        public Guid HeightGuid { get; set; }

        public decimal? Width { get; set; }
        public int? WidthMeasurementTypeID { get; set; }
        public Guid WidthGuid { get; set; }

        public decimal? Length { get; set; }
        public int? LengthMeasurementTypeID { get; set; }
        public Guid LengthGuid { get; set; }

        public int? Stock { get; set; }

        public DateTime? Created_at { get; set; }

        public Guid UserGuid { get; set; }
        public int? Created_by { get; set; } = null;

        public DateTime? Deleted_at { get; set; }

        public int? Deleted_by { get; set; }
    }
}
