using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Utility;

namespace MediaServer.Services
{
    public class MockImageRepository : IImageRepository
    {
        private Dictionary<string, Image> images = new Dictionary<string, Image>();

        public async Task<string> AddImageAsync(Image image)
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

        public async Task<Image> GetImageAsync(string hash)
        {
            Image image = null;

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
