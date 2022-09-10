namespace Loja.Web.Presentation.Models.Registration.Order.ViewModel
{
    public class PaymentMethodViewModel
    {
        public Guid GuidID { get; set; }

        public string? Name { get; set; }

        public bool IsCard { get; set; }
    }
}
