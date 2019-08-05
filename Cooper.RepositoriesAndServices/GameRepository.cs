using Cooper.Configuration;
using Cooper.DAO;
using Cooper.DAO.Models;
using Cooper.Models;
using Cooper.Repositories.Mapping;
using System.Collections.Generic;

namespace Cooper.Repositories
{
    public class GameRepository : IRepository<Game>
    {
        private GameDAO gameDAO;
        private ModelsMapper mapper;
        public GameRepository(IConfigProvider configProvider)
        {
            gameDAO = new GameDAO(configProvider);
            mapper = new ModelsMapper();
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
