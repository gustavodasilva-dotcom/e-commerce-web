namespace Loja.Web.Presentation.Models.Registration.Address.ViewModel
{
    public class AddressesViewModel
    {
        public int ID { get; set; }

        public Guid GuidID { get; set; }

        public string? Number { get; set; }

        public string? Comment { get; set; }

        public StreetsViewModel? Street { get; set; }

        public NeighborhoodsViewModel? Neighborhood { get; set; }

        public CitiesViewModel? City { get; set; }

        public StatesViewModel? State { get; set; }
    
        public CountriesViewModel? Country { get; set; }
    }
}
