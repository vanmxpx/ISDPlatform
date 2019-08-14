using System.Collections.Generic;

namespace Cooper.ORM
{
    public interface ICRUD
    {
        
        long Create(string table, string idColumn, EntityORM entity, bool returning = true); //return column id or 0 for verification

        IEnumerable<EntityORM> Read(string table, HashSet<string> attributes, DbTools.WhereRequest[] whereRequests = null);

        bool Update(string table, EntityORM entity, DbTools.WhereRequest[] whereRequests = null);

        bool Delete(object id, string table, string idColumn);
    }
}
