using Cooper.Services.Interfaces;

namespace Cooper.Services
{
    public class MediaserverConf : IMediaserverConf
    {
        public string GetApiUrl { get; set; }
        public string UploadApiUrl { get; set; }
    }
}
