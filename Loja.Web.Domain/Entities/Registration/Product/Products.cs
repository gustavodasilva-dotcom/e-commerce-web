using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
using Loja.Web.Presentation.Models.Registration.Product.Model;
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
		public decimal? Price { get; private set; }
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
		public int? Stock { get; private set; }
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

        #region UpdateAsync
        public async Task<bool> UpdateAsync(ProductsModel model)
        {
            var updated = false;
            try
            {
                var connect = await ConnectAsync();
                updated = await connect.UpdateAsync(new Products
                {
                    ID = model.ID.Value,
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
            return updated;
        }
        #endregion

        #region UpdateAsync
        public async Task<bool> UpdateAsync(Products product, int? quantity = null)
        {
            var updated = false;
            try
            {
                var connect = await ConnectAsync();
                updated = await connect.UpdateAsync(new Products
                {
                    ID = product.ID,
                    GuidID = product.GuidID,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    CurrencyID = product.CurrencyID,
                    Discount = product.Discount,
                    SubcategoryID = product.SubcategoryID,
                    ManufacturerID = product.ManufacturerID,
                    WeightMeasurementTypeID = product.WeightMeasurementTypeID,
                    Weight = product.Weight,
                    HeightMeasurementTypeID = product.HeightMeasurementTypeID,
                    Height = product.Height,
                    WidthMeasurementTypeID = product.WidthMeasurementTypeID,
                    Width = product.Width,
                    LengthMeasurementTypeID = product.LengthMeasurementTypeID,
                    Length = product.Length,
                    Stock = quantity != null ? quantity : product.Stock,
                    Active = product.Active,
                    Deleted = product.Deleted,
                    Created_at = product.Created_at,
                    Created_by = product.Created_by,
                    Deleted_at = product.Deleted_at,
                    Deleted_by = product.Deleted_by
                });
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine(e);
#endif
                throw new Exception(e.Message);
            }
            return updated;
        }
        #endregion

        #endregion
    }
}
