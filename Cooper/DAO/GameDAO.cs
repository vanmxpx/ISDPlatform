﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.DAO.Models;
using Cooper.ORM;
using Cooper.DAO.Mapping;
using NLog;

namespace Cooper.DAO
{
    public class GameDAO : IDAO<GameDb>
    {

        private CRUD crud;
        Logger logger;

        private string table;
        private string idColumn;
        private HashSet<string> attributes;
        
        public GameDAO()
        {
            crud = new CRUD();
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
            EntityORM entity = crud.Read(id, idColumn, attributes, table);

            EntityMapping.Map(entity, out GameDb game);

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

            List<EntityORM> entities = (List<EntityORM>)crud.ReadAll(table, attributes);

            foreach (EntityORM entity in entities)              // Mapping entities to games
            {
                EntityMapping.Map(entity, out GameDb game);
                games.Add(game);
            }

            return games;
        }


        #region Interop properties info reading

        private List<long> GetPlayersList(long idGame) // TODO
        {
            List<long> playersList = new List<long>();

            string sqlExpression = $"SELECT idUser from USERSGAMES WHERE ID = {idGame}";


            return playersList;
        }


        #endregion

        #endregion
        
        public long Save(GameDb game)
        {
            EntityORM entity = EntityMapping.Map(game, attributes);

            long idGame = crud.Create(table, idColumn, entity);

            return idGame;
        }

        public void Delete(long id)
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