namespace Loja.Web.Presentation.Models.Registration.Finance.ViewModel
{
    public class CardIssuerViewModel
    {
        public int ID { get; set; }

        public Guid GuidID { get; set; }

        public string Name { get; set; }

        public string Length { get; set; }

        public string Prefixes { get; set; }

        public string CheckDigit { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public DateTime Created_at { get; set; }

        public DateTime? Deleted_at { get; set; }
    }
}
