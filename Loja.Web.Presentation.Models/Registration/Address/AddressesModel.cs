namespace Loja.Web.Presentation.Models.Registration.Address
{
    public class AddressesModel
    {
        public int? ID { get; set; } = null;

        public Guid GuidID { get; set; } = Guid.NewGuid();

        public string? PostalCode { get; set; }

        public string? Name { get; set; }
        
        public int Number { get; set; }
        
        public string? Comment { get; set; }

        public int NeighborhoodID { get; set; } = 0;

        public bool Active { get; set; } = true;

        public bool Deleted { get; set; } = false;
        
        public DateTime Created_at { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
    }
}
