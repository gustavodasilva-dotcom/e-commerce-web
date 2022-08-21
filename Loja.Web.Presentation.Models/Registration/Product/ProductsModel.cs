namespace Loja.Web.Presentation.Models.Registration.Product
{
    public class ProductsModel
    {
		public int? ID { get; set; } = null;

		public Guid GuidID { get; set; } = Guid.Empty;

		public string Name { get; set; }
		
		public string Description { get; set; }
		
		public decimal Price { get; set; }
		
		public float Discount { get; set; }
		
		public int SubcategoryID { get; set; }
		
		public int ManufacturerID { get; set; }
		
		public float? Weight { get; set; }
		
		public float? Height { get; set; }
		
		public float? Width { get; set; }
		
		public float? Length { get; set; }
		
		public int Stock { get; set; }

		public bool Active { get; set; } = true;

		public bool Deleted { get; set; } = false;
		
		public DateTime Created_at { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));

		public int? Created_by { get; set; } = null;

		public DateTime? Deleted_at { get; set; } = null;

		public int? Deleted_by { get; set; } = null;
	}
}
