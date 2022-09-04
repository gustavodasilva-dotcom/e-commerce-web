namespace Loja.Web.Presentation.Models.Registration.Order
{
    public class CardInfoModel
    {
        public string? CardNumber { get; set; }

        public string? NameAtTheCard { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public string? CVV { get; set; }

        public int Quantity { get; set; }

        public int BankingBrandID { get; set; }

        public int? UserID { get; set; } = null;
    }
}
