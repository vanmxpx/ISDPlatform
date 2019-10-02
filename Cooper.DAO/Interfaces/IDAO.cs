using System.Collections.Generic;

namespace Cooper.DAO
{
    public interface IDAO<Entity>
    {
        IEnumerable<Entity> GetAll();
        Entity Get(object id);
        long Save(Entity entity);
        bool Delete(object id);
        bool Update(Entity entity);
    }
}
