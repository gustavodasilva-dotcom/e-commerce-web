﻿using Loja.Web.Domain.Entities.Registration.Product;
using Loja.Web.Presentation.Models.Registration.Product.Model;
using Loja.Web.Presentation.Models.Registration.Product.ViewModel;

namespace Loja.Web.Application.Interfaces.Registration.Product
{
    public interface IProductApplication
    {
        Task<ProductViewModel?> GetByIDAsync(Guid guid);
        Task<List<ProductViewModel>> GetAllAsync();
        Task<Products> SaveAsync(ProductsModel model);
        Task<decimal?> SaveProductRatingAsync(ProductsRatingsModel model);
    }
}
