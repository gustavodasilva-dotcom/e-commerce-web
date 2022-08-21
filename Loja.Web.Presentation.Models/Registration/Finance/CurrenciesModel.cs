namespace Loja.Web.Presentation.Models.Registration.Finance
{
    public class CurrenciesModel
    {
		public int? ID { get; set; } = null;

		public Guid GuidID { get; set; } = Guid.NewGuid();

		public string Name { get; set; }

		public string Symbol { get; set; }

		public decimal USExchangeRate { get; set; }

		public DateTime LastUpdated { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));

		public bool Active { get; set; } = true;

		public bool Deleted { get; set; } = false;
		
		public DateTime Created_at { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));

		public int? Created_by { get; set; } = null;

		public DateTime? Deleted_at { get; set; } = null;

		public int? Deleted_by { get; set; } = null;
	}
}
