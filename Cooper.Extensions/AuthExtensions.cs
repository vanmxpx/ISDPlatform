using Cooper.Models;
using Cooper.Repositories;
using Microsoft.AspNetCore.Http;

namespace Cooper.Extensions
{
    public static class AuthExtensions
    {
        public static User GetAuthorizedUser(this HttpRequest Request, UserRepository repository)
        {
            var token = GetUserToken(Request);
            if (token == null) return null;
            User user = repository.GetByJWToken(token);
            return user;
        }

        public static string GetUserToken(this HttpRequest Request)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            return token;
        }
    }
}
