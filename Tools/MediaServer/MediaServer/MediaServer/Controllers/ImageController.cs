using MediaServer.Models;
using MediaServer.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using Utility;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MediaServer.Controllers
{
    [Route("api/image")]
    public class ImageController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IImageRepository _imageRepository;
        private readonly ILogger _logger;

        public ImageController(IHostingEnvironment appEnvironment, IImageRepository imageRepository, ILogger<ImageController> logger)
        {
            _appEnvironment = appEnvironment;
            _imageRepository = imageRepository;
            _logger = logger;
        }

        /// <summary>
        /// Uploads an image to the media server.
        /// </summary>
        /// <returns>Returns json with file name or error description</returns>
        /// <response code="200">If the image is successfully uploaded</response>
        /// <response code="400">If the image is not uploaded to the media server</response> 
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ImageModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [Route("upload")]
        public async Task<IActionResult> Upload()
        {
            IFormFile file = null;
            IFormFileCollection files = HttpContext.Request.Form.Files;

            // We take only one file if the collection contains files.
            if (files.Count != 0)
            {
                file = HttpContext.Request.Form.Files[0];
            }

            return await UploadImage(file);
        }

        private async Task<IActionResult> UploadImage(IFormFile uploadedFile)
        {
            string errorMessage;

            if (uploadedFile != null)
            {
                if (uploadedFile.Length < 10 * 1024 * 1024)
                {
                    if (uploadedFile.IsImage())
                    {
                        Image<Rgb24> image = uploadedFile.ToImage();

                        float width = image.Width;
                        float height = image.Height;

                        // Resize the image if necessary.
                        if (width > height && width > 1000)
                        {
                            image.ResizeImage(1000, (int)(height / width * 1000));
                        }
                        else if (height > width && height > 1000)
                        {
                            image.ResizeImage((int)(width / height * 1000), 1000);
                        }
                        else if (height == width && height > 1000)
                        {
                            image.ResizeImage(1000, 1000);
                        }
                        else
                        {
                            // We have to create a new bitmap, because using the image created from the input stream directly causes an error.
                            image.ResizeImage((int)width, (int)height);
                        }

                        string fileName = await _imageRepository.AddImageAsync(image);

                        _logger.LogInformation("Uploaded file has been successfully added. FileName: '{0}'.", uploadedFile.FileName);

                        return Json(new ImageModel() { FileName = fileName });
                    }
                    else
                    {
                        errorMessage = string.Format("Uploaded file is not image. FileName: '{0}'. ContentType: '{1}'.", uploadedFile.FileName, uploadedFile.ContentType);
                        _logger.LogWarning(errorMessage);
                    }
                }
                else
                {
                    errorMessage = string.Format("Uploaded file size is more than 1 MB. FileName: '{0}'. ContentType: '{1}'. ContentSize: '{2}'.", uploadedFile.FileName, uploadedFile.ContentType, (float)uploadedFile.Length / 1024 / 1024);
                    _logger.LogWarning(errorMessage);
                }
            }
            else
            {
                errorMessage = "Uploaded file is null.";
                _logger.LogWarning(errorMessage);
            }

            return BadRequest(new ErrorModel { ErrorMessage = errorMessage });
        }

        /// <summary>
        /// Returns a image with the specified name, if it exists.
        /// </summary>
        /// <param name="fileName">The name of the image to be returned</param>
        /// <returns>Required image or code 404</returns>
        /// <response code="200">If required image found</response>
        /// <response code="404">If required image not found</response>  
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{fileName}")]
        public async Task<IActionResult> Download(string fileName)
        {
            string path = _appEnvironment.WebRootPath + "/Images";
            string filePath = Path.Combine(path, fileName);

            Image<Rgb24> image = await _imageRepository.GetImageAsync(filePath);

            if (image != null)
            {
                _logger.LogInformation("Image '{0}' found.", filePath);
                return PhysicalFile(filePath, ImageHelper.GetImageContentType(filePath));
            }
            else
            {
                _logger.LogWarning("Image '{0}' not found.", filePath);
                return NotFound();
            }
        }
    }
}