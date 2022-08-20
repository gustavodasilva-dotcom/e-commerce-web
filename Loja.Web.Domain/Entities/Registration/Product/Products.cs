using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
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
		public float Discount { get; private set; }
		public int SubcategoryID { get; private set; }
		public int ManufacturerID { get; private set; }
		public float? Weight { get; private set; }
		public float? Height { get; private set; }
		public float? Width { get; private set; }
		public float? Length { get; private set; }
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

        #endregion
    }
}
