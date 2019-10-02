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

        public GameDAO(ISession session)
        {
            crud = new CRUD(session);
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

            var whereRequest = new WhereRequest(idColumn, Operators.Equal, id.ToString());

            List<EntityORM> entities = (List<EntityORM>)crud.Read(table, attributes, whereRequest);

            if (entities.Any())
            {
                EntityMapping.Map(entities[0], out game);
            }

            return game;
        }

        public GameDb GetByName(string name)
        {
            GameDb game = null;

            var whereRequest = new WhereRequest("LINK", Operators.Equal, $"'{name}'");

            List<EntityORM> entities = (List<EntityORM>)crud.Read(table, attributes, whereRequest);

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

        public bool Delete(object id)
        {
            bool isDeleted = crud.Delete(id, table, idColumn);

            if (isDeleted)
            {
                logger.Info($"Game with id={id} was successfully deleted from table {table}.");
            }
            else
            {
                logger.Info($"Deleting game with id={id} was failed.");
            }

            return isDeleted;
        }

        public bool Update(GameDb game)
        {
            EntityORM entity = EntityMapping.Map(game, attributes);

            // Making sure that ID value is not touched.
            entity.attributeValue.Remove("ID");

            var whereRequest = new WhereRequest(idColumn, Operators.Equal, game.Id.ToString());

            bool isUpdated = crud.Update(table, entity, whereRequest);

            if (isUpdated)
            {
                logger.Info($"Game with id={game.Id} was successfully updated.");
            }
            else
            {
                logger.Info($"Updating game with id={game.Id} was failed.");
            }

            return isUpdated;
        }

    }
}