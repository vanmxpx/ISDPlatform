using Cooper.DAO;
using Cooper.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Cooper.Controllers
{
    public class GameController: ControllerBase
    {
        GameDataAccessLayer objgame = new GameDataAccessLayer();
        [HttpGet]
        [Route("api/Game/Index")]
        public IEnumerable<Game> Index()
        {
            return objgame.GetAllGames();
        }
        [HttpPost]
        [Route("api/Game/Create")]
        public bool Create([FromBody] Game Game)
        {
            return objgame.AddGame(Game);
        }
        [HttpGet]
        [Route("api/Game/Details/{id}")]
        public Game Details(int id)
        {
            return objgame.GetGameData(id);
        }
        [HttpPut]
        [Route("api/Game/Edit")]
        public bool Edit([FromBody]Game Game)
        {
            return objgame.UpdateGame(Game);
        }
        [HttpDelete]
        [Route("api/Game/Delete/{id}")]
        public bool Delete(int id)
        {
            return objgame.DeleteGame(id);
        }
    }
}
