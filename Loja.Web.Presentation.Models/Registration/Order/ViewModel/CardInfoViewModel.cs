namespace Loja.Web.Presentation.Models.Registration.Order.ViewModel
{
    public class CardInfoViewModel
    {
        public int ID { get; set; }
        
        public Guid GuidID { get; set; }
        
        public string? CardNumber { get; set; }
        
        public string? NameAtTheCard { get; set; }
        
        public int? Month { get; set; }
        
        public int? Year { get; set; }
        
        public string? CVV { get; set; }
        
        public int? Quantity { get; set; }
        
        public int? UserID { get; set; }
        
        public bool Active { get; set; }
        
        public bool Deleted { get; set; }
        
        public DateTime Created_at { get; set; }
    }
}
