using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.Models;

namespace Cooper.Repository
{
    public interface IUserRepository
    {
        User GetByJWToken(string token);
    }
}
