using Cooper.Services.Interfaces;

namespace Cooper.Services
{
    public class Provider : IProvider
    {
        public string AppID { get; set; }
        public string AppSecretKey { get; set; }
    }
}
