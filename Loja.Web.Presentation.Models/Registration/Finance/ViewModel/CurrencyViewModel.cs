namespace Loja.Web.Presentation.Models.Registration.Finance.ViewModel
{
    public class CurrencyViewModel
    {
		public int ID { get; set; }

		public Guid GuidID { get; set; }

		public string Name { get; set; }

		public string Symbol { get; set; }

		public decimal USExchangeRate { get; set; }

		public DateTime LastUpdated { get; set; }

		public bool Active { get; set; }

		public bool Deleted { get; set; }

		public DateTime Created_at { get; set; }

		public int? Created_by { get; set; }

		public DateTime? Deleted_at { get; set; }

		public int? Deleted_by { get; set; }
	}
}
