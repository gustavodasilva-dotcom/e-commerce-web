using Dapper.Contrib.Extensions;
using Loja.Web.Infra.Data.Repositories;
using Loja.Web.Presentation.Models.Registration.Manufacturer.Model;
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
        public string? Name { get; private set; }
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
                    GuidID = Guid.NewGuid(),
                    Name = model?.Name?.Trim(),
                    BrazilianCompany = model.BrazilianCompany,
                    CAGE = model?.CAGE?.Trim(),
                    NCAGE = model?.NCAGE?.Trim(),
                    SEC = model?.SEC?.Trim(),
                    FederalTaxpayerRegistrationNumber = model?.FederalTaxpayerRegistrationNumber?.Trim(),
                    StateTaxpayerRegistrationNumber = model?.StateTaxpayerRegistrationNumber?.Trim(),
                    ContactID = (int)model.Contacts.ID,
                    AddressID = (int)model.Addresses.ID,
                    Active = true,
                    Deleted = false,
                    Created_at = DateTime.Now,
                    Created_by = model.Created_by,
                    Deleted_at = null,
                    Deleted_by = null
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
        public async Task<bool> UpdateAsync(ManufacturersModel model, Manufacturers manufacturer)
        {
            var updated = false;
            try
            {
                var connect = await ConnectAsync();
                updated = await connect.UpdateAsync(new Manufacturers
                {
                    ID = manufacturer.ID,
                    GuidID = manufacturer.GuidID,
                    Name = model?.Name?.Trim(),
                    BrazilianCompany = model.BrazilianCompany,
                    CAGE = model?.CAGE?.Trim(),
                    NCAGE = model?.NCAGE?.Trim(),
                    SEC = model?.SEC?.Trim(),
                    FederalTaxpayerRegistrationNumber = model?.FederalTaxpayerRegistrationNumber?.Trim(),
                    StateTaxpayerRegistrationNumber = model?.StateTaxpayerRegistrationNumber?.Trim(),
                    ContactID = (int)model.Contacts.ID,
                    AddressID = (int)model.Addresses.ID,
                    Active = manufacturer.Active,
                    Deleted = manufacturer.Deleted,
                    Created_at = manufacturer.Created_at,
                    Created_by = manufacturer.Created_by,
                    Deleted_at = manufacturer.Deleted_at,
                    Deleted_by = manufacturer.Deleted_by
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
