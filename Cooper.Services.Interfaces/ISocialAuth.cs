namespace Cooper.Services.Interfaces
{
    public interface ISocialAuth
    {
        bool getCheckAuth(string provider, string token, string id);
    }
}
