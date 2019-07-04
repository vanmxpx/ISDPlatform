using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.Models;
using Cooper.DAO;
using Cooper.DAO.Models;
using Cooper.Repository.Mapping;
using Cooper.Configuration;
using NLog;

namespace Cooper.Repository
{
    public class GameRepository : IRepository<Game>
    {
        private GameDAO gameDAO;
        private ModelsMapper mapper;
        private readonly ILogger logger;

        public GameRepository(IConfigProvider configProvider, ILogger logger)
        {
            gameDAO = new GameDAO(configProvider, logger);
            mapper = new ModelsMapper();

            this.logger = logger;
        }
        
        public IEnumerable<Game> GetAll()
        {
            List<GameDb> games = (List<GameDb>)gameDAO.GetAll();

            List<Game> games_newType = new List<Game>();

            foreach (GameDb game in games)
            {
                Game game_newType = mapper.Map(game);

                games_newType.Add(game_newType);
            }

            return games_newType;
        }

        public Game Get(long id)
        {
            GameDb game = gameDAO.GetExtended(id);

            Game game_newTyped = null;


            if (game != null)
            {
                game_newTyped = mapper.Map(game);
            }

            return game_newTyped;
        }

        public long Create(Game game)
        {
            GameDb gameDb = mapper.Map(game);

            return gameDAO.Save(gameDb);
        }

        public void Update(Game game)
        {
            GameDb gameDb = mapper.Map(game);

            gameDAO.Update(gameDb);
        }

        public void Delete(long id)
        {
            gameDAO.Delete(id);
        }

        

    }
}
