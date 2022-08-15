namespace Loja.Web.Presentation.Models.Registration.Address
{
    public class AddressesModel
    {
        public int? ID { get; set; } = null;

        public Guid GuidID { get; set; } = Guid.NewGuid();

        public string? PostalCode { get; set; }

        public string? Name { get; set; }
        
        public string? Number { get; set; }
        
        public string? Comment { get; set; }

        public string? Neighborhood { get; set; } = null;
        public int? NeighborhoodID { get; set; } = null;

        public string? City { get; set; } = null;
        public int? CityID { get; set; } = null;

        public string? State { get; set; } = null;
        public int? StateID { get; set; } = null;

        public string? Country { get; set; } = null;
        public int? CountryID { get; set; } = null;

        public bool Active { get; set; } = true;

        public bool Deleted { get; set; } = false;
        
        public DateTime Created_at { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
    }
}
