namespace Loja.Web.Presentation.Models.Registration.Order
{
    public class CardInfoModel
    {
        public Guid GuidID { get; set; }

        public string? CardNumber { get; set; }

        public string? NameAtTheCard { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int CVV { get; set; }
    }
}
