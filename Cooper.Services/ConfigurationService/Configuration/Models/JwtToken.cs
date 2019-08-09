using Cooper.Services.Interfaces;

namespace Cooper.Services
{
    public class JwtToken : IJwtToken
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int Lifetime { get; set; }
    }
}
