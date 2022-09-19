namespace Loja.Web.Presentation.Models.Registration.Product.Model
{
    public class CategoriesModel
    {
        public Guid GuidID { get; set; } = Guid.Empty;

        public string? Name { get; set; }

        public Guid UserGuid { get; set; } = Guid.Empty;
        public int? Created_by { get; set; } = null;
        
        public DateTime? Deleted_at { get; set; } = null;

        public int? Deleted_by { get; set; } = null;
    }
}
