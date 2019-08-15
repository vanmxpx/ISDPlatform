using System.Collections.Generic;

namespace Cooper.Repositories
{
    public interface IRepository<Entity>
    {
        IEnumerable<Entity> GetAll();
        Entity Get(object obj);
        long Create(Entity entity);
        void Update(Entity entity);
        void Delete(long id);
    }
}