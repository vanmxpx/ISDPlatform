using Cooper.DAO;
using Cooper.DAO.Models;
using Cooper.Models;
using Cooper.Repositories.Mapping;
using Cooper.Services.Interfaces;
using System.Collections.Generic;

namespace Cooper.Repositories
{
    public class GameRepository : IRepository<Game>
    {
        private readonly GameDAO gameDAO;
        private readonly ModelsMapper mapper;

        public GameRepository(ISession session)
        {
            gameDAO = new GameDAO(session);
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

        public Game Get(object obj)
        {

            Game game_newTyped = null;
            GameDb game = null;
            if (obj is string name)
            {
                game = gameDAO.GetByName(name);
            }
            else if (obj is long id)
            {
               game = gameDAO.Get(id);
            }
            else
            {
                throw new System.Exception("Object type error!");
            }

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

        public bool Update(Game game)
        {
            GameDb gameDb = mapper.Map(game);

            bool isUpdated = gameDAO.Update(gameDb);

            return isUpdated;
        }

        public bool Delete(long id)
        {
            bool isDeleted = gameDAO.Delete(id);

            return isDeleted;
        }
    }
}
