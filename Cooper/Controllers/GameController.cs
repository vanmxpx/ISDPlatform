using Cooper.Models;
using Cooper.ORM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Cooper.Controllers
{
    public class GameController: ControllerBase
    {
        GameORM objgame = new GameORM();
        [HttpGet]
        [Route("api/Game/Index")]
        public IEnumerable<Game> Index()
        {
            return objgame.GetAll();
        }
        [HttpPost]
        [Route("api/Game/Create")]
        public long Create([FromBody] Game game)
        {
            return objgame.Add(game);
        }
        [HttpGet]
        [Route("api/Game/Details/{id}")]
        public Game Details(long id)
        {
            return objgame.GetData(id);
        }
        [HttpPut]
        [Route("api/Game/Edit")]
        public int Edit([FromBody]Game game)
        {
            return objgame.Update(game);
        }
        [HttpDelete]
        [Route("api/Game/Delete/{id}")]
        public int Delete(long id)
        {
            return objgame.Delete(id);
        }
    }
}
