using Cooper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.ORM
{
    internal interface IORM <Entity>
    {
        IEnumerable<Entity> GetAll();

        long Add(Entity entity);

        int Update(Entity entity);

        Entity GetData(long id);

        int Delete(long id);
    }
}
