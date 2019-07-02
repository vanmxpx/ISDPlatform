using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.ORM
{
    internal interface ICRUD
    {
        
        long Create(string table, string idColumn, EntityORM entity, bool returning = true); //return column id or 0 for verification

        IEnumerable<EntityORM> Read(string table, HashSet<string> attributes, DbTools.WhereRequest[] whereRequests = null);

        bool Update(long id, string table, string idColumn, EntityORM attributeValue);

        bool Delete(object id, string table, string idColumn);
    }
}
