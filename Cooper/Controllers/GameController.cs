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
        GameDAO objgame = new GameDAO();
        [HttpGet]
        [Route("api/Game/Index")]
        public IEnumerable<Game> Index()
        {
            return objgame.GetAllEntities();
        }
        [HttpPost]
        [Route("api/Game/Create")]
        public bool Create([FromBody] Game game)
        {
            return objgame.AddEntity(game);
        }
        [HttpGet]
        [Route("api/Game/Details/{id}")]
        public Game Details(long id)
        {
            return objgame.GetEntityData(id);
        }
        [HttpPut]
        [Route("api/Game/Edit")]
        public bool Edit([FromBody]Game game)
        {
            return objgame.UpdateEntity(game);
        }
        [HttpDelete]
        [Route("api/Game/Delete/{id}")]
        public bool Delete(long id)
        {
            return objgame.DeleteEntity(id);
        }
    }
}
