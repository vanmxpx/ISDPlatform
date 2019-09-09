using Cooper.Models;

namespace Cooper.Repositories
{
    public interface IUserRepository
    {
        User GetByJWToken(string token);
    }
}