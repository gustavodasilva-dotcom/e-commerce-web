using Loja.Web.Domain.Entities.Registration.Image;
using Loja.Web.Presentation.Models.Registration.Image;
using Microsoft.AspNetCore.Http;

namespace Loja.Web.Application.Interfaces.Registration.Image
{
    public interface IImageApplication
    {
        Task<IEnumerable<Images?>> GetAllAsync();
        Task<IEnumerable<string?>> GetBases64ByProductIDAsync(Guid productID);
        Task<List<Images>> InsertAsync(List<string> bases64);
        Task<List<long?>> InsertProductsImagesAsync(DefaultObjectImagesModel defaultObjectImages);
        string ConvertToBase64(IFormFile file);
    }
}
