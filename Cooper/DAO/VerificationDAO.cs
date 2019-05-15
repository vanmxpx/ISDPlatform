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
        private Logger logger;

        private CRUD crud;

        private string table;
        private string idColumn;
        private HashSet<string> attributes;
        private HashSet<string> unique_attributes;

        public VerificationDAO(IConfigProvider configProvider)
        {
            crud = new CRUD(configProvider);
            dbConnect = new DbConnect(configProvider);
            Connection = dbConnect.GetConnection();
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
            return crud.ReadAll(table, attributes).Select(item => { 
                EntityMapping.Map(item, out VerificationDb verify); 
                return verify; });
        }

        public long Save(VerificationDb user)
        {
            return 111;
        }

        public void Delete(long id)
        {
            if (crud.Delete(id, table, idColumn)) 
            {
                 logger.Info($"User with id={id} was successfully deleted from table {table}.");
            }
            else {
                logger.Info($"Deleting user with id={id} was failed.");
            }
        }

        public VerificationDb Get(long id)
        {
            string attribute = "ID";

            return null;
        }

        public void Update(VerificationDb user)
        {
            /*EntityORM entity = EntityMapping.Map(user, attributes);

            // Making sure that ID value is not touched.
            entity.attributeValue.Remove("ID"); 

            bool ifUpdated = crud.Update(user.Id, table, idColumn, entity);

            if (ifUpdated)
            {
                logger.Info($"User with id={user.Id} was successfully updated.");
            }
            else
            {
                logger.Info($"Updating user with id={user.Id} was failed.");
            }*/
        }
    }
}