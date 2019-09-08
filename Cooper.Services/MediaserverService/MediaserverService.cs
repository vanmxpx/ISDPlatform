using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cooper.Services
{
    class Mediaserver : IMediaserver
    {
        private readonly string apiUploadUrl;
        public Mediaserver(IConfigProvider configProvider, IHttpContextAccessor httpContextAccessor)
        {
            this.apiUploadUrl = configProvider.MediaserverConf.UploadApiUrl;
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            byte[] bytes = GetBytes(file);
            string output = null;

            using (var client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StreamContent(new MemoryStream(bytes)), file.FileName, file.FileName);

                    using (HttpResponseMessage message = await client.PostAsync(apiUploadUrl, content))
                    {
                        output = await message.Content.ReadAsStringAsync();
                    }
                }
            }

            return output;
        }

        private byte[] GetBytes(IFormFile file)
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
    }
}
