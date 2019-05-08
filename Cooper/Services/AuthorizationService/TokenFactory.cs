using System;
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
        public TokenFactory(Login login)
        {
            this.login = login;
        }

        public string GetTokenString()
        {
            var signinCredentials = new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);


            var identity = GetIdentity(login.Username);

            var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        claims: identity.Claims,
                        expires: DateTime.Now.AddMinutes(AuthOptions.LIFETIME),
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

    }
}
