using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Cooper.Configuration;
using Cooper.Controllers.ViewModels;

namespace Cooper.Services.Authorization
{
    public class TokenFactory
    {
        private readonly Login login;
        private readonly IConfigProvider configProvider;

        public TokenFactory(Login login, IConfigProvider configProvider)
        {
            this.configProvider = configProvider;
            this.login = login;
        }

        public string GetTokenString()
        {
            var key = GetSymmetricSecurityKey(configProvider.JwtToken.Key);

            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var identity = GetIdentity(login.Username);

            var jwt = new JwtSecurityToken(
                        issuer: configProvider.JwtToken.Issuer,
                        audience: configProvider.JwtToken.Audience,
                        claims: identity.Claims,
                        expires: DateTime.Now.AddMinutes(configProvider.JwtToken.Lifetime),
                        signingCredentials: signinCredentials);

            string tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);

            return tokenString;
        }

        private ClaimsIdentity GetIdentity(string username)
        {
            var claims = new List<Claim>
                {
                    new Claim("username", username)
                };

            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        private SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

    }
}
