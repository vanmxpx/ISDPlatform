using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Utility;

namespace MediaServer.Controllers
{
    public class ImageController : Controller
    {
        private IHostingEnvironment _appEnvironment;

        public ImageController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upload(IFormFile uploadedFile)
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

                string path = _appEnvironment.WebRootPath + "/Images/" + uploadedFile.FileName;
                image.Save(path);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Download(string fileName)
        {
            string path = _appEnvironment.WebRootPath + "/Images";
            string filePath = Path.Combine(path, fileName);

            if (System.IO.File.Exists(filePath))
            {
                return PhysicalFile(filePath, ImageHelper.GetImageContentType(filePath));
            }

            string defaultFilePath = Path.Combine(path, "default.png");
            return PhysicalFile(defaultFilePath, ImageHelper.GetImageContentType(defaultFilePath));
        }
    }
}