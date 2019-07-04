using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.DAO.Models;
using Cooper.ORM;
using Cooper.DAO.Mapping;
using NLog;
using Oracle.ManagedDataAccess.Client;
using Cooper.Configuration;

namespace Cooper.DAO
{
    public class VerificationDAO : IDAO<VerificationDb>
    {
        private DbConnect dbConnect;
        private OracleConnection Connection;
        private readonly ILogger logger;

        private CRUD crud;

        private string table;
        private string idColumn;
        private HashSet<string> attributes;
        private HashSet<string> unique_attributes;

        public VerificationDAO(IConfigProvider configProvider, ILogger logger)
        {
            crud = new CRUD(configProvider, logger);
            dbConnect = new DbConnect(configProvider);
            Connection = dbConnect.GetConnection();
            this.logger = logger;

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

        public void Delete(object id)
        {
            if (crud.Delete(id, table, idColumn)) 
            {
                 logger.Info($"Token with id={id} was successfully deleted from table {table}.");
            }
            else {
                logger.Info($"Deleting token with id={id} was failed.");
            }
        }

        public VerificationDb Get(object id)
        {
            VerificationDb verify = null;
            List<EntityORM> entities = (List<EntityORM>)(crud.Read(table, attributes, new DbTools.WhereRequest[] { new DbTools.WhereRequest(idColumn, DbTools.RequestOperator.Equal, id) }));

            if (entities.Any())
            {
                EntityMapping.Map(entities[0], out verify);
            }

            return verify;
        }
        public void Update(VerificationDb user) {
            //TODO: update
        }
    }
}