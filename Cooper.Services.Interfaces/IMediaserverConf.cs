namespace Cooper.Services.Interfaces
{
    public interface IMediaserverConf
    {
        string GetApiUrl { get; set; }
        string UploadApiUrl { get; set; }
    }
}
