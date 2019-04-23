using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Cooper.Configuration
{
    public class AuthOptions
    {

        // TODO: make constants environmental variable at least

        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "http://localhost:5001";
        const string KEY = "mysupersecret_secretkey!123";
        public const int LIFETIME = 1;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
