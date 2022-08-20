using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
using Loja.Web.Presentation.Models.Registration.Manufacturer;
using System.Diagnostics;

namespace Loja.Web.Domain.Entities.Registration.Manufacturer
{
    [Table("Manufacturers")]
    public class Manufacturers : Repository
    {
        #region << PROPERTIES >>
        [Key]
        public int ID { get; private set; }
        public Guid GuidID { get; private set; }
        public string Name { get; private set; }
        public bool BrazilianCompany { get; private set; }
        public string? CAGE { get; private set; }
        public string? NCAGE { get; private set; }
        public string? SEC { get; private set; }
        public string? FederalTaxpayerRegistrationNumber { get; private set; }
        public string? StateTaxpayerRegistrationNumber { get; private set; }
        public int ContactID { get; private set; }
        public int AddressID { get; private set; }
        public bool Active { get; private set; }
        public bool Deleted { get; private set; }
        public DateTime Created_at { get; private set; }
        public int? Created_by { get; private set; }
        public DateTime? Deleted_at { get; private set; }
        public int? Deleted_by { get; private set; }
        #endregion

        #region << METHODS >>

        #region GetAllAsync
        public async Task<IEnumerable<Manufacturers>> GetAllAsync()
        {
            try
            {
                var connect = await ConnectAsync();
                return await connect.GetAllAsync<Manufacturers>();
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
        public async Task<long?> InsertAsync(ManufacturersModel model)
        {
            long? id = null;
            try
            {
                var connect = await ConnectAsync();
                id = await connect.InsertAsync(new Manufacturers
                {
                    GuidID = model.GuidID,
                    Name = model.Name,
                    BrazilianCompany = model.BrazilianCompany,
                    CAGE = model.CAGE,
                    NCAGE = model.NCAGE,
                    SEC = model.SEC,
                    FederalTaxpayerRegistrationNumber = model.FederalTaxpayerRegistrationNumber,
                    StateTaxpayerRegistrationNumber = model.StateTaxpayerRegistrationNumber,
                    ContactID = (int)model.Contacts.ID,
                    AddressID = (int)model.Addresses.ID,
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
