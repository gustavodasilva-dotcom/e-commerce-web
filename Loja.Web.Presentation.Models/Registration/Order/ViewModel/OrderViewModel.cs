using Loja.Web.Presentation.Models.Registration.Product.ViewModel;

namespace Loja.Web.Presentation.Models.Registration.Order.ViewModel
{
    public class OrderViewModel
    {
        public Guid GuidID { get; set; }

        public string? Tracking { get; set; }

        public decimal? Total { get; set; }

        public PaymentMethodViewModel? PaymentMethod { get; set; }

        public CardInfoViewModel? CardInfo { get; set; }

        public OrderStatusViewModel? OrderStatus { get; set; }

        public List<ProductViewModel>? Products { get; set; }

        public bool Active { get; set; }
        
        public bool Deleted { get; set; }
        
        public DateTime Created_at { get; set; }
    }
}
