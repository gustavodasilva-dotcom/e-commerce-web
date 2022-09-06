namespace Loja.Web.Presentation.Models.Registration.Address.ViewModel
{
    public class StreetsViewModel
    {
        public int ID { get; set; }

        public Guid GuidID { get; set; }

        public string? PostalCode { get; set; }

        public string? Name { get; set; }

        public int NeighborhoodID { get; set; }
    }
}
