namespace Loja.Web.Presentation.Models.Registration.Order.Model
{
    public class StepOneModel
    {
        #region Front-end filled
        public Guid OrderGuid { get; set; }
        public List<Guid> ProductGuid { get; set; }
        public List<int> ProductQuantity { get; set; }
        public bool IsCard { get; set; }
        public Guid PaymentGuid { get; set; }
        public CardInfoModel CardInfo { get; set; }
        #endregion

        #region Back-end filled
        public Guid? UserGuid { get; set; } = null;
        public int? UserID { get; set; } = null;
        public long? OrderID { get; set; } = null;
        public int? OrderStatusID { get; set; } = null;
        public int? PaymentMethodID { get; set; } = null;
        #endregion
    }
}
