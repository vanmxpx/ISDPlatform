using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Upload(IFormFile uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.IsImage())
            {
                string path = _appEnvironment.WebRootPath + "/Images/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
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