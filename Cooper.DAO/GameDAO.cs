using Cooper.DAO.Mapping;
using Cooper.DAO.Models;
using Cooper.ORM;
using Cooper.Services.Interfaces;
using NLog;
using System.Collections.Generic;
using System.Linq;

namespace Cooper.DAO
{
    public class GameDAO : IGameDAO
    {
        private readonly CRUD crud;
        private readonly Logger logger;

        private string table;
        private string idColumn;
        private HashSet<string> attributes;

        public GameDAO(IConfigProvider configProvider)
        {
            crud = new CRUD(configProvider);
            logger = LogManager.GetLogger("CooperLoger");

            table = "GAMES";
            idColumn = "ID";
            attributes = new HashSet<string>()
            {
                "ID", "NAME", "DESCRIPTION", "GENRE", "LINK", "LOGOURL",
                "COVERURL", "ISVERIFIED"
            };
        }


        #region Get methods

        public GameDb Get(long id)
        {
            GameDb game = null;
            List<EntityORM> entities = (List<EntityORM>)(crud.Read(table, attributes, new DbTools.WhereRequest[] { new DbTools.WhereRequest(idColumn, DbTools.RequestOperator.Equal, id) }));

            if (entities.Any())
            {
                EntityMapping.Map(entities[0], out game);
            }

            return game;
        }

        public GameDb GetByName(string name)
        {
            GameDb game = null;
            List<EntityORM>  entities = (List<EntityORM>)(crud.Read(table, attributes, new DbTools.WhereRequest[] { new DbTools.WhereRequest("LINK", DbTools.RequestOperator.Equal, $"'{name}'") }));
            if (entities.Any())
            {
                EntityMapping.Map(entities[0], out game);
            }

            return game;
        }

        /// <summary>
        /// Return the GameDb object with interop properties
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GameDb GetExtended(long id)
        {
            GameDb game = Get(id);

            //game.PlayersList = GetPlayersList(id);

            return game;
        }

        public IEnumerable<GameDb> GetAll()
        {
            List<GameDb> games = new List<GameDb>();

            List<EntityORM> entities = (List<EntityORM>)crud.Read(table, attributes);

            foreach (EntityORM entity in entities)
            {
                EntityMapping.Map(entity, out GameDb game);
                games.Add(game);
            }

            return games;
        }


        #region Interop properties info reading

        private List<long> GetPlayersList(long idGame)
        {
            // TODO
            List<long> playersList = new List<long>();

            string sqlExpression = $"SELECT idUser from {table} WHERE ID = {idGame}";


            return playersList;
        }


        #endregion

        #endregion

        public long Save(GameDb game)
        {
            EntityORM entity = EntityMapping.Map(game, attributes);

            // Making sure that ID value is not touched.
            entity.attributeValue.Remove("ID");

            long idGame = crud.Create(table, idColumn, entity);

            return idGame;
        }

        public void Delete(object id)
        {
            bool ifDeleted = crud.Delete(id, table, idColumn);

            if (ifDeleted)
            {
                logger.Info($"Game with id={id} was successfully deleted from table {table}.");
            }
            else
            {
                logger.Info($"Deleting game with id={id} was failed.");
            }

        }

        public void Update(GameDb game)
        {
            EntityORM entity = EntityMapping.Map(game, attributes);

            // Making sure that ID value is not touched.
            entity.attributeValue.Remove("ID");

            bool ifUpdated = crud.Update(game.Id, table, idColumn, entity);

            if (ifUpdated)
            {
                logger.Info($"Game with id={game.Id} was successfully updated.");
            }
            else
            {
                logger.Info($"Updating game with id={game.Id} was failed.");
            }
        }

    }
}
