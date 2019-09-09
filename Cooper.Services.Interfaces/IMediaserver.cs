using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Cooper.Services.Interfaces
{
    public interface IMediaserver
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
