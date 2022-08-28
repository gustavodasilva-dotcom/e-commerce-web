using Loja.Web.Application.Interfaces.Registration.Image;
using Loja.Web.Domain.Entities.Registration.Image;
using Loja.Web.Presentation.Models.Registration.Image;
using Microsoft.AspNetCore.Http;

namespace Loja.Web.Application.Applications.Registration.Image
{
    public class ImageApplication : IImageApplication
    {
        #region << PROPERTIES >>
        private readonly Images _images = new();
        private readonly ProductsImages _productsImages = new();
        #endregion

        #region << METHODS >>
        
        #region GetAllAsync
        public async Task<IEnumerable<Images?>> GetAllAsync()
        {
            return await _images.GetAllAsync();
        }
        #endregion

        #region InsertAsync
        public async Task<List<Images>> InsertAsync(List<string> bases64)
        {
            long? imageID;
            List<Images>? result = new();
            foreach (var base64 in bases64)
            {
                imageID = await _images.InsertAsync(base64);
                if (imageID is null)
                {
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
                }
                var images = await _images.GetAllAsync();
                var image = images.FirstOrDefault(x => x.ID == imageID);
                if (image is null)
                {
                    throw new Exception("An error occurred while executing the process. Please, contact the system administrator.");
                }
                result?.Add(image);
            }
            return result;
        }
        #endregion

        #region InsertProductsImagesAsync
        public async Task<List<long?>> InsertProductsImagesAsync(DefaultObjectImagesModel defaultObjectImages)
        {
            List<long?> ids = new();
            foreach (var id in defaultObjectImages.ImagesIDs)
            {
                var productImageID = await _productsImages.InsertAsync(defaultObjectImages.ObjectID, id);
                if (productImageID != null)
                {
                    ids.Add(productImageID);
                }
            }
            return ids;
        }
        #endregion

        #region ConvertToBase64
        public string ConvertToBase64(IFormFile file)
        {
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            byte[] fileBytes = ms.ToArray();
            return Convert.ToBase64String(fileBytes);
        }
        #endregion

        #endregion
    }
}
