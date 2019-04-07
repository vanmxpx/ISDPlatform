using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cooper.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{
    [Route("api/game/statistics")]
    [ApiController]
    public class GameStatisticsController : ControllerBase
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Statistics> GetAllStatistics()
        {
            return new List<Statistics>();
        }

        public IEnumerable<Statistics> GetAllStatisticsByUserId(long userId)    // get all game statistics for concrete user
        {
            return new List<Statistics>();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IEnumerable<Statistics> GetAllStatisticByGameId(long id)                    // get all game statistics for concreate game
        {
            return new List<Statistics>();
        }

        public Statistics GetStatisticsById(long id)
        {
            return new Statistics();
        }
        
        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Statistics statistics)
        {
            // DAO MISSED
            return Ok(statistics);
        }
        
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            // DAO MISSED
            return Ok();
        }
    }
}
