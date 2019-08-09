namespace Cooper.Services.Interfaces
{
    public interface IProvider
    {
        string AppID { get; set; }
        string AppSecretKey { get; set; }
    }
}
