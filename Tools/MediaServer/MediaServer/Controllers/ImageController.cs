using MediaServer.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Utility;

namespace MediaServer.Controllers
{
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upload(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                if (uploadedFile.Length < 10 * 1024 * 1024)
                {
                    if (uploadedFile.IsImage())
                    {
                        Image image = uploadedFile.ToImage();

                        float width = image.Width;
                        float height = image.Height;

                        if (width > height && width > 1000)
                        {
                            image = image.ResizeImage(1000, (int)(height / width * 1000));
                        }
                        else if (height > width && height > 1000)
                        {
                            image = image.ResizeImage((int)(width / height * 1000), 1000);
                        }
                        else if (height == width && height > 1000)
                        {
                            image = image.ResizeImage(1000, 1000);
                        }
                        else
                        {
                            // We have to create a new bitmap, because using the image created from the input stream directly causes an error.
                            image = new Bitmap(image);
                        }

                        await _imageRepository.AddImageAsync(image);

                        _logger.LogInformation("Uploaded file has been successfully added. FileName: '{0}'.", uploadedFile.FileName);
                    }
                    else
                    {
                        _logger.LogWarning("Uploaded file is not image. FileName: '{0}'. ContentType: '{1}'.", uploadedFile.FileName, uploadedFile.ContentType);
                    }
                }
                else
                {
                    _logger.LogWarning("Uploaded file weight is more than 1 MB. FileName: '{0}'. ContentType: '{1}'. ContentSize: '{2}'.", uploadedFile.FileName, uploadedFile.ContentType, (float)uploadedFile.Length / 1024 / 1024);
                }
            }
            else
            {
                _logger.LogWarning("Uploaded file is null.");
            }

            return RedirectToAction("Index");
        }

        [Route("Image")]
        public async Task<IActionResult> Download(string fileName)
        {
            string path = _appEnvironment.WebRootPath + "/Images";
            string filePath = Path.Combine(path, fileName);
            string defaultFilePath = Path.Combine(path, "default.jpg");

            Image image = await _imageRepository.GetImageAsync(filePath);

            if (image != null)
            {
                return PhysicalFile(filePath, ImageHelper.GetImageContentType(filePath));
            }
            else
            {
                _logger.LogWarning("Image '{0}' not found.", filePath);
                return PhysicalFile(defaultFilePath, ImageHelper.GetImageContentType(defaultFilePath));
            }
        }
    }
}