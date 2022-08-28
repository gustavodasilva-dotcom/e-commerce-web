using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Domain.Entities.Registration.Finance;
using Loja.Web.Domain.Entities.Registration.Manufacturer;
using Loja.Web.Domain.Entities.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product;
using System.Globalization;

namespace Loja.Web.Application.Applications.Registration.Product
{
    public class ProductApplication : IProductApplication
    {
        #region << PROPERTIES >>
        private readonly Products _products = new();
        private readonly Currencies _currencies = new();
        private readonly Manufacturers _manufacturers = new();
        private readonly Subcategories _subcategories = new();
        private readonly Measurements _measurements = new();
        #endregion

        #region << METHODS >>

        #region PUBLIC

        #region GetAllAsync
        public async Task<IEnumerable<Products?>> GetAllAsync()
        {
            return await _products.GetAllAsync();
        }
        #endregion

        #region ProcessAsync
        public async Task<Products> ProcessAsync(ProductsModel model)
        {
            ValidateModel(ref model);
            await ValidateKeys(model);
            long? productID = null;
            Products? product = null;
            var products = await _products.GetAllAsync();
            if (products.Any(x => x.Name == model?.Name?.Trim() &&
                x.ManufacturerID == model.ManufacturerID &&
                x.SubcategoryID == model.SubcategoryID))
            {
                if (!model.IsEdit)
                {
                    throw new Exception("There's already a product registered with the same name, manufacturer and subcategory.");
                }
            }
            if (model.IsEdit)
            {
                var productToBeUpdated = products?.FirstOrDefault(x => x?.GuidID == model?.GuidID);
                model.ID = productToBeUpdated?.ID;
                model.Created_at = model.Created_at;
                model.Created_by = model.Created_by;
                model.Deleted_at = model.Deleted_at;
                model.Deleted_by = model.Deleted_by;
                if (!await _products.UpdateAsync(model))
                {
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
                }
                productID = model.ID;
            }
            else
            {
                productID = await _products.InsertAsync(model);
            }
            if (productID is null)
            {
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }
            products = await _products.GetAllAsync();
            product = products.FirstOrDefault(x => x.ID == productID);
            if (product is null)
            {
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }
            return product;
        }
        #endregion

        #endregion

        #region PRIVATE

        #region Validate
        private static void ValidateModel(ref ProductsModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new Exception("The product's name cannot be null or empty.");
            }
            if (string.IsNullOrEmpty(model.Description))
            {
                throw new Exception("The product's description cannot be null or empty.");
            }
            model.Description = ValidateDescription(model.@Description);
            if (string.IsNullOrEmpty(model.Price))
            {
                throw new Exception("Please, inform the product's price.");
            }
            if (!decimal.TryParse(model.Price.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal price))
            {
                throw new Exception("The product's price is not numeric.");
            }
            model.PriceConverted = price;
            if (model.PriceConverted <= 0)
            {
                throw new Exception("The product's price cannot be null, equal or less then 0.");
            }
            if (model.Discount < 0 || model.Discount > 100)
            {
                throw new Exception("The product's price cannot be less then 0 or greater then 100.");
            }
            if (!string.IsNullOrEmpty(model.Weight))
            {
                if (!decimal.TryParse(model.Weight, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal weight))
                {
                    throw new Exception("The product's weight is not numeric.");
                }
                model.WeightConverted = weight;
                if (model.WeightConverted < 0)
                {
                    throw new Exception("The product's weight cannot be less then 0.");
                }
            }
            if (!string.IsNullOrEmpty(model.Height))
            {
                if (!decimal.TryParse(model.Height, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal height))
                {
                    throw new Exception("The product's height is not numeric.");
                }
                model.HeightConverted = height;
                if (model.HeightConverted < 0)
                {
                    throw new Exception("The product's height cannot be less then 0.");
                }
            }
            if (!string.IsNullOrEmpty(model.Width))
            {
                if (!decimal.TryParse(model.Width, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal width))
                {
                    throw new Exception("The product's width is not numeric.");
                }
                model.WidthConverted = width;
                if (model.WidthConverted < 0)
                {
                    throw new Exception("The product's width cannot be less then 0.");
                }
            }
            if (!string.IsNullOrEmpty(model.Length))
            {
                if (!decimal.TryParse(model.Length, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal length))
                {
                    throw new Exception("The product's length is not numeric.");
                }
                model.LengthConverted = length;
                if (model.LengthConverted < 0)
                {
                    throw new Exception("The product's length cannot be less then 0.");
                }
            }
            if (model.Stock < 0)
            {
                throw new Exception("The product's stock cannot be less then 0.");
            }
        }

        private static string ValidateDescription(string description)
        {
            return description.Replace("\r\n", "<br>");
        }
        #endregion

        #region ValidateKeys
        private async Task ValidateKeys(ProductsModel model)
        {
            var currencies = await _currencies.GetAllAsync();
            try
            {
                if (model.CurrencyID is null || model.CurrencyID == 0)
                {
                    throw new Exception("Please, select a currency.");
                }
                else
                {
                    model.CurrencyID = currencies?.FirstOrDefault(x => x?.ID == model.CurrencyID)?.ID;
                }
            }
            catch (Exception)
            {
                throw new Exception("Please, select a currency.");
            }
            var manufacturers = await _manufacturers.GetAllAsync();
            try
            {
                if (model.ManufacturerID is null || model.ManufacturerID == 0)
                {
                    throw new Exception("Please, select a manufacturer.");
                }
                else
                {
                    model.ManufacturerID = manufacturers?.FirstOrDefault(x => x?.ID == model.ManufacturerID)?.ID;
                }
            }
            catch (Exception)
            {
                throw new Exception("Please, select a manufacturer.");
            }
            var subcategories = await _subcategories.GetAllAsync();
            try
            {
                if (model.SubcategoryID is null || model.SubcategoryID == 0)
                {
                    throw new Exception("Please, select a subcategory.");
                }
                else
                {
                    model.SubcategoryID = subcategories?.FirstOrDefault(x => x?.ID == model.SubcategoryID)?.ID;
                }
            }
            catch (Exception)
            {
                throw new Exception("Please, select a subcategory.");
            }
            var measurements = await _measurements.GetAllAsync();
            if (model.WeightMeasurementTypeID != null)
            {
                try
                {
                    if (model.WeightMeasurementTypeID == 0)
                    {
                        throw new Exception("Please, select a weight.");
                    }
                    else
                    {
                        model.WeightMeasurementTypeID = measurements?.FirstOrDefault(x => x?.ID == model.WeightMeasurementTypeID)?.ID;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Please, select a weight.");
                }
            }
            if (model.HeightMeasurementTypeID != null)
            {
                try
                {
                    if (model.HeightMeasurementTypeID is null || model.HeightMeasurementTypeID == 0)
                    {
                        throw new Exception("Please, select a height.");
                    }
                    else
                    {
                        model.HeightMeasurementTypeID = measurements?.FirstOrDefault(x => x?.ID == model.HeightMeasurementTypeID)?.ID;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Please, select a height.");
                }
            }
            if (model.WidthMeasurementTypeID != null)
            {
                try
                {
                    if (model.WidthMeasurementTypeID is null || model.WidthMeasurementTypeID == 0)
                    {
                        throw new Exception("Please, select a width.");
                    }
                    else
                    {
                        model.WidthMeasurementTypeID = measurements?.FirstOrDefault(x => x?.ID == model.WidthMeasurementTypeID)?.ID;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Please, select a width.");
                }
            }
            if (model.LengthMeasurementTypeID != null)
            {
                try
                {
                    if (model.LengthMeasurementTypeID is null || model.LengthMeasurementTypeID == 0)
                    {
                        throw new Exception("Please, select a length.");
                    }
                    else
                    {
                        model.LengthMeasurementTypeID = measurements?.FirstOrDefault(x => x?.ID == model.LengthMeasurementTypeID)?.ID;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Please, select a length.");
                }
            }
        }
        #endregion

        #endregion

        #endregion
    }
}
