﻿using Cooper.Models;
using System.Collections.Generic;
namespace Cooper.Repositories
{
    public interface IUserRepository
    {
        User GetByJWToken(string token);
        IList<User> GetUsersById(IList<long> UsersId);
        bool UpdateAvatar(string url, long userId);
    }
}