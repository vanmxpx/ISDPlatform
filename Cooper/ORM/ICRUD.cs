using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.ORM
{
    internal interface ICRUD
    {
        
        long Create(string table, string idColumn, EntityORM attributeValue);

        EntityORM Read(long id, string table, string idColumn, HashSet<string> attributes);

        IEnumerable<EntityORM> ReadAll(string table, HashSet<string> attributes);

        bool Update(long id, string table, string idColumn, EntityORM attributeValue);

        bool Delete(long id, string table, string idColumn);
    }
}
