using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Upload(IFormFile uploadedFile)
        {
            if (uploadedFile != null && IsImage(GetBytesFromFormFile(uploadedFile)))
            {
                string path = _appEnvironment.WebRootPath + "/Images/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
            }

            return RedirectToAction("Index");
        }

        public byte[] GetBytesFromFormFile(IFormFile file)
        {
            byte[] fileBytes = new byte[0];

            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }
            }

            return fileBytes;
        }

        public bool IsImage(byte[] data)
        {
            bool isImage = false;

            using (var imageReadStream = new MemoryStream(data))
            {
                try
                {
                    Image possibleImage = Image.FromStream(imageReadStream);
                    isImage = true;
                }
                catch
                {
                    isImage = false;
                }
            }

            return isImage;
        }

        public IActionResult Download(string fileName)
        {
            string path = _appEnvironment.WebRootPath + "/Images";
            string filePath = Path.Combine(path, fileName);

            if (System.IO.File.Exists(filePath))
            {
                return PhysicalFile(filePath, GetContentType(filePath));
            }

            string defaultFilePath = Path.Combine(path, "default.png");
            return PhysicalFile(defaultFilePath, GetContentType(defaultFilePath));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"}
            };
        }
    }
}