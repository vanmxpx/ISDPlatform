using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MediaServer.Services
{
    public interface IImageRepository
    {
        Task<Image<Rgb24>> GetImageAsync(string path);
        Task<string> AddImageAsync(Image<Rgb24> image);
    }
}
