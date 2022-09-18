namespace Loja.Web.Presentation.Models.Registration.Product.ViewModel
{
    public class CategoryViewModel
    {
        public int ID { get; set; }

        public Guid GuidID { get; set; }

        public string? Name { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public DateTime Created_at { get; set; }

        public int? Created_by { get; set; }

        public DateTime? Deleted_at { get; set; }

        public int? Deleted_by { get; set; }
    }
}
