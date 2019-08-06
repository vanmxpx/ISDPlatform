using System.Collections.Generic;

namespace Cooper.DAO
{
    public interface IDAO<Entity>
    {
        IEnumerable<Entity> GetAll();
        Entity Get(object id);
        long Save(Entity entity);
        void Delete(object id);
        void Update(Entity entity);
    }
}
