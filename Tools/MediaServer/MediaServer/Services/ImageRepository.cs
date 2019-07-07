using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Utility;

namespace MediaServer.Services
{
    public class ImageRepository : IImageRepository
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly string imageFolderPath;

        public ImageRepository(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            imageFolderPath = $"{_appEnvironment.WebRootPath}/Images/";
        }

        public async Task<Image> GetImageAsync(string path)
        {
            Image image;

            try
            {
                byte[] bytes = await File.ReadAllBytesAsync(path);
                return bytes.ToImage();
            }
            catch
            {
                image = null;
            }

            return image;
        }

        public async Task<bool> AddImageAsync(Image image, string fileName)
        {
            byte[] bytes = image.ToByteArray();
            bool result;

            try
            {
                await File.WriteAllBytesAsync(Path.Combine(imageFolderPath, fileName), bytes);
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}
