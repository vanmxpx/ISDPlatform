using MediaServer.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public ImageController(IHostingEnvironment appEnvironment, IImageRepository imageRepository)
        {
            _appEnvironment = appEnvironment;
            _imageRepository = imageRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upload(IFormFile uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.IsImage())
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

                await _imageRepository.AddImageAsync(image, uploadedFile.FileName);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Download(string fileName)
        {
            string path = _appEnvironment.WebRootPath + "/Images";
            string filePath = Path.Combine(path, fileName);
            string defaultFilePath = Path.Combine(path, "default.png");

            Image image = await _imageRepository.GetImageAsync(filePath);

            if (image != null)
            {
                return PhysicalFile(filePath, ImageHelper.GetImageContentType(filePath));
            }
            else
            {
                return PhysicalFile(defaultFilePath, ImageHelper.GetImageContentType(defaultFilePath));
            }
        }
    }
}