using Loja.Web.Application.Interfaces.Registration.Image;
using Loja.Web.Presentation.Models.Registration.Image;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Loja.Web.Presentation.MVC.Controllers.Registration.Image
{
    public class ImagesController : DefaultController
    {
        #region << PROPRIEDADES >>
        private readonly IImageApplication _imageApplication;
        #endregion

        #region << CONSTRUCTOR >>
        public ImagesController(IImageApplication imageApplication)
        {
            _imageApplication = imageApplication;
        }
        #endregion

        #region << METHODS >>

        #region GetBases64ByProductID
        [HttpGet]
        public async Task<JsonResult> GetBases64ByProductID(Guid productID)
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                var bases64 = await _imageApplication.GetBases64ByProductIDAsync(productID);
                if (bases64.Any())
                {
                    result.Code = 1;
                    result.Bases64 = bases64;
                }
                else
                {
                    result.Message = "This product has no images.";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region InsertImage
        [HttpPost]
        public async Task<JsonResult> UploadImages()
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                if (Request.Form.Files.Count == 6)
                {
                    List<string> bases64 = new();
                    foreach (var file in Request.Form.Files)
                    {
                        bases64.Add(_imageApplication.ConvertToBase64(file));
                    }
                    var images = await _imageApplication.InsertAsync(bases64);
                    result.Images = images.Select(x => new
                    {
                        x.ID,
                        x.GuidID,
                        x.Base64
                    });
                    result.Code = 1;
                }
                else
                {
                    result.Message = "There has to be exactly 6 files to upload.";
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #region InsertProductsImages
        [HttpPost]
        public async Task<JsonResult> InsertProductsImages(DefaultObjectImagesModel defaultObjectImages)
        {
            dynamic result = new ExpandoObject();
            result.Code = 0;
            try
            {
                var images = await _imageApplication.InsertProductsImagesAsync(defaultObjectImages);
                if (images.Count == defaultObjectImages.ImagesIDs.Count)
                {
                    result.Code = 1;
                }
                else
                {
                    result.Message = string.Format("An error occurred while inserting {0} images.",
                        images.Count - defaultObjectImages.ImagesIDs.Count);
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion

        #endregion
    }
}
