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

        public async Task<string> AddImageAsync(Image image)
        {
            byte[] bytes = image.ToByteArray();
            string fileName = image.GetHash() + ".png";

            if(!File.Exists(Path.Combine(imageFolderPath, fileName)))
            {
                try
                {
                    await File.WriteAllBytesAsync(Path.Combine(imageFolderPath, fileName), bytes);
                }
                catch
                {
                    fileName = null;
                }
            }

            return fileName;
        }
    }
}
