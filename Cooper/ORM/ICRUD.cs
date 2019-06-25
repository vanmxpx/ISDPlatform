using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cooper.ORM
{
    internal interface ICRUD
    {
        
        long Create(string table, string idColumn, EntityORM entity, bool returning = true); //return column id or 0 for verification

        EntityORM Read(object attribute_value, string attribute_name, HashSet<string> attributes, string table); //Read unique field

        IEnumerable<EntityORM> ReadAll(object attribute_value, string attribute_name, HashSet<string> attributes, string table); //Read entities by parameter

        IEnumerable<EntityORM> ReadAll(string table, HashSet<string> attributes); //Read all entries in table

        bool Update(long id, string table, string idColumn, EntityORM attributeValue);

        bool Delete(object id, string table, string idColumn);
    }
}
