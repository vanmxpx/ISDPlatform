using Cooper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.ORM
{
    internal interface IORM <Entity>
    {
        IEnumerable<Entity> GetAllEntities();

        bool AddEntity(Entity entity);

        bool UpdateEntity(Entity entity);

        Entity GetEntityData(long id);

        bool DeleteEntity(long id);
    }
}
