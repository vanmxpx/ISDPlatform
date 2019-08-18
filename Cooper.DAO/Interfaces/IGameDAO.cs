using Cooper.DAO.Models;
using System.Collections.Generic;

namespace Cooper.DAO
{
    public interface IGameDAO
    {
        IEnumerable<GameDb> GetAll();
        GameDb Get(long id);
        GameDb GetByName(string name);
        long Save(GameDb entity);
        void Delete(object id);
        void Update(GameDb entity);
    }
}
