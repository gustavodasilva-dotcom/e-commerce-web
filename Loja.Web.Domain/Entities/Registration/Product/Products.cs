using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
using Loja.Web.Presentation.Models.Registration.Product;
using System.Diagnostics;

namespace Loja.Web.Domain.Entities.Registration.Product
{
    [Table("Products")]
    public class Products : Repository
    {
        #region << PROPERTIES >>
        [Key]
		public int ID { get; private set; }
		public Guid GuidID { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }
		public decimal Price { get; private set; }
        public int CurrencyID { get; private set; }
        public int Discount { get; private set; }
		public int SubcategoryID { get; private set; }
		public int ManufacturerID { get; private set; }
        public int? WeightMeasurementTypeID { get; private set; }
		public decimal? Weight { get; private set; }
        public int? HeightMeasurementTypeID { get; private set; }
		public decimal? Height { get; private set; }
        public int? WidthMeasurementTypeID { get;  private set; }
		public decimal? Width { get; private set; }
        public int? LengthMeasurementTypeID { get; private set; }
        public decimal? Length { get; private set; }
		public int Stock { get; private set; }
		public bool Active { get; private set; }
		public bool Deleted { get; private set; }
		public DateTime Created_at { get; private set; }
		public int? Created_by { get; private set; }
		public DateTime? Deleted_at { get; private set; }
		public int? Deleted_by { get; private set; }
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<IEnumerable<Products>> GetAllAsync()
        {
            try
            {
                var connect = await ConnectAsync();
                return await connect.GetAllAsync<Products>();
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine(e);
#endif
                throw new Exception(e.Message);
            }
        }
        #endregion

        #region InsertAsync
        public async Task<long?> InsertAsync(ProductsModel model)
        {
            long? id = null;
            try
            {
                var connect = await ConnectAsync();
                id = await connect.InsertAsync(new Products
                {
                    GuidID = model.GuidID,
                    Name = model.Name.Trim(),
                    Description = model.Description.Trim(),
                    Price = model.PriceConverted.Value,
                    CurrencyID = model.CurrencyID.Value,
                    Discount = model.Discount,
                    SubcategoryID = model.SubcategoryID.Value,
                    ManufacturerID = model.ManufacturerID.Value,
                    WeightMeasurementTypeID = model.WeightMeasurementTypeID,
                    Weight = model.WeightConverted,
                    HeightMeasurementTypeID = model.HeightMeasurementTypeID,
                    Height = model.HeightConverted,
                    WidthMeasurementTypeID = model.WidthMeasurementTypeID,
                    Width = model.WidthConverted,
                    LengthMeasurementTypeID = model.LengthMeasurementTypeID,
                    Length = model.LengthConverted,
                    Stock = model.Stock,
                    Active = model.Active,
                    Deleted = model.Deleted,
                    Created_at = model.Created_at,
                    Created_by = model.Created_by,
                    Deleted_at = model.Deleted_at,
                    Deleted_by = model.Deleted_by
                });
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine(e);
#endif
                throw new Exception(e.Message);
            }
            return id;
        }
        #endregion

        #endregion
    }
}
