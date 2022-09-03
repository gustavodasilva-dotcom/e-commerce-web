namespace Loja.Web.Presentation.Models.Registration.Order
{
    public class StepOneModel
    {
        public Guid ProductGuid { get; set; }

        public Guid? UserGuid { get; set; } = null;

        public CardInfoModel? CardInfo { get; set; }
    }
}
