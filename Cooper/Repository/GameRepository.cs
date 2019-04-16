using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.Models;
using Cooper.DAO;
using Cooper.DAO.Models;
using AutoMapper;

namespace Cooper.Repository
{
    public class GameRepository : IRepository<Game>
    {
        private GameDAO gameDAO;
        public GameRepository()
        {
            gameDAO = new GameDAO();
        }
        
        public IEnumerable<Game> GetAll()
        {
            List<GameDb> games = (List<GameDb>)gameDAO.GetAll();

            List<Game> games_newType = new List<Game>();

            foreach (GameDb game in games)
            {
                //Game game_newType = mapper.Map<Game>(game);
                Game game_newType = GameMap(game);

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
                game_newTyped = Mapper.Map<Game>(game);
                //Game game_newTyped = GameMap(game);
            }

            return game_newTyped;
        }

        public long Create(Game game)
        {
            GameDb gameDb = GameMap(game);

            return gameDAO.Save(gameDb);
        }

        public void Update(Game game)
        {
            GameDb gameDb = GameMap(game);

            gameDAO.Update(gameDb);
        }

        public void Delete(long id)
        {
            gameDAO.Delete(id);
        }

        #region Mapping
        private Game GameMap(GameDb game)
        {
            Game game_newType = new Game();

            #region Transfer main attributes

            game_newType.Id = game.Id;
            game_newType.Name = game.Name;
            game_newType.Description = game.Description;
            game_newType.Genre = game.Genre;
            game_newType.Link = game.Link;
            game_newType.LogoURL = game.LogoURL;
            game_newType.CoverURL = game.CoverURL;
            game_newType.IsVerified = game.IsVerified;

            #endregion

            #region Transfering interop attributes
            //EMPTY

            #endregion

            return game_newType;
        }

        private GameDb GameMap(Game game)
        {
            GameDb game_newType = new GameDb();

            #region Transfer main attributes

            game_newType.Id = game.Id;
            game_newType.Name = game.Name;
            game_newType.Description = game.Description;
            game_newType.Genre = game.Genre;
            game_newType.Link = game.Link;
            game_newType.LogoURL = game.LogoURL;
            game_newType.CoverURL = game.CoverURL;
            game_newType.IsVerified = game.IsVerified;

            #endregion

            #region Transfering interop attributes
            //EMPTY

            #endregion

            return game_newType;
        }

        #endregion

    }
}
