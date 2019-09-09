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
    public class ResetTokenDAO : IDAO<ResetTokenDb>
    {
        private readonly DbConnect dbConnect;
        private readonly OracleConnection Connection;
        private readonly Logger logger;
        private readonly CRUD crud;

        private string table;
        private string idColumn;
        private HashSet<string> attributes;
        private HashSet<string> unique_attributes;

        public ResetTokenDAO(IConfigProvider configProvider)
        {
            crud = new CRUD(configProvider);
            dbConnect = new DbConnect(configProvider);
            Connection = dbConnect.GetConnection();
            logger = LogManager.GetCurrentClassLogger();

            table = "RESETTOKENS";
            idColumn = "TOKEN";

            attributes = new HashSet<string>()
            {
                "EMAIL", "TOKEN"
            };

            unique_attributes = new HashSet<string>()
            {
                "EMAIL", "TOKEN"
            };
        }

        public IEnumerable<ResetTokenDb> GetAll()
        {
            return crud.Read(table, attributes).Select(item => {
                EntityMapping.Map(item, out ResetTokenDb resetTokenDb);
                return resetTokenDb;
            });
        }

        public long Save(ResetTokenDb resetToken)
        {
            EntityORM entity = EntityMapping.Map(resetToken, attributes);
            long resetToken_id = crud.Create(table, idColumn, entity, false);
            logger.Info($"Reset token was created");
            return resetToken_id;
        }

        public void Delete(object id)
        {
            if (crud.Delete(id, table, idColumn))
            {
                logger.Info($"Token with id={id} was successfully deleted from table {table}.");
            }
            else
            {
                logger.Info($"Deleting token with id={id} was failed.");
            }
        }

        public ResetTokenDb Get(object id)
        {
            ResetTokenDb resetTokenDb = null;
            List<EntityORM> entities = (List<EntityORM>)(crud.Read(table, attributes, new DbTools.WhereRequest[] { new DbTools.WhereRequest(idColumn, DbTools.RequestOperator.Equal, id) }));

            if (entities.Any())
            {
                EntityMapping.Map(entities[0], out resetTokenDb);
            }

            return resetTokenDb;
        }

        public void Update(ResetTokenDb resetTokenDb)
        {
            //TODO: update
        }

        public bool IfTokenExists(string token)
        {
            ResetTokenDb resetTokenDb = GetByToken(token);

            return (resetTokenDb != null);
        }

        public ResetTokenDb GetByToken(string token)
        {
            string attribute = "TOKEN";

            token = $"\'{token}\'";       // tuning string for sql query

            return GetByUniqueAttribute(token, attribute);
        }

        public string GetEmailByToken(string token)
        {
            return GetByToken(token).Email;
        }

        public ResetTokenDb GetByUniqueAttribute(object attribute_value, string attribute_name)
        {
            ResetTokenDb resetTokenDb = null;

            if (!unique_attributes.Contains(attribute_name))
            {
                logger.Info($"{attribute_name} is not unique attribute. GET-method failed..");
                return resetTokenDb;
            }

            List<EntityORM> entities = (List<EntityORM>)(crud.Read(table, attributes, new DbTools.WhereRequest[] { new DbTools.WhereRequest(attribute_name, DbTools.RequestOperator.Equal, attribute_value) }));

            if (entities.Any())
            {
                EntityMapping.Map(entities[0], out resetTokenDb);
            }

            return resetTokenDb;
        }
    }
}