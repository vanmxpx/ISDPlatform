using Cooper.Configuration;
using Cooper.Controllers.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

            ClaimsIdentity identity = GetIdentity(login.Username);

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

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        private SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

    }
}
