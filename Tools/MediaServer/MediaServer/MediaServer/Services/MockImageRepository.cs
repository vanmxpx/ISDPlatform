using System.Collections.Generic;
using System.Threading.Tasks;
using Utility;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MediaServer.Services
{
    public class MockImageRepository : IImageRepository
    {
        private Dictionary<string, Image<Rgb24>> images = new Dictionary<string, Image<Rgb24>>();

        public async Task<string> AddImageAsync(Image<Rgb24> image)
        {
            string hash = image.GetHash();

            if (!images.ContainsKey(hash))
            {
                try
                {
                    await Task.Run(() => images.Add(hash, image));
                }
                catch
                {
                    hash = null;
                }
            }

            return hash;
        }

        public async Task<Image<Rgb24>> GetImageAsync(string hash)
        {
            Image<Rgb24> image = null;

            await Task.Run(() =>
            {
                if (images.ContainsKey(hash))
                {
                    image = images[hash];
                }
            });
            
            return image;
        }
    }
}
