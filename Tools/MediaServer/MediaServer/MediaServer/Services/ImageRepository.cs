using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
using Utility;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

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

        public async Task<Image<Rgb24>> GetImageAsync(string path)
        {
            Image<Rgb24> image;

            try
            {
                byte[] bytes = await File.ReadAllBytesAsync(path);
                return Image.Load<Rgb24>(bytes);
            }
            catch
            {
                image = null;
            }

            return image;
        }

        public async Task<string> AddImageAsync(Image<Rgb24> image)
        {
            byte[] bytes = image.ToByteArray();
            string fileName = image.GetHash() + ".jpg";

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
