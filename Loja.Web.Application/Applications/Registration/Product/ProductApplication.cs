using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Domain.Entities.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product;
using System.Globalization;

namespace Loja.Web.Application.Applications.Registration.Product
{
    public class ProductApplication : IProductApplication
    {
        #region << PROPERTIES >>
        private readonly Products _products = new();
        #endregion

        #region << METHODS >>

        #region PUBLIC

        #region GetAllAsync
        public async Task<IEnumerable<Products?>> GetAllAsync()
        {
            return await _products.GetAllAsync();
        }
        #endregion

        #region InsertAsync
        public async Task<Products> InsertAsync(ProductsModel model)
        {
            Validate(ref model);
            Products? product = null;
            var products = await _products.GetAllAsync();
            if (products.Any(x => x.Name == model.Name.Trim() && x.ManufacturerID == model.ManufacturerID && x.SubcategoryID == model.SubcategoryID))
            {
                throw new Exception("There's already a product registered with the same name, manufacturer and subcategory.");
            }
            var productID = await _products.InsertAsync(model);
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
        private static void Validate(ref ProductsModel model)
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
            if (!decimal.TryParse(model.Price, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal price))
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
            if (string.IsNullOrEmpty(model.Weight))
            {
                throw new Exception("Please, inform the product's weight.");
            }
            if (!decimal.TryParse(model.Weight, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal weight))
            {
                throw new Exception("The product's weight is not numeric.");
            }
            model.WeightConverted = weight;
            if (model.WeightConverted < 0)
            {
                throw new Exception("The product's weight cannot be less then 0.");
            }
            if (string.IsNullOrEmpty(model.Height))
            {
                throw new Exception("Please, inform the product's height.");
            }
            if (!decimal.TryParse(model.Height, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal height))
            {
                throw new Exception("The product's height is not numeric.");
            }
            model.HeightConverted = height;
            if (model.HeightConverted < 0)
            {
                throw new Exception("The product's height cannot be less then 0.");
            }
            if (string.IsNullOrEmpty(model.Width))
            {
                throw new Exception("Please, inform the product's width.");
            }
            if (!decimal.TryParse(model.Width, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal width))
            {
                throw new Exception("The product's width is not numeric.");
            }
            model.WidthConverted = width;
            if (model.WidthConverted < 0)
            {
                throw new Exception("The product's width cannot be less then 0.");
            }
            if (string.IsNullOrEmpty(model.Length))
            {
                throw new Exception("Please, inform the product's length.");
            }
            if (!decimal.TryParse(model.Length, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal length))
            {
                throw new Exception("The product's length is not numeric.");
            }
            model.LengthConverted = length;
            if (model.LengthConverted < 0)
            {
                throw new Exception("The product's length cannot be less then 0.");
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

        #endregion

        #endregion
    }
}
