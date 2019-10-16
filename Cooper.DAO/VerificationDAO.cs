using Cooper.DAO.Mapping;
using Cooper.DAO.Models;
using Cooper.ORM;
using Cooper.Services.Interfaces;
using NLog;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Linq;

namespace Cooper.DAO
{
    public class VerificationDAO : IDAO<VerificationDb>
    {
        private readonly Logger logger;
        private readonly CRUD crud;

        private string table;
        private string idColumn;
        private HashSet<string> attributes;
        private HashSet<string> unique_attributes;

        public VerificationDAO(ISession session)
        {
            crud = new CRUD(session);
            logger = LogManager.GetLogger("CooperLoger");

            table = "TOKENS";
            idColumn = "TOKEN";

            attributes = new HashSet<string>()
            {
                "EMAIL", "TOKEN", "ENDVERIFYDATE"
            };

            unique_attributes = new HashSet<string>()
            {
                "EMAIL", "TOKEN"
            };
        }

        public IEnumerable<VerificationDb> GetAll()
        {   
            return crud.Read(table, attributes).Select(item => { 
                EntityMapping.Map(item, out VerificationDb verify); 
                return verify; });
        }

        public long Save(VerificationDb verify)
        {
            EntityORM entity = EntityMapping.Map(verify, attributes);

            long verify_id = crud.Create(table, idColumn, entity, false);

            logger.Info($"User with id = {verify_id} was created");

            return verify_id;
        }

        public bool Delete(object id)
        {
            bool isDeleted = crud.Delete(id, table, idColumn);

            if (isDeleted) 
            {
                 logger.Info($"Token with id={id} was successfully deleted from table {table}.");
            }
            else {
                logger.Info($"Deleting token with id={id} was failed.");
            }

            return isDeleted;
        }

        public VerificationDb Get(object id)
        {
            VerificationDb verify = null;

            var whereRequest = new WhereRequest(idColumn, Operators.Equal, id.ToString());

            List<EntityORM> entities = (List<EntityORM>)crud.Read(table, attributes, whereRequest);

            if (entities.Any())
            {
                EntityMapping.Map(entities[0], out verify);
            }

            return verify;
        }
        public bool Update(VerificationDb user) {
            //TODO: update
            return true;
        }
    }
}