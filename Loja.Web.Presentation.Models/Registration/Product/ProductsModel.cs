﻿namespace Loja.Web.Presentation.Models.Registration.Product
{
    public class ProductsModel
    {
		public int? ID { get; set; } = null;

		public Guid GuidID { get; set; } = Guid.NewGuid();

		public string Name { get; set; }
		
		public string Description { get; set; }
		
		public string Price { get; set; }
		public decimal? PriceConverted { get; set; } = null;

		public int? CurrencyID { get; set; }

		public int Discount { get; set; } = 0;
		
		public int? SubcategoryID { get; set; }
		
		public int? ManufacturerID { get; set; }
		
		public string Weight { get; set; }
		public decimal? WeightConverted { get; set; } = null;

		public string Height { get; set; }
		public decimal? HeightConverted { get; set; } = null;

		public string Width { get; set; }
		public decimal? WidthConverted { get; set; } = null;

		public string Length { get; set; }
		public decimal? LengthConverted { get; set; } = null;

		public int Stock { get; set; }

		public bool Active { get; set; } = true;

		public bool Deleted { get; set; } = false;
		
		public DateTime Created_at { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));

		public int? Created_by { get; set; } = null;
		public Guid? Created_by_Guid { get; set; } = null;

		public DateTime? Deleted_at { get; set; } = null;

		public int? Deleted_by { get; set; } = null;
	}
}