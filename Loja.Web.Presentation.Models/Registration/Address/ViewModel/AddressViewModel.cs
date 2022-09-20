namespace Loja.Web.Presentation.Models.Registration.Address.ViewModel
{
    public class AddressViewModel
    {
        public int ID { get; set; }

        public Guid GuidID { get; set; }

        public string? Number { get; set; }

        public string? Comment { get; set; }

        public StreetViewModel? Street { get; set; }

        public NeighborhoodViewModel? Neighborhood { get; set; }

        public CityViewModel? City { get; set; }

        public StateViewModel? State { get; set; }
    
        public CountryViewModel? Country { get; set; }
    }
}
