using Loja.Web.Application.Interfaces.Registration.Finance;
using Loja.Web.Application.Interfaces.Registration.Image;
using Loja.Web.Application.Interfaces.Registration.Manufacturer;
using Loja.Web.Application.Interfaces.Registration.Product;
using Loja.Web.Domain.Entities.Registration.Finance;
using Loja.Web.Domain.Entities.Registration.Manufacturer;
using Loja.Web.Domain.Entities.Registration.Order;
using Loja.Web.Domain.Entities.Registration.Product;
using Loja.Web.Domain.Entities.Security;
using Loja.Web.Presentation.Models.Registration.Product.Model;
using Loja.Web.Presentation.Models.Registration.Product.ViewModel;

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
        private readonly Users _users = new();
        private readonly ProductsRatings _productsRatings = new();
        private readonly OrdersProducts _ordersProducts = new();

        private readonly IImageApplication _imageApplication;
        private readonly ICurrencyApplication _currencyApplication;
        private readonly ISubcategoryApplication _subcategoryApplication;
        private readonly IManufacturerApplication _manufacturerApplication;
        private readonly IMeasurementApplication _measurementApplication;
        #endregion

        #region << CONSTRUCTOR >>
        public ProductApplication(IImageApplication imageApplication,
                                  ICurrencyApplication currencyApplication,
                                  ISubcategoryApplication subcategoryApplication,
                                  IManufacturerApplication manufacturerApplication,
                                  IMeasurementApplication measurementApplication)
        {
            _imageApplication = imageApplication;
            _currencyApplication = currencyApplication;
            _subcategoryApplication = subcategoryApplication;
            _manufacturerApplication = manufacturerApplication;
            _measurementApplication = measurementApplication;
        }
        #endregion

        #region << METHODS >>

        #region PUBLIC

        #region GetAllAsync
        public async Task<List<ProductViewModel>> GetAllAsync()
        {
            List<ProductViewModel> productsReturn = new();

            var products = await _products.GetAllAsync() ??
                throw new Exception("There's no products registered.");

            var currencies = await _currencyApplication.GetAllAsync();
            var subcategories = await _subcategoryApplication.GetAllAsync();
            var manufacturers = await _manufacturerApplication.GetAllAsync();
            var measurements = await _measurementApplication.GetAllAsync();
            
            foreach (var product in products.Where(x => x.Active && !x.Deleted))
            {
                var currency = currencies.FirstOrDefault(x => x.ID == product.CurrencyID && x.Active && !x.Deleted);
                var subcategory = subcategories.FirstOrDefault(x => x.ID == product.SubcategoryID && x.Active && !x.Deleted);
                var manufacturer = manufacturers.FirstOrDefault(x => x.ID == product.ManufacturerID && x.Active && !x.Deleted);

                var weight = measurements.FirstOrDefault(x => x.ID == product.WeightMeasurementTypeID && x.Active && !x.Deleted);
                var height = measurements.FirstOrDefault(x => x.ID == product.HeightMeasurementTypeID && x.Active && !x.Deleted);
                var width = measurements.FirstOrDefault(x => x.ID == product.WidthMeasurementTypeID && x.Active && !x.Deleted);
                var length = measurements.FirstOrDefault(x => x.ID == product.LengthMeasurementTypeID && x.Active && !x.Deleted);

                var weightModel = weight == null ? null : new MeasurementViewModel
                {
                    ID = weight.ID,
                    GuidID = weight.GuidID,
                    Name = weight.Name,
                    Value = product.Weight,
                    MeasurementType = weight.MeasurementType,
                    Active = weight.Active,
                    Deleted = weight.Deleted,
                    Created_at = weight.Created_at,
                    Created_by = weight.Created_by,
                    Deleted_at = weight.Deleted_at,
                    Deleted_by = weight.Deleted_by
                };

                var heightModel = height == null ? null : new MeasurementViewModel
                {
                    ID = height.ID,
                    GuidID = height.GuidID,
                    Name = height.Name,
                    Value = product.Height,
                    MeasurementType = height.MeasurementType,
                    Active = height.Active,
                    Deleted = height.Deleted,
                    Created_at = height.Created_at,
                    Created_by = height.Created_by,
                    Deleted_at = height.Deleted_at,
                    Deleted_by = height.Deleted_by
                };

                var widthModel = width == null ? null : new MeasurementViewModel
                {
                    ID = width.ID,
                    GuidID = width.GuidID,
                    Name = width.Name,
                    Value = product.Width,
                    MeasurementType = width.MeasurementType,
                    Active = width.Active,
                    Deleted = width.Deleted,
                    Created_at = width.Created_at,
                    Created_by = width.Created_by,
                    Deleted_at = width.Deleted_at,
                    Deleted_by = width.Deleted_by
                };

                var lengthModel = length == null ? null : new MeasurementViewModel
                {
                    ID = length.ID,
                    GuidID = length.GuidID,
                    Name = length.Name,
                    Value = product.Length,
                    MeasurementType = length.MeasurementType,
                    Active = length.Active,
                    Deleted = length.Deleted,
                    Created_at = length.Created_at,
                    Created_by = length.Created_by,
                    Deleted_at = length.Deleted_at,
                    Deleted_by = length.Deleted_by
                };

                var bases64 = await _imageApplication.GetBases64ByProductIDAsync(product.GuidID);

                productsReturn.Add(new ProductViewModel
                {
                    ID = product.ID,
                    GuidID = product.GuidID,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Discount = product.Discount,
                    Currency = currency,
                    Subcategory = subcategory,
                    Manufacturer = manufacturer,
                    Weight = weightModel,
                    Height = heightModel,
                    Width = widthModel,
                    Length = lengthModel,
                    Bases64 = bases64?.ToList(),
                    Rating = await GetProductRatingAsync(product),
                    Stock = product.Stock,
                    Active = product.Active,
                    Deleted = product.Deleted,
                    Created_at = product.Created_at
                });
            }

            return productsReturn;
        }
        #endregion

        #region GetByIDAsync
        public async Task<ProductViewModel?> GetByIDAsync(Guid guid)
        {
            var products = await GetAllAsync();

            return products.FirstOrDefault(x => x.GuidID == guid);
        }
        #endregion

        #region GetMostSoldsAsync
        public async Task<List<ProductViewModel>?> GetMostSoldsAsync()
        {
            List<ProductViewModel>? mostSoldsReturn = null;

            var products = await _products.GetAllAsync();
            var ordersProducts = await _ordersProducts.GetAllAsync();

            var soldInMonth = ordersProducts.Where(x => x.Created_at.Month == DateTime.Now.Month).ToList();
            var soldInMonthGrouped = soldInMonth.GroupBy(x => x.ProductID).ToList();

            foreach (var product in soldInMonthGrouped.Take(4))
            {
                var productDetails = products.FirstOrDefault(x => x.ID == product.Key && x.Active && !x.Deleted);

                if (productDetails is not null)
                {
                    if (mostSoldsReturn is null) mostSoldsReturn = new();
                    var productModel = await GetByIDAsync(productDetails.GuidID);
                    if (productModel is not null) mostSoldsReturn.Add(productModel);
                }
            }

            return mostSoldsReturn;
        }
        #endregion

        #region SaveAsync
        public async Task<Products> SaveAsync(ProductsModel model)
        {
            Validate(model);

            var users = await _users.GetAllAsync();
            var products = await _products.GetAllAsync();
            var currencies = await _currencies.GetAllAsync();
            var measurements = await _measurements.GetAllAsync();
            var subcategories = await _subcategories.GetAllAsync();
            var manufacturers = await _manufacturers.GetAllAsync();

            var product = products.FirstOrDefault(x => x.GuidID == model.GuidID && x.Active && !x.Deleted);

            if (model.GuidID != Guid.Empty)
            {
                if (product == null)
                    throw new Exception("The product was not found. Please, contact the system administrator.");

                model.ID = product.ID;
                model.GuidID = product.GuidID;
                model.Created_at = product.Created_at;
                model.Created_by = product.Created_by;
                model.Deleted_at = product.Deleted_at;
                model.Deleted_by = product.Deleted_by;
            }
            else
                model.Created_by = users?.Where(x => x.GuidID == model.UserGuid && x.Active && !x.Deleted).FirstOrDefault()?.ID;

            var productSubcategory = subcategories.FirstOrDefault(x => x.GuidID == model.SubcategoryGuid);
            var productManufacturer = manufacturers.FirstOrDefault(x => x.GuidID == model.ManufacturerGuid);

            model.CurrencyID = currencies.First(x => x.GuidID == model.CurrencyGuid && x.Active && !x.Deleted).ID;
            model.SubcategoryID = subcategories.First(x => x.GuidID == model.SubcategoryGuid && x.Active && !x.Deleted).ID;
            model.ManufacturerID = manufacturers.First(x => x.GuidID == model.ManufacturerGuid && x.Active && !x.Deleted).ID;
            
            if (model.WeightGuid != Guid.Empty)
                model.WeightMeasurementTypeID = measurements.First(x => x.GuidID == model.WeightGuid && x.Active && !x.Deleted).ID;

            if (model.HeightGuid != Guid.Empty)
                model.HeightMeasurementTypeID = measurements.First(x => x.GuidID == model.HeightGuid && x.Active && !x.Deleted).ID;

            if (model.WidthGuid != Guid.Empty)
                model.WidthMeasurementTypeID = measurements.First(x => x.GuidID == model.WidthGuid && x.Active && !x.Deleted).ID;

            if (model.LengthGuid != Guid.Empty)
                model.LengthMeasurementTypeID = measurements.First(x => x.GuidID == model.LengthGuid && x.Active && !x.Deleted).ID;

            if (model.GuidID == Guid.Empty)
            {
                if (products.Any(x => x.Name == model?.Name?.Trim() &&
                                      x.ManufacturerID == productManufacturer?.ID &&
                                      x.SubcategoryID == productSubcategory?.ID))
                {
                    throw new Exception("There's already a product registered with the same name, manufacturer and subcategory.");
                }

                model.ID = (int)await _products.InsertAsync(model);
            }
            else
            {
                if (!await _products.UpdateAsync(model, product))
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
            }

            products = await _products.GetAllAsync();

            product = products.FirstOrDefault(x => x.ID == model.ID) ??
                throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");

            return product;
        }
        #endregion

        #region SaveProductRating
        public async Task<ProductRatingViewModel?> SaveProductRatingAsync(ProductsRatingsModel model)
        {
            var users = await _users.GetAllAsync();
            var products = await _products.GetAllAsync();

            var product = products.FirstOrDefault(x => x.GuidID == model.ProductGuid && x.Active && !x.Deleted) ??
                throw new Exception("The product was not found. Please, contact the system administrator.");

            model.ProductID = product.ID;
            model.Created_by = users?.FirstOrDefault(x => x.GuidID == model.UserGuid && x.Active && !x.Deleted)?.ID;

            if (await _productsRatings.InsertAsync(model) is null)
                throw new Exception("The product was not found. Please, contact the system administrator.");

            return await GetProductRatingAsync(product);
        }
        #endregion

        #region GetProductRating
        private async Task<ProductRatingViewModel?> GetProductRatingAsync(Products product)
        {
            ProductRatingViewModel? productRatingReturn = null;

            var productsRatings = await _productsRatings.GetAllAsync();

            var productRatings = productsRatings.Where(x => x.ProductID == product?.ID);

            if (productRatings.Any())
            {
                decimal totalRating = 0;

                foreach (var productRating in productRatings)
                    totalRating += productRating.Rating;

                totalRating = totalRating / productRatings.Count();

                productRatingReturn = new ProductRatingViewModel
                {
                    Rating = totalRating,
                    TotalRatings = productRatings.Count()
                };
            }

            return productRatingReturn;
        }
        #endregion

        #endregion

        #region PRIVATE

        #region Validate
        private static void Validate(ProductsModel model)
        {
            if (string.IsNullOrEmpty(model.Name)) throw new Exception("The product's name cannot be null or empty.");
            if (string.IsNullOrEmpty(model.Description)) throw new Exception("The product's description cannot be null or empty.");
            if (model.Price == null || model.Price <= 0) throw new Exception("Please, inform the product's price.");
            if (model.Discount < 0 || model.Discount >= 100) throw new Exception("The product's price cannot be less then 0 or greater then 100.");
            if (model.Stock < 0) throw new Exception("The product's stock cannot be less then 0.");

            if (model.WeightGuid != Guid.Empty)
                if (model.Weight < 0 || model.Weight == null) throw new Exception("The product's weight cannot be less than 0.");

            if (model.HeightGuid != Guid.Empty)
                if (model.Height < 0 || model.Height == null) throw new Exception("The product's height cannot be less than 0.");

            if (model.WidthGuid != Guid.Empty)
                if (model.Width < 0 || model.Width == null) throw new Exception("The product's width cannot be less than 0.");

            if (model.LengthGuid != Guid.Empty)
                if (model.Length < 0 || model.Length == null) throw new Exception("The product's length cannot be less than 0.");

            if (model.CurrencyGuid == Guid.Empty) throw new Exception("Please, inform the product's price currency.");
            if (model.SubcategoryGuid == Guid.Empty) throw new Exception("Please, inform the product's subcategory.");
            if (model.ManufacturerGuid == Guid.Empty) throw new Exception("Please, inform the product's manufacturer.");
        }
        #endregion

        #endregion

        #endregion
    }
}
