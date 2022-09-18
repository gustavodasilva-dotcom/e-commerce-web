namespace Loja.Web.Presentation.Models.Registration.Product.Model
{
    public class SubcategoriesModel
    {
        public string? Name { get; set; }

        public Guid CategoryGuid { get; set; }
        public int? CategoryID { get; set; } = null;

        public int? Created_by { get; set; } = null;
        public Guid UserGuid { get; set; } = Guid.Empty;

        public DateTime? Deleted_at { get; set; } = null;

        public int? Deleted_by { get; set; } = null;
    }
}
