using Cooper.Models;
using Cooper.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.Extensions
{
    public static class AuthExtensions
    {
        public static User GetAuthorizedUser(this HttpRequest Request, UserRepository repository)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (token == null) return null;
            User user = repository.GetByJWToken(token);
            return user;
        }
    }
}
